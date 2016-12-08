using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Sample.Modal;

namespace Sample
{
    public partial class frmOrderType : Form
    {
        private string sendUrl = Helper.url + "documents";
        public string returnValue;
        public string returnName;
        public frmOrderType()
        {
            InitializeComponent();
        }

        private void frmOrderType_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                dataGridView1.AutoGenerateColumns = false;
                cmbCondition.SelectedIndex = 0;
                cmbType.SelectedIndex = 0;
                GetAllDocuments(sendUrl);
                cmbCondition.Focus();
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
            DialogResult=DialogResult.Cancel;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("請選擇單別", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
                    return;
                }
                returnValue = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
                returnName = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void txtCondition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    string urlFinally = sendUrl;
                    if (!string.IsNullOrEmpty(txtCondition.Text.Trim()))
                    {
                        if (cmbCondition.SelectedIndex == 0 && cmbType.SelectedIndex == 0)
                        {
                            urlFinally += "/" + txtCondition.Text.Trim();
                        }
                    }
                    GetAllDocuments(urlFinally);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "異常", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnConfirm_Click(null, null);
        }

        private async void GetAllDocuments(string urlFinally)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("system_id", "WMSSZ");
            HttpResponseMessage response = await client.GetAsync(urlFinally);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                if (!responseBody.StartsWith("["))
                {
                    responseBody = "[" + responseBody + "]";
                }
                dataGridView1.DataSource = JsonConvert.DeserializeObject<List<CMMDX>>(responseBody);
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                responseBody = responseBody.Replace("\\", "").Replace("\"error\",", "").Trim(new char[] { '"' });
                MessageBox.Show(JsonConvert.DeserializeObject<List<Error>>(responseBody)[0].message);
                dataGridView1.DataSource = null;
            }
            else
            {
                MessageBox.Show("Exception");
                dataGridView1.DataSource = null;
            }
        }
    }
}
