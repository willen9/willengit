using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using REGAL.Data.DataAccess;

namespace P1618401
{
    public partial class FrmScreen01 : Form
    {
        private DataAccess dataAccess = null;
        private DataTable dtSource;
        private string mode = "";
        public FrmScreen01()
        {
            InitializeComponent();
        }

        private void FrmScreen01_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                lblTotal.Text = "";
                dataAccess = new DataAccess();
                dataAccess.ConnectionString = Share.conStr;
                dataAccess.ProviderName = Share.provide;

                dtSource = dataAccess.ExecuteDataTable(
                    "select GCode,LCode,Description,Unit,Stock_QTY ,Safe_Qty ,USystem,Remark from MATERMST order by GCode");
                dataGridView1.DataSource = dtSource;

                lblTotal.Text = "筆數：" + dtSource.Rows.Count.ToString() + "筆";
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

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (txtGCode.Text.Trim().Length == 0)
            {
                Messages.Warning("請搜尋相關資料予以刪除！");
                return;
            }
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //if (int.Parse(
                //       dataAccess.ExecuteScalar("select count(*) from STOCKMST WHERE GCode =@GCode", new DbParameter[]
                //{
                //    DataAccess.CreateParameter("GCode", DbType.String, txtGCode.Text.Trim())
                //}).ToString()) > 0)
                if (int.Parse(
                      dataAccess.ExecuteScalar("select count(*) from STOCKMST WHERE GCode ='" + txtGCode.Text.Trim() + "'").ToString()) > 0)
                {
                    Messages.Warning("尚有庫存無法刪除!!");
                    return;
                }
                if (Messages.Question("是否確定刪除?") == DialogResult.No)
                {
                    return;
                }
                //dataAccess.ExecuteNonQuery("Delete from MATERMST where GCode=@GCode", new DbParameter[]
                //{
                //    DataAccess.CreateParameter("GCode", DbType.String, txtGCode.Text.Trim())
                //});
                dataAccess.ExecuteNonQuery("Delete from MATERMST where GCode='" + txtGCode.Text.Trim() + "'");
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                lblStyle.Text = "查  詢";
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                lblStyle.Text = "瀏  覽";
                ShowData();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (mode == "A")
                {
                    if (txtGCode.Text.Trim().Length == 0)
                    {
                        Messages.Warning("Global Code不允許空值，請輸入值!!");
                        txtGCode.Focus();
                        return;
                    }
                    if (txtLCode.Text.Trim().Length == 0)
                    {
                        Messages.Warning("Local Code不允許空值，請輸入值!!");
                        txtLCode.Focus();
                        return;
                    }
                    //if (int.Parse(
                    //           dataAccess.ExecuteScalar("select count(*) from MATERMST where GCode like @GCode and LCode like @LCode ",
                    //               new DbParameter[]
                    //{
                    //    DataAccess.CreateParameter("GCode", DbType.String, "%"+txtGCode.Text.Trim()+"%"),
                    //    DataAccess.CreateParameter("LCode", DbType.String, "%"+txtLCode.Text.Trim()+"%")
                    //}).ToString()) > 0)
                    if (int.Parse(
                             dataAccess.ExecuteScalar("select count(*) from MATERMST where GCode like '%" + txtGCode.Text.Trim() + "%' and LCode like '%" + txtLCode.Text.Trim() + "%' ").ToString()) > 0)
                    {
                        Messages.Warning("資料重複！！不允許新增！！");
                        txtGCode.Focus();
                        txtGCode.SelectAll();
                        return;
                    }
                }
                if (mode == "M")
                {
                    if (txtGCode.Text.Trim().Length == 0 || txtLCode.Text.Trim().Length == 0)
                    {
                        Messages.Warning("請搜尋相關資料予以修改");
                        return;
                    }
                }
                if (txtUnit.Text.Trim().Length > 0)
                {
                    try
                    {
                        decimal.Parse(txtUnit.Text.Trim());
                    }
                    catch
                    {
                        Messages.Warning("單位格式不正確！");
                        txtUnit.Focus();
                        txtUnit.SelectAll();
                        return;
                    }
                }
                if (txtSafe.Text.Trim().Length > 0)
                {
                    try
                    {
                        decimal.Parse(txtSafe.Text.Trim());
                    }
                    catch
                    {
                        Messages.Warning("安全量格式不正確！");
                        txtSafe.Focus();
                        txtSafe.SelectAll();
                        return;
                    }
                }

                //if (int.Parse(dataAccess.ExecuteScalar("select count(*) from MATERMST where GCode=@GCode ",
                //                      new DbParameter[]
                //        {
                //            DataAccess.CreateParameter("GCode", DbType.String, txtGCode.Text.Trim())
                //        }).ToString()) == 0)
                //{
                //    dataAccess.ExecuteNonQuery(
                //             "insert into MATERMST(GCode,Description,UNIT,LCode,SAFE_QTY,USYSTEM,REMARK,USER_ID,CREATE_DATE) values (@GCode,@Description,@UNIT,@LCode,@SAFE_QTY,@USYSTEM,@REMARK,@USER_ID,@CREATE_DATE)",
                //             new DbParameter[]
                //    {
                //        DataAccess.CreateParameter("GCode", DbType.String, txtGCode.Text.Trim()),
                //        DataAccess.CreateParameter("Description", DbType.String, txtDes.Text.Trim()),
                //        DataAccess.CreateParameter("UNIT", DbType.String, txtUnit.Text.Trim().Length==0?"0":txtUnit.Text.Trim()),
                //        DataAccess.CreateParameter("LCode", DbType.String, txtLCode.Text.Trim()),
                //        DataAccess.CreateParameter("SAFE_QTY", DbType.String, txtSafe.Text.Trim().Length==0?"0":txtSafe.Text.Trim()),
                //        DataAccess.CreateParameter("USYSTEM", DbType.String, txtUsys.Text.Trim()),
                //        DataAccess.CreateParameter("REMARK", DbType.String, txtRemark.Text.Trim()),
                //        DataAccess.CreateParameter("USER_ID", DbType.String,""),
                //        DataAccess.CreateParameter("CREATE_DATE", DbType.String,DateTime.Now.ToString("yyyyMMdd"))
                //    });
                //}
                //else
                //{
                //    dataAccess.ExecuteNonQuery(
                //                "update MATERMST set LCode=@LCode,Description=@Description,UNIT=@UNIT,SAFE_QTY=@SAFE_QTY,USYSTEM=@USYSTEM,USER_ID=@USER_ID,CREATE_DATE=@CREATE_DATE,REMARK=@REMARK where GCode=@GCode ",
                //                new DbParameter[]
                //    {
                //        DataAccess.CreateParameter("LCode", DbType.String, txtLCode.Text.Trim()),
                //        DataAccess.CreateParameter("Description", DbType.String, txtDes.Text.Trim()),
                //        DataAccess.CreateParameter("UNIT", DbType.String, txtUnit.Text.Trim().Length==0?"0":txtUnit.Text.Trim()),
                //        DataAccess.CreateParameter("SAFE_QTY", DbType.String, txtSafe.Text.Trim().Length==0?"0":txtSafe.Text.Trim()),
                //        DataAccess.CreateParameter("USYSTEM", DbType.String, txtUsys.Text.Trim()),
                //        DataAccess.CreateParameter("USER_ID", DbType.String, ""),
                //        DataAccess.CreateParameter("CREATE_DATE", DbType.String, DateTime.Now.ToString("yyyyMMdd")),
                //        DataAccess.CreateParameter("REMARK", DbType.String, txtRemark.Text.Trim()),
                //        DataAccess.CreateParameter("GCode", DbType.String, txtGCode.Text.Trim())
                //    });
                //}
                if (int.Parse(dataAccess.ExecuteScalar("select count(*) from MATERMST where GCode='" + txtGCode.Text.Trim() + "' ").ToString()) == 0)
                {
                    dataAccess.ExecuteNonQuery(
                             "insert into MATERMST(GCode,Description,UNIT,LCode,SAFE_QTY,USYSTEM,REMARK,USER_ID,CREATE_DATE) values ('" + txtGCode.Text.Trim() + "','" + txtDes.Text.Trim() + "','" + (txtUnit.Text.Trim().Length == 0 ? "0" : txtUnit.Text.Trim()) + "','" + txtLCode.Text.Trim() + "'," + (txtSafe.Text.Trim().Length == 0 ? "0" : txtSafe.Text.Trim()) + ",'" + txtUsys.Text.Trim() + "','" + txtRemark.Text.Trim() + "','','" + DateTime.Now.ToString("yyyyMMdd") + "')");
                }
                else
                {
                    dataAccess.ExecuteNonQuery(
                                "update MATERMST set LCode='" + txtLCode.Text.Trim() + "',Description='" + txtDes.Text.Trim() + "',UNIT='"+(txtUnit.Text.Trim().Length==0?"0":txtUnit.Text.Trim())+"',SAFE_QTY="+(txtSafe.Text.Trim().Length==0?"0":txtSafe.Text.Trim())+",USYSTEM='"+txtUsys.Text.Trim()+"',USER_ID='',CREATE_DATE='"+DateTime.Now.ToString("yyyyMMdd")+"',REMARK='"+ txtRemark.Text.Trim()+"' where GCode='"+txtGCode.Text.Trim()+"' ");
                }
                ExecuteSQL();
                SaveBtn();
                Messages.Asterisk("資  料  " + (mode == "A" ? "新  增" : "修   改") + "  成  功  ！");
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

        private void btnModify_Click(object sender, EventArgs e)
        {
            lblStyle.Text = "修  改";
            ModifyBtn();
            mode = "M";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            lblStyle.Text = "新  增";
            AddBtn();
            mode = "A";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    txtGCode.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtLCode.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtDes.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtUnit.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    txtStore.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    txtSafe.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    txtUsys.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    txtRemark.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
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

        private void ExecuteSQL()
        {
            string sql =
                "select GCode,LCode,Description,Unit,Stock_QTY ,Safe_Qty ,USystem,Remark from MATERMST where 1=1 ";
            if (txtGCode.Text.Trim().Length > 0)
            {
                //sql += " and  GCode LIKE @GCode ";
                sql += " and  GCode LIKE '%" + txtGCode.Text.Trim() + "%' ";
            }
            if (txtDes.Text.Trim().Length > 0)
            {
                //sql += " and Description like @Description ";
                sql += " and Description like '%" + txtDes.Text.Trim() + "%' ";
            }
            if (txtUsys.Text.Trim().Length > 0)
            {
                //sql += " and USystem like @USystem ";
                sql += " and USystem like '%" + txtUsys.Text.Trim() + "%' ";
            }
            if (txtLCode.Text.Trim().Length > 0)
            {
                //sql += " and LCode like @LCode ";
                sql += " and LCode like '%" + txtLCode.Text.Trim() + "%' ";
            }
            sql += " order by GCode ";

            //dtSource = dataAccess.ExecuteDataTable(sql, new DbParameter[]
            //{
            //    DataAccess.CreateParameter("GCode", DbType.String, "%" + txtGCode.Text.Trim() + "%"),
            //    DataAccess.CreateParameter("Description", DbType.String, "%" + txtDes.Text.Trim() + "%"),
            //    DataAccess.CreateParameter("USystem", DbType.String, "%" + txtUsys.Text.Trim() + "%"),
            //    DataAccess.CreateParameter("LCode", DbType.String, "%" + txtLCode.Text.Trim() + "%"),
            //});
            dtSource = dataAccess.ExecuteDataTable(sql);

            dataGridView1.DataSource = dtSource;

            lblTotal.Text = "筆數：" + dtSource.Rows.Count.ToString() + "筆";
        }

        private void FieldClear()
        {
            txtGCode.Text =
                txtDes.Text =
                    txtLCode.Text = txtUnit.Text = txtSafe.Text = txtStore.Text = txtUsys.Text = txtRemark.Text = "";
        }

        private void ShowData()
        {
            FieldClear();
            btnAdd.Enabled = true;
            btnModify.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = true;
            btnSearch.Enabled = true;
            btnDel.Enabled = false;
            btnReturn.Enabled = true;
            txtGCode.Enabled = true;
            txtLCode.Enabled = true;
        }

        private void AddBtn()
        {
            txtGCode.Enabled = txtLCode.Enabled = true;
            btnAdd.Enabled = false;
            btnModify.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            btnSearch.Enabled = true;
            btnDel.Enabled = true;
            btnReturn.Enabled = true;
            txtGCode.Focus();
        }

        private void ModifyBtn()
        {
            btnAdd.Enabled = false;
            btnModify.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            btnSearch.Enabled = true;
            btnDel.Enabled = true;
            btnReturn.Enabled = true;
            txtGCode.Enabled = false;
            txtLCode.Enabled = false;
            txtDes.Focus();
        }

        private void SaveBtn()
        {
            btnSave.Enabled = false;
            btnDel.Enabled = false;
        }
    }
}
