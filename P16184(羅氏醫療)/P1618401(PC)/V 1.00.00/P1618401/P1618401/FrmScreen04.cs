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
    public partial class FrmScreen04: Form
    {
        private DataAccess dataAccess = null;
        private DataTable dtMain=new DataTable( );
        private DataTable dtLeft=new DataTable( );
        private DataTable dtRight=new DataTable( );
        private DataTable dtPrint;
        private string GCode;
        private string Location;
        public FrmScreen04()
        {
            InitializeComponent();
        }

        private void FrmScreen04_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                lblTotal.Text = lblLeft.Text=lblRight.Text = "";
                dataAccess = new DataAccess();
                dataAccess.ConnectionString = Share.conStr;
                dataAccess.ProviderName = Share.provide;

                ExecuteSQL();
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
                ExecuteSQL();
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
                ExecuteSQL();
                ExecuteSQL_prt();
                if (dtPrint.Rows.Count > 0)
                {
                    frm04 frm04 = new frm04();
                    frm04.dtSource = dtPrint;
                    frm04.ShowDialog();
                    frm04.Close();
                    frm04.Dispose();
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
                FiledClear();
                ExecuteSQL();
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    GCode = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    ExecuteGrid2();
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

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Location = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                    ExecuteGrid3();
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

        private void FiledClear()
        {
            txtGlobalStart.Text = txtGlobalEnd.Text = txtLocalStart.Text = txtLocalEnd.Text = txtSystem.Text = "";
        }

        private void ExecuteSQL()
        {
            if (txtGlobalEnd.Text.Trim().Length == 0)
            {
                txtGlobalEnd.Text = txtGlobalStart.Text;
            }
            if (txtLocalEnd.Text.Trim().Length == 0)
            {
                txtLocalEnd.Text = txtLocalStart.Text;
            }
            //string sql = "SELECT M.GCODE AS GlobalCode,M.LCODE AS LocalCode,M.Description,M.USystem,M.STOCK_QTY FROM MATERMST AS M where 1=1 ";
            //if (txtGlobalStart.Text.Trim().Length > 0)
            //{
            //    sql += " and M.GCode >= @GCodeStart ";
            //}
            //if (txtGlobalEnd.Text.Trim().Length > 0)
            //{
            //    sql += " and M.GCode <= @GCodeEnd ";
            //}
            //if (txtLocalStart.Text.Trim().Length > 0)
            //{
            //    sql += " and M.LCode >= @LCodeStart ";
            //}
            //if (txtLocalEnd.Text.Trim().Length > 0)
            //{
            //    sql += " and M.LCode <= @LCodeEnd ";
            //}
            //if (txtSystem.Text.Trim().Length > 0)
            //{
            //    sql += " and M.USystem like @USystem ";
            //}
            //sql += " order by M.GCode ";
            //dtMain = dataAccess.ExecuteDataTable(sql, new DbParameter[]
            //{
            //    DataAccess.CreateParameter("GCodeStart", DbType.String, txtGlobalStart.Text.Trim()),
            //    DataAccess.CreateParameter("GCodeEnd", DbType.String, txtGlobalEnd.Text.Trim()),
            //    DataAccess.CreateParameter("LCodeStart", DbType.String, txtLocalStart.Text.Trim()),
            //    DataAccess.CreateParameter("LCodeEnd", DbType.String, txtLocalEnd.Text.Trim()),
            //    DataAccess.CreateParameter("USystem", DbType.String, "%"+txtSystem.Text.Trim()+"%")
            //});
            string sql = "SELECT M.GCODE AS GlobalCode,M.LCODE AS LocalCode,M.Description,M.USystem,M.STOCK_QTY FROM MATERMST AS M where 1=1 ";
            if (txtGlobalStart.Text.Trim().Length > 0)
            {
                sql += " and M.GCode >= '"+txtGlobalStart.Text.Trim()+"' ";
            }
            if (txtGlobalEnd.Text.Trim().Length > 0)
            {
                sql += " and M.GCode <= '"+txtGlobalEnd.Text.Trim()+"' ";
            }
            if (txtLocalStart.Text.Trim().Length > 0)
            {
                sql += " and M.LCode >= '"+txtLocalStart.Text.Trim()+"' ";
            }
            if (txtLocalEnd.Text.Trim().Length > 0)
            {
                sql += " and M.LCode <= '" + txtLocalEnd.Text.Trim() + "' ";
            }
            if (txtSystem.Text.Trim().Length > 0)
            {
                sql += " and M.USystem like '%" + txtSystem.Text.Trim() + "%' ";
            }
            sql += " order by M.GCode ";
            dtMain = dataAccess.ExecuteDataTable(sql);
            lblTotal.Text = "筆數："+dtMain.Rows.Count+ " 筆";
            lblLeft.Text = lblRight.Text = "";
            dataGridView1.DataSource = dtMain;
            dtLeft.Clear();
            dtRight.Clear();
            dataGridView2.DataSource = dtLeft;
            dataGridView3.DataSource = dtRight;
            if (dtMain.Rows.Count > 0)
            {
                dataGridView1.Rows[0].Selected = true;
                GCode = dtMain.Rows[0]["GlobalCode"].ToString();
                ExecuteGrid2();
            }
        }

        private void ExecuteGrid2()
        {
            //dtLeft =
            //    dataAccess.ExecuteDataTable(
            //        "SELECT S.LOCATION,S.IN_QTY,S.OUT_QTY,(S.LAST_QTY+S.IN_QTY-S.OUT_QTY) AS STOCK_QTY FROM STOCKMST AS S WHERE S.GCODE = @GCODE ORDER BY S.LOCATION",
            //        new DbParameter[]
            //        {
            //            DataAccess.CreateParameter("GCODE", DbType.String, GCode)
            //        });
            dtLeft =
              dataAccess.ExecuteDataTable(
                  "SELECT S.LOCATION,S.IN_QTY,S.OUT_QTY,(S.LAST_QTY+S.IN_QTY-S.OUT_QTY) AS STOCK_QTY FROM STOCKMST AS S WHERE S.GCODE = '" + GCode + "' ORDER BY S.LOCATION");
            dataGridView2.DataSource = dtLeft;
            dtRight.Clear();
            dataGridView3.DataSource = dtRight;
            if (dtLeft.Rows.Count > 0)
            {
                lblLeft.Text = "筆數：" + dtLeft.Rows.Count + " 筆";
            }
            else
            {
                lblLeft.Text = "";
            }
            lblRight.Text = "";
            if (dtLeft.Rows.Count > 0)
            {
                dataGridView2.Rows[0].Selected = true;
                Location = dtLeft.Rows[0]["LOCATION"].ToString();
                //ExecuteGrid3();
            }
        }

        private void ExecuteGrid3()
        {
            string status = "";
            if (rabGoodsIn.Checked)
            {
                status = "進貨";
            }
            else if (rabGoodsOut.Checked)
            {
                status = "出貨";
            }
            else if (rabBack.Checked)
            {
                status = "退回";
            }
            if (dtMain.Rows.Count > 0 && dtLeft.Rows.Count > 0)
            {
                //dtRight =
                //    dataAccess.ExecuteDataTable(
                //        "SELECT T.TRANS_DATE  ,T.LOCATION AS LOC,T.QTY,T.STATUS FROM TRANSDTL AS T WHERE T.GCODE = @GCODE AND T.LOCATION = @LOCATION AND T.STATUS LIKE @STATUS ORDER BY T.TRANS_DATE,T.LOCATION ",
                //        new DbParameter[]
                //        {
                //            DataAccess.CreateParameter("GCODE", DbType.String, GCode),
                //            DataAccess.CreateParameter("LOCATION", DbType.String, Location),
                //            DataAccess.CreateParameter("STATUS", DbType.String, status + "%")
                //        });
                dtRight =
                   dataAccess.ExecuteDataTable(
                       "SELECT T.TRANS_DATE  ,T.LOCATION AS LOC,T.QTY,T.STATUS FROM TRANSDTL AS T WHERE T.GCODE = '" + GCode + "' AND T.LOCATION = '" + Location + "' AND T.STATUS LIKE '" + status + "%' ORDER BY T.TRANS_DATE,T.LOCATION ");
                dataGridView3.DataSource = dtRight;
                if (dtRight.Rows.Count > 0)
                {
                    lblRight.Text = "筆數：" + dtRight.Rows.Count + " 筆";
                }
                else
                {
                    lblRight.Text = "";
                }
            }
            else
            {
                dtRight.Clear();
                dataGridView3.DataSource = dtRight ;
                lblRight.Text = "";
            }
        }

        private void ExecuteSQL_prt()
        {
            string status = "";
            if (rabGoodsIn.Checked)
            {
                status = "進貨";
            }
            else if (rabGoodsOut.Checked)
            {
                status = "出貨";
            }
            else if (rabBack.Checked)
            {
                status = "退回";
            }
            string sql = "SELECT M.GCODE AS GlobalCode,M.LCODE AS LocalCode,M.Description,M.USystem,M.STOCK_QTY,STOCKMST.LOCATION ,STOCKMST.REAL_QTY,TRANSDTL.STATUS,TRANSDTL.TRANS_DATE,TRANSDTL.QTY FROM MATERMST AS M,STOCKMST,TRANSDTL WHERE M.GCODE=STOCKMST.GCODE AND STOCKMST.GCODE=TRANSDTL.GCODE ";
            if (status.Length > 0)
            {
                //sql += " AND TRANSDTL.STATUS=@STATUS ";
                sql += " AND TRANSDTL.STATUS='"+status+"' ";
            }
            if (txtGlobalStart.Text.Trim().Length > 0)
            {
                //sql += " and  M.GCode >= @GStart ";
                sql += " and  M.GCode >= '"+txtGlobalStart.Text.Trim()+"' ";
            }
            if (txtGlobalEnd.Text.Trim().Length > 0)
            {
                //sql += "  and  M.GCode <= @GEnd ";
                sql += "  and  M.GCode <= '" + txtGlobalEnd.Text.Trim() + "' ";
            }
            //sql += " and  M.GCode >= '03008690001' ";
            //sql += "  and  M.GCode <='03008690001' ";
            if (txtLocalStart.Text.Trim().Length > 0)
            {
                //sql += " and M.LCode >=@LCodeStart ";
                sql += " and M.LCode >='" + txtLocalStart.Text.Trim() + "' ";
            }
            if (txtLocalEnd.Text.Trim().Length > 0)
            {
                //sql += " and M.LCode <=@LCodeEnd ";
                sql += " and M.LCode <='" + txtLocalEnd.Text.Trim() + "' ";
            }
            if (txtSystem.Text.Trim().Length > 0)
            {
                //sql += " and M.USystem like  @USystem ";
                sql += " and M.USystem like  '%" + txtSystem.Text.Trim() + "%' ";
            }
            sql += " order by M.GCode ";
            //dtPrint = dataAccess.ExecuteDataTable(sql, new DbParameter[]
            //{
            //    DataAccess.CreateParameter("STATUS", DbType.String, status),
            //    DataAccess.CreateParameter("GStart", DbType.String, txtGlobalStart.Text.Trim()),
            //    DataAccess.CreateParameter("GEnd", DbType.String, txtGlobalEnd.Text.Trim()),
            //    DataAccess.CreateParameter("LCodeStart", DbType.String, txtLocalStart.Text.Trim()),
            //    DataAccess.CreateParameter("LCodeEnd", DbType.String, txtLocalEnd.Text.Trim()),
            //    DataAccess.CreateParameter("USystem", DbType.String, "%" + txtSystem.Text.Trim() + "%")
            //});
            dtPrint = dataAccess.ExecuteDataTable(sql);
        }
    }
}
