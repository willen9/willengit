using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Xml;
using NPOI.HSSF.UserModel;
using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;
using Rebex.Net;
using REGAL.Data.DataAccess;
using System.Configuration;

namespace RegalPlatForm
{
    class Program
    {
        static void Main(string[] args)
        {
            int index = 0;
            try
            {
                DataAccess dataAccess = new DataAccess();
                dataAccess.ConnectionString = ConfigurationManager.AppSettings["conStr"].ToString();
                dataAccess.ProviderName = "System.Data.SqlClient";

                if (args.Length > 0)
                {
                    if (args[0].ToUpper() == "C") //轉檔
                    {
                        if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Template\Template.xls"))
                        {
                            Console.WriteLine("Excel模板不存在");
                            Console.Read();
                            return;
                        }
                        if (!Directory.Exists(ConfigurationManager.AppSettings["ExcelPath"]))
                        {
                            Directory.CreateDirectory(ConfigurationManager.AppSettings["ExcelPath"]);
                        }
                        DataTable dtExport = dataAccess.ExecuteDataTable(@"SELECT d.[MN-MachineType],d.[MN-NO],
                                                                            d.[MN-TESTER],
                                                                            d.[MN-DATE]+' '+d.[MN-TIME] AS DT,
                                                                            d.[MN-CPU1_TEMP],
                                                                            --d.[MN-CPU1_TEMP],d.[MN-AIR_TEMP],
                                                                            cast((convert(DECIMAL(8,4),d.[MN-CPU1_TEMP])-convert(DECIMAL(8,4),d.[MN-AIR_TEMP])) AS float) AS CPUMUL,
                                                                            --d.[MN-CPU1_TEMP],d.[MN-AIR_TEMP],d.[MN-W],
                                                                            (CAST((convert(DECIMAL(8,4),d.[MN-CPU1_TEMP])-convert(DECIMAL(8,4),d.[MN-AIR_TEMP])/convert(DECIMAL(8,4),d.[MN-W])) AS FLOAT)) AS CPUR,
                                                                            d.[MN-CPU2_TEMP],
                                                                            --d.[MN-CPU2_TEMP],d.[MN-AIR_TEMP],
                                                                            cast((convert(DECIMAL(8,4),d.[MN-CPU2_TEMP])-convert(DECIMAL(8,4),d.[MN-AIR_TEMP])) AS float) AS VGAMUL,
                                                                            --d.[MN-CPU2_TEMP],d.[MN-AIR_TEMP],d.[MN-W],
                                                                            (CAST((convert(DECIMAL(8,4),d.[MN-CPU2_TEMP])-convert(DECIMAL(8,4),d.[MN-AIR_TEMP])/convert(DECIMAL(8,4),d.[MN-W])) AS FLOAT)) AS VGAR,
                                                                            --d.[MN-CPU1_TEMP],d.[MN-CPU2_TEMP],
                                                                            cast((convert(DECIMAL(8,4),d.[MN-CPU1_TEMP])-convert(DECIMAL(8,4),d.[MN-CPU2_TEMP])) AS float) AS CPUVGAMUL,
                                                                            (CAST((convert(DECIMAL(8,4),d.[MN-CPU1_TEMP])-convert(DECIMAL(8,4),d.[MN-AIR_TEMP])/convert(DECIMAL(8,4),d.[MN-W])) AS FLOAT))-(CAST((convert(DECIMAL(8,4),d.[MN-CPU2_TEMP])-convert(DECIMAL(8,4),d.[MN-AIR_TEMP])/convert(DECIMAL(8,4),d.[MN-W])) AS FLOAT)) AS CPUVGAR,
                                                                            d.[MN-AIR_TEMP],
                                                                            d.[MN-W1],
                                                                            d.[MN-W2],
                                                                            d.[MN-STATES],                                                                           
                                                                            '0' AS ErrorCode
                                                                            FROM DASTA AS d
                                                                            inner join DASOP as G on d.[MN-MachineType]=G.SOP001
                                                                            WHERE d.[MN-DATE]+d.[MN-TIME] >=CONVERT(varchar(100),DATEADD(DAY,-1,GETDATE()), 111)+'00:00:00'
                                                                            AND d.[MN-DATE]+d.[MN-TIME] <CONVERT(varchar(100),GETDATE(), 111)+'00:00:00'
                                                                            AND d.[MN-STATES]='Pass'
                                                                            AND G.SOP001<>'' AND G.SOP022<>''
                                                                            AND d.[MN-AIR_TEMP] BETWEEN '22' AND '28' 
                                                                            ORDER BY d.[MN-MachineType] ");
                        if (dtExport.Rows.Count == 0)
                        {
                            return;
                        }
                        string machineType = "";
                        List<string> lstGolden = new List<string>();
                        FileStream fs = null;
                        HSSFWorkbook ws = null;
                        ISheet sheet = null;
                        IRow row = null;
                        DataTable dtHeader = null;
                        int rowIndex = 6;
                        int goldenCount = 0;
                        string fileName = "";
                        string date = DateTime.Now.ToString("yyyyMMdd");
                        List<string> lstMac = new List<string>();
                        foreach (DataRow dr in dtExport.Rows)
                        {
                            index = rowIndex;
                            if (machineType == "")
                            {
                                fileName = ConfigurationManager.AppSettings["ExcelPath"] + @"\" +
                                           dr["MN-MachineType"].ToString() + "_ARS-CQ_" + date +
                                           ".xls";
                                File.Copy(AppDomain.CurrentDomain.BaseDirectory + @"Template\Template.xls", fileName, true);
                                fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
                                ws = new HSSFWorkbook(fs);
                                sheet = ws.GetSheetAt(0);
                                rowIndex = 6;
                                if (!lstMac.Contains(dr["MN-MachineType"].ToString()))
                                {
                                    lstMac.Add(dr["MN-MachineType"].ToString());
                                }
                                machineType = dr["MN-MachineType"].ToString().ToLower();
                             
                                DataRow[] drs =
                               dtExport.Select("[MN-NO] like '%GOLDEN' AND [MN-MachineType]='" +
                                               dr["MN-MachineType"].ToString() + "'", "MN-TESTER");
                               
                                foreach (DataRow drGolden in drs)
                                {
                                    if (lstGolden.Contains(drGolden["MN-TESTER"].ToString()))
                                    {
                                        continue;
                                    }
                                    row = sheet.GetRow(rowIndex);
                                    row.GetCell(0).SetCellValue(drGolden["MN-NO"].ToString());
                                    row.GetCell(1).SetCellValue(drGolden["MN-TESTER"].ToString().Substring(0, 1));
                                    row.GetCell(2).SetCellValue(drGolden["MN-TESTER"].ToString().Substring(drGolden["MN-TESTER"].ToString().Length - 1));
                                    row.GetCell(3).SetCellValue(drGolden["DT"].ToString());
                                    row.GetCell(4).SetCellValue(drGolden["MN-CPU1_TEMP"].ToString());
                                    row.GetCell(5).SetCellValue(drGolden["CPUMUL"].ToString());
                                    row.GetCell(6).SetCellValue(drGolden["CPUR"].ToString());
                                    row.GetCell(7).SetCellValue(drGolden["MN-CPU2_TEMP"].ToString());
                                    row.GetCell(8).SetCellValue(drGolden["VGAMUL"].ToString());
                                    row.GetCell(9).SetCellValue(drGolden["VGAR"].ToString());
                                    row.GetCell(10).SetCellValue(drGolden["CPUVGAMUL"].ToString());
                                    row.GetCell(11).SetCellValue(drGolden["CPUVGAR"].ToString());
                                    row.GetCell(12).SetCellValue(drGolden["MN-AIR_TEMP"].ToString());
                                    row.GetCell(13).SetCellValue(drGolden["MN-W1"].ToString());
                                    row.GetCell(14).SetCellValue(drGolden["MN-W2"].ToString());
                                    row.GetCell(15).SetCellValue(drGolden["MN-STATES"].ToString());
                                    row.GetCell(16).SetCellValue(drGolden["ErrorCode"].ToString());
                                    rowIndex++;
                                    goldenCount++;
                                    if (!lstGolden.Contains(drGolden["MN-TESTER"].ToString()))
                                    {
                                        lstGolden.Add(drGolden["MN-TESTER"].ToString());
                                    }
                                    if (goldenCount >= 6)
                                    {
                                        break;
                                    }
                                }
                                lstGolden.Clear();
                            }
                            else if (dr["MN-MachineType"].ToString().ToLower() != machineType)
                            {
                                dtHeader = dataAccess.ExecuteDataTable("select * from DASOP where SOP001=@SOP001",
                                 new DbParameter[]
                                    {
                                        DataAccess.CreateParameter("SOP001", DbType.String, machineType)
                                    });
                                if (dtHeader.Rows.Count > 0)
                                {
                                    row = sheet.GetRow(1);
                                    row.GetCell(3).SetCellValue(machineType);
                                    row.GetCell(5).SetCellValue(dtHeader.Rows[0]["SOP022"].ToString());
                                    row.GetCell(9).SetCellValue((rowIndex-6).ToString());
                                    row = sheet.GetRow(2);
                                    row.GetCell(1).SetCellValue(dtHeader.Rows[0]["SOP023"].ToString());
                                    row.GetCell(3).SetCellValue(dtHeader.Rows[0]["SOP024"].ToString());
                                    row.GetCell(5).SetCellValue(dtHeader.Rows[0]["SOP005"].ToString());
                                    row.GetCell(7).SetCellValue(dtHeader.Rows[0]["SOP006"].ToString());
                                    row.GetCell(9).SetCellValue(dtHeader.Rows[0]["SOP002"].ToString());
                                    row.GetCell(11).SetCellValue(dtHeader.Rows[0]["SOP003"].ToString());
                                    row.GetCell(13).SetCellValue(dtHeader.Rows[0]["SOP025"].ToString());
                                }

                                using (fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
                                {
                                    ws.Write(fs);
                                }
                                fileName = ConfigurationManager.AppSettings["ExcelPath"] + @"\" +
                                          dr["MN-MachineType"].ToString() + "_ARS-CQ_" + date +
                                          ".xls";
                                File.Copy(AppDomain.CurrentDomain.BaseDirectory + @"Template\Template.xls", fileName, true);
                                fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
                                ws = new HSSFWorkbook(fs);
                                sheet = ws.GetSheetAt(0);
                                rowIndex = 6;
                                goldenCount = 0;
                                if (!lstMac.Contains(dr["MN-MachineType"].ToString()))
                                {
                                    lstMac.Add(dr["MN-MachineType"].ToString());
                                }
                                machineType = dr["MN-MachineType"].ToString().ToLower();
                                DataRow[] drs =
                                       dtExport.Select("[MN-NO] like '%GOLDEN' AND [MN-MachineType]='" +
                                                       dr["MN-MachineType"].ToString() + "'", "MN-TESTER");
                               
                                foreach (DataRow drGolden in drs)
                                {
                                    if (lstGolden.Contains(drGolden["MN-TESTER"].ToString()))
                                    {
                                        continue;
                                    }
                                    row = sheet.GetRow(rowIndex);
                                    row.GetCell(0).SetCellValue(drGolden["MN-NO"].ToString());
                                    row.GetCell(1).SetCellValue(drGolden["MN-TESTER"].ToString().Substring(0, 1));
                                    row.GetCell(2).SetCellValue(drGolden["MN-TESTER"].ToString().Substring(drGolden["MN-TESTER"].ToString().Length - 1));
                                    row.GetCell(3).SetCellValue(drGolden["DT"].ToString());
                                    row.GetCell(4).SetCellValue(drGolden["MN-CPU1_TEMP"].ToString());
                                    row.GetCell(5).SetCellValue(drGolden["CPUMUL"].ToString());
                                    row.GetCell(6).SetCellValue(drGolden["CPUR"].ToString());
                                    row.GetCell(7).SetCellValue(drGolden["MN-CPU2_TEMP"].ToString());
                                    row.GetCell(8).SetCellValue(drGolden["VGAMUL"].ToString());
                                    row.GetCell(9).SetCellValue(drGolden["VGAR"].ToString());
                                    row.GetCell(10).SetCellValue(drGolden["CPUVGAMUL"].ToString());
                                    row.GetCell(11).SetCellValue(drGolden["CPUVGAR"].ToString());
                                    row.GetCell(12).SetCellValue(drGolden["MN-AIR_TEMP"].ToString());
                                    row.GetCell(13).SetCellValue(drGolden["MN-W1"].ToString());
                                    row.GetCell(14).SetCellValue(drGolden["MN-W2"].ToString());
                                    row.GetCell(15).SetCellValue(drGolden["MN-STATES"].ToString());
                                    row.GetCell(16).SetCellValue(drGolden["ErrorCode"].ToString());
                                    rowIndex++;
                                    if (!lstGolden.Contains(drGolden["MN-TESTER"].ToString()))
                                    {
                                        lstGolden.Add(drGolden["MN-TESTER"].ToString());
                                    }
                                    goldenCount++;
                                    if (goldenCount >= 6)
                                    {
                                        break;
                                    }
                                }
                                lstGolden.Clear();
                            }
                            if (dr["MN-NO"].ToString().Contains("GOLDEN"))
                            {
                                continue;
                            }
                            row = sheet.GetRow(rowIndex);
                            if (row == null)
                            {
                                row = sheet.CreateRow(rowIndex);
                                row.CreateCell(0).SetCellValue(dr["MN-NO"].ToString());
                                row.CreateCell(1).SetCellValue(dr["MN-TESTER"].ToString().Substring(0, 1));
                                row.CreateCell(2).SetCellValue(dr["MN-TESTER"].ToString().Substring(dr["MN-TESTER"].ToString().Length - 1));
                                row.CreateCell(3).SetCellValue(dr["DT"].ToString());
                                row.CreateCell(4).SetCellValue(dr["MN-CPU1_TEMP"].ToString());
                                row.CreateCell(5).SetCellValue(dr["CPUMUL"].ToString());
                                row.CreateCell(6).SetCellValue(dr["CPUR"].ToString());
                                row.CreateCell(7).SetCellValue(dr["MN-CPU2_TEMP"].ToString());
                                row.CreateCell(8).SetCellValue(dr["VGAMUL"].ToString());
                                row.CreateCell(9).SetCellValue(dr["VGAR"].ToString());
                                row.CreateCell(10).SetCellValue(dr["CPUVGAMUL"].ToString());
                                row.CreateCell(11).SetCellValue(dr["CPUVGAR"].ToString());
                                row.CreateCell(12).SetCellValue(dr["MN-AIR_TEMP"].ToString());
                                row.CreateCell(13).SetCellValue(dr["MN-W1"].ToString());
                                row.CreateCell(14).SetCellValue(dr["MN-W2"].ToString());
                                row.CreateCell(15).SetCellValue(dr["MN-STATES"].ToString());
                                row.CreateCell(16).SetCellValue(dr["ErrorCode"].ToString());
                                rowIndex++;
                                continue;
                            }
                            row.GetCell(0).SetCellValue(dr["MN-NO"].ToString());
                            row.GetCell(1).SetCellValue(dr["MN-TESTER"].ToString().Substring(0, 1));
                            row.GetCell(2).SetCellValue(dr["MN-TESTER"].ToString().Substring(dr["MN-TESTER"].ToString().Length - 1));
                            row.GetCell(3).SetCellValue(dr["DT"].ToString());
                            row.GetCell(4).SetCellValue(dr["MN-CPU1_TEMP"].ToString());
                            row.GetCell(5).SetCellValue(dr["CPUMUL"].ToString());
                            row.GetCell(6).SetCellValue(dr["CPUR"].ToString());
                            row.GetCell(7).SetCellValue(dr["MN-CPU2_TEMP"].ToString());
                            row.GetCell(8).SetCellValue(dr["VGAMUL"].ToString());
                            row.GetCell(9).SetCellValue(dr["VGAR"].ToString());
                            row.GetCell(10).SetCellValue(dr["CPUVGAMUL"].ToString());
                            row.GetCell(11).SetCellValue(dr["CPUVGAR"].ToString());
                            row.GetCell(12).SetCellValue(dr["MN-AIR_TEMP"].ToString());
                            row.GetCell(13).SetCellValue(dr["MN-W1"].ToString());
                            row.GetCell(14).SetCellValue(dr["MN-W2"].ToString());
                            row.GetCell(15).SetCellValue(dr["MN-STATES"].ToString());
                            row.GetCell(16).SetCellValue(dr["ErrorCode"].ToString());
                            rowIndex++;
                        }
                        if (fs != null)
                        {
                            dtHeader = dataAccess.ExecuteDataTable("select * from DASOP where SOP001=@SOP001",
                                new DbParameter[]
                                    {
                                        DataAccess.CreateParameter("SOP001", DbType.String, machineType)
                                    });
                            if (dtHeader.Rows.Count > 0)
                            {
                                row = sheet.GetRow(1);
                                row.GetCell(3).SetCellValue(machineType);
                                row.GetCell(5).SetCellValue(dtHeader.Rows[0]["SOP022"].ToString());
                                row.GetCell(9).SetCellValue((rowIndex-6).ToString());
                                row = sheet.GetRow(2);
                                row.GetCell(1).SetCellValue(dtHeader.Rows[0]["SOP023"].ToString());
                                row.GetCell(3).SetCellValue(dtHeader.Rows[0]["SOP024"].ToString());
                                row.GetCell(5).SetCellValue(dtHeader.Rows[0]["SOP005"].ToString());
                                row.GetCell(7).SetCellValue(dtHeader.Rows[0]["SOP006"].ToString());
                                row.GetCell(9).SetCellValue(dtHeader.Rows[0]["SOP002"].ToString());
                                row.GetCell(11).SetCellValue(dtHeader.Rows[0]["SOP003"].ToString());
                                row.GetCell(13).SetCellValue(dtHeader.Rows[0]["SOP025"].ToString());
                            }
                            using ( fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
                            {
                                ws.Write(fs);
                            }
                        }
                        if (lstMac.Count > 0)
                        {
                            GenXML(lstMac, date);
                        }
                    }
                    else if (args[0].ToUpper() == "U") //ftp上傳
                    {
                        if (!Directory.Exists(ConfigurationManager.AppSettings["ExcelPath"]))
                        {
                            return;
                        }
                        DirectoryInfo directoryInfo = new DirectoryInfo(ConfigurationManager.AppSettings["ExcelPath"]);
                        FileInfo[] uploadFiles = directoryInfo.GetFiles();
                        if (uploadFiles.Length == 0)
                        {
                            return;
                        }
                        if (uploadFiles.Length > 0)
                        {
                            if (!Directory.Exists(ConfigurationManager.AppSettings["BackUpPath"]))
                            {
                                Directory.CreateDirectory(ConfigurationManager.AppSettings["BackUpPath"]);
                            }
                            //Open FTP connection.
                            Ftp ftp = new Ftp();
                            ftp.Connect(ConfigurationManager.AppSettings["IP"], 21);
                            ftp.Passive = false;
                            ftp.TransferType = FtpTransferType.Ascii;
                            ftp.Login(ConfigurationManager.AppSettings["USERNAME"], ConfigurationManager.AppSettings["PASSWORD"]);
                            if (ftp.State != FtpState.Ready)
                            {
                                Console.WriteLine("FTP伺服器連接失敗");
                                Console.Read();
                                return;
                            }

                            //Process files
                            foreach (FileInfo str in uploadFiles)
                            {
                                if (IsFileInUse(str.FullName))
                                {
                                    if (!ftp.DirectoryExists(ConfigurationManager.AppSettings["FTPPATH"]))
                                    {
                                        ftp.CreateDirectory(ConfigurationManager.AppSettings["FTPPATH"]);
                                    }
                                    ftp.PutFile(str.FullName, @"\" + ConfigurationManager.AppSettings["FTPPATH"] + @"\" + str.FullName.Substring(str.FullName.LastIndexOf(@"\") + 1));
                                    File.Move(str.FullName, ConfigurationManager.AppSettings["BackUpPath"] + @"\" + str.FullName.Substring(str.FullName.LastIndexOf(@"\") + 1));
                                }
                            }

                            //Close FTP connection.
                            ftp.Logoff();
                            ftp.Disconnect();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message+"@"+index);
                Console.Read();
            }
        }

        /// 判斷文件是否在使用中方法
        /// <summary>
        /// 判斷文件是否在使用中方法
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsFileInUse(string fileName)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read,
                FileShare.None);
                fs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// 產生XML方法
        /// <summary>
        /// 產生XML方法
        /// </summary>
        /// <param name="list"></param>
        /// <param name="date"></param>
        public static void GenXML(List<string> list, string date)
        {
            string fileName = ConfigurationManager.AppSettings["ExcelPath"] + @"\ARS-CQ_" + date + @".XML";
            XmlDocument xmlDoc = new XmlDocument();
            //创建类型声明节点  
            //XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "", "");
            xmlDoc.AppendChild(node);
            //创建根节点  
            XmlNode root = xmlDoc.CreateElement("Data");
            xmlDoc.AppendChild(root);

            CreateNode(xmlDoc, root, "Day", DateTime.ParseExact(date, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd"));
            foreach (string s in list)
            {
                CreateNode(xmlDoc, root, "Model", s);
            }
            xmlDoc.Save(fileName);
        }

        /// 创建节点    
        /// <summary>    
        /// 创建节点    
        /// </summary>    
        /// <param name="xmldoc"></param>  xml文档  
        /// <param name="parentnode"></param>父节点    
        /// <param name="name"></param>  节点名  
        /// <param name="value"></param>  节点值  
        public static void CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
        {
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parentNode.AppendChild(node);
        }
    }
}
