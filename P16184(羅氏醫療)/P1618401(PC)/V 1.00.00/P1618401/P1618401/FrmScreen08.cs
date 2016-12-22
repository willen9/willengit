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
    public partial class FrmScreen08 : Form
    {
        private DataAccess dataAccess = null;
        private DataTable dtSource = new DataTable();
        public FrmScreen08()
        {
            InitializeComponent();
        }

        private void FrmScreen08_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                btnModify.Enabled = true;
                btnSave.Enabled = false;
                btnCancel.Enabled = true;
                btnSearch.Enabled = true;
                btnDel.Enabled = false;
                btnReturn.Enabled = true;
                lblCusNo.Visible = false;
                txtCusNo.Visible = false;

                dataAccess = new DataAccess();
                dataAccess.ConnectionString = Share.conStr;
                dataAccess.ProviderName = Share.provide;

                ExecuteSQL();
               
                lblTotal.Text = "筆數：" + dtSource.Rows.Count + " 筆";
                if (dtSource.Rows.Count > 0)
                {
                    btnModify.Enabled = true;
                }
                dataGridView1.DataSource = dtSource;
                lblStyle.Text = "查詢模式";
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
            txtGlobalEnd.Visible = false;
            txtDateEnd.Visible = false;
            txtLocalEnd.Visible = false;
            lblCusNo.Visible = txtCusNo.Visible = true;

            txtGlobalStart.Enabled = false;
            txtDateStart.Enabled = false;
            label2.Visible = label4.Visible = label6.Visible = false;

            btnModify.Enabled = false;
            btnSave.Enabled = true;
            btnDel.Enabled = true;
            btnSearch.Enabled = false;
            btnImport.Enabled = false;
            lblStyle.Text = "編輯模式";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (txtLocalStart.Text.Trim().Length > 0)
                {
                    if (dtSource.Rows.Count == 0)
                    {
                        Messages.Warning("無資料不可更新");
                        return;
                    }
                    else
                    {
                        //dataAccess.ExecuteNonQuery("update ERRDATA set Location=@Location,Custno=@Custno where STATUS=@STATUS and GCODE=@GCODE and Location=@Loc", new DbParameter[]
                        //{
                        //    DataAccess.CreateParameter("Location",DbType.String,txtLocalStart.Text.Trim()),
                        //    DataAccess.CreateParameter("Custno",DbType.String,txtCusNo.Text.Trim()),
                        //    DataAccess.CreateParameter("STATUS",DbType.String,dataGridView1.CurrentRow.Cells[4].Value.ToString()),
                        //    DataAccess.CreateParameter("GCODE",DbType.String,dataGridView1.CurrentRow.Cells[0].Value.ToString()),
                        //    DataAccess.CreateParameter("Loc",DbType.String,dataGridView1.CurrentRow.Cells[1].Value.ToString())
                        //});
                        dataAccess.ExecuteNonQuery("update ERRDATA set Location='" + txtLocalStart.Text.Trim() + "',Custno='" + txtCusNo.Text.Trim() + "' where STATUS='" + dataGridView1.CurrentRow.Cells[4].Value.ToString() + "' and GCODE='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "' and Location='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "'");
                        ExecuteSQL();
                        dataGridView1.DataSource = dtSource;
                    }
                }


                txtGlobalEnd.Visible = true;
                txtDateEnd.Visible = true;
                txtLocalEnd.Visible = true;
                lblCusNo.Visible = false;
                txtCusNo.Visible = false;

                txtGlobalStart.Enabled = true;
                txtDateStart.Enabled = true;
                label2.Visible = label4.Visible = label6.Visible = true;

                FieldClear();
                btnModify.Enabled = true;
                btnSave.Enabled = false;
                btnDel.Enabled = false;
                btnSearch.Enabled = true;
                btnImport.Enabled = true;

                lblStyle.Text = "查詢模式";
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
            txtGlobalEnd.Visible = true;
            txtDateEnd.Visible = true;
            txtLocalEnd.Visible = true;
            lblCusNo.Visible = false;
            txtCusNo.Visible = false;

            txtGlobalStart.Enabled = true;
            txtDateStart.Enabled = true;

            label2.Visible = label4.Visible = label6.Visible = true;

            FieldClear();
            dtSource.Clear();
            dataGridView1.DataSource = dtSource;
            btnModify.Enabled = true;
            btnSave.Enabled = false;
            btnDel.Enabled = false;
            btnSearch.Enabled = true;
            btnImport.Enabled = true;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ExecuteSQL();

                lblTotal.Text = "筆數：" + dtSource.Rows.Count + " 筆";
                if (dtSource.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dtSource;
                    btnModify.Enabled = true;
                }
                lblStyle.Text = "查詢模式";
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
                lblTotal.Text = "筆數：" + dtSource.Rows.Count + " 筆";
                if (dtSource.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dtSource;
                    btnModify.Enabled = true;
                }
                btnSearch.Enabled = false;
                btnImport.Enabled = false;
                lblStyle.Text = "查詢模式";
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

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                txtDateStart.Text = txtDateEnd.Text = "";
                rabAll.Checked = true;
                ExecuteSQL();

                lblTotal.Text = "筆數：" + dtSource.Rows.Count + " 筆";
                if (dtSource.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dtSource;
                }
                if (Messages.Question("將異常資料將再次轉入!!") == DialogResult.Yes)
                {
                    Data_Sort();
                }
                btnCancel_Click(null, null);
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

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (txtGlobalStart.Text.Trim().Length > 0)
            {
                Messages.Warning("請搜尋相關資料予以刪除！");
                return;
            }
            if (Messages.Question("是否確定刪除!!") == DialogResult.No)
            {
                return;
            }
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //dataAccess.ExecuteNonQuery(
                //     "Delete from ERRDATA where GCode=@GCode and TRANS_DATE =@TRANS_DATE and Location=@Location and Custno=@Custno ",
                //     new DbParameter[]
                //{
                //    DataAccess.CreateParameter("GCode", DbType.String, txtGlobalStart.Text.Trim()),
                //    DataAccess.CreateParameter("TRANS_DATE", DbType.String, txtDateStart.Text.Trim()),
                //    DataAccess.CreateParameter("Location", DbType.String, txtLocalStart.Text.Trim()),
                //    DataAccess.CreateParameter("Custno", DbType.String, txtCusNo.Text.Trim())
                //});
                dataAccess.ExecuteNonQuery(
                    "Delete from ERRDATA where GCode='" + txtGlobalStart.Text.Trim() + "' and TRANS_DATE ='" + txtDateStart.Text.Trim() + "' and Location='" + txtLocalStart.Text.Trim() + "' and Custno='" + txtCusNo.Text.Trim() + "' ");
                FieldClear();
                ExecuteSQL();
                dataGridView1.DataSource = dtSource;
                txtGlobalEnd.Visible = true;
                txtDateEnd.Visible = true;
                txtLocalEnd.Visible = true;
                lblCusNo.Visible = false;
                txtCusNo.Visible = false;

                txtGlobalStart.Enabled = true;
                txtDateStart.Enabled = true;

                label2.Visible = label4.Visible = label6.Visible = true;

                FieldClear();
                btnModify.Enabled = true;
                btnSave.Enabled = false;
                btnDel.Enabled = false;
                btnSearch.Enabled = true;
                btnImport.Enabled = true;
                lblStyle.Text = "查詢模式";
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
                    txtGlobalEnd.Visible = false;
                    txtDateEnd.Visible = false;
                    txtLocalEnd.Visible = false;
                    lblCusNo.Visible = txtCusNo.Visible = true;
                    txtGlobalStart.Enabled = txtDateStart.Enabled = false;
                    label2.Visible = label4.Visible = label6.Visible = false;

                    GridToField(e.RowIndex);
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

        private void FieldClear()
        {
            txtGlobalStart.Text =
                txtGlobalEnd.Text =
                    txtDateStart.Text = txtDateEnd.Text = txtLocalStart.Text = txtLocalEnd.Text = txtCusNo.Text = "";
        }

        private void ExecuteSQL()
        {
            if (txtGlobalEnd.Text.Trim().Length == 0)
            {
                txtGlobalEnd.Text = txtGlobalStart.Text;
            }
            if(txtDateEnd.Text.Trim().Length==0)
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
            //string sql = "SELECT * FROM ERRDATA AS ERR WHERE  STATUS LIKE @STATUS ";
            //if (txtGlobalStart.Text.Trim().Length > 0)
            //{
            //    sql += " and GCODE >=@GCODEStart ";
            //}
            //if (txtGlobalEnd.Text.Trim().Length > 0)
            //{
            //    sql += " and GCODE <=@GCODEEnd ";
            //}
            //if (txtDateStart.Text.Trim().Length > 0)
            //{
            //    sql += " and TRANS_DATE >=@TRANS_DATEStart ";
            //}
            //if (txtDateEnd.Text.Trim().Length > 0)
            //{
            //    sql += " and TRANS_DATE <=@TRANS_DATEEnd ";
            //}
            //if (txtLocalStart.Text.Trim().Length > 0)
            //{
            //    sql += " And Location >= @LocationStart ";
            //}
            //if (txtLocalEnd.Text.Trim().Length > 0)
            //{
            //    sql += " And Location <= @LocationEnd ";
            //}
            //dtSource=dataAccess.ExecuteDataTable(sql, new DbParameter[]
            //{
            //    DataAccess.CreateParameter("STATUS", DbType.String, status + "%"),
            //    DataAccess.CreateParameter("GCODEStart", DbType.String, txtGlobalStart.Text.Trim()),
            //    DataAccess.CreateParameter("GCODEEnd", DbType.String, txtGlobalEnd.Text.Trim()),
            //    DataAccess.CreateParameter("TRANS_DATEStart", DbType.String, txtDateStart.Text.Trim()),
            //    DataAccess.CreateParameter("TRANS_DATEEnd", DbType.String, txtDateEnd.Text.Trim()),
            //    DataAccess.CreateParameter("LocationStart", DbType.String, txtLocalStart.Text.Trim()),
            //    DataAccess.CreateParameter("LocationEnd", DbType.String, txtLocalEnd.Text.Trim())
            //});
            string sql = "SELECT * FROM ERRDATA AS ERR WHERE  STATUS LIKE '"+status+"%' ";
            if (txtGlobalStart.Text.Trim().Length > 0)
            {
                sql += " and GCODE >='" + txtGlobalStart.Text.Trim() + "' ";
            }
            if (txtGlobalEnd.Text.Trim().Length > 0)
            {
                sql += " and GCODE <='" + txtGlobalEnd.Text.Trim() + "' ";
            }
            if (txtDateStart.Text.Trim().Length > 0)
            {
                sql += " and TRANS_DATE >='" + txtDateStart.Text.Trim() + "' ";
            }
            if (txtDateEnd.Text.Trim().Length > 0)
            {
                sql += " and TRANS_DATE <='" + txtDateEnd.Text.Trim() + "' ";
            }
            if (txtLocalStart.Text.Trim().Length > 0)
            {
                sql += " And Location >= '" + txtLocalStart.Text.Trim() + "' ";
            }
            if (txtLocalEnd.Text.Trim().Length > 0)
            {
                sql += " And Location <= '" + txtLocalEnd.Text.Trim() + "' ";
            }
            dtSource = dataAccess.ExecuteDataTable(sql);
        }

        private void Data_Sort()
        {
            DbTransaction tran = dataAccess.CreateDbTransaction();
            try
            {
                string sql = " SELECT *  FROM ERRDATA WHERE  STATUS LIKE '%' ";
                DataTable dtTemp = dataAccess.ExecuteDataTable(sql);
                foreach (DataRow dr in dtTemp.Rows)
                {
                    //switch (dr[4].ToString())
                    //{
                    //    case "進貨":
                    //        if (dataAccess.ExecuteNonQuery(
                    //            "update STOCKMST Set IN_QTY=IN_QTY+@IN_QTY,REAL_QTY=LAST_QTY + (IN_QTY+ @IN_QTY) - OUT_QTY  where gcode=@gcode AND LOCATION=@LOCATION",
                    //            tran, new DbParameter[]
                    //            {
                    //                DataAccess.CreateParameter("IN_QTY", DbType.String, dr[2].ToString()),
                    //                DataAccess.CreateParameter("gcode", DbType.String, dr[0].ToString()),
                    //                DataAccess.CreateParameter("LOCATION", DbType.String, dr[1].ToString())
                    //            }) == 0)
                    //        {
                    //            dataAccess.ExecuteNonQuery(
                    //                "INSERT INTO STOCKMST(GCODE,LOCATION,IN_QTY,CREATE_DATE,USER_ID) VALUES (@GCODE,@LOCATION,@IN_QTY,@CREATE_DATE,@USER_ID)",
                    //                tran, new DbParameter[]
                    //                {
                    //                    DataAccess.CreateParameter("GCODE", DbType.String, dr[0].ToString()),
                    //                    DataAccess.CreateParameter("LOCATION", DbType.String, dr[1].ToString()),
                    //                    DataAccess.CreateParameter("IN_QTY", DbType.String, dr[2].ToString()),
                    //                    DataAccess.CreateParameter("CREATE_DATE", DbType.String, dr[3].ToString()),
                    //                    DataAccess.CreateParameter("USER_ID", DbType.String, dr[5].ToString())
                    //                });
                    //        }
                    //        dataAccess.ExecuteNonQuery(
                    //            "Update MATERMST Set Stock_QTY=Stock_QTY+ @Stock_QTY Where gcode=@gcode", tran,
                    //            new DbParameter[]
                    //            {
                    //                DataAccess.CreateParameter("Stock_QTY", DbType.String, dr[2].ToString()),
                    //                DataAccess.CreateParameter("gcode", DbType.String, dr[0].ToString())
                    //            });
                    //        dataAccess.ExecuteNonQuery(
                    //            "delete from ERRDATA where STATUS='進貨' and gcode=@gcode AND LOCATION=@LOCATION", tran,
                    //            new DbParameter[]
                    //            {
                    //                DataAccess.CreateParameter("gcode", DbType.String, dr[0].ToString()),
                    //                DataAccess.CreateParameter("LOCATION", DbType.String, dr[1].ToString())
                    //            });
                    //        break;
                    //    case "出貨":
                    //        if (dataAccess.ExecuteNonQuery(
                    //            "Update STOCKMST Set OUT_QTY = OUT_QTY+@OUT_QTY, REAL_QTY=LAST_QTY + IN_QTY - (OUT_QTY + @OUT_QTY) where GCODE=@GCODE  and LOCATION = @LOCATION ",
                    //            tran, new DbParameter[]
                    //            {
                    //                DataAccess.CreateParameter("OUT_QTY", DbType.String, dr[2].ToString()),
                    //                DataAccess.CreateParameter("GCODE", DbType.String, dr[0].ToString()),
                    //                DataAccess.CreateParameter("LOCATION", DbType.String, dr[1].ToString()),
                    //            }) > 0)
                    //        {
                    //            dataAccess.ExecuteNonQuery(
                    //                "Update MATERMST Set Stock_QTY=Stock_QTY- @Stock_QTY Where gcode=@gcode", tran,
                    //                new DbParameter[]
                    //                {
                    //                    DataAccess.CreateParameter("Stock_QTY", DbType.String, dr[2].ToString()),
                    //                    DataAccess.CreateParameter("gcode", DbType.String, dr[0].ToString())
                    //                });
                    //            dataAccess.ExecuteNonQuery(
                    //                "delete from ERRDATA where STATUS='出貨' and gcode=@gcode AND LOCATION=@LOCATION",
                    //                tran,
                    //                new DbParameter[]
                    //                {
                    //                    DataAccess.CreateParameter("gcode", DbType.String, dr[0].ToString()),
                    //                    DataAccess.CreateParameter("LOCATION", DbType.String, dr[1].ToString())
                    //                });
                    //        }
                    //        break;
                    //    case "退貨":
                    //        if (dataAccess.ExecuteNonQuery(
                    //            "Update STOCKMST Set OUT_QTY=OUT_QTY -@OUT_QTY , REAL_QTY=LAST_QTY + IN_QTY - (OUT_QTY - @OUT_QTY)  where gcode=@gcode AND LOCATION=@LOCATION ",
                    //            tran, new DbParameter[]
                    //            {
                    //                DataAccess.CreateParameter("OUT_QTY", DbType.String, dr[2].ToString()),
                    //                DataAccess.CreateParameter("gcode", DbType.String, dr[0].ToString()),
                    //                DataAccess.CreateParameter("LOCATION", DbType.String, dr[1].ToString())
                    //            }) > 0)
                    //        {
                    //            dataAccess.ExecuteNonQuery(
                    //                "Update MATERMST Set Stock_QTY=Stock_QTY+ @Stock_QTY Where gcode=@gcode", tran,
                    //                new DbParameter[]
                    //                {
                    //                    DataAccess.CreateParameter("Stock_QTY", DbType.String, dr[2].ToString()),
                    //                    DataAccess.CreateParameter("gcode", DbType.String, dr[0].ToString())
                    //                });
                    //            dataAccess.ExecuteNonQuery(
                    //                "delete from ERRDATA where STATUS='退貨' and gcode=@gcode AND LOCATION=@LOCATION",
                    //                tran,
                    //                new DbParameter[]
                    //                {
                    //                    DataAccess.CreateParameter("gcode", DbType.String, dr[0].ToString()),
                    //                    DataAccess.CreateParameter("LOCATION", DbType.String, dr[1].ToString())
                    //                });
                    //        }
                    //        break;
                    //}
                    switch (dr[4].ToString())
                    {
                        case "進貨":
                            if (dataAccess.ExecuteNonQuery(
                                "update STOCKMST Set IN_QTY=IN_QTY+" + dr[2] + ",REAL_QTY=LAST_QTY + (IN_QTY+ " + dr[2] + ") - OUT_QTY  where gcode='" + dr[0] + "' AND LOCATION='" + dr[1] + "'",
                                tran) == 0)
                            {
                                dataAccess.ExecuteNonQuery(
                                    "INSERT INTO STOCKMST(GCODE,LOCATION,IN_QTY,CREATE_DATE,USER_ID) VALUES ('"+dr[0]+"','"+dr[1]+"',"+dr[2]+",'"+ dr[3]+"','"+dr[5]+"')",tran);
                            }
                            dataAccess.ExecuteNonQuery(
                                "Update MATERMST Set Stock_QTY=Stock_QTY+" + dr[2] + " Where gcode='"+dr[0]+"'", tran);
                            dataAccess.ExecuteNonQuery(
                                "delete from ERRDATA where STATUS='進貨' and gcode='" + dr[0] + "' AND LOCATION='" + dr[1] + "'", tran);
                            break;
                        case "出貨":
                            if (dataAccess.ExecuteNonQuery(
                                "Update STOCKMST Set OUT_QTY = OUT_QTY+" + dr[2] + ", REAL_QTY=LAST_QTY + IN_QTY - (OUT_QTY + " + dr[2] + ") where GCODE='" + dr[0] + "'  and LOCATION = '" + dr[1] + "' ",tran) > 0)
                            {
                                dataAccess.ExecuteNonQuery("Update MATERMST Set Stock_QTY=Stock_QTY- " + dr[2] + " Where gcode='" + dr[0] + "'", tran);
                                dataAccess.ExecuteNonQuery("delete from ERRDATA where STATUS='進貨' and gcode='" + dr[0] + "' AND LOCATION='" + dr[1] + "'", tran);
                            }
                            break;
                        case "退貨":
                            if (dataAccess.ExecuteNonQuery(
                                "Update STOCKMST Set OUT_QTY=OUT_QTY -" + dr[2] + " , REAL_QTY=LAST_QTY + IN_QTY - (OUT_QTY - " + dr[2] + ")  where gcode='" + dr[0] + "' AND LOCATION='" + dr[1] + "' ",tran) > 0)
                            {
                                dataAccess.ExecuteNonQuery(
                                    "Update MATERMST Set Stock_QTY=Stock_QTY+ " + dr[2] + " Where gcode='" + dr[0] + "' ", tran);
                                dataAccess.ExecuteNonQuery(
                                    "delete from ERRDATA where STATUS='退貨' and gcode='" + dr[0] + "' AND LOCATION='" + dr[1] + "'",tran);
                            }
                            break;
                    }
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
            finally
            {
                tran.Dispose();
            }
        }

        private void GridToField(int index)
        {
            FieldClear();
            txtGlobalStart.Text = dataGridView1.Rows[index].Cells["GCode"].Value.ToString();
            txtDateStart.Text = dataGridView1.Rows[index].Cells["TRANS_DATE"].Value.ToString();
            txtLocalStart.Text = dataGridView1.Rows[index].Cells["Location"].Value.ToString();
            txtCusNo.Text = dataGridView1.Rows[index].Cells["Custno"].Value.ToString();
            btnModify.Enabled = true;
            btnSave.Enabled = false;
            btnDel.Enabled = false;
            btnSearch.Enabled = false;
            btnImport.Enabled = false;
        }
    }
}
