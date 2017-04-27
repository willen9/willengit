using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Cells;
using Ini;
using REGAL.Data.DataAccess;

namespace P1612603
{
    class Program
    {
        private static IniFile ini = new IniFile(Environment.CurrentDirectory + "\\Setup.ini");
        private static string templatePath;

        static void Main(string[] args)
        {
            try
            {
                if (!File.Exists(Environment.CurrentDirectory + "\\Setup.ini"))
                {
                    //產生ini檔
                    Console.WriteLine("Setup.ini設定檔遺失!");
                    Console.WriteLine("設定檔自動產生中…");
                    ini.IniWriteValue("Setup", "StrMail", "chengwm@regalscan.com.tw");//mail發送人
                    ini.IniWriteValue("Setup", "StrPWD", "123456");//mail發送人密碼
                    ini.IniWriteValue("Setup", "StrHost", "regalscan.com.tw");//mail發送主機
                    ini.IniWriteValue("Setup", "TemplatePath", "C:\\Templates");//模板路徑
                    ini.IniWriteValue("Setup", "conStr", "data source=192.168.60.240;initial catalog=P16126;user id=sa;password=N@07305506");
                }
                templatePath = ini.IniReadValue("Setup", "TemplatePath");
                string mailSender = ini.IniReadValue("Setup", "StrMail");
                string mailPwd = ini.IniReadValue("Setup", "StrPWD");
                string mailHost = ini.IniReadValue("Setup", "StrHost");

                DataAccess dataAccess = new DataAccess();
                dataAccess.ConnectionString = ini.IniReadValue("Setup", "conStr");
                dataAccess.ProviderName = "System.Data.SqlClient";
                dataAccess.EnableDebugMode = true;

                if (!Directory.Exists(Environment.CurrentDirectory + "\\DifExport"))
                {
                    Directory.CreateDirectory(Environment.CurrentDirectory + "\\DifExport");
                }
                string date = DateTime.Now.ToString("yyyyMMdd");
                if (!Directory.Exists(Environment.CurrentDirectory + "\\DifExport\\" + date))
                {
                    Directory.CreateDirectory(Environment.CurrentDirectory + "\\DifExport\\" + date);
                }
                for (int i = 1; i <= 10; i++)
                {
                    GeneralFileAndSend("A" + i.ToString("000"), Environment.CurrentDirectory + "\\DifExport\\" + date, dataAccess, mailSender, mailPwd, mailHost);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("發生異常，請聯繫帝商工程師，異常原因：" + ex.Message);
                using (StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + "\\log.txt", true))
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " Message:" + ex.Message);
                }
                Console.Read();
            }
        }

        /// <summary>
        /// 產生Excel檔并發送方法
        /// </summary>
        /// <param name="workNo">作業別</param>
        /// <param name="filePath">生成Excel路徑</param>
        /// <param name="dataAccess"></param>
        /// <returns></returns>
        private static void GeneralFileAndSend(string workNo, string filePath, DataAccess dataAccess, string mailSender, string mailPwd, string mailHost)
        {
            DataTable dtSum = null;
            DataTable dtDetail = null;
            string sql;
            Workbook workbook;
            Worksheet worksheet;
            DataTable dtOrganizations = dataAccess.ExecuteDataTable("SELECT * FROM FRTA003 AS f");
            foreach (DataRow drOrg in dtOrganizations.Rows)
            {
                switch (workNo)
                {
                    #region 提貨單撿貨

                    case "A001":
                        dtSum = dataAccess.ExecuteDataTable(@"SELECT distinct s.TripID,s.TripName
                                                            FROM STTS003 AS s                                                        
                                                            WHERE s.ActionCode = '11' AND s.OrganizationCode=@OrganizationCode
                                                            AND s.ActionDate = CONVERT(varchar(100), GETDATE(), 112)",new DbParameter[]
                        {
                            DataAccess.CreateParameter("OrganizationCode",DbType.String,drOrg["ORGCode"].ToString())
                        });
                        if (dtSum.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtSum.Rows)
                            {
                                sql = @"SELECT temp3.*,
                                        ISNULL(temp4.ProductLot,'') AS ProductLot,
                                        CAST(ISNULL(temp4.ShippingQty,0) AS INT) AS ShippingQty
                                        FROM 
                                        (
                                            SELECT case when temp1.ITEM_NUMBER IS NULL THEN temp2.ProductNo ELSE temp1.ITEM_NUMBER END AS ITEM_NUM,
                                            CAST(ISNULL(temp1.REQUESTED_QUANTITY,0) AS INT) AS RequestedQty,
                                            temp2.TripID
                                            FROM 
                                            (
                                                SELECT xtv.TRIP_ID,xtv.ITEM_NUMBER,sum(cast(xtv.REQUESTED_QUANTITY AS INT)) AS REQUESTED_QUANTITY 
                                                FROM XXOM_TRIP_V AS xtv 
                                                WHERE xtv.TRIP_ID=@TRIP_ID AND xtv.ORGANIZATION_CODE=@OrganizationCode
                                                GROUP BY xtv.TRIP_ID,xtv.ITEM_NUMBER
                                            ) temp1
                                            FULL JOIN
                                            (
                                                SELECT s.TripID,s.ProductNo,s.RequestedQty,SUM(ISNULL(s.ShippingQty,0)) AS ShippingQty 
                                                FROM STTS004 AS s
                                                WHERE s.TripID=@TRIP_ID AND s.ActionCode='11' AND s.OrganizationCode=@OrganizationCode
                                                GROUP BY s.TripID,s.ProductNo,s.Delivery_Detail_ID,s.RequestedQty
                                            ) temp2 ON temp1.TRIP_ID=temp2.TripID AND temp1.ITEM_NUMBER=temp2.ProductNo
                                            WHERE ISNULL(temp1.REQUESTED_QUANTITY,-1)!=ISNULL(temp2.ShippingQty,-1)                                       
                                        ) temp3 
                                        LEFT JOIN 
                                        (
	                                        SELECT s.TripID,s.ProductNo,s.ProductLot,SUM(ISNULL(s.ShippingQty,0)) AS ShippingQty 
                                            FROM STTS004 AS s
                                            WHERE s.TripID=@TRIP_ID AND s.ActionCode='11' AND s.OrganizationCode=@OrganizationCode 
                                            GROUP BY s.TripID,s.ProductNo,s.ProductLot
                                        ) temp4 ON temp3.TripID=temp4.TripID AND temp3.ITEM_NUM=temp4.ProductNo  ";
                                dtDetail = dataAccess.ExecuteDataTable(sql, new DbParameter[]
                                {
                                    DataAccess.CreateParameter("TRIP_ID", DbType.String, dr["TripID"].ToString()),
                                    DataAccess.CreateParameter("OrganizationCode", DbType.String, drOrg["ORGCode"].ToString())
                                });
                                if (dtDetail.Rows.Count == 0)
                                {
                                    continue;
                                }
                                if (!File.Exists(templatePath + "\\DETR01CUR.xlsx"))
                                {
                                    throw new Exception("提貨單撿貨模板DETR01CUR.xlsx不存在");
                                }
                                using (FileStream fs = new FileStream(templatePath + "\\DETR01CUR.xlsx", FileMode.Open,
                                    FileAccess.Read))
                                {
                                    workbook = new Workbook(fs);
                                    worksheet = workbook.Worksheets[0];
                                    Cells cells = worksheet.Cells;

                                    cells[1, 0].PutValue("提貨單號:" + dr["TripID"].ToString());
                                    cells[2, 0].PutValue("航程代號:" + dr["TripName"].ToString());
                                    int rowIndex = 4;
                                    foreach (DataRow drDt in dtDetail.Rows)
                                    {
                                        cells[rowIndex, 0].PutValue(drDt["ITEM_NUM"]);
                                        cells[rowIndex, 1].PutValue(drDt["ProductLot"]);
                                        cells[rowIndex, 2].PutValue(drDt["RequestedQty"]);
                                        cells[rowIndex++, 3].PutValue(drDt["ShippingQty"]);
                                    }
                                    if (!Directory.Exists(filePath + "\\" + workNo))
                                    {
                                        Directory.CreateDirectory(filePath + "\\" + workNo);
                                    }
                                    workbook.Save(filePath + "\\" + workNo + "\\" + dr["TripID"]+"_"+ drOrg["ORGCode"].ToString()+".xlsx");
                                }
                            }
                            SendEmail(mailSender, "南僑流通履歷系統", "提貨單撿貨差異", "", mailSender, mailPwd, mailHost, dataAccess,
                                workNo, filePath, drOrg["ORGCode"].ToString());
                        }
                        break;

                    #endregion

                    #region 轉播單撿貨

                    case "A002":
                        dtSum =dataAccess.ExecuteDataTable(
                                @"SELECT  s.TripID,s.TripName,s.TransferNo,s.CarNo,s2.OrganizationCode
                                                            FROM STTS003 AS s
                                                            LEFT JOIN STTS004 AS s2 ON s.TripID = s2.TripID AND s.ActionCode = s2.ActionCode
                                                            WHERE s.ActionCode = '12' AND s.OrganizationCode=@OrganizationCode
                                                            AND s.ActionDate = CONVERT(varchar(100), GETDATE(), 112)
                                                            GROUP BY  s.TripID,s.TripName,s.TransferNo,s.CarNo,s2.OrganizationCode",new DbParameter[ ]
                                {
                                    DataAccess.CreateParameter("OrganizationCode",DbType.String,drOrg["ORGCode"])
                                });
                        if (dtSum.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtSum.Rows)
                            {
                                sql =
                                    @"SELECT case when temp1.ITEM_NUM IS NULL THEN temp2.ProductNo ELSE temp1.ITEM_NUM END AS ITEM_NUM,
                                            CAST(ISNULL(temp1.REQUESTED_QUANTITY,0) AS INT) AS RequestedQty,
                                            ISNULL(temp1.LOT_NUMBER,'') AS LOT_NUMBER,
                                            CAST(ISNULL(temp2.ShippingQty,0) AS INT) AS ShippingQty,
                                            ISNULL(temp2.ProductLot,'') AS ProductLot,
                                            temp1.REQUESTED_UOM 
                                            FROM 
                                            (
                                                SELECT 
                                                xstv.ITEM_NUM,
                                                xstv.TRANSFER_NUMBER,
                                                xstv.REQUESTED_QUANTITY,
                                                xstv.LOT_NUMBER,
                                                xstv.REQUESTED_UOM
                                                FROM XXINV_SUB_TRX_V AS xstv  
                                                WHERE xstv.TRANSFER_NUMBER=@TRANSFER_NUMBER AND xstv.FM_ORG_CODE=@OrganizationCode2      
                                            ) temp1
                                            FULL JOIN
                                            (
                                                SELECT s.TripID,s.TripName,s.TransferNo,s2.ProductNo,s2.ProductLot,s2.RequestedQty,sum(s2.ShippingQty) AS ShippingQty
                                                FROM STTS003 AS s 
                                                LEFT JOIN STTS004 AS s2 ON s.TripID=s2.TripID AND s.ActionCode=s2.ActionCode
                                                WHERE s.ActionCode='12'	AND s.TransferNo=@TRANSFER_NUMBER AND s2.OrganizationCode= @OrganizationCode2 
                                                --AND s.CarNo=@CarNo 
                                                --AND s2.OrganizationCode=@OrganizationCode    
                                                GROUP BY s.TripID,s.TripName,s.TransferNo,s2.ProductNo,s2.ProductLot,s2.RequestedQty
                                            ) temp2 ON temp1.TRANSFER_NUMBER=temp2.TransferNo AND temp1.ITEM_NUM=temp2.ProductNo AND temp1.LOT_NUMBER=temp2.ProductLot 
                                            WHERE isnull(temp1.REQUESTED_QUANTITY,-1)!=isnull(temp2.ShippingQty,-1)
                                            OR isnull(temp1.LOT_NUMBER,-1)!=isnull(temp2.ProductLot,-1) ";
                                dtDetail = dataAccess.ExecuteDataTable(sql, new DbParameter[]
                                {
                                    DataAccess.CreateParameter("TRANSFER_NUMBER", DbType.String,dr["TransferNo"].ToString()),
                                    DataAccess.CreateParameter("OrganizationCode2", DbType.String, drOrg["ORGCode"].ToString()),
                                    DataAccess.CreateParameter("CarNo", DbType.String, dr["CarNo"].ToString()),
                                    DataAccess.CreateParameter("OrganizationCode", DbType.String,dr["OrganizationCode"].ToString())
                                });
                                if (dtDetail.Rows.Count == 0)
                                {
                                    continue;
                                }
                                if (!File.Exists(templatePath + "\\DETR02CUR.xlsx"))
                                {
                                    throw new Exception("轉播單撿貨模板DETR02CUR.xlsx不存在");
                                }
                                using (FileStream fs = new FileStream(templatePath + "\\DETR02CUR.xlsx", FileMode.Open,
                                    FileAccess.Read))
                                {
                                    workbook = new Workbook(fs);
                                    worksheet = workbook.Worksheets[0];
                                    Cells cells = worksheet.Cells;

                                    cells[1, 0].PutValue("轉撥單號:" + dr["TransferNo"].ToString());
                                    cells[2, 0].PutValue("航程代號:" + dr["TripName"].ToString());
                                    cells[3, 0].PutValue("組織代號:" + dr["OrganizationCode"].ToString());
                                    int rowIndex = 5;
                                    foreach (DataRow drDt in dtDetail.Rows)
                                    {
                                        cells[rowIndex, 0].PutValue(drDt["ITEM_NUM"]);
                                        cells[rowIndex, 1].PutValue(drDt["LOT_NUMBER"]);
                                        cells[rowIndex, 2].PutValue(drDt["ProductLot"]);
                                        cells[rowIndex, 3].PutValue(drDt["RequestedQty"]);
                                        cells[rowIndex++, 4].PutValue(drDt["ShippingQty"]);
                                    }
                                    if (!Directory.Exists(filePath + "\\" + workNo))
                                    {
                                        Directory.CreateDirectory(filePath + "\\" + workNo);
                                    }
                                    workbook.Save(filePath + "\\" + workNo + "\\" + dr["TransferNo"] + "_" + dr["CarNo"] +
                                                  "_" + drOrg["ORGCode"]  + ".xlsx");
                                }
                            }
                            SendEmail(mailSender, "南僑流通履歷系統", "轉播單撿貨差異", "", mailSender, mailPwd, mailHost, dataAccess,
                                workNo, filePath, drOrg["ORGCode"].ToString());
                        }
                        break;

                    #endregion

                    #region 外車直送撿貨

                    case "A003":
                        dtSum = dataAccess.ExecuteDataTable(@"SELECT  s.TripID,s.TripName,s2.CustomerID
                                                            FROM STTS003 AS s
                                                            LEFT JOIN STTS004 AS s2 ON s.TripID = s2.TripID AND s.ActionCode = s2.ActionCode
                                                            WHERE s.ActionCode = '14'  AND s.OrganizationCode=@OrganizationCode
                                                            AND s.ActionDate = CONVERT(varchar(100), GETDATE(), 112)
                                                            GROUP BY s.TripID,s.TripName,s2.CustomerID", new DbParameter[]
                        {
                            DataAccess.CreateParameter("OrganizationCode",DbType.String,drOrg["ORGCode"].ToString())
                        });
                        if (dtSum.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtSum.Rows)
                            {
                                sql =
                                    @"SELECT temp3.*,
                                        ISNULL(temp4.ProductLot,'') AS ProductLot,
                                        CAST(ISNULL(temp4.ShippingQty,0) AS INT) AS ShippingQty
                                        FROM 
                                        (                                        
                                            SELECT case when temp1.ITEM_NUMBER IS NULL THEN temp2.ProductNo ELSE temp1.ITEM_NUMBER END AS ITEM_NUM,
                                            CAST(ISNULL(temp1.REQUESTED_QUANTITY,0) AS INT) AS RequestedQty,
                                            temp2.TripID
                                            FROM 
                                            (
                                                SELECT xtv.TRIP_ID,xtv.ITEM_NUMBER,sum(cast(xtv.REQUESTED_QUANTITY AS INT)) AS REQUESTED_QUANTITY
                                                FROM XXOM_TRIP_V AS xtv
                                                WHERE xtv.TRIP_ID=@TRIP_ID AND xtv.ORGANIZATION_CODE=@OrganizationCode   
                                                GROUP BY xtv.TRIP_ID,xtv.ITEM_NUMBER
                                            ) temp1
                                            FULL JOIN
                                            (
                                                SELECT s.TripID,s.ProductNo,s.RequestedQty,SUM(ISNULL(s.ShippingQty,0)) AS ShippingQty 
                                                FROM STTS004 AS s
                                                WHERE s.TripID=@TRIP_ID AND s.ActionCode='14' AND s.OrganizationCode=@OrganizationCode 
                                                --AND s.CustomerID=@CustomerID	
                                                GROUP BY s.TripID,s.ProductNo,s.Delivery_Detail_ID,s.RequestedQty
                                            ) temp2 ON temp1.TRIP_ID=temp2.TripID  AND temp1.ITEM_NUMBER=temp2.ProductNo
                                            WHERE ISNULL(temp1.REQUESTED_QUANTITY,-1)!=ISNULL(temp2.ShippingQty,-1)
                                        ) temp3
                                        LEFT JOIN 
                                        (
	                                        SELECT s.TripID,s.ProductNo,s.ProductLot,SUM(ISNULL(s.ShippingQty,0)) AS ShippingQty 
                                            FROM STTS004 AS s
                                            WHERE s.TripID=@TRIP_ID AND s.ActionCode='14' AND s.OrganizationCode=@OrganizationCode 
                                            GROUP BY s.TripID,s.ProductNo,s.ProductLot
                                        ) temp4 ON temp3.TripID=temp4.TripID AND temp3.ITEM_NUM=temp4.ProductNo";
                                dtDetail = dataAccess.ExecuteDataTable(sql, new DbParameter[]
                                {
                                    DataAccess.CreateParameter("TRIP_ID", DbType.String, dr["TripID"].ToString()),
                                    DataAccess.CreateParameter("OrganizationCode", DbType.String, drOrg["ORGCode"].ToString()),
                                    DataAccess.CreateParameter("CustomerID", DbType.String, dr["CustomerID"].ToString())
                                });
                                if (dtDetail.Rows.Count == 0)
                                {
                                    continue;
                                }
                                if (!File.Exists(templatePath + "\\DETR03CUR.xlsx"))
                                {
                                    throw new Exception("外車直送撿貨模板DETR03CUR.xlsx不存在");
                                }
                                using (FileStream fs = new FileStream(templatePath + "\\DETR03CUR.xlsx", FileMode.Open,
                                    FileAccess.Read))
                                {
                                    workbook = new Workbook(fs);
                                    worksheet = workbook.Worksheets[0];
                                    Cells cells = worksheet.Cells;

                                    cells[1, 0].PutValue("提貨單號:" + dr["TripID"].ToString());
                                    cells[2, 0].PutValue("航程代號:" + dr["TripName"].ToString());

                                    int rowIndex = 4;
                                    foreach (DataRow drDt in dtDetail.Rows)
                                    {
                                        cells[rowIndex, 0].PutValue(drDt["ITEM_NUM"]);
                                        cells[rowIndex, 1].PutValue(drDt["ProductLot"]);
                                        cells[rowIndex, 2].PutValue(drDt["RequestedQty"]);
                                        cells[rowIndex++, 3].PutValue(drDt["ShippingQty"]);
                                    }
                                    if (!Directory.Exists(filePath + "\\" + workNo))
                                    {
                                        Directory.CreateDirectory(filePath + "\\" + workNo);
                                    }
                                    workbook.Save(filePath + "\\" + workNo + "\\" + dr["TripID"] + "_" +
                                                  dr["CustomerID"] + "_" + drOrg["ORGCode"].ToString() + ".xlsx");
                                }
                            }
                            SendEmail(mailSender, "南僑流通履歷系統", "外車直送撿貨差異", "", mailSender, mailPwd, mailHost, dataAccess,
                                workNo, filePath, drOrg["ORGCode"].ToString());
                        }
                        break;

                    #endregion

                    #region 營所進貨

                    case "A006":
                        dtSum =
                            dataAccess.ExecuteDataTable(@"SELECT  s.TripID,s.TripName,s.TransferNo,s2.OrganizationCode
                                                            FROM STTS003 AS s
                                                            LEFT JOIN STTS004 AS s2 ON s.TripID = s2.TripID AND s.ActionCode = s2.ActionCode
                                                            WHERE s.ActionCode = '40' AND s.OrganizationCode=@OrganizationCode
                                                            AND s.ActionDate = CONVERT(varchar(100), GETDATE(), 112)
                                                            GROUP BY  s.TripID,s.TripName,s.TransferNo,s2.OrganizationCode", new DbParameter[]
                        {
                            DataAccess.CreateParameter("OrganizationCode",DbType.String,drOrg["ORGCode"].ToString())
                        });
                        if (dtSum.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtSum.Rows)
                            {
                                sql = @"SELECT case when temp1.ProductNo IS NULL THEN temp2.ActProductNo ELSE temp1.ProductNo END AS ITEM_NUM,
                                        isnull(temp1.ProductLot,'') AS LOT_NUMBER,
                                        isnull(temp2.ActProductLot,'') AS ProductLot,
                                        cast(isnull(temp1.RequestQty,0) AS INT) AS RequestedQty,
                                        cast(isnull(temp2.ShippingQty,0) AS INT) AS ShippingQty,temp1.TripID
                                        FROM 
                                        (
                                            SELECT s.TripID,s.TransferNo,s2.ProductNo,s2.ProductLot,sum(s2.ShippingQty) AS RequestQty
                                            FROM STTS003 AS s
                                            LEFT JOIN STTS004 AS s2 ON s.TripID=s2.TripID AND s.ActionCode=s2.ActionCode
                                            WHERE s.TransferNo=@TransferNo AND s.ActionCode='12'
                                            AND S2.OrganizationCode=@OrganizationCode
                                            GROUP BY s.TripID,s.TransferNo,s2.ProductNo,s2.ProductLot
                                        ) temp1
                                        FULL JOIN 
                                        (
                                            SELECT s.TripID AS ActTripID,s.TransferNo AS ActTransferNo,s2.ProductNo AS ActProductNo,s2.ProductLot AS ActProductLot,sum(s2.ShippingQty) AS ShippingQty
                                            FROM STTS003 AS s
                                            LEFT JOIN STTS004 AS s2 ON s.TripID=s2.TripID AND s.ActionCode=s2.ActionCode
                                            WHERE s.TransferNo=@TransferNo AND s.ActionCode='40' 
                                            AND S2.OrganizationCode=@OrganizationCode
                                            GROUP BY s.TripID,s.TransferNo,s2.ProductNo,s2.ProductLot
                                        ) temp2 ON temp1.TripID=temp2.ActTripID AND temp1.TransferNo=temp2.ActTransferNo AND temp1.ProductNo=temp2.ActProductNo AND temp1.ProductLot=temp2.ActProductLot
                                        WHERE isnull(temp1.RequestQty,-1)!=isnull(temp2.ShippingQty,-1)
                                        OR isnull(temp1.ProductLot,-1)!=isnull(temp2.ActProductLot,-1) 
                                        ORDER BY temp1.TripID DESC ";
                                dtDetail = dataAccess.ExecuteDataTable(sql, new DbParameter[]
                                {
                                    DataAccess.CreateParameter("TransferNo", DbType.String,dr["TransferNo"].ToString()),
                                    DataAccess.CreateParameter("OrganizationCode", DbType.String,drOrg["ORGCode"].ToString())
                                });
                                if (dtDetail.Rows.Count == 0)
                                {
                                    continue;
                                }
                                if (!File.Exists(templatePath + "\\DETR05CUR.xlsx"))
                                {
                                    throw new Exception("營所進貨模板DETR05CUR.xlsx不存在");
                                }
                                using (FileStream fs = new FileStream(templatePath + "\\DETR05CUR.xlsx", FileMode.Open,
                                    FileAccess.Read))
                                {
                                    workbook = new Workbook(fs);
                                    worksheet = workbook.Worksheets[0];
                                    Cells cells = worksheet.Cells;

                                    cells[1, 0].PutValue("轉撥單號:" + dr["TransferNo"].ToString());
                                    cells[2, 0].PutValue("航程代號:" + dr["TripName"].ToString());
                                    cells[3, 0].PutValue("組織代號:" + dr["OrganizationCode"].ToString());
                                    int rowIndex = 5;
                                    foreach (DataRow drDt in dtDetail.Rows)
                                    {
                                        cells[rowIndex, 0].PutValue(drDt["ITEM_NUM"]);
                                        cells[rowIndex, 1].PutValue(drDt["LOT_NUMBER"]);
                                        cells[rowIndex, 2].PutValue(drDt["ProductLot"]);
                                        cells[rowIndex, 3].PutValue(drDt["RequestedQty"]);
                                        cells[rowIndex++, 4].PutValue(drDt["ShippingQty"]);
                                    }
                                    if (!Directory.Exists(filePath + "\\" + workNo))
                                    {
                                        Directory.CreateDirectory(filePath + "\\" + workNo);
                                    }
                                    workbook.Save(filePath + "\\" + workNo + "\\" + dr["TransferNo"] + "_" + drOrg["ORGCode"].ToString() + ".xlsx");
                                }
                            }
                            SendEmail(mailSender, "南僑流通履歷系統", "營所進貨差異", "", mailSender, mailPwd, mailHost, dataAccess,
                                workNo, filePath, drOrg["ORGCode"].ToString());
                        }
                        break;

                    #endregion

                    #region 出貨上車

                    case "A007":
                        dtSum = dataAccess.ExecuteDataTable(@"SELECT  distinct s.TripID,s.TripName
                                                            FROM STTS003 AS s                                                           
                                                            WHERE s.ActionCode = '20' AND s.OrganizationCode=@OrganizationCode 
                                                            AND s.ActionDate = CONVERT(varchar(100), GETDATE(), 112)
                                                            GROUP BY  s.TripID,s.TripName", new DbParameter[]
                        {
                            DataAccess.CreateParameter("OrganizationCode",DbType.String,drOrg["ORGCode"].ToString())
                        });
                        if (dtSum.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtSum.Rows)
                            {
                                sql = @"SELECT case when temp1.ProductNo IS NULL THEN temp2.ActProductNo ELSE temp1.ProductNo END AS ITEM_NUM,
                                        isnull(temp1.ProductLot,'') AS LOT_NUMBER,
                                        isnull(temp2.ActProductLot,'') AS ProductLot,
                                        cast(isnull(temp1.RequestQty,0) AS INT) AS RequestedQty,
                                        cast(isnull(temp2.ShippingQty,0) AS INT) AS ShippingQty,temp1.TripID
                                        FROM 
                                        (
                                            SELECT s.TripID,s.ProductNo,s.ProductLot,sum(s.ShippingQty) AS RequestQty
                                            FROM STTS004 AS s
                                            WHERE s.TripID=@TripID AND s.ActionCode='11' AND s.OrganizationCode= @OrganizationCode
                                            GROUP BY s.TripID,s.ProductNo,s.ProductLot
                                        ) temp1
                                        FULL JOIN 
                                        (
                                            SELECT s.TripID AS ActTripID,s.ProductNo AS ActProductNo,s.ProductLot AS ActProductLot,sum(s.ShippingQty) AS ShippingQty
                                            FROM STTS004 AS s                          
                                            WHERE s.TripID=@TripID AND s.ActionCode='20' AND s.OrganizationCode= @OrganizationCode
                                            GROUP BY s.TripID,s.ProductNo,s.ProductLot
                                        ) temp2 ON temp1.TripID=temp2.ActTripID  AND temp1.ProductNo=temp2.ActProductNo AND temp1.ProductLot=temp2.ActProductLot
                                        WHERE isnull(temp1.RequestQty,-1)!=isnull(temp2.ShippingQty,-1)
                                        OR isnull(temp1.ProductLot,-1)!=isnull(temp2.ActProductLot,-1) 
                                        ORDER BY temp1.TripID DESC   ";
                                dtDetail = dataAccess.ExecuteDataTable(sql, new DbParameter[]
                                {
                                    DataAccess.CreateParameter("TripID", DbType.String, dr["TripID"].ToString()),
                                    DataAccess.CreateParameter("OrganizationCode", DbType.String,drOrg["ORGCode"].ToString())
                                });
                                if (dtDetail.Rows.Count == 0)
                                {
                                    continue;
                                }
                                if (!File.Exists(templatePath + "\\DETR06CUR.xlsx"))
                                {
                                    throw new Exception("出貨上車模板DETR06CUR.xlsx不存在");
                                }
                                using (FileStream fs = new FileStream(templatePath + "\\DETR06CUR.xlsx", FileMode.Open,
                                    FileAccess.Read))
                                {
                                    workbook = new Workbook(fs);
                                    worksheet = workbook.Worksheets[0];
                                    Cells cells = worksheet.Cells;

                                    cells[1, 0].PutValue("提貨單號:" + dr["TripID"].ToString());
                                    cells[2, 0].PutValue("航程代號:" + dr["TripName"].ToString());
                                    int rowIndex = 4;
                                    foreach (DataRow drDt in dtDetail.Rows)
                                    {
                                        cells[rowIndex, 0].PutValue(drDt["ITEM_NUM"]);
                                        cells[rowIndex, 1].PutValue(drDt["LOT_NUMBER"]);
                                        cells[rowIndex, 2].PutValue(drDt["ProductLot"]);
                                        cells[rowIndex, 3].PutValue(drDt["RequestedQty"]);
                                        cells[rowIndex++, 4].PutValue(drDt["ShippingQty"]);
                                    }
                                    if (!Directory.Exists(filePath + "\\" + workNo))
                                    {
                                        Directory.CreateDirectory(filePath + "\\" + workNo);
                                    }
                                    workbook.Save(filePath + "\\" + workNo + "\\" + dr["TripID"] + "_" + drOrg["ORGCode"].ToString() + ".xlsx");
                                }
                            }
                            SendEmail(mailSender, "南僑流通履歷系統", "出貨上車差異", "", mailSender, mailPwd, mailHost, dataAccess,
                                workNo, filePath, drOrg["ORGCode"].ToString());
                        }
                        break;

                    #endregion

                    #region 出貨下車

                    case "A008":
                        dtSum = dataAccess.ExecuteDataTable(@"SELECT  s.TripID,s.TripName,s2.CustomerID,f.CustomerName
                                                            FROM STTS003 AS s
                                                            LEFT JOIN STTS004 AS s2 ON s.TripID = s2.TripID AND s.ActionCode = s2.ActionCode
                                                            LEFT JOIN FRTA002 AS f ON s2.CustomerID=f.CustomerNo
                                                            WHERE s.ActionCode = '30' AND s.OrganizationCode=@OrganizationCode 
                                                            AND s.ActionDate = CONVERT(varchar(100), GETDATE(), 112)
                                                            GROUP BY s.TripID,s.TripName,s2.CustomerID,f.CustomerName", new DbParameter[]
                        {
                            DataAccess.CreateParameter("OrganizationCode",DbType.String,drOrg["ORGCode"].ToString())
                        });
                        if (dtSum.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtSum.Rows)
                            {
                                sql = @"SELECT temp.ProductNo AS ITEM_NUM,
                                        isnull(temp.ProductLot,'') AS ProductLot,
                                        cast(isnull(temp.RequestedQty,0) AS INT) AS RequestedQty,
                                        cast(isnull(temp.ShippingQty,0) AS INT) AS ShippingQty,
                                        temp.TripID
                                        FROM 
                                        (
                                            SELECT s.TripID,s.ProductNo,s.ProductLot,s.RequestedQty,s.CustomerID,sum(isnull(s.ShippingQty,0)) AS ShippingQty
                                            FROM STTS004 AS s
                                            WHERE s.TripID=@TripID
                                            AND s.ActionCode='30' 
                                            AND s.OrganizationCode= @OrganizationCode 
                                            AND s.CustomerID=@CustomerID
                                            GROUP BY s.TripID,s.ProductNo,s.ProductLot,s.RequestedQty,s.CustomerID 
                                        ) temp
                                        WHERE isnull(temp.RequestedQty,-1)!=isnull(temp.ShippingQty,-1)
                                        ORDER BY temp.TripID DESC ";
                                dtDetail = dataAccess.ExecuteDataTable(sql, new DbParameter[]
                                {
                                    DataAccess.CreateParameter("TripID", DbType.String, dr["TripID"].ToString()),
                                    DataAccess.CreateParameter("OrganizationCode", DbType.String, drOrg["ORGCode"].ToString()),
                                    DataAccess.CreateParameter("CustomerID", DbType.String, dr["CustomerID"].ToString())
                                });
                                if (dtDetail.Rows.Count == 0)
                                {
                                    continue;
                                }
                                if (!File.Exists(templatePath + "\\DETR07CUR.xlsx"))
                                {
                                    throw new Exception("出貨下車模板DETR07CUR.xlsx不存在");
                                }
                                using (FileStream fs = new FileStream(templatePath + "\\DETR07CUR.xlsx", FileMode.Open,
                                    FileAccess.Read))
                                {
                                    workbook = new Workbook(fs);
                                    worksheet = workbook.Worksheets[0];
                                    Cells cells = worksheet.Cells;

                                    cells[1, 0].PutValue("簽收單號:" + dr["TripID"]);
                                    cells[1, 3].PutValue("航程代號:" + dr["TripName"]);
                                    cells[2, 0].PutValue("客戶代號:" + dr["CustomerID"]);
                                    cells[2, 3].PutValue("客戶名稱:" + dr["CustomerName"]);
                                    int rowIndex = 4;
                                    foreach (DataRow drDt in dtDetail.Rows)
                                    {
                                        cells[rowIndex, 0].PutValue(drDt["ITEM_NUM"]);
                                        cells[rowIndex, 1].PutValue(drDt["ProductLot"]);
                                        cells[rowIndex, 2].PutValue(drDt["RequestedQty"]);
                                        cells[rowIndex++, 3].PutValue(drDt["ShippingQty"]);
                                    }
                                    if (!Directory.Exists(filePath + "\\" + workNo))
                                    {
                                        Directory.CreateDirectory(filePath + "\\" + workNo);
                                    }
                                    workbook.Save(filePath + "\\" + workNo + "\\" + dr["TripID"] + "_" +dr["CustomerID"] + "_" + drOrg["ORGCode"].ToString() + ".xlsx");
                                }
                            }
                            SendEmail(mailSender, "南僑流通履歷系統", "出貨下車差異", "", mailSender, mailPwd, mailHost, dataAccess,
                                workNo, filePath, drOrg["ORGCode"].ToString());
                        }
                        break;

                    #endregion

                    #region 外車直送下車

                    case "A004":
                        dtSum =
                            dataAccess.ExecuteDataTable(
                                @"SELECT s.TripID,s.TripName,s2.CustomerID,f.CustomerName                               
                                                            FROM STTS003 AS s
                                                            LEFT JOIN STTS004 AS s2 ON s.TripID = s2.TripID AND s.ActionCode = s2.ActionCode
                                                            LEFT JOIN FRTA002 AS f ON s2.CustomerID=f.CustomerNo  
                                                            WHERE s2.ActionCode = '32' AND s.OrganizationCode=@OrganizationCode 
                                                            AND s.ActionDate = CONVERT(varchar(100), GETDATE(), 112)
                                                            GROUP BY s.TripID,s.TripName,s2.CustomerID,f.CustomerName", new DbParameter[]
                        {
                            DataAccess.CreateParameter("OrganizationCode",DbType.String,drOrg["ORGCode"].ToString())
                        });
                        if (dtSum.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtSum.Rows)
                            {
                                sql = @"SELECT case when temp1.ProductNo IS NULL THEN temp2.ActProductNo ELSE temp1.ProductNo END AS ITEM_NUM,
                                        isnull(temp1.ProductLot,'') AS LOT_NUMBER,
                                        isnull(temp2.ActProductLot,'') AS ProductLot,
                                        cast(isnull(temp1.RequestQty,0) AS INT) AS RequestedQty,
                                        cast(isnull(temp2.ShippingQty,0) AS INT) AS ShippingQty,temp1.TripID
                                        FROM 
                                        (
                                            SELECT s.TripID,s.ProductNo,s.ProductLot,sum(s.ShippingQty) AS RequestQty
                                            FROM STTS004 AS s
                                            WHERE s.TripID=@TripID AND s.ActionCode='14' AND s.OrganizationCode=@OrganizationCode 
                                            --AND s.CustomerID=@CustomerID
                                            GROUP BY s.TripID,s.ProductNo,s.ProductLot
                                        ) temp1
                                        FULL JOIN 
                                        (
                                            SELECT s.TripID AS ActTripID,s.ProductNo AS ActProductNo,s.ProductLot AS ActProductLot,sum(s.ShippingQty) AS ShippingQty
                                            FROM STTS004 AS s
                                            WHERE s.TripID=@TripID AND s.ActionCode='32' AND s.OrganizationCode=@OrganizationCode
                                            --AND s.CustomerID=@CustomerID
                                            GROUP BY s.TripID,s.ProductNo,s.ProductLot
                                        ) temp2 ON temp1.TripID=temp2.ActTripID  AND temp1.ProductNo=temp2.ActProductNo AND temp1.ProductLot=temp2.ActProductLot
                                        WHERE isnull(temp1.RequestQty,-1)!=isnull(temp2.ShippingQty,-1)
                                        OR isnull(temp1.ProductLot,-1)!=isnull(temp2.ActProductLot,-1) 
                                        ORDER BY temp1.TripID DESC ";
                                dtDetail = dataAccess.ExecuteDataTable(sql, new DbParameter[]
                                {
                                    DataAccess.CreateParameter("TripID", DbType.String, dr["TripID"].ToString()),
                                    DataAccess.CreateParameter("OrganizationCode", DbType.String,drOrg["ORGCode"].ToString()),
                                    DataAccess.CreateParameter("CustomerID", DbType.String, dr["CustomerID"].ToString())
                                });
                                if (dtDetail.Rows.Count == 0)
                                {
                                    continue;
                                }
                                if (!File.Exists(templatePath + "\\DETR08CUR.xlsx"))
                                {
                                    throw new Exception("外車直送下車模板DETR08CUR.xlsx不存在");
                                }
                                using (FileStream fs = new FileStream(templatePath + "\\DETR08CUR.xlsx", FileMode.Open,
                                    FileAccess.Read))
                                {
                                    workbook = new Workbook(fs);
                                    worksheet = workbook.Worksheets[0];
                                    Cells cells = worksheet.Cells;

                                    cells[1, 0].PutValue("簽收單號:" + dr["TripID"]);
                                    cells[1, 3].PutValue("航程代號:" + dr["TripName"]);
                                    cells[2, 0].PutValue("客戶代號:" + dr["CustomerID"]);
                                    cells[2, 3].PutValue("客戶名稱:" + dr["CustomerName"]);
                                    int rowIndex = 4;
                                    foreach (DataRow drDt in dtDetail.Rows)
                                    {
                                        cells[rowIndex, 0].PutValue(drDt["ITEM_NUM"]);
                                        cells[rowIndex, 1].PutValue(drDt["LOT_NUMBER"]);
                                        cells[rowIndex, 2].PutValue(drDt["ProductLot"]);
                                        cells[rowIndex, 3].PutValue(drDt["RequestedQty"]);
                                        cells[rowIndex++, 4].PutValue(drDt["ShippingQty"]);
                                    }
                                    if (!Directory.Exists(filePath + "\\" + workNo))
                                    {
                                        Directory.CreateDirectory(filePath + "\\" + workNo);
                                    }
                                    workbook.Save(filePath + "\\" + workNo + "\\" + dr["TripID"] + "_" +
                                                  dr["CustomerID"] + "_" + drOrg["ORGCode"].ToString() + ".xlsx");
                                }
                            }
                            SendEmail(mailSender, "南僑流通履歷系統", "外車直送下車差異", "", mailSender, mailPwd, mailHost, dataAccess,
                                workNo, filePath, drOrg["ORGCode"].ToString());
                        }
                        break;

                    #endregion

                    #region 退貨單退貨

                    case "A010":
                        dtSum = dataAccess.ExecuteDataTable(@"SELECT s.RefundNo,s.CustomerID,f.CustomerName
                                                            FROM STTS005 AS s
                                                            LEFT JOIN STTS006 AS s2 ON s.RefundNo=s2.RefundNo
                                                            LEFT JOIN FRTA002 AS f ON s.CustomerID=f.CustomerNo
                                                            WHERE s.OrganizationCode=@OrganizationCode
                                                            AND s.ActionDate = CONVERT(varchar(100), GETDATE(), 112)
                                                            GROUP BY s.RefundNo,s.CustomerID,f.CustomerName",new DbParameter[]
                        {
                            DataAccess.CreateParameter("OrganizationCode",DbType.String,drOrg["ORGCode"].ToString())
                        });
                        if (dtSum.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtSum.Rows)
                            {
                                sql = @"SELECT * 
                                        FROM
                                        (
                                        SELECT s.ProductNo, s.ProductLot, sum(isnull(s.ShippingQty, 0)) AS ShippingQty, isnull(s.RequestedQty, 0) AS REQUESTED_QUANTITY
                                        FROM STTS006 AS s
                                        LEFT JOIN STTS005 AS s2 ON s.RefundNo = s2.RefundNo
                                        WHERE s.RefundNo =@RefundNo AND s.OrganizationCode=@OrganizationCode
                                        --AND s2.CustomerID=@CustomerID 
                                        GROUP BY s.ProductNo, s.ProductLot, s.RequestedQty) temp
                                        where temp.ShippingQty != temp.REQUESTED_QUANTITY ";
                                dtDetail = dataAccess.ExecuteDataTable(sql, new DbParameter[]
                                {
                                    DataAccess.CreateParameter("RefundNo", DbType.String, dr["RefundNo"].ToString()),
                                    DataAccess.CreateParameter("OrganizationCode", DbType.String,  drOrg["ORGCode"].ToString()),
                                    DataAccess.CreateParameter("CustomerID", DbType.String, dr["CustomerID"].ToString())
                                });
                                if (dtDetail.Rows.Count == 0)
                                {
                                    continue;
                                }
                                if (!File.Exists(templatePath + "\\DETR10CUR.xlsx"))
                                {
                                    throw new Exception("退貨單退貨模板DETR10CUR.xlsx不存在");
                                }
                                using (FileStream fs = new FileStream(templatePath + "\\DETR10CUR.xlsx", FileMode.Open,
                                    FileAccess.Read))
                                {
                                    workbook = new Workbook(fs);
                                    worksheet = workbook.Worksheets[0];
                                    Cells cells = worksheet.Cells;

                                    cells[1, 0].PutValue("退貨單號:" + dr["RefundNo"]);
                                    cells[2, 0].PutValue("客戶代號:" + dr["CustomerID"]);
                                    cells[3, 0].PutValue("客戶名稱:" + dr["CustomerName"]);
                                    int rowIndex = 5;
                                    foreach (DataRow drDt in dtDetail.Rows)
                                    {
                                        cells[rowIndex, 0].PutValue(drDt["ProductNo"]);
                                        cells[rowIndex, 1].PutValue(drDt["ProductLot"]);
                                        cells[rowIndex, 2].PutValue(drDt["REQUESTED_QUANTITY"]);
                                        cells[rowIndex, 3].PutValue(drDt["ShippingQty"]);
                                    }
                                    if (!Directory.Exists(filePath + "\\" + workNo))
                                    {
                                        Directory.CreateDirectory(filePath + "\\" + workNo);
                                    }
                                    workbook.Save(filePath + "\\" + workNo + "\\" + dr["TripID"] + "_" +
                                                  dr["CustomerID"] + "_" + drOrg["ORGCode"].ToString() + ".xlsx");
                                }
                            }
                            SendEmail(mailSender, "南僑流通履歷系統", "退貨單退貨差異", "", mailSender, mailPwd, mailHost, dataAccess,
                                workNo, filePath, drOrg["ORGCode"].ToString());
                        }
                        break;

                        #endregion
                }
            }
        }

        public static void SendEmail(string SenderEmail, string SenderName, string email_title, string email_body, string account, string password, string Host, DataAccess dataAccess, string workType, string filePath, string organization)
        {
            try
            {
                if (!Directory.Exists(filePath + "\\" + workType))
                {
                    return;
                }
                DirectoryInfo directoryInfo = new DirectoryInfo(filePath + "\\" + workType);
                
                FileInfo[] fileInfos = directoryInfo.GetFiles("*.xlsx");
                if (fileInfos.Length == 0)
                {
                    return;
                }
                MailMessage msg = new MailMessage();
                msg.IsBodyHtml = true;

                msg.From = new MailAddress(SenderEmail, SenderName);
                DataTable dtReceivers =
                    dataAccess.ExecuteDataTable(@"SELECT e.Email 
                                                    FROM AlarmConfig AS ac
                                                    LEFT JOIN Employees AS e ON ac.EmpID =e.EmpID 
                                                    WHERE isnull(e.Email,'')!='' AND  ac.CB001 = @CB001
                                                    AND ac.EmpID IN(SELECT e.EmpID FROM Employees AS e WHERE e.ORGCode = @ORGCode)",
                        new DbParameter[]
                        {
                            DataAccess.CreateParameter("CB001", DbType.String, workType),
                            DataAccess.CreateParameter("ORGCode", DbType.String, organization)
                        });
                if (dtReceivers.Rows.Count == 0)
                {
                    return;
                }
                foreach (DataRow dr in dtReceivers.Rows)
                {
                    msg.To.Add(dr["Email"].ToString());
                }
                foreach (FileInfo fileInfo in fileInfos)
                {
                    if (Path.GetFileNameWithoutExtension(fileInfo.FullName).EndsWith(organization))
                    {
                        msg.Attachments.Add(new Attachment(fileInfo.FullName));
                    }
                }
                msg.Subject = email_title;
                msg.Body = email_body;

                SmtpClient SmtpMail = new SmtpClient();
                SmtpMail.Host = Host;
                SmtpMail.Credentials = new System.Net.NetworkCredential(account, password);
                SmtpMail.UseDefaultCredentials = false;
                SmtpMail.DeliveryMethod=SmtpDeliveryMethod.Network;
                SmtpMail.Send(msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
