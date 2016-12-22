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
    public partial class FrmScreen06 : Form
    {
        private DataAccess dataAccess = null;
        private DataTable dtPrint = new DataTable();
        private DataTable dtSource = new DataTable();
        public FrmScreen06()
        {
            InitializeComponent();
        }

        private void FrmScreen06_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                lblCount.Text = "";
                dataAccess = new DataAccess();
                dataAccess.ConnectionString = Share.conStr;
                dataAccess.ProviderName = Share.provide;

                txtGlobalStart.Focus();
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
            FieldClear();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                lblCount.Text = "";
                ExecuteSQL();
                if (dtPrint.Rows.Count > 0)
                {
                    frm06 frm06 = new frm06();
                    frm06.dtSource = dtPrint;
                    frm06.ShowDialog();
                    frm06.Close();
                    frm06.Dispose();
                }
                else
                {
                    Messages.Warning("無符合的資料供列印！");
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
                lblCount.Text = "";
                ExecuteSQL1();
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
            txtGlobalEnd.Text = txtGlobalStart.Text = txtLocalEnd.Text = txtLocalStart.Text = txtSystem.Text = "";
        }

        private void ExecuteSQL1()
        {
            if (txtGlobalEnd.Text.Trim().Length == 0)
            {
                txtGlobalEnd.Text = txtGlobalStart.Text;
            }
            if (txtLocalEnd.Text.Trim().Length == 0)
            {
                txtLocalEnd.Text = txtLocalStart.Text;
            }
            //string sql = "SELECT M.GCODE AS GlobalCode ,M.LCODE AS LocalCode,M.Description,M.USystem,M.Safe_Qty,M.STOCK_QTY FROM MATERMST AS M where M.STOCK_QTY< M.Safe_Qty ";
            //if (txtGlobalStart.Text.Trim().Length > 0)
            //{
            //    sql += " and M.GCODE >=@GCODEStart ";
            //}
            //if (txtGlobalEnd.Text.Trim().Length > 0)
            //{
            //    sql += " and M.GCODE <=@GCODEEnd ";
            //}
            //if (txtLocalStart.Text.Trim().Length > 0)
            //{
            //    sql += " and M.LCODE >=@LCODEStart ";
            //}
            //if (txtLocalEnd.Text.Trim().Length > 0)
            //{
            //    sql += " and M.LCODE <=@LCODEEnd ";
            //}
            //if (txtSystem.Text.Trim().Length > 0)
            //{
            //    sql += " and M.USystem like @USystem ";
            //}
            //sql += " order by M.GCode ";
            //dtPrint= dataAccess.ExecuteDataTable(sql, new DbParameter[]
            //{
            //    DataAccess.CreateParameter("GCODEStart", DbType.String, txtGlobalStart.Text.Trim()),
            //    DataAccess.CreateParameter("GCODEEnd", DbType.String, txtGlobalEnd.Text.Trim()),
            //    DataAccess.CreateParameter("LCODEStart", DbType.String, txtLocalStart.Text.Trim()),
            //    DataAccess.CreateParameter("LCODEEnd", DbType.String, txtLocalEnd.Text.Trim()),
            //    DataAccess.CreateParameter("USystem", DbType.String, "%" + txtSystem.Text.Trim() + "%")
            //});
            string sql = "SELECT M.GCODE AS GlobalCode ,M.LCODE AS LocalCode,M.Description,M.USystem,M.Safe_Qty,M.STOCK_QTY FROM MATERMST AS M where M.STOCK_QTY< M.Safe_Qty ";
            if (txtGlobalStart.Text.Trim().Length > 0)
            {
                sql += " and M.GCODE >='" + txtGlobalStart.Text.Trim() + "' ";
            }
            if (txtGlobalEnd.Text.Trim().Length > 0)
            {
                sql += " and M.GCODE <='" + txtGlobalEnd.Text.Trim() + "' ";
            }
            if (txtLocalStart.Text.Trim().Length > 0)
            {
                sql += " and M.LCODE >='" + txtLocalStart.Text.Trim() + "' ";
            }
            if (txtLocalEnd.Text.Trim().Length > 0)
            {
                sql += " and M.LCODE <='" + txtLocalEnd.Text.Trim() + "' ";
            }
            if (txtSystem.Text.Trim().Length > 0)
            {
                sql += " and M.USystem like '%" + txtSystem.Text.Trim() + "%' ";
            }
            sql += " order by M.GCode ";
            dtSource = dataAccess.ExecuteDataTable(sql);
            if (dtSource.Rows.Count > 0)
            {
                lblCount.Text = "搜尋筆數：" + dtSource.Rows.Count + " 筆";
            }
            else
            {
                lblCount.Text = "";
            }
            dataGridView1.DataSource = dtSource;
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
            //string sql = "SELECT M.GCODE AS GlobalCode ,M.LCODE AS LocalCode,M.Description,M.USystem,M.Safe_Qty,M.STOCK_QTY FROM MATERMST AS M where M.STOCK_QTY< M.Safe_Qty ";
            //if (txtGlobalStart.Text.Trim().Length > 0)
            //{
            //    sql += " and M.GCODE >=@GCODEStart ";
            //}
            //if (txtGlobalEnd.Text.Trim().Length > 0)
            //{
            //    sql += " and M.GCODE <=@GCODEEnd ";
            //}
            //if (txtLocalStart.Text.Trim().Length > 0)
            //{
            //    sql += " and M.LCODE >=@LCODEStart ";
            //}
            //if (txtLocalEnd.Text.Trim().Length > 0)
            //{
            //    sql += " and M.LCODE <=@LCODEEnd ";
            //}
            //if (txtSystem.Text.Trim().Length > 0)
            //{
            //    sql += " and M.USystem like @USystem ";
            //}
            //sql += " order by M.GCode ";
            //dtPrint= dataAccess.ExecuteDataTable(sql, new DbParameter[]
            //{
            //    DataAccess.CreateParameter("GCODEStart", DbType.String, txtGlobalStart.Text.Trim()),
            //    DataAccess.CreateParameter("GCODEEnd", DbType.String, txtGlobalEnd.Text.Trim()),
            //    DataAccess.CreateParameter("LCODEStart", DbType.String, txtLocalStart.Text.Trim()),
            //    DataAccess.CreateParameter("LCODEEnd", DbType.String, txtLocalEnd.Text.Trim()),
            //    DataAccess.CreateParameter("USystem", DbType.String, "%" + txtSystem.Text.Trim() + "%")
            //});
            string sql = "SELECT M.GCODE AS GlobalCode ,M.LCODE AS LocalCode,M.Description,M.USystem,M.Safe_Qty,M.STOCK_QTY FROM MATERMST AS M where M.STOCK_QTY< M.Safe_Qty ";
            if (txtGlobalStart.Text.Trim().Length > 0)
            {
                sql += " and M.GCODE >='" + txtGlobalStart.Text.Trim() + "' ";
            }
            if (txtGlobalEnd.Text.Trim().Length > 0)
            {
                sql += " and M.GCODE <='" + txtGlobalEnd.Text.Trim() + "' ";
            }
            if (txtLocalStart.Text.Trim().Length > 0)
            {
                sql += " and M.LCODE >='" + txtLocalStart.Text.Trim() + "' ";
            }
            if (txtLocalEnd.Text.Trim().Length > 0)
            {
                sql += " and M.LCODE <='" + txtLocalEnd.Text.Trim() + "' ";
            }
            if (txtSystem.Text.Trim().Length > 0)
            {
                sql += " and M.USystem like '%" + txtSystem.Text.Trim() + "%' ";
            }
            sql += " order by M.GCode ";
            dtPrint = dataAccess.ExecuteDataTable(sql);
            if (dtPrint.Rows.Count > 0)
            {
                lblCount.Text = "搜尋筆數：" + dtPrint.Rows.Count + " 筆";
            }
            else
            {
                lblCount.Text = "";
            }
        }
    }
}
