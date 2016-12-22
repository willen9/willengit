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
    public partial class FrmScreen03 : Form
    {
        private DataAccess dataAccess = null;
        private DataTable dtMaterial=new DataTable( );
        private DataTable dtStore=new DataTable( );
        private DataTable dtPrint=new DataTable( );
        public FrmScreen03()
        {
            InitializeComponent();
        }

        private void FrmScreen03_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                lblTotal.Text = lblContent.Text = "";
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

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                FieldClear();
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ExecuteSQL();
                ExecuteSQL_prt();
                if (dtPrint.Rows.Count > 0)
                {
                    frm03 frm03 = new frm03();
                    frm03.dtSource = dtPrint;
                    frm03.ShowDialog();
                    frm03.Close();
                    frm03.Dispose();
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

        private void FieldClear()
        {
            txtDes.Text =
                txtGlobalEnd.Text = txtGlobalStart.Text = txtLocalEnd.Text = txtLocalStart.Text = txtSystem.Text = "";
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
            string sql = "SELECT M.GCODE AS GlobalCode,M.LCODE AS LocalCode,M.Description,M.USystem  FROM MATERMST AS M where 1=1 ";
            if (txtGlobalStart.Text.Trim().Length > 0)
            {
                //sql += " and  M.GCode >=@GCodeStart ";
                sql += " and  M.GCode >='" + txtGlobalStart.Text.Trim() + "' ";
            }
            if (txtGlobalEnd.Text.Trim().Length > 0)
            {
                //sql += " and  M.GCode <= @GCodeEnd ";
                sql += " and  M.GCode <= '" + txtGlobalEnd.Text.Trim() + "' ";
            }
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
                //sql += " and M.USystem like @USystem ";
                sql += " and M.USystem like '%" + txtSystem.Text.Trim() + "%' ";
            }
            if (txtDes.Text.Trim().Length > 0)
            {
                sql += " and M.Description like '%" +txtDes.Text.Trim()+ "%' ";
            }
            sql += " order by M.GCode ";
            //dtMaterial = dataAccess.ExecuteDataTable(sql, new DbParameter[]
            //{
            //    DataAccess.CreateParameter("GCodeStart", DbType.String, txtGlobalStart.Text.Trim()),
            //    DataAccess.CreateParameter("GCodeEnd", DbType.String, txtGlobalEnd.Text.Trim()),
            //    DataAccess.CreateParameter("LCodeStart", DbType.String, txtLocalStart.Text.Trim()),
            //    DataAccess.CreateParameter("LCodeEnd", DbType.String, txtLocalEnd.Text.Trim()),
            //    DataAccess.CreateParameter("USystem", DbType.String, "%" + txtSystem.Text.Trim() + "%")
            //});
            dtMaterial = dataAccess.ExecuteDataTable(sql);
            lblTotal.Text = "筆數：" + dtMaterial.Rows.Count + " 筆"; 
            dataGridView1.DataSource = dtMaterial;
            dtStore.Clear();
            dataGridView2.DataSource = dtStore;
            if (dtMaterial.Rows.Count > 0)
            {
                dataGridView1.Rows[0].Selected = true;
                ExecuteGrid2(dtMaterial.Rows[0]["GlobalCode"].ToString());
            }
            else
            {
                lblContent.Text = "";
            }
        }

        private void ExecuteGrid2(string gCode)
        {
            //dtStore =
            //    dataAccess.ExecuteDataTable(
            //        "SELECT S.LOCATION,S.IN_QTY,S.OUT_QTY,(S.LAST_QTY+S.IN_QTY-S.OUT_QTY) AS STOCK_QTY FROM STOCKMST AS S WHERE S.GCODE = @GCODE ORDER BY S.LOCATION",
            //        new DbParameter[]
            //        {
            //            DataAccess.CreateParameter("GCODE", DbType.String, gCode)
            //        });
            dtStore =
             dataAccess.ExecuteDataTable(
                 "SELECT S.LOCATION,S.IN_QTY,S.OUT_QTY,(S.LAST_QTY+S.IN_QTY-S.OUT_QTY) AS STOCK_QTY FROM STOCKMST AS S WHERE S.GCODE = '"+gCode+"' ORDER BY S.LOCATION");
            dataGridView2.DataSource = dtStore;
            lblContent.Text = "筆數：" + dtStore.Rows.Count + " 筆";
        }

        private void ExecuteSQL_prt()
        {
            //string sql =
            //    "SELECT M.GCODE AS GlobalCode,M.LCODE AS LocalCode,M.Description,M.USystem,M.STOCK_QTY,STOCKMST.LOCATION,STOCKMST.REAL_QTY FROM MATERMST AS M,STOCKMST WHERE M.GCODE=STOCKMST.GCODE";
            //if (txtGlobalStart.Text.Trim().Length > 0)
            //{
            //    sql += " and  M.GCode >=@GCodeStart ";
            //}
            //if (txtGlobalEnd.Text.Trim().Length > 0)
            //{
            //    sql += " and  M.GCode <= @GCodeEnd ";
            //}
            //if (txtLocalStart.Text.Trim().Length > 0)
            //{
            //    sql += " and M.LCode >=@LCodeStart ";
            //}
            //if (txtLocalEnd.Text.Trim().Length > 0)
            //{
            //    sql += " and M.LCode <=@LCodeEnd ";
            //}
            //if (txtSystem.Text.Trim().Length > 0)
            //{
            //    sql += " and M.USystem like @USystem ";
            //}
            //sql += " order by M.GCode ";

            //dtPrint = dataAccess.ExecuteDataTable(sql, new DbParameter[]
            //{
            //    DataAccess.CreateParameter("GCodeStart", DbType.String, txtGlobalStart.Text.Trim()),
            //    DataAccess.CreateParameter("GCodeEnd", DbType.String, txtGlobalEnd.Text.Trim()),
            //    DataAccess.CreateParameter("LCodeStart", DbType.String, txtLocalStart.Text.Trim()),
            //    DataAccess.CreateParameter("LCodeEnd", DbType.String, txtLocalEnd.Text.Trim()),
            //    DataAccess.CreateParameter("USystem", DbType.String, "%" + txtSystem.Text.Trim() + "%")
            //});
            string sql =
              "SELECT M.GCODE AS GlobalCode,M.LCODE AS LocalCode,M.Description,M.USystem,M.STOCK_QTY,STOCKMST.LOCATION,STOCKMST.REAL_QTY FROM MATERMST AS M,STOCKMST WHERE M.GCODE=STOCKMST.GCODE";
            if (txtGlobalStart.Text.Trim().Length > 0)
            {
                sql += " and  M.GCode >='" + txtGlobalStart.Text.Trim() + "' ";
            }
            if (txtGlobalEnd.Text.Trim().Length > 0)
            {
                sql += " and  M.GCode <= '" + txtGlobalEnd.Text.Trim() + "' ";
            }
            if (txtLocalStart.Text.Trim().Length > 0)
            {
                sql += " and M.LCode >='" + txtLocalStart.Text.Trim() + "' ";
            }
            if (txtLocalEnd.Text.Trim().Length > 0)
            {
                sql += " and M.LCode <='" + txtLocalEnd.Text.Trim() + "' ";
            }
            if (txtSystem.Text.Trim().Length > 0)
            {
                sql += " and M.USystem like '%" + txtSystem.Text.Trim() + "%' ";
            }
            sql += " order by M.GCode ";

            dtPrint = dataAccess.ExecuteDataTable(sql);
        }
    }
}
