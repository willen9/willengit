using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using REGAL.Data.DataAccess;
using RegalPrinter;

namespace P1618401
{
    public partial class FrmScreen05 : Form
    {
        private DataAccess dataAccess = null;
        private DataTable dtSource = new DataTable();
        public FrmScreen05()
        {
            InitializeComponent();
        }

        private void FrmScreen05_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                dataAccess = new DataAccess();
                dataAccess.ConnectionString = Share.conStr;
                dataAccess.ProviderName = Share.provide;

                txtGlobalStart.Focus();
                btnPrint.Enabled = false;
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
            if (txtCount.Text.Trim().Length == 0)
            {
                Messages.Warning("請輸入列印數量");
                txtCount.Focus();
                return;
            }
            try
            {
                if (int.Parse(txtCount.Text.Trim()) <= 0)
                {
                    Messages.Warning("列印數量不能小于0");
                    txtCount.Focus();
                    txtCount.SelectAll();
                    return;
                }
            }
            catch 
            {
                Messages.Warning("列印數量格式不正確");
                txtCount.Focus();
                txtCount.SelectAll();
                return;
            }
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                foreach (DataRow dr in dtSource.Rows)
                {
                    PrtLabel(dr[0].ToString(), dr[1].ToString(), (dr[2].ToString().Length > 25 ? dr[2].ToString().Substring(0, 25) : dr[2].ToString()), int.Parse(txtCount.Text.Trim()));
                }
                btnPrint.Enabled = false;
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
            FieldCls();
            dtSource.Clear();
            dataGridView1.DataSource = dtSource;
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FieldCls()
        {
            txtGlobalStart.Text = txtGlobalEnd.Text = txtLocalStart.Text = txtLocalEnd.Text=txtSystem.Text = "";
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
            //string sql = "SELECT GCODE,LCODE,DESCRIPTION,USYSTEM FROM MATERMST where 1=1 ";
            //bool condition = false;
            //if (txtGlobalStart.Text.Trim().Length > 0)
            //{
            //    condition = true;
            //    sql += " and GCODE >= @GCODEStart";
            //}
            //if (txtGlobalEnd.Text.Trim().Length > 0)
            //{
            //    condition = true;
            //    sql += " and GCODE <= @GCODEEnd";
            //}
            //if (txtLocalStart.Text.Trim().Length > 0)
            //{
            //    condition = true;
            //    sql += " and LCODE >=@LCODEStart ";
            //}
            //if (txtLocalEnd.Text.Trim().Length > 0)
            //{
            //    condition = true;
            //    sql += " and LCODE <=@LCODEEnd ";
            //}
            //if (txtDes.Text.Trim().Length > 0)
            //{
            //    condition = true;
            //    sql += " and Description like @Description ";
            //}
            //if (txtSystem.Text.Trim().Length > 0)
            //{
            //    condition = true;
            //    sql += " and USystem like  @USystem ";
            //}
            //if (!condition)
            //{
            //    sql += " and USystem like @USystem ";
            //}
            //sql += " ORDER BY GCODE ";
            //dtSource = dataAccess.ExecuteDataTable(sql, new DbParameter[]
            //{
            //    DataAccess.CreateParameter("GCODEStart", DbType.String, txtGlobalStart.Text.Trim()),
            //    DataAccess.CreateParameter("GCODEEnd", DbType.String, txtGlobalEnd.Text.Trim()),
            //    DataAccess.CreateParameter("LCODEStart", DbType.String, txtLocalStart.Text.Trim()),
            //    DataAccess.CreateParameter("LCODEEnd", DbType.String, txtLocalEnd.Text.Trim()),
            //    DataAccess.CreateParameter("USystem", DbType.String, condition ? "%" + txtSystem.Text.Trim() + "%" : "%"),
            //    DataAccess.CreateParameter("Description", DbType.String, "%"+txtDes.Text.Trim()+"%")
            //});
            string sql = "SELECT GCODE,LCODE,DESCRIPTION,USYSTEM FROM MATERMST where 1=1 ";
            bool condition = false;
            if (txtGlobalStart.Text.Trim().Length > 0)
            {
                condition = true;
                sql += " and GCODE >= '" + txtGlobalStart.Text.Trim() + "'";
            }
            if (txtGlobalEnd.Text.Trim().Length > 0)
            {
                condition = true;
                sql += " and GCODE <= '" + txtGlobalEnd.Text.Trim() + "'";
            }
            if (txtLocalStart.Text.Trim().Length > 0)
            {
                condition = true;
                sql += " and LCODE >='" + txtLocalStart.Text.Trim() + "' ";
            }
            if (txtLocalEnd.Text.Trim().Length > 0)
            {
                condition = true;
                sql += " and LCODE <='" + txtLocalEnd.Text.Trim() + "' ";
            }
            if (txtDes.Text.Trim().Length > 0)
            {
                condition = true;
                sql += " and Description like '%" + txtDes.Text.Trim() + "%' ";
            }
            if (txtSystem.Text.Trim().Length > 0)
            {
                condition = true;
                sql += " and USystem like '%" + txtSystem.Text.Trim() + "%' ";
            }
            if (!condition)
            {
                sql += " and USystem like '%' ";
            }
            sql += " ORDER BY GCODE ";
            dtSource = dataAccess.ExecuteDataTable(sql);
            dataGridView1.DataSource = dtSource;
        }

        private void PrtLabel(string barcode,string str1,string str2,int page)
        {
            Ring rp = new Ring();
            rp.StartPrinter(Share.printName);
            //設置紙張大小及相關配置
            rp.FMT(1, "65", "30", true, 0, 1);
            //清除Buffer
            rp.ACL();
            rp.FAG(0);

            //打印條碼

            if (barcode.Length > 11)
            {
                rp.BDN(2, 5);
            }
            else
            {
                rp.BDN(3, 7);
            }

            rp.BFL("5", "5", 0, 1, "10", "*"+barcode+"*");
            rp.GetBIM("5", "16", new Font("Verdana", 8, FontStyle.Bold), barcode);
            rp.GetBIM("5", "21", new Font("Verdana", 8, FontStyle.Bold), str1);
            rp.GetBIM("5", "25", new Font("Verdana", 7, FontStyle.Bold), str2);

            //打印數量
            rp.PRT(page, 0, 1);
            //告知打印結束
            rp.EndPrinter();
        }
    }
}
