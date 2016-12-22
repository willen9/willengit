using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using P1618401.ReportFrm;
using REGAL.Data.DataAccess;

namespace P1618401
{
    public partial class FrmScreen07 : Form
    {
        private DataAccess dataAccess = null;
        private DataTable dtSource = new DataTable();
        private DataTable dtPrint=new DataTable( );
        public FrmScreen07()
        {
            InitializeComponent();
        }

        private void FrmScreen07_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                dataAccess = new DataAccess();
                dataAccess.ConnectionString = Share.conStr;
                dataAccess.ProviderName = Share.provide;

                txtDateStart.Text = txtDateEnd.Text = DateTime.Now.ToString("yyyyMMdd");
                lblCount.Text = "筆數：0 筆";
                btnPrint.Enabled = false;
                txtDateStart.Focus();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ExecuteSQL_prt();
                lblCount.Text = "";
                dtSource = dataAccess.ExecuteDataTable("select * from TRANSQTY");
                if (dtSource.Rows.Count == 0)
                {
                    lblCount.Text = "無符合的資料供列印！";
                }
                lblCount.Text = "筆數："+dtSource.Rows.Count+ " 筆";
                dataGridView1.DataSource = dtSource;
                btnPrint.Enabled = true;
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ExecuteSQL_prt();
                lblCount.Text = "";
                dtPrint = dataAccess.ExecuteDataTable("select * from TRANSQTY");
                if (dtPrint.Rows.Count > 0)
                {
                    frm07 frm07 = new frm07();
                    frm07.dtSource = dtPrint;
                    frm07.ShowDialog();
                    frm07.Close();
                    frm07.Dispose();
                }
                else
                {
                    lblCount.Text ="無符合的資料供列印！";
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                lblCount.Text = "";
                FieldClear();
                btnPrint.Enabled = false;
                dtSource.Clear();
                dataGridView1.DataSource = dtSource;
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

        private void txtDateStart_Leave(object sender, EventArgs e)
        {
            if (txtDateStart.Text.Trim().Length == 0)
            {
                txtDateStart.Text = DateTime.Now.ToString("yyyyMMdd");
            }
        }

        private void txtDateEnd_Leave(object sender, EventArgs e)
        {
            if (txtDateEnd.Text.Trim().Length == 0)
            {
                txtDateEnd.Text = txtDateStart.Text;
            }
        }

        private void FieldClear()
        {
            txtGlobalStart.Text = txtGlobalEnd.Text = "";
        }

        private void ExecuteSQL_prt()
        {
            DbTransaction tran = null;
            if (txtGlobalEnd.Text.Trim().Length == 0)
            {
                txtGlobalEnd.Text = txtGlobalStart.Text;
            }
            if (txtDateEnd.Text.Trim().Length == 0)
            {
                txtDateEnd.Text = txtDateStart.Text;
            }
            string status = "";
            if (rabGoodsIn.Checked)
            {
                status = "進貨";
            }
            if (rabGoodsOut.Checked)
            {
                status = "出貨";
            }
            if (rabBack.Checked)
            {
                status = "退回";
            }
            string sql = "";
            string sql1 = "";
            DataTable dtTemp1;
            DataTable dtTemp2;
            if (txtGlobalStart.Text.Trim().Length > 0)
            {
                //sql1 += " And T.GCODE >=@GCODEStart ";
                sql1 += " And T.GCODE >='" + txtGlobalStart.Text.Trim() + "' ";
            }
            if (txtGlobalEnd.Text.Trim().Length > 0)
            {
                //sql1 += " And T.GCODE <=@GCODEEnd ";
                sql1 += " And T.GCODE <='" + txtGlobalEnd.Text.Trim() + "' ";
            }
            if (txtDateStart.Text.Trim().Length > 0)
            {
                //sql1 += " And T.TRANS_DATE >=@TRANS_DATEStart ";
                sql1 += " And T.TRANS_DATE >='" + txtDateStart.Text.Trim() + "' ";
            }
            if (txtDateEnd.Text.Trim().Length > 0)
            {
                //sql1 += " And T.TRANS_DATE <=@TRANS_DATEEEnd ";
                sql1 += " And T.TRANS_DATE <='" + txtDateEnd.Text.Trim() + "' ";
            }
            try
            {
                tran = dataAccess.CreateDbTransaction();
                dataAccess.ExecuteNonQuery("drop table TRANSQTY", tran);
                switch (status)
                {
                    case "":
                        sql =
                            "SELECT T.GCode As GCode,'' As LCode,'' As Description,T.Location As Location,Sum(T.QTY) As IN_QTY,0 AS OUT_QTY,'' As Stock_QTY,T.Custno As CustNo INTO TRANSQTY FROM TRANSDTL AS T WHERE  T.STATUS='進貨'  OR T.STATUS='退回' ";
                        sql += sql1;
                        sql += " GROUP by T.GCode,T.Location,T.Custno ";
                        //dataAccess.ExecuteNonQuery(sql, tran, new DbParameter[]
                        //{
                        //    DataAccess.CreateParameter("GCODEStart", DbType.String, txtGlobalStart.Text.Trim()),
                        //    DataAccess.CreateParameter("GCODEEnd", DbType.String, txtGlobalEnd.Text.Trim()),
                        //    DataAccess.CreateParameter("TRANS_DATEStart", DbType.String, txtDateStart.Text.Trim()),
                        //    DataAccess.CreateParameter("TRANS_DATEEEnd", DbType.String, txtDateEnd.Text.Trim())
                        //});
                        dataAccess.ExecuteNonQuery(sql, tran);

                        sql =
                            "SELECT T.GCode As GCode,T.Location As Location,Sum(T.QTY) As OUT_QTY FROM TRANSDTL AS T WHERE  T.STATUS='出貨' ";
                        sql = sql + sql1 + " GROUP by T.GCode,T.Location ";
                        //dtTemp1 = dataAccess.ExecuteDataTable(sql, tran, new DbParameter[]
                        //{
                        //    DataAccess.CreateParameter("GCODEStart", DbType.String, txtGlobalStart.Text.Trim()),
                        //    DataAccess.CreateParameter("GCODEEnd", DbType.String, txtGlobalEnd.Text.Trim()),
                        //    DataAccess.CreateParameter("TRANS_DATEStart", DbType.String, txtDateStart.Text.Trim()),
                        //    DataAccess.CreateParameter("TRANS_DATEEEnd", DbType.String, txtDateEnd.Text.Trim())
                        //});
                        dtTemp1 = dataAccess.ExecuteDataTable(sql, tran);
                        foreach (DataRow dr in dtTemp1.Rows)
                        {
                            //if (dataAccess.ExecuteNonQuery(
                            //    "UPDATE TRANSQTY SET OUT_QTY = @OUT_QTY WHERE GCode =@GCode And Location =@Location ",
                            //    tran,
                            //    new DbParameter[]
                            //    {
                            //        DataAccess.CreateParameter("OUT_QTY", DbType.String, dr["OUT_QTY"].ToString()),
                            //        DataAccess.CreateParameter("GCode", DbType.String, dr["GCode"].ToString()),
                            //        DataAccess.CreateParameter("Location", DbType.String, dr["Location"].ToString())
                            //    }) == 0)
                            //{
                            //    dataAccess.ExecuteNonQuery(
                            //        "INSERT INTO TRANSQTY(GCode,Location,IN_QTY,OUT_QTY) VALUES(@GCode,@Location,'0',@OUT_QTY)",
                            //        tran, new DbParameter[]
                            //        {
                            //            DataAccess.CreateParameter("GCode", DbType.String, dr["GCode"].ToString()),
                            //            DataAccess.CreateParameter("Location", DbType.String, dr["Location"].ToString()),
                            //            DataAccess.CreateParameter("OUT_QTY", DbType.String, dr["OUT_QTY"].ToString())
                            //        });
                            //}
                            if (dataAccess.ExecuteNonQuery(
                               "UPDATE TRANSQTY SET OUT_QTY = " + dr["OUT_QTY"] + " WHERE GCode ='" + dr["GCode"] + "' And Location ='" + dr["Location"] + "' ",tran) == 0)
                            {
                                //dataAccess.ExecuteNonQuery(
                                //    "INSERT INTO TRANSQTY(GCode,Location,IN_QTY,OUT_QTY) VALUES(@GCode,@Location,'0',@OUT_QTY)",
                                //    tran, new DbParameter[]
                                //    {
                                //        DataAccess.CreateParameter("GCode", DbType.String, dr["GCode"].ToString()),
                                //        DataAccess.CreateParameter("Location", DbType.String, dr["Location"].ToString()),
                                //        DataAccess.CreateParameter("OUT_QTY", DbType.String, dr["OUT_QTY"].ToString())
                                //    });
                                dataAccess.ExecuteNonQuery(
                                  "INSERT INTO TRANSQTY(GCode,Location,IN_QTY,OUT_QTY) VALUES('" + dr["GCode"] + "','" + dr["Location"] + "','0'," + dr["OUT_QTY"] + ")",tran);
                            }
                        }
                        sql =
                            "SELECT M.GCODE AS GCode,M.LCODE AS LCode,M.Description As Description,M.Stock_QTY As Stock_QTY,T.GCode FROM MATERMST AS M,TRANSDTL AS T WHERE M.GCODE=T.GCODE Order by T.GCode ";
                        dtTemp2 = dataAccess.ExecuteDataTable(sql, tran);
                        foreach (DataRow dr in dtTemp2.Rows)
                        {
                            //dataAccess.ExecuteNonQuery(
                            //    "UPDATE TRANSQTY SET LCode = @LCode,Description =@Description,Stock_QTY = @Stock_QTY WHERE GCode =@GCode ",
                            //    tran, new DbParameter[]
                            //    {
                            //        DataAccess.CreateParameter("LCode", DbType.String, dr["LCode"].ToString()),
                            //        DataAccess.CreateParameter("Description", DbType.String,
                            //            dr["Description"].ToString()),
                            //        DataAccess.CreateParameter("Stock_QTY", DbType.String, dr["Stock_QTY"].ToString()),
                            //        DataAccess.CreateParameter("GCode", DbType.String, dr["GCode"].ToString())
                            //    });
                            dataAccess.ExecuteNonQuery(
                             "UPDATE TRANSQTY SET LCode = '" + dr["LCode"] + "',Description ='" + dr["Description"] + "',Stock_QTY =" + dr["Stock_QTY"] + "  WHERE GCode ='" + dr["GCode"] + "' ",tran);
                        }
                        break;
                    case "進貨":
                        sql = "SELECT T.GCode As GCode,'' As LCode,'' As Description,T.Location As Location,Sum(T.QTY) As IN_QTY,0 AS OUT_QTY,'' As Stock_QTY,T.Custno As CustNo INTO TRANSQTY FROM TRANSDTL AS T WHERE  T.STATUS='進貨'";
                        sql = sql + sql1 + " GROUP by T.GCode,T.Location,T.Custno ";
                        //dataAccess.ExecuteNonQuery(sql, tran, new DbParameter[]
                        //{
                        //    DataAccess.CreateParameter("GCODEStart", DbType.String, txtGlobalStart.Text.Trim()),
                        //    DataAccess.CreateParameter("GCODEEnd", DbType.String, txtGlobalEnd.Text.Trim()),
                        //    DataAccess.CreateParameter("TRANS_DATEStart", DbType.String, txtDateStart.Text.Trim()),
                        //    DataAccess.CreateParameter("TRANS_DATEEEnd", DbType.String, txtDateEnd.Text.Trim())
                        //});
                        dataAccess.ExecuteNonQuery(sql, tran);
                        sql = "SELECT M.GCODE AS GCode,M.LCODE AS LCode,M.Description As Description,M.Stock_QTY As Stock_QTY,T.GCode FROM MATERMST AS M,TRANSDTL AS T WHERE M.GCODE=T.GCODE Order by T.GCode ";
                        dtTemp1 = dataAccess.ExecuteDataTable(sql, tran);
                        foreach (DataRow dr in dtTemp1.Rows)
                        {
                            //dataAccess.ExecuteNonQuery("UPDATE TRANSQTY SET LCode =@LCode,Description = @Description,Stock_QTY=@Stock_QTY WHERE GCode = @GCode", tran, new DbParameter[]
                            //{
                            //    DataAccess.CreateParameter("LCode",DbType.String,dr["LCode"].ToString()),
                            //    DataAccess.CreateParameter("Description",DbType.String,dr["Description"].ToString()),
                            //    DataAccess.CreateParameter("Stock_QTY",DbType.String,dr["Stock_QTY"].ToString()),
                            //    DataAccess.CreateParameter("GCode",DbType.String,dr["GCode"].ToString())
                            //});
                            dataAccess.ExecuteNonQuery("UPDATE TRANSQTY SET LCode ='" + dr["LCode"] + "',Description = '" + dr["Description"] + "',Stock_QTY=" + dr["Stock_QTY"] + " WHERE GCode = '" + dr["GCode"] + "'", tran);
                        }
                        break;
                    case "出貨":
                        sql = "SELECT T.GCode As GCode,'' As LCode,'' As Description,T.Location As Location,0 As IN_QTY,Sum(T.QTY) AS OUT_QTY,'' As Stock_QTY,T.Custno As CustNo INTO TRANSQTY FROM TRANSDTL AS T WHERE  T.STATUS='出貨'";
                        sql = sql + sql1 + " GROUP by T.GCode,T.Location,T.Custno ";
                        //dataAccess.ExecuteNonQuery(sql, tran, new DbParameter[]
                        //{
                        //    DataAccess.CreateParameter("GCODEStart", DbType.String, txtGlobalStart.Text.Trim()),
                        //    DataAccess.CreateParameter("GCODEEnd", DbType.String, txtGlobalEnd.Text.Trim()),
                        //    DataAccess.CreateParameter("TRANS_DATEStart", DbType.String, txtDateStart.Text.Trim()),
                        //    DataAccess.CreateParameter("TRANS_DATEEEnd", DbType.String, txtDateEnd.Text.Trim())
                        //});
                        dataAccess.ExecuteNonQuery(sql, tran);
                        sql = "SELECT M.GCODE AS GCode,M.LCODE AS LCode,M.Description As Description,M.Stock_QTY As Stock_QTY,T.GCode FROM MATERMST AS M,TRANSDTL AS T WHERE M.GCODE=T.GCODE Order by T.GCode ";
                        dtTemp1 = dataAccess.ExecuteDataTable(sql,tran);
                        foreach (DataRow dr in dtTemp1.Rows)
                        {
                            //dataAccess.ExecuteNonQuery("UPDATE TRANSQTY SET LCode =@LCode,Description =@Description,Stock_QTY =@Stock_QTY WHERE GCode =@GCode",tran,new DbParameter[ ]
                            //{
                            //    DataAccess.CreateParameter("LCode",DbType.String,dr["LCode"].ToString()),
                            //    DataAccess.CreateParameter("Description",DbType.String,dr["Description"].ToString()),
                            //    DataAccess.CreateParameter("Stock_QTY",DbType.String,dr["Stock_QTY"].ToString()),
                            //    DataAccess.CreateParameter("GCode",DbType.String,dr["GCode"].ToString())
                            //});
                            dataAccess.ExecuteNonQuery("UPDATE TRANSQTY SET LCode ='" + dr["LCode"] + "',Description ='" + dr["Description"] + "',Stock_QTY =" + dr["Stock_QTY"] + " WHERE GCode ='" + dr["GCode"] + "'", tran);
                        }
                        break;
                    case "退回":
                        sql = "SELECT T.GCode As GCode,'' As LCode,'' As Description,T.Location As Location,Sum(T.QTY) As IN_QTY,0 AS OUT_QTY,'' As Stock_QTY,T.Custno As CustNo INTO TRANSQTY FROM TRANSDTL AS T WHERE  T.STATUS='退回' ";
                        sql = sql + sql1 + " GROUP by T.GCode,T.Location,T.Custno ";
                        //dataAccess.ExecuteNonQuery(sql, tran, new DbParameter[]
                        //{
                        //    DataAccess.CreateParameter("GCODEStart", DbType.String, txtGlobalStart.Text.Trim()),
                        //    DataAccess.CreateParameter("GCODEEnd", DbType.String, txtGlobalEnd.Text.Trim()),
                        //    DataAccess.CreateParameter("TRANS_DATEStart", DbType.String, txtDateStart.Text.Trim()),
                        //    DataAccess.CreateParameter("TRANS_DATEEEnd", DbType.String, txtDateEnd.Text.Trim())
                        //});
                        dataAccess.ExecuteNonQuery(sql, tran);
                        dtTemp1 =
                            dataAccess.ExecuteDataTable(
                                "SELECT M.GCODE AS GCode,M.LCODE AS LCode,M.Description As Description,M.Stock_QTY As Stock_QTY,T.GCode FROM MATERMST AS M,TRANSDTL AS T WHERE M.GCODE=T.GCODE Order by T.GCode",
                                tran);
                        foreach (DataRow dr in dtTemp1.Rows)
                        {
                            //dataAccess.ExecuteNonQuery(
                            //    "UPDATE TRANSQTY SET LCode =@LCode,Description=@Description,Stock_QTY=@Stock_QTY WHERE GCode =@GCode",
                            //    tran, new DbParameter[]
                            //    {
                            //        DataAccess.CreateParameter("LCode", DbType.String, dr["LCode"].ToString()),
                            //        DataAccess.CreateParameter("Description", DbType.String,
                            //            dr["Description"].ToString()),
                            //        DataAccess.CreateParameter("Stock_QTY", DbType.String, dr["Stock_QTY"].ToString()),
                            //        DataAccess.CreateParameter("GCode", DbType.String, dr["GCode"].ToString())
                            //    });
                            dataAccess.ExecuteNonQuery(
                               "UPDATE TRANSQTY SET LCode ='" + dr["LCode"] + "',Description='" + dr["Description"] + "',Stock_QTY=" + dr["Stock_QTY"] + " WHERE GCode ='" + dr["GCode"] + "'",tran);
                        }
                        break;
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
                throw ex;
            }
            finally
            {
                if (tran != null)
                {
                    tran.Dispose();
                }
            }
        }
    }
}
