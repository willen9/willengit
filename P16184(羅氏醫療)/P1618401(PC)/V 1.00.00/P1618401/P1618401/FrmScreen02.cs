using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using P1618401.ReportFrm;
using REGAL.Data.DataAccess;

namespace P1618401
{
    public partial class FrmScreen02 : Form
    {
        private DataAccess dataAccess = null;
        private DataTable dtInv;
        private DataTable dtRecord;
        private string status;
        public FrmScreen02()
        {
            InitializeComponent();
        }

        private void FrmScreen02_Load(object sender, EventArgs e)
        {
            dataAccess = new DataAccess();
            dataAccess.ConnectionString = Share.conStr;
            dataAccess.ProviderName = Share.provide;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ExecuteGrid1();
                LoadState();

                if (status == "盤點中")
                {
                    btnMain.Enabled = false;
                    btnEnd.Enabled = true;
                }
                else
                {
                    btnMain.Enabled = true;
                    btnEnd.Enabled = false;
                }
                if (int.Parse(dataAccess.ExecuteScalar("select count(*) from INVENMST").ToString()) > 0)
                {
                    btnPrintInv.Enabled = btnPrintMul.Enabled = true;
                }
                else
                {
                    btnPrintInv.Enabled = btnPrintMul.Enabled = false;
                }
            }
            catch (Exception ex)
            {
               Messages.Error(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrintInv_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                DataTable dt3 =
                      dataAccess.ExecuteDataTable(
                          "SELECT INVENMST.GCode as GlobalCode,INVENMST.Location,INVENMST.LAST_QTY,INVENMST.INVE_QTY ,MATERMST.LCode as LocalCode,MATERMST.Description As Description from INVENMST,MATERMST where  MATERMST.GCode=INVENMST.GCode  order by INVENMST.GCode");
                if (dt3.Rows.Count > 0)
                {
                    frm0202 frm0202 = new frm0202();
                    frm0202.dtSource = dt3;
                    frm0202.ShowDialog();
                    frm0202.Close();
                    frm0202.Dispose();
                }
                else
                {
                    Messages.Warning("沒有盤點資料，可供列印！");
                }
            }
            catch (Exception ex)
            {
                Messages.Error(ex.Message);   
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnPrintMul_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                DataTable dt3 =
                       dataAccess.ExecuteDataTable(
                           "SELECT INVENMST.GCode as GlobalCode,INVENMST.Location,INVENMST.LAST_QTY,INVENMST.REAL_QTY,INVENMST.REAL_QTY-INVENMST.LAST_QTY as R1 ,MATERMST.LCode as LocalCode from INVENMST,MATERMST where MATERMST.GCode=INVENMST.GCode and INVENMST.REAL_QTY-INVENMST.LAST_QTY<>0 order by INVENMST.GCode");
                if (dt3.Rows.Count > 0)
                {
                    frm0201 frm0201 = new frm0201();
                    frm0201.dtSource = dt3;
                    frm0201.ShowDialog();
                    frm0201.Close();
                    frm0201.Dispose();
                }
                else
                {
                    Messages.Warning("沒有盤差資料，可供列印！");
                }
            }
            catch (Exception ex)
            {
                Messages.Error(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            if (Messages.Question("執行盤點結轉,將過帳到商品主檔") == DialogResult.No)
            {
                return;
            }
            DbTransaction tran = null;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                tran = dataAccess.CreateDbTransaction();
                dataAccess.ExecuteNonQuery("update matermst set stock_qty = '0' ", tran);

                DataTable dt3 = dataAccess.ExecuteDataTable("SELECT * from INVENMST order by GCode", tran);
                if (dt3.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt3.Rows)
                    {
                        if (dr["gcode"].ToString().Length > 0 && dr["location"].ToString().Length > 0)
                        {
                            //DataTable dt4 =
                            //    dataAccess.ExecuteDataTable(
                            //        "SELECT * FROM STOCKMST Where GCode = @GCode and LOCATION = @LOCATION ORDER BY GCode",
                            //        tran,
                            //        new DbParameter[]
                            //        {
                            //            DataAccess.CreateParameter("GCode", DbType.String, dr["Gcode"]),
                            //            DataAccess.CreateParameter("LOCATION", DbType.String, dr["LOCATION"])
                            //        });
                            DataTable dt4 =dataAccess.ExecuteDataTable("SELECT * FROM STOCKMST Where GCode = '" + dr["Gcode"] + "' and LOCATION = '" + dr["LOCATION"] + "' ORDER BY GCode",tran);
                            decimal qty = decimal.Parse(dr["INVE_QTY"].ToString().Trim().Length == 0
                                ? "0"
                                : dr["INVE_QTY"].ToString().Trim()) +
                                          decimal.Parse(dr["ADJUST_QTY"].ToString().Trim().Length == 0
                                              ? "0"
                                              : dr["ADJUST_QTY"].ToString().Trim());
                            if (dt4.Rows.Count > 0)
                            {
                                //dataAccess.ExecuteNonQuery(
                                //    "update STOCKMST set LAST_QTY=@LAST_QTY,REAL_QTY=0,IN_QTY=0,OUT_QTY=0,LOCATION=@LOCATION,USER_ID=@USER_ID,CREATE_DATE=@CREATE_DATE Where GCode = @GCode and LOCATION = @LOCATION ",
                                //    tran, new DbParameter[]
                                //    {
                                //        DataAccess.CreateParameter("LAST_QTY", DbType.String, qty),
                                //        DataAccess.CreateParameter("LOCATION", DbType.String, dr["LOCATION"]),
                                //        DataAccess.CreateParameter("USER_ID", DbType.String, dr["USER_ID"]),
                                //        DataAccess.CreateParameter("CREATE_DATE", DbType.String,dr["INVE_DATE"]),
                                //        DataAccess.CreateParameter("GCode", DbType.String, dr["Gcode"])
                                //    });
                                //dataAccess.ExecuteNonQuery(
                                //    "update matermst set stock_qty = stock_qty + @Para1  where gcode = @gcode", tran,
                                //    new DbParameter[]
                                //    {
                                //        DataAccess.CreateParameter("Para1", DbType.String, qty),
                                //        DataAccess.CreateParameter("gcode", DbType.String, dr["Gcode"])
                                //    });
                                dataAccess.ExecuteNonQuery("update STOCKMST set LAST_QTY=" + qty + ",REAL_QTY=0,IN_QTY=0,OUT_QTY=0,LOCATION='" + dr["LOCATION"] + "',USER_ID='" + dr["USER_ID"] + "',CREATE_DATE='" + dr["INVE_DATE"] + "' Where GCode = '" + dr["Gcode"] + "' and LOCATION = '" + dr["LOCATION"] + "' ", tran);
                                dataAccess.ExecuteNonQuery("update matermst set stock_qty = stock_qty + " + qty + "  where gcode = '" + dr["Gcode"] + "'", tran);
                            }
                            else
                            {
                                //dataAccess.ExecuteNonQuery(
                                //    "insert into STOCKMST(GCode,LAST_QTY,REAL_QTY,IN_QTY,OUT_QTY,LOCATION,USER_ID,CREATE_DATE) values (@GCode,@LAST_QTY,0,0,0,@LOCATION,@USER_ID,@CREATE_DATE)",
                                //    tran, new DbParameter[]
                                //    {
                                //        DataAccess.CreateParameter("GCode", DbType.String, dr["Gcode"]),
                                //        DataAccess.CreateParameter("LAST_QTY", DbType.String, qty),
                                //        DataAccess.CreateParameter("LOCATION", DbType.String, dr["LOCATION"]),
                                //        DataAccess.CreateParameter("USER_ID", DbType.String, dr["USER_ID"]),
                                //        DataAccess.CreateParameter("CREATE_DATE", DbType.String,dr["INVE_DATE"])
                                //    });
                                //if (int.Parse(
                                //    dataAccess.ExecuteScalar("select count(*) from MATERMST where GCode=@GCode", tran,
                                //        new DbParameter[]
                                //        {
                                //            DataAccess.CreateParameter("GCode", DbType.String, dr["Gcode"])
                                //        }).ToString()) > 0)
                                //{
                                //    dataAccess.ExecuteNonQuery(
                                //        "update MATERMST set STOCK_QTY=@STOCK_QTY,USER_ID=@USER_ID,CREATE_DATE=@CREATE_DATE where gcode=@gcode",
                                //        tran, new DbParameter[]
                                //        {
                                //            DataAccess.CreateParameter("STOCK_QTY", DbType.String, qty),
                                //            DataAccess.CreateParameter("USER_ID", DbType.String,dr["USER_ID"]),
                                //            DataAccess.CreateParameter("CREATE_DATE", DbType.String,dr["INVE_DATE"]),
                                //            DataAccess.CreateParameter("gcode", DbType.String, dr["Gcode"])
                                //        });
                                //}
                                //else
                                //{
                                //    dataAccess.ExecuteNonQuery(
                                //        "INSERT INTO MATERMST(GCode,STOCK_QTY,USER_ID,CREATE_DATE) values (@GCode,@STOCK_QTY,@USER_ID,@CREATE_DATE)",
                                //        tran, new DbParameter[]
                                //        {
                                //            DataAccess.CreateParameter("GCode", DbType.String, dr["Gcode"]),
                                //            DataAccess.CreateParameter("STOCK_QTY", DbType.String, qty),
                                //            DataAccess.CreateParameter("USER_ID", DbType.String,dr["USER_ID"]),
                                //            DataAccess.CreateParameter("CREATE_DATE", DbType.String,dr["INVE_DATE"])
                                //        });
                                //}
                                dataAccess.ExecuteNonQuery(
                                        "insert into STOCKMST(GCode,LAST_QTY,REAL_QTY,IN_QTY,OUT_QTY,LOCATION,USER_ID,CREATE_DATE) values ('" + dr["Gcode"] + "'," + qty + ",0,0,0,'" + dr["LOCATION"] + "','" + dr["USER_ID"] + "','" + dr["INVE_DATE"] + "')",tran);
                                if (int.Parse(
                                    dataAccess.ExecuteScalar("select count(*) from MATERMST where GCode='" + dr["Gcode"] + "'", tran).ToString()) > 0)
                                {
                                    dataAccess.ExecuteNonQuery(
                                        "update MATERMST set STOCK_QTY=" + qty + ",USER_ID='" + dr["USER_ID"] + "',CREATE_DATE='" + dr["INVE_DATE"] + "' where gcode='" + dr["Gcode"] + "'",tran);
                                }
                                else
                                {
                                    dataAccess.ExecuteNonQuery(
                                        "INSERT INTO MATERMST(GCode,STOCK_QTY,USER_ID,CREATE_DATE) values ('" + dr["Gcode"] + "'," + qty + ",'" + dr["USER_ID"] + "','" + dr["INVE_DATE"] + "')",tran);
                                }
                            }
                        }
                        else
                        {
                            using (
                                StreamWriter sw =
                                    new StreamWriter(
                                        Application.StartupPath + "\\ERRLog_" + DateTime.Now.Month + "_" +
                                        DateTime.Now.Day + ".log", true, Encoding.Default))
                            {
                                sw.WriteLine(
                                    "----------------------------------------------------------------------------");
                                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " 盤點轉結錯誤的資料為");
                                if (dr["GCode"].ToString().Trim().Length == 0 &&
                                    dr["location"].ToString().Trim().Length == 0)
                                {
                                    sw.WriteLine("沒有GCode\t沒有Location\t" + dr["LAST_QTY"] + "\t" +
                                                 dr["INVE_QTY"] + "\t" + dr["Real_QTY"] + "\t" +
                                                 dr["ADJUST_QTY"] + "\t" + dr["INVE_Date"]);
                                }
                                if (dr["GCode"].ToString().Trim().Length == 0)
                                {
                                    sw.WriteLine("沒有GCode\t" + dr["Location"] + "\t" +
                                                 dr["LAST_QTY"] + "\t" + dr["INVE_QTY"] + "\t" +
                                                 dr["Real_QTY"] + "\t" + dr["ADJUST_QTY"] + "\t" +
                                                 dr["INVE_Date"]);
                                }
                                else if (dr["location"].ToString().Trim().Length == 0)
                                {
                                    sw.WriteLine(dr["GCode"] + "\t" + "沒有Location" + "\t" +
                                                 dr["LAST_QTY"] + "\t" + dr["INVE_QTY"] + "\t" +
                                                 dr["Real_QTY"] + "\t" + dr["ADJUST_QTY"] + "\t" +
                                                 dr["INVE_Date"]);
                                }
                                sw.WriteLine(
                                    "----------------------------------------------------------------------------");
                                sw.Close();
                            }
                        }
                    }
                    tran.Commit();
                    tran = null;
                    Messages.Asterisk("主檔結轉完成!!");
                    btnMain.Enabled = false;
                    btnEnd.Enabled = false;
                    btnPrintMul.Enabled = btnPrintInv.Enabled = true;
                    btnReturn.Enabled = true;
                    status = "未盤點";
                    WriteState();
                }
            }
            catch (Exception ex)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
                Messages.Error(ex.Message);
            }
            finally
            {
                if (tran != null)
                {
                    tran.Dispose();
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            if (Messages.Question("異常資料將再次轉入!!") == DialogResult.No)
            {
                return;
            }
            DbTransaction tran = dataAccess.CreateDbTransaction();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Data_Sort(tran);
                if (
                    int.Parse(
                        dataAccess.ExecuteScalar("select count(*) from ERRDATA WHERE  STATUS LIKE '%'", tran).ToString()) >
                    0)
                {
                    if (Messages.Question("仍有異常資料尚未處理完畢\r\n是否清除 轉檔異常資料 ？") == DialogResult.Yes)
                    {
                        Data_Clear();
                    }
                    else
                    {
                        Messages.Asterisk("停止盤點");
                        return;
                    }
                }
                if (Messages.Question("盤點主檔將被刪除!!") == DialogResult.No)
                {
                    return;
                }
                dataAccess.ExecuteNonQuery("update matermst set stock_qty = '0'", tran);

                DataTable dt3 = dataAccess.ExecuteDataTable("SELECT * from STOCKMST  order by GCode", tran);
                DataTable dt4 =dataAccess.ExecuteDataTable("SELECT GCODE,LOCATION,REAL_QTY,INVE_QTY,ADJUST_QTY from INVENMST", tran);


                if (dt4.Rows.Count > 0)
                {
                    dataAccess.ExecuteNonQuery("delete from INVENMST", tran);
                }
                if (dt3.Rows.Count > 0)
                {
                    decimal rQty = 0;
                    foreach (DataRow dr in dt3.Rows)
                    {
                        rQty = decimal.Parse(dr["LAST_QTY"].ToString().Length == 0 ? "0" : dr["LAST_QTY"].ToString()) +
                               decimal.Parse(dr["IN_QTY"].ToString().Length == 0 ? "0" : dr["IN_QTY"].ToString()) -
                               decimal.Parse(dr["OUT_QTY"].ToString().Length == 0 ? "0" : dr["OUT_QTY"].ToString());
                        //dataAccess.ExecuteNonQuery("insert into INVENMST(GCode,Location,REAL_QTY,INVE_QTY,ADJUST_QTY) values(@GCode,@Location,@REAL_QTY,@INVE_QTY,@ADJUST_QTY)",
                        //    tran,
                        //    new DbParameter[]
                        //    {
                        //        DataAccess.CreateParameter("GCode", DbType.String, dr["GCode"]),
                        //        DataAccess.CreateParameter("Location", DbType.String, dr["Location"]),
                        //        DataAccess.CreateParameter("REAL_QTY", DbType.String, rQty),
                        //        DataAccess.CreateParameter("INVE_QTY", DbType.String, "0"),
                        //        DataAccess.CreateParameter("ADJUST_QTY", DbType.String, "0")
                        //    });
                        dataAccess.ExecuteNonQuery("insert into INVENMST(GCode,Location,REAL_QTY,INVE_QTY,ADJUST_QTY) values('" + dr["GCode"] + "','" + dr["Location"] + "',"+rQty+",0,0)",tran);
                    }
                }
                tran.Commit();
                tran = null;
                Messages.Asterisk("主檔轉檔完成!!");
                btnMain.Enabled = false;
                btnEnd.Enabled = true;
                btnPrintMul.Enabled = btnPrintInv.Enabled = true;
                btnReturn.Enabled = true;
                status = "盤點中";
                WriteState();
            }
            catch (Exception ex)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
               Messages.Error(ex.Message);
            }
            finally
            {
                if (tran != null)
                {
                    tran.Dispose();
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    ExecuteGrid2(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                }
                catch (Exception ex)
                {
                    Messages.Error(ex.Message);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private void ExecuteGrid1()
        {
            string sql = "SELECT M.GCODE AS GlobalCode,M.LCODE AS LocalCode,M.Description,M.USystem FROM MATERMST AS M ORDER BY M.GCODE";
            dtInv = dataAccess.ExecuteDataTable(sql);
            dataGridView1.DataSource = dtInv;
        }

        private void ExecuteGrid2(string gCode)
        {
            dtRecord =
                dataAccess.ExecuteDataTable(
                    "SELECT GCODE,LOCATION,REAL_QTY,INVE_QTY,ADJUST_QTY FROM INVENMST where GCode=@GCode ORDER BY GCode", new DbParameter[]
                    {
                        DataAccess.CreateParameter("GCode",DbType.String,gCode)
                    });
            dataGridView2.DataSource = dtRecord;
        }

        private void LoadState()
        {
            using (FileStream fs = new FileStream(Application.StartupPath + "\\StateLog.Log", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                StreamReader sr = new StreamReader(fs, Encoding.Default);
                status = sr.ReadLine();
                sr.Close();
                sr.Dispose();
            }
        }

        private void WriteState()
        {
            using (FileStream fs = new FileStream(Application.StartupPath + "\\StateLog.Log", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                sw.WriteLine(status);
                sw.Close();
                sw.Dispose();
            }
        }

        private void Data_Sort(DbTransaction tran)
        {
            DataTable dtTemp = dataAccess.ExecuteDataTable("SELECT *  FROM ERRDATA WHERE  STATUS LIKE '%'", tran);
            foreach (DataRow dr in dtTemp.Rows)
            {
                //switch (dr["STATUS"].ToString())
                //{
                //    case "進貨":
                //        if (dataAccess.ExecuteNonQuery(
                //            "Update STOCKMST Set IN_QTY=IN_QTY+ @Para1, REAL_QTY=LAST_QTY + (IN_QTY+ @Para1) - OUT_QTY  where gcode=@gcode AND LOCATION=@LOCATION ",
                //            tran,
                //            new DbParameter[]
                //                {
                //                    DataAccess.CreateParameter("Para1", DbType.String, dr[2]),
                //                    DataAccess.CreateParameter("gcode", DbType.String, dr[0]),
                //                    DataAccess.CreateParameter("LOCATION", DbType.String, dr[1])
                //                }) == 0)
                //        {
                //            dataAccess.ExecuteNonQuery(
                //                "INSERT INTO STOCKMST(GCODE,LOCATION,IN_QTY,CREATE_DATE,USER_ID) VALUES (@GCODE,@LOCATION,@IN_QTY,@CREATE_DATE,@USER_ID)",
                //                tran,
                //                new DbParameter[]
                //                    {
                //                        DataAccess.CreateParameter("GCODE", DbType.String, dr[0]),
                //                        DataAccess.CreateParameter("LOCATION", DbType.String, dr[1]),
                //                        DataAccess.CreateParameter("IN_QTY", DbType.String, dr[2]),
                //                        DataAccess.CreateParameter("CREATE_DATE", DbType.String, dr[3]),
                //                        DataAccess.CreateParameter("USER_ID", DbType.String, dr[5])
                //                    });
                //        }
                //        dataAccess.ExecuteNonQuery(
                //            "Update MATERMST Set Stock_QTY=Stock_QTY+ @Para1 Where gcode=@gcode", tran,
                //            new DbParameter[]
                //                {
                //                    DataAccess.CreateParameter("Para1", DbType.String, dr[2]),
                //                    DataAccess.CreateParameter("gcode", DbType.String, dr[0])
                //                });
                //        dataAccess.ExecuteNonQuery(
                //            "delete from ERRDATA where GCode=@GCode and Location=@Location and STATUS=@STATUS", tran,
                //            new DbParameter[]
                //                {
                //                    DataAccess.CreateParameter("GCode", DbType.String, dr["GCode"]),
                //                    DataAccess.CreateParameter("Location", DbType.String, dr["Location"]),
                //                    DataAccess.CreateParameter("STATUS", DbType.String, dr["STATUS"])
                //                });
                //        break;

                //    case "出貨":
                //        if (dataAccess.ExecuteNonQuery(
                //            "Update STOCKMST Set OUT_QTY = OUT_QTY+@Para1, REAL_QTY=LAST_QTY + IN_QTY - (OUT_QTY + @Para1) where GCODE=@GCODE and LOCATION = @LOCATION",
                //            tran, new DbParameter[]
                //                {
                //                    DataAccess.CreateParameter("Para1", DbType.String, dr[2]),
                //                    DataAccess.CreateParameter("GCODE", DbType.String, dr[0]),
                //                    DataAccess.CreateParameter("LOCATION", DbType.String, dr[1])
                //                }) > 0)
                //        {
                //            dataAccess.ExecuteNonQuery(
                //                "Update MATERMST Set Stock_QTY=Stock_QTY - @Para1 Where gcode=@gcode ", tran,
                //                new DbParameter[]
                //                    {
                //                        DataAccess.CreateParameter("Para1", DbType.String, dr[2]),
                //                        DataAccess.CreateParameter("gcode", DbType.String, dr[0])
                //                    });
                //            dataAccess.ExecuteNonQuery(
                //                "delete from ERRDATA where GCode=@GCode and Location=@Location and STATUS=@STATUS",
                //                tran,
                //                new DbParameter[]
                //                    {
                //                        DataAccess.CreateParameter("GCode", DbType.String, dr["GCode"]),
                //                        DataAccess.CreateParameter("Location", DbType.String, dr["Location"]),
                //                        DataAccess.CreateParameter("STATUS", DbType.String, dr["STATUS"])
                //                    });
                //        }
                //        break;

                //    case "退貨":
                //        if (dataAccess.ExecuteNonQuery(
                //            "Update STOCKMST Set OUT_QTY=OUT_QTY - @Para1, REAL_QTY=LAST_QTY + IN_QTY - (OUT_QTY - @Para1)  where gcode=@gcode AND LOCATION=@LOCATION ",
                //            tran, new DbParameter[]
                //                {
                //                    DataAccess.CreateParameter("Para1", DbType.String, dr[2]),
                //                    DataAccess.CreateParameter("gcode", DbType.String, dr[0]),
                //                    DataAccess.CreateParameter("LOCATION", DbType.String, dr[1])
                //                }) > 0)
                //        {
                //            dataAccess.ExecuteNonQuery(
                //                "update MATERMST Set Stock_QTY=Stock_QTY + @Para1 Where gcode=@gcode", tran,
                //                new DbParameter[]
                //                    {
                //                        DataAccess.CreateParameter("Para1", DbType.String, dr[2]),
                //                        DataAccess.CreateParameter("gcode", DbType.String, dr[0])
                //                    });
                //            dataAccess.ExecuteNonQuery(
                //                "delete from ERRDATA where GCode=@GCode and Location=@Location and STATUS=@STATUS",
                //                tran,
                //                new DbParameter[]
                //                    {
                //                        DataAccess.CreateParameter("GCode", DbType.String, dr["GCode"]),
                //                        DataAccess.CreateParameter("Location", DbType.String, dr["Location"]),
                //                        DataAccess.CreateParameter("STATUS", DbType.String, dr["STATUS"])
                //                    });
                //        }
                //        break;
                //}
                switch (dr["STATUS"].ToString())
                {
                    case "進貨":
                        if (dataAccess.ExecuteNonQuery(
                            "Update STOCKMST Set IN_QTY=IN_QTY+ " + dr[2] + ", REAL_QTY=LAST_QTY + (IN_QTY+ " + dr[2] + ") - OUT_QTY  where gcode='" + dr[0] + "' AND LOCATION='" + dr[1] + "' ",
                            tran) == 0)
                        {
                            dataAccess.ExecuteNonQuery(
                                "INSERT INTO STOCKMST(GCODE,LOCATION,IN_QTY,CREATE_DATE,USER_ID) VALUES ('" + dr[0] + "','" + dr[1] + "'," + dr[2] + ",'" + dr[3] + "','" + dr[5] + "')",
                                tran);
                        }
                        dataAccess.ExecuteNonQuery(
                            "Update MATERMST Set Stock_QTY=Stock_QTY+ " + dr[2] + " Where gcode='" + dr[0] + "'", tran);
                        dataAccess.ExecuteNonQuery(
                            "delete from ERRDATA where GCode='" + dr["GCode"] + "' and Location='" + dr["Location"] + "' and STATUS='" + dr["STATUS"] + "'", tran);
                        break;

                    case "出貨":
                        if (dataAccess.ExecuteNonQuery(
                            "Update STOCKMST Set OUT_QTY = OUT_QTY+" + dr[2] + ", REAL_QTY=LAST_QTY + IN_QTY - (OUT_QTY + " + dr[2] + ") where GCODE='" + dr[0] + "' and LOCATION = '" + dr[1] + "'",
                            tran) > 0)
                        {
                            dataAccess.ExecuteNonQuery(
                                "Update MATERMST Set Stock_QTY=Stock_QTY - " + dr[2] + " Where gcode='" + dr[0] + "' ", tran);
                            dataAccess.ExecuteNonQuery(
                                "delete from ERRDATA where GCode='" + dr["GCode"] + "' and Location='" + dr["Location"] + "' and STATUS='" + dr["STATUS"] + "'",
                                tran);
                        }
                        break;

                    case "退貨":
                        if (dataAccess.ExecuteNonQuery(
                            "Update STOCKMST Set OUT_QTY=OUT_QTY - " + dr[2] + ", REAL_QTY=LAST_QTY + IN_QTY - (OUT_QTY - " + dr[2] + ")  where gcode='" + dr[0] + "' AND LOCATION='" + dr[1] + "' ",
                            tran) > 0)
                        {
                            dataAccess.ExecuteNonQuery(
                                "update MATERMST Set Stock_QTY=Stock_QTY + " + dr[2] + " Where gcode='" + dr[0] + "'", tran);
                            dataAccess.ExecuteNonQuery(
                                "delete from ERRDATA where GCode='" + dr["GCode"] + "' and Location='" + dr["Location"] + "' and STATUS='" + dr["STATUS"] + "'",
                                tran);
                        }
                        break;
                }
            }
        }

        private void Data_Clear()
        {
            dataAccess.ExecuteNonQuery("delete from ERRDATA WHERE  STATUS LIKE '%' ");
        }
    }
}
