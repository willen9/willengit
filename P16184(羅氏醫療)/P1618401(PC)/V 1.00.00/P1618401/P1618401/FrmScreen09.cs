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
    public partial class FrmScreen09 : Form
    {
        private DataAccess dataAccess = null;
        private DataTable dtSource = new DataTable();
        public FrmScreen09()
        {
            InitializeComponent();
        }

        private void FrmScreen09_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                dataAccess = new DataAccess();
                dataAccess.ConnectionString = Share.conStr;
                dataAccess.ProviderName = Share.provide;

                ExecuteSQL();

                lblCount.Text = "筆數：" + dtSource.Rows.Count + " 筆";
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ExecuteSQL();
                lblCount.Text = "搜尋筆數：" + dtSource.Rows.Count + " 筆";
                dataGridView1.DataSource = dtSource;
                if (dtSource.Rows.Count == 0)
                {
                    lblCount.Text="無符合的資料！";
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ExecuteSQL();
                lblCount.Text = "搜尋筆數：" + dtSource.Rows.Count + " 筆";
                if (dtSource.Rows.Count > 0)
                {
                    frm09 frm09 = new frm09();
                    frm09.dtSource = dtSource;
                    frm09.ShowDialog();
                    frm09.Close();
                    frm09.Dispose();
                }
                else
                {
                    lblCount.Text="無符合的資料！";
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
                txtDateStart.Text = txtDateEnd.Text = "";
                rabAll.Checked = true;
                ExecuteSQL();
                lblCount.Text = "筆數：" + dtSource.Rows.Count + " 筆";
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

        private void ExecuteSQL()
        {
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
            //string sql = "SELECT * FROM ERRDATA  WHERE  STATUS LIKE @STATUS ";
            string sql = "SELECT * FROM ERRDATA  WHERE  STATUS LIKE '" + status + "%' ";
            if (txtDateStart.Text.Trim().Length > 0)
            {
                //sql += " And TRANS_DATE >=@TRANS_DATEStart ";
                sql += " And TRANS_DATE >='" + txtDateStart.Text.Trim() + "' ";
            }
            if (txtDateEnd.Text.Trim().Length > 0)
            {
                //sql += " And TRANS_DATE <=@TRANS_DATEEnd ";
                sql += " And TRANS_DATE <='"+txtDateEnd.Text.Trim()+"' ";
            }
            //dtSource = dataAccess.ExecuteDataTable(sql, new DbParameter[]
            //{
            //    DataAccess.CreateParameter("STATUS", DbType.String, status + "%"),
            //    DataAccess.CreateParameter("TRANS_DATEStart", DbType.String, txtDateStart.Text.Trim()),
            //    DataAccess.CreateParameter("TRANS_DATEEnd", DbType.String, txtDateEnd.Text.Trim())
            //});
            dtSource = dataAccess.ExecuteDataTable(sql);
        }
    }
}
