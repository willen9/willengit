using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.ReportingServices.Rendering.ImageRenderer;
using REGAL.Data.DataAccess;

namespace P1618401
{
    public partial class FrmMain : Form
    {
        private DataAccess dataAccess = null;
        private IniFile ini = new IniFile(Path.GetDirectoryName(Application.ExecutablePath) + "\\Setup.ini");
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (!File.Exists(Path.GetDirectoryName(Application.ExecutablePath) + "\\Setup.ini"))
                {
                    //產生ini檔
                    MessageBox.Show("設定檔遺失!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("設定檔生成中 ……", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ini.IniWriteValue("Setup", "PrintName", "Ring 4012PIM");
                    ini.IniWriteValue("Setup", "HTRECV", "C:\\HTRECV");
                    ini.IniWriteValue("Setup", "HTRSEND", "C:\\HTRSEND");
                }

                Share.conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + @"\RGSN.mdb";
                Share.provide = "System.Data.OleDb";

                string ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                this.Text += " V " + ver.Split('.')[0] + "." + ver.Split('.')[1] + "." + ver.Split('.')[2];

                Share.printName = ini.IniReadValue("Setup", "PrintName");

                dataAccess = new DataAccess();
                dataAccess.ConnectionString = Share.conStr;
                dataAccess.ProviderName = Share.provide;

                timer1.Enabled = true;
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

        private void 離開ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 物料資料登錄ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                CloseOtherWindow();
                FrmScreen01 frmScreen01 = new FrmScreen01();
                frmScreen01.MdiParent = this;
                frmScreen01.Show();
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

        private void 物料盤點作業ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                CloseOtherWindow();
                FrmScreen02 frmScreen02 = new FrmScreen02();
                frmScreen02.MdiParent = this;
                frmScreen02.Show();
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

        private void 庫存查詢ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                CloseOtherWindow();
                FrmScreen03 frmScreen03 = new FrmScreen03();
                frmScreen03.MdiParent = this;
                frmScreen03.Show();
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

        private void 異動記錄查詢ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                CloseOtherWindow();
                FrmScreen04 frmScreen04 = new FrmScreen04();
                frmScreen04.MdiParent = this;
                frmScreen04.Show();
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

        private void 物料標籤列印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                CloseOtherWindow();
                FrmScreen05 frmScreen05 = new FrmScreen05();
                frmScreen05.MdiParent = this;
                frmScreen05.Show();
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

        private void 安全用量表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                CloseOtherWindow();
                FrmScreen06 frmScreen06 = new FrmScreen06();
                frmScreen06.MdiParent = this;
                frmScreen06.Show();
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

        private void 物料進出表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                CloseOtherWindow();
                FrmScreen07 frmScreen07 = new FrmScreen07();
                frmScreen07.MdiParent = this;
                frmScreen07.Show();
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

        private void 異常資料查詢ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                CloseOtherWindow();
                FrmScreen08 frmScreen08 = new FrmScreen08();
                frmScreen08.MdiParent = this;
                frmScreen08.Show();
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

        private void 轉檔異常表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                CloseOtherWindow();
                FrmScreen09 frmScreen09 = new FrmScreen09();
                frmScreen09.MdiParent = this;
                frmScreen09.Show();
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

        private void 接收檔案ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process prs = new Process();
                prs = Process.Start(Application.StartupPath + "\\Regal Trans\\Regal_HTTrans.exe", "recv");

                bool b = true;
                while (b)
                {
                    if (prs.HasExited)
                    {
                        b = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Messages.Error(ex.Message);
            }
        }

        private void 匯出檔案ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            return;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                DataTable dtExport = dataAccess.ExecuteDataTable("select * from STOCKMST ");
                if (dtExport.Rows.Count == 0)
                {
                    Messages.Warning("無庫存數據需要匯出");
                    return;
                }
                if (File.Exists(ini.IniReadValue("Setup", "HTRSEND") + "\\Main.txt"))
                {
                    File.Delete(ini.IniReadValue("Setup", "HTRSEND") + "\\Main.txt");
                }
                using (
                    FileStream fs = new FileStream(ini.IniReadValue("Setup", "HTRSEND") + "\\Main.txt",
                        FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    StreamWriter sw = new StreamWriter(fs, Encoding.ASCII);
                    foreach (DataRow dr in dtExport.Rows)
                    {
                        //sw.WriteLine(JoinSpaces(dr["LOCATION"].ToString(),5,0)+);
                    }
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
                string content = "MAIN.TXT    051316101310                                主檔   ";
                if (Get_FILEGOG(content))
                {
                    Messages.Asterisk("傳輸成功");
                }
                else
                {
                    Messages.Asterisk("傳輸失敗");
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            DbTransaction tran = null;
            try
            {
                if (!Directory.Exists(ini.IniReadValue("Setup", "HTRECV")))
                {
                    Directory.CreateDirectory(ini.IniReadValue("Setup", "HTRECV"));
                    return;
                }
                DirectoryInfo directoryInfo = new DirectoryInfo(ini.IniReadValue("Setup", "HTRECV"));
                FileInfo[] fileInfos = directoryInfo.GetFiles("*.TXT");
                if (fileInfos.Length == 0)
                {
                    return;
                }

                using (FileStream fs = new FileStream(fileInfos[0].FullName, FileMode.Open, FileAccess.Read))
                {
                    StreamReader sr = new StreamReader(fs, Encoding.Default);
                    tran = dataAccess.CreateDbTransaction();
                    while (sr.Peek() > -1)
                    {
                        string line = sr.ReadLine();
                        if (line.Length == 62)
                        {
                            string LOCATION = line.Substring(0, 5).Trim();
                            string GCODE = line.Substring(5, 20).Trim();
                            string QTY = line.Substring(25, 5).Trim();
                            string TRANS_DATE = line.Substring(30, 8).Trim();
                            string USER_ID = line.Substring(44, 10).Trim();
                            string STATUS = line.Substring(54, 2).Trim();
                            string CUSTNO = line.Substring(56, 6).Trim();

                            if (STATUS != "盤點")
                            {
                                //dataAccess.ExecuteNonQuery(
                                //    "INSERT INTO TRANSDTL(GCODE,LOCATION,QTY,TRANS_DATE,USER_ID,STATUS,CUSTNO) VALUES (@GCODE,@LOCATION,@QTY,@TRANS_DATE,@USER_ID,@STATUS,@CUSTNO)",
                                //    tran, new DbParameter[]
                                //    {
                                //        DataAccess.CreateParameter("GCODE", DbType.String, GCODE),
                                //        DataAccess.CreateParameter("LOCATION", DbType.String, LOCATION),
                                //        DataAccess.CreateParameter("QTY", DbType.String, QTY),
                                //        DataAccess.CreateParameter("TRANS_DATE", DbType.String, TRANS_DATE),
                                //        DataAccess.CreateParameter("USER_ID", DbType.String, USER_ID),
                                //        DataAccess.CreateParameter("STATUS", DbType.String, STATUS),
                                //        DataAccess.CreateParameter("CUSTNO", DbType.String, CUSTNO)
                                //    });
                                dataAccess.ExecuteNonQuery(
                                    "INSERT INTO TRANSDTL(GCODE,LOCATION,QTY,TRANS_DATE,USER_ID,STATUS,CUSTNO) VALUES ('" + GCODE + "','" + LOCATION + "'," + QTY + ",'" + TRANS_DATE + "','" + USER_ID + "','" + STATUS + "','" + CUSTNO + "')",
                                    tran);
                            }
                            //switch (STATUS)
                            //{
                            //    case "進貨":
                            //        if (dataAccess.ExecuteNonQuery(
                            //            "Update STOCKMST Set IN_QTY=IN_QTY+@QTY , REAL_QTY=LAST_QTY + (IN_QTY+ @QTY) - OUT_QTY  where gcode=@GCODE AND LOCATION=@LOCATION ",
                            //            tran, new DbParameter[]
                            //            {
                            //                DataAccess.CreateParameter("QTY", DbType.String, QTY),
                            //                DataAccess.CreateParameter("GCODE", DbType.String, GCODE),
                            //                DataAccess.CreateParameter("LOCATION", DbType.String, LOCATION)
                            //            }) == 0)
                            //        {
                            //            dataAccess.ExecuteNonQuery(
                            //                "INSERT INTO ERRDATA(GCODE,LOCATION,QTY,TRANS_DATE,USER_ID,STATUS,CUSTNO) VALUES (@GCODE,@LOCATION,@QTY,@TRANS_DATE,@USER_ID,@STATUS,@CUSTNO)",
                            //                tran, new DbParameter[]
                            //                {
                            //                    DataAccess.CreateParameter("GCODE", DbType.String, GCODE),
                            //                    DataAccess.CreateParameter("LOCATION", DbType.String, LOCATION),
                            //                    DataAccess.CreateParameter("QTY", DbType.String, QTY),
                            //                    DataAccess.CreateParameter("TRANS_DATE", DbType.String, TRANS_DATE),
                            //                    DataAccess.CreateParameter("USER_ID", DbType.String, USER_ID),
                            //                    DataAccess.CreateParameter("STATUS", DbType.String, STATUS),
                            //                    DataAccess.CreateParameter("CUSTNO", DbType.String, CUSTNO)
                            //                });
                            //        }
                            //        else
                            //        {
                            //            dataAccess.ExecuteNonQuery(
                            //                "Update MATERMST Set Stock_QTY=Stock_QTY+ @QTY Where gcode=@GCODE ",
                            //                tran, new DbParameter[]
                            //                {
                            //                    DataAccess.CreateParameter("QTY", DbType.String, QTY),
                            //                    DataAccess.CreateParameter("GCODE", DbType.String, GCODE)
                            //                });
                            //        }
                            //        break;
                            //    case "出貨":
                            //        if (dataAccess.ExecuteNonQuery(
                            //            "Update STOCKMST Set OUT_QTY = OUT_QTY+@QTY, REAL_QTY=LAST_QTY + IN_QTY - (OUT_QTY + @QTY) where GCODE=@GCODE  and LOCATION = @LOCATION ",
                            //            tran, new DbParameter[]
                            //            {
                            //                DataAccess.CreateParameter("QTY", DbType.String, QTY),
                            //                DataAccess.CreateParameter("GCODE", DbType.String, GCODE),
                            //                DataAccess.CreateParameter("LOCATION", DbType.String, LOCATION)
                            //            }) == 0)
                            //        {
                            //            dataAccess.ExecuteNonQuery(
                            //                "INSERT INTO ERRDATA(GCODE,LOCATION,QTY,TRANS_DATE,USER_ID,STATUS,CUSTNO) VALUES (@GCODE,@LOCATION,@QTY,@TRANS_DATE,@USER_ID,@STATUS,@CUSTNO)",
                            //                tran, new DbParameter[]
                            //                {
                            //                    DataAccess.CreateParameter("GCODE", DbType.String, GCODE),
                            //                    DataAccess.CreateParameter("LOCATION", DbType.String, LOCATION),
                            //                    DataAccess.CreateParameter("QTY", DbType.String, QTY),
                            //                    DataAccess.CreateParameter("TRANS_DATE", DbType.String, TRANS_DATE),
                            //                    DataAccess.CreateParameter("USER_ID", DbType.String, USER_ID),
                            //                    DataAccess.CreateParameter("STATUS", DbType.String, STATUS),
                            //                    DataAccess.CreateParameter("CUSTNO", DbType.String, CUSTNO)
                            //                });
                            //        }
                            //        else
                            //        {
                            //            dataAccess.ExecuteNonQuery(
                            //                "Update MATERMST Set Stock_QTY=Stock_QTY- @QTY Where gcode=@GCODE ",
                            //                tran, new DbParameter[]
                            //                {
                            //                    DataAccess.CreateParameter("QTY", DbType.String, QTY),
                            //                    DataAccess.CreateParameter("GCODE", DbType.String, GCODE)
                            //                });
                            //        }
                            //        break;
                            //    case "退回":
                            //        if (dataAccess.ExecuteNonQuery(
                            //            "Update STOCKMST Set OUT_QTY = OUT_QTY-@QTY, REAL_QTY=LAST_QTY + IN_QTY - (OUT_QTY - @QTY) where GCODE=@GCODE  and LOCATION = @LOCATION ",
                            //            tran, new DbParameter[]
                            //            {
                            //                DataAccess.CreateParameter("QTY", DbType.String, QTY),
                            //                DataAccess.CreateParameter("GCODE", DbType.String, GCODE),
                            //                DataAccess.CreateParameter("LOCATION", DbType.String, LOCATION)
                            //            }) == 0)
                            //        {
                            //            dataAccess.ExecuteNonQuery(
                            //                "INSERT INTO ERRDATA(GCODE,LOCATION,QTY,TRANS_DATE,USER_ID,STATUS,CUSTNO) VALUES (@GCODE,@LOCATION,@QTY,@TRANS_DATE,@USER_ID,@STATUS,@CUSTNO)",
                            //                tran, new DbParameter[]
                            //                {
                            //                    DataAccess.CreateParameter("GCODE", DbType.String, GCODE),
                            //                    DataAccess.CreateParameter("LOCATION", DbType.String, LOCATION),
                            //                    DataAccess.CreateParameter("QTY", DbType.String, QTY),
                            //                    DataAccess.CreateParameter("TRANS_DATE", DbType.String, TRANS_DATE),
                            //                    DataAccess.CreateParameter("USER_ID", DbType.String, USER_ID),
                            //                    DataAccess.CreateParameter("STATUS", DbType.String, STATUS),
                            //                    DataAccess.CreateParameter("CUSTNO", DbType.String, CUSTNO)
                            //                });
                            //        }
                            //        else
                            //        {
                            //            dataAccess.ExecuteNonQuery(
                            //                "Update MATERMST Set Stock_QTY=Stock_QTY+@QTY Where gcode=@GCODE ",
                            //                tran, new DbParameter[]
                            //                {
                            //                    DataAccess.CreateParameter("QTY", DbType.String, QTY),
                            //                    DataAccess.CreateParameter("GCODE", DbType.String, GCODE)
                            //                });
                            //        }
                            //        break;
                            //    case "盤點":
                            //        if (dataAccess.ExecuteNonQuery(
                            //            "Update INVENMST Set INVE_QTY =(INVE_QTY + @QTY) Where GCODE=@GCODE and location =@LOCATION ",
                            //            tran, new DbParameter[]
                            //            {
                            //                DataAccess.CreateParameter("QTY", DbType.String, QTY),
                            //                DataAccess.CreateParameter("GCODE", DbType.String, GCODE),
                            //                DataAccess.CreateParameter("LOCATION", DbType.String, LOCATION)
                            //            }) == 0)
                            //        {
                            //            dataAccess.ExecuteNonQuery(
                            //                "INSERT INTO INVENMST(GCODE,LOCATION,INVE_QTY,INVE_DATE,USER_ID) VALUES (@GCODE,@LOCATION,@INVE_QTY,@INVE_DATE,@USER_ID)",
                            //                tran, new DbParameter[]
                            //                {
                            //                    DataAccess.CreateParameter("GCODE", DbType.String, GCODE),
                            //                    DataAccess.CreateParameter("LOCATION", DbType.String, LOCATION),
                            //                    DataAccess.CreateParameter("INVE_QTY", DbType.String, QTY),
                            //                    DataAccess.CreateParameter("INVE_DATE", DbType.String, TRANS_DATE),
                            //                    DataAccess.CreateParameter("USER_ID", DbType.String, USER_ID)
                            //                });
                            //        }
                            //        break;
                            //}
                            switch (STATUS)
                            {
                                case "進貨":
                                    if (dataAccess.ExecuteNonQuery(
                                        "Update STOCKMST Set IN_QTY=IN_QTY+" + QTY + " , REAL_QTY=LAST_QTY + (IN_QTY+ " + QTY + ") - OUT_QTY  where gcode='" + GCODE + "' AND LOCATION='" + LOCATION + "' ",
                                        tran) == 0)
                                    {
                                        dataAccess.ExecuteNonQuery(
                                            "INSERT INTO ERRDATA(GCODE,LOCATION,QTY,TRANS_DATE,USER_ID,STATUS,CUSTNO) VALUES ('" + GCODE + "','" + LOCATION + "'," + QTY + ",'" + TRANS_DATE + "','" + USER_ID + "','" + STATUS + "','" + CUSTNO + "')",
                                            tran);
                                    }
                                    else
                                    {
                                        dataAccess.ExecuteNonQuery(
                                            "Update MATERMST Set Stock_QTY=Stock_QTY+ " + QTY + " Where gcode='" + GCODE + "' ",tran);
                                    }
                                    break;
                                case "出貨":
                                    if (dataAccess.ExecuteNonQuery(
                                        "Update STOCKMST Set OUT_QTY = OUT_QTY+" + QTY + ", REAL_QTY=LAST_QTY + IN_QTY - (OUT_QTY + " + QTY + ") where GCODE='" + GCODE + "'  and LOCATION = '" + LOCATION + "' ",
                                        tran) == 0)
                                    {
                                        dataAccess.ExecuteNonQuery(
                                            "INSERT INTO ERRDATA(GCODE,LOCATION,QTY,TRANS_DATE,USER_ID,STATUS,CUSTNO) VALUES ('" + GCODE + "','" + LOCATION + "'," + QTY + ",'" + TRANS_DATE + "','" + USER_ID + "','" + STATUS + "','" + CUSTNO + "')",
                                            tran);
                                    }
                                    else
                                    {
                                        dataAccess.ExecuteNonQuery(
                                            "Update MATERMST Set Stock_QTY=Stock_QTY- " + QTY + " Where gcode='" + GCODE + "' ",tran);
                                    }
                                    break;
                                case "退回":
                                    if (dataAccess.ExecuteNonQuery(
                                        "Update STOCKMST Set OUT_QTY = OUT_QTY-" + QTY + ", REAL_QTY=LAST_QTY + IN_QTY - (OUT_QTY - " + QTY + ") where GCODE='" + GCODE + "'  and LOCATION = '" + LOCATION + "' ",
                                        tran) == 0)
                                    {
                                        dataAccess.ExecuteNonQuery(
                                            "INSERT INTO ERRDATA(GCODE,LOCATION,QTY,TRANS_DATE,USER_ID,STATUS,CUSTNO) VALUES ('" + GCODE + "','" + LOCATION + "'," + QTY + ",'" + TRANS_DATE + "','" + USER_ID + "','" + STATUS + "','" + CUSTNO + "')",
                                            tran);
                                    }
                                    else
                                    {
                                        dataAccess.ExecuteNonQuery(
                                            "Update MATERMST Set Stock_QTY=Stock_QTY+" + QTY + " Where gcode='" + GCODE + "' ",tran);
                                    }
                                    break;
                                case "盤點":
                                    if (dataAccess.ExecuteNonQuery(
                                        "Update INVENMST Set INVE_QTY =(INVE_QTY + " + QTY + ") Where GCODE='" + GCODE + "' and location ='" + LOCATION + "' ",
                                        tran) == 0)
                                    {
                                        dataAccess.ExecuteNonQuery(
                                            "INSERT INTO INVENMST(GCODE,LOCATION,INVE_QTY,INVE_DATE,USER_ID) VALUES ('" + GCODE + "','" + LOCATION + "'," + QTY + ",'" + TRANS_DATE + "','" + USER_ID + "')",
                                            tran);
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            if (line.Length > 0 && line.Length < 62)
                            {
                                using (
                                    StreamWriter sw =
                                        new StreamWriter(
                                            Application.StartupPath + @"\" + "ERRLog_" + DateTime.Now.Month.ToString() +
                                            "-" +
                                            DateTime.Now.Day.ToString() + ".Log", true, Encoding.Default))
                                {
                                    sw.WriteLine(
                                        "----------------------------------------------------------------------------");
                                    sw.WriteLine(DateTime.Now + "讀入錯誤的資料為");
                                    sw.WriteLine(line);
                                    sw.WriteLine(
                                        "----------------------------------------------------------------------------");
                                }
                            }
                        }
                    }
                    tran.Commit();
                    tran = null;
                    sr.Close();
                    sr.Dispose();
                }
                fileInfos[0].CopyTo(fileInfos[0].FullName.Replace(".TXT",
                    DateTime.Now.Month + "-" + DateTime.Now.Day + ".BAK"),true);
                File.Delete(fileInfos[0].FullName);
            }
            catch (Exception ex)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
                //Messages.Error(ex.Message);
                timer1.Enabled = false;
                Messages.Error("資料異常！\r\n停止匯入資料庫！");
                timer1.Enabled = true;
            }
            finally
            {
                if (tran != null)
                {
                    tran.Dispose();
                }
            }
        }

        private void CloseOtherWindow()
        {
            foreach (Form form in this.MdiChildren)
            {
                form.Close();
                form.Dispose();
            }
        }

        /// <summary>
        /// 得到新字符串
        /// </summary>
        /// <param name="srcString">原字符串</param>
        /// <param name="spaceLen">補入的字符串</param>
        /// <param name="ax">0:在右邊補空 1：在左邊補空</param>
        /// <returns></returns>
        public static string JoinSpaces(string srcString, int speceLen, int ax)
        {
            string outString = String.Empty;

            int strLen = Encoding.Default.GetBytes(srcString).Length;
            if (ax == 0)
            {
                outString = srcString.PadRight(speceLen, ' ');
                return outString.Substring(0, srcString.Length + (speceLen - srcString.Length));
            }
            else
            {
                outString = srcString.PadLeft(speceLen, ' ');
                return outString.Substring(outString.Length - speceLen, speceLen);
            }
        }

        private bool Get_FILEGOG(string Content)
        {
            try
            {
                if (File.Exists(Application.StartupPath + "\\Regal Trans\\FILE.LOG"))
                    File.Delete(Application.StartupPath + "\\Regal Trans\\FILE.LOG");

                FileStream fsWriteFileLog = new FileStream(Application.StartupPath + "\\Regal Trans\\FILE.LOG", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter swFileLog = new StreamWriter(fsWriteFileLog, Encoding.Default);

                swFileLog.WriteLine(Content);
                swFileLog.Flush();
                swFileLog.Close();
                fsWriteFileLog.Close();

                Process prs = new Process();
                prs = Process.Start(Application.StartupPath + "\\Regal Trans\\Regal_HTTrans.exe", "send");

                bool b = true;
                while (b)
                {
                    if (prs.HasExited)
                    {
                        b = false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "讯息");
                return false;
            }
        }
    }
}
