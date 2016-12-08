using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Sample.Modal;

namespace Sample
{
    public partial class MainForm : Form
    {
        private string mode = "";
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            panel3.Enabled = panel4.Enabled = panel6.Enabled = false;
            foreach (DataGridViewColumn dc in dataGridView1.Columns)
            {
                dc.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                panel3.Enabled = panel4.Enabled = panel6.Enabled = true;
                btnCheck.Enabled = btnCancelCheck.Enabled = btnAdd.Enabled = btnDel.Enabled = btnModify.Enabled = false;
                btnCancel.Enabled = btnSave.Enabled = true;

                txtCreator.Text = txtChecker.Text = txtOperator.Text = "1";
                txtCheckDate.Text = txtOrderDate.Text = txtLotDate.Text = DateTime.Now.ToString("yyyyMMdd");
                txtPostDate.Text = dateTimePicker1.Text;
                List<MMMDB> lst = dataGridView1.DataSource as List<MMMDB>;
                if (lst == null)
                {
                    lst = new List<MMMDB>();
                }
                MMMDB mmmdb = new MMMDB();
                lst.Add(mmmdb);
                dataGridView1.DataSource = lst;
                for (int i = 0; i < lst.Count - 1; i++)
                {
                    dataGridView1.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                }
                mode = "A";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (mode == "A")
                {
                    if (dataGridView1.Rows[0].Cells[0] == null)
                    {
                        MessageBox.Show("請選擇料號", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                            MessageBoxDefaultButton.Button1);
                        return;
                    }
                    if (txtOrderNo.Text.Length == 0)
                    {
                        MessageBox.Show("單據號碼不能為空", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                           MessageBoxDefaultButton.Button1);
                        btnOrderType.Focus();
                        return;
                    }
                    if (txtOrderDate.Text.Trim().Length > 0)
                    {
                        try
                        {
                            DateTime.ParseExact(txtOrderDate.Text.Trim(), "yyyyMMdd",
                                System.Globalization.CultureInfo.CurrentCulture);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("制單日期格式不正確", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
                            txtOrderDate.Focus();
                            return;
                        }
                    }
                    if (txtCheckDate.Text.Trim().Length > 0)
                    {
                        try
                        {
                            DateTime.ParseExact(txtCheckDate.Text.Trim(), "yyyyMMdd",
                                System.Globalization.CultureInfo.CurrentCulture);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("核准日期格式不正確", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
                            txtCheckDate.Focus();
                            return;
                        }
                    }
                    if (txtLotDate.Text.Trim().Length > 0)
                    {
                        try
                        {
                            DateTime.ParseExact(txtLotDate.Text.Trim(), "yyyyMMdd",
                                System.Globalization.CultureInfo.CurrentCulture);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("發票日期格式不正確", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
                            txtLotDate.Focus();
                            return;
                        }
                    }
                    MMDAA mmdaa = new MMDAA();
                    mmdaa.DAA001 = txtOrderType.Text.Trim();
                    mmdaa.DAA002 = txtOrderNo.Text.Trim();
                    mmdaa.DAA003 = txtPostDate.Text.Trim();
                    mmdaa.DAA004 = txtOrderDate.Text.Trim();
                    mmdaa.DAA005 = txtOperator.Text.Trim();
                    mmdaa.DAA006 = txtCreator.Text.Trim();
                    mmdaa.DAA007 = txtSupplier.Text.Trim();
                    mmdaa.DAA008 = lblSupplier.Text.Trim();
                    mmdaa.DAA009 = txtNo.Text.Trim();
                    mmdaa.DAA010 = txtLotNo.Text.Trim();
                    mmdaa.DAA011 = txtLotDate.Text.Trim();
                    mmdaa.DAA015 = decimal.Parse(txtPerSum.Text.Trim().Length == 0 ? "0" : txtPerSum.Text.Trim());
                    mmdaa.DAA016 = decimal.Parse(txtSum.Text.Trim().Length == 0 ? "0" : txtSum.Text.Trim());
                    mmdaa.DAA020 = "";
                    mmdaa.DAA030 = txtPlant.Text.Trim();
                    mmdaa.DAA041 = 0;
                    mmdaa.DAA042 = 0;
                    mmdaa.DAA800 = null;
                    mmdaa.DAA801 = null;
                    mmdaa.DAA851 =
                    mmdaa.DAA852 = mmdaa.DAA854 = mmdaa.DAA855 = mmdaa.DAA857 = mmdaa.DAA858 = mmdaa.DAA992 = null;
                    mmdaa.DAA993 = mmdaa.DAA994 = 0;
                    mmdaa.DAA995 = mmdaa.DAA996 = null;
                    mmdaa.DAA997 = txtCheckDate.Text.Trim();
                    mmdaa.DAA998 = txtChecker.Text.Trim();
                    mmdaa.DAA999 = "N";
                    mmdaa.CreatedDate = DateTime.Now;
                    mmdaa.Creator = "";

                    List<MMMDB> lst = dataGridView1.DataSource as List<MMMDB>;
                    if (lst != null)
                    {
                        lst.RemoveAt(lst.Count - 1);
                    }
                    List<MMDAB> items = new List<MMDAB>();
                    MMDAB mmdab = null;
                    int i = 1;
                    foreach (MMMDB mmmdb in lst)
                    {
                        mmdab = new MMDAB();
                        mmdab.DAB001 = txtOrderType.Text.Trim();
                        mmdab.DAB002 = txtOrderNo.Text.Trim();
                        mmdab.DAB003 = (i++).ToString("0000");
                        mmdab.DAB004 = mmmdb.MDB001;
                        mmdab.DAB005 = mmmdb.MDB002;
                        mmdab.DAB006 = mmmdb.MDB004;
                        mmdab.DAB007 = txtPlant.Text.Trim();
                        mmdab.DAB025 = 0;
                        mmdab.DAB026 = 0;
                        mmdab.DAB993 = 0;
                        mmdab.DAB994 = 0;
                        mmdab.DAB997 = txtCheckDate.Text.Trim();
                        mmdab.DAB998 = txtChecker.Text.Trim();
                        mmdab.DAB999 = "N";
                        mmdab.CreatedDate = DateTime.Now;
                        items.Add(mmdab);
                    }
                    mmdaa.items = items;
                    PostMmdaa(mmdaa);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
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
                txtOrderType.Text =
                    lblOrderType.Text =
                        txtOrderDate.Text =
                            txtCheckDate.Text =
                                txtChecker.Text =
                                    txtOrderNo.Text =
                                        txtCreator.Text =
                                            txtPlant.Text =
                                                lblPlant.Text =
                                                    txtOrderNo.Text =
                                                        txtSupplier.Text =
                                                            lblSupplier.Text =
                                                                txtLotNo.Text = txtOperator.Text = txtLotDate.Text =txtNo.Text= "";
                txtSum.Text = txtPerSum.Text = "0";
                txtPostDate.Text = "";
                dataGridView1.DataSource = null;
                panel3.Enabled = panel4.Enabled = panel6.Enabled = false;
                btnCheck.Enabled = btnCancelCheck.Enabled = btnAdd.Enabled = btnDel.Enabled = btnModify.Enabled = true;
                btnCancel.Enabled = btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {

        }

        private void btnCheck_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelCheck_Click(object sender, EventArgs e)
        {

        }

        private void btnOrderType_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                frmOrderType frmOrderType = new frmOrderType();
                if (frmOrderType.ShowDialog() == DialogResult.OK)
                {
                    txtOrderType.Text = frmOrderType.returnValue;
                    lblOrderType.Text = frmOrderType.returnName;
                    GetOrderNumber(txtOrderType.Text.Trim());
                }
                frmOrderType.Close();
                frmOrderType.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnPlantType_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                frmPlantType frmPlantType = new frmPlantType();
                if (frmPlantType.ShowDialog() == DialogResult.OK)
                {
                    txtPlant.Text = frmPlantType.returnValue;
                    lblPlant.Text = frmPlantType.returnName;
                }
                frmPlantType.Close();
                frmPlantType.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                frmSupplier frmSupplier = new frmSupplier();
                if (frmSupplier.ShowDialog() == DialogResult.OK)
                {
                    txtSupplier.Text = frmSupplier.returnValue;
                    lblSupplier.Text = frmSupplier.returnName;
                }
                frmSupplier.Close();
                frmSupplier.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnOperation_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                frmOperator frmOperator = new frmOperator();
                frmOperator.ShowDialog();
                frmOperator.Close();
                frmOperator.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (e.RowIndex > -1 && e.RowIndex == dataGridView1.RowCount - 1 && dataGridView1.Rows[e.RowIndex].Cells[0].Value == null)
                {
                    frmMaterial frmMaterial = new frmMaterial();
                    if (frmMaterial.ShowDialog() == DialogResult.OK)
                    {
                        MMMDB mmmdb = frmMaterial.returnValue;
                        List<MMMDB> lst = dataGridView1.DataSource as List<MMMDB>;
                        if (lst != null)
                        {
                            lst.RemoveAt(lst.Count - 1);
                        }
                        bool isExist = false;
                        decimal sum = 0;
                        foreach (MMMDB dr in lst)
                        {
                            if (dr.MDB001 == mmmdb.MDB001)
                            {
                                isExist = true;
                            }
                            sum += dr.MDB035 == null ? 0 : decimal.Parse(dr.MDB035.ToString());
                        }
                        if (!isExist)
                        {
                            lst.Add(mmmdb);
                            sum += mmmdb.MDB035 == null ? 0 : decimal.Parse(mmmdb.MDB035.ToString());
                        }
                        txtPerSum.Text = lst.Count.ToString();
                        txtSum.Text = sum.ToString();
                        lst.Add(new MMMDB());
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = lst;
                        for (int i = 0; i < lst.Count - 1; i++)
                        {
                            dataGridView1.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                        }
                    }
                    frmMaterial.Close();
                    frmMaterial.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            txtPostDate.Text = dateTimePicker1.Text;
        }

        private async void GetOrderNumber(string orderType)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("system_id", "WMSSZ");
            HttpResponseMessage response = await client.GetAsync(Helper.url + "documents/" + orderType + "/number");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                txtOrderNo.Text = responseBody.Replace("\"", "");
            }
            else
            {
                MessageBox.Show("Exception");
            }
        }

        private async void PostMmdaa(MMDAA mmdaa)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("system_id", "WMSSZ");
            string sendBody = JsonConvert.SerializeObject(mmdaa);
            using (HttpContent httpContent = new StringContent(sendBody))
            {
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await client.PostAsync("http://localhost/wmsapi/transactions/mme", httpContent);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    mode = "";
                    btnCancel_Click(null, null);
                    MessageBox.Show("保存成功", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk,
                        MessageBoxDefaultButton.Button1);
                }
                else
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Exception");
                }
            }
        }
    }
}
