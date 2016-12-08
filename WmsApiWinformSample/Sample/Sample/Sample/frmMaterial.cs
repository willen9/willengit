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
    public partial class frmMaterial : Form
    {
        private string sendUrl = Helper.url + "materials";
        public MMMDB  returnValue;

        public frmMaterial()
        {
            InitializeComponent();
        }

        private void frmOrderType_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                foreach (DataGridViewColumn dc in dataGridView1.Columns)
                {
                    dc.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }
                cmbCondition.SelectedIndex = 0;
                cmbType.SelectedIndex = 0;
                GetAllMaterials(sendUrl);
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
                    MessageBox.Show("請選擇料號", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
                    return;
                }
                DataGridViewRow dr = dataGridView1.Rows[dataGridView1.CurrentRow.Index];
                MMMDB mmmdb = new MMMDB();
                mmmdb.MDB001 = dr.Cells[0].Value == null ? null : dr.Cells[0].Value.ToString();
                mmmdb.MDB002 = dr.Cells[1].Value == null ? null : dr.Cells[1].Value.ToString();
                mmmdb.MDB003 = dr.Cells[2].Value == null ? null : dr.Cells[2].Value.ToString();
                mmmdb.MDB004 = dr.Cells[3].Value == null ? null : dr.Cells[3].Value.ToString();
                mmmdb.MDB005 = dr.Cells[4].Value == null ? null : dr.Cells[4].Value.ToString();
                mmmdb.MDB006 = dr.Cells[5].Value == null ? null : dr.Cells[5].Value.ToString();
                mmmdb.MDB007 = dr.Cells[6].Value == null ? null : dr.Cells[6].Value.ToString();
                mmmdb.MDB008 = dr.Cells[7].Value == null ? null : dr.Cells[7].Value.ToString();
                mmmdb.MDB009 = dr.Cells[8].Value == null ? null : dr.Cells[8].Value.ToString();
                mmmdb.MDB010 = dr.Cells[9].Value == null ? null : dr.Cells[9].Value.ToString();
                mmmdb.MDB011 = dr.Cells[10].Value == null ? null : dr.Cells[10].Value.ToString();
                if (dr.Cells[11].Value == null || dr.Cells[11].Value.ToString().Length == 0)
                {
                    mmmdb.MDB012 = null;
                }
                else
                {
                    mmmdb.MDB012 = decimal.Parse(dr.Cells[11].Value.ToString());
                }
                mmmdb.MDB013 = dr.Cells[12].Value == null ? null : dr.Cells[12].Value.ToString();
                mmmdb.MDB014 = dr.Cells[13].Value == null ? null : dr.Cells[13].Value.ToString();
                mmmdb.MDB015 = dr.Cells[14].Value == null ? null : dr.Cells[14].Value.ToString();
                mmmdb.MDB016 = dr.Cells[15].Value == null ? null : dr.Cells[15].Value.ToString();
                mmmdb.MDB017 = dr.Cells[16].Value == null ? null : dr.Cells[16].Value.ToString();
                mmmdb.MDB018 = dr.Cells[17].Value == null ? null : dr.Cells[17].Value.ToString();
                mmmdb.MDB019 = dr.Cells[18].Value == null ? null : dr.Cells[18].Value.ToString();
                mmmdb.MDB020 = dr.Cells[19].Value == null ? null : dr.Cells[19].Value.ToString();
                mmmdb.MDB021 = dr.Cells[20].Value == null ? null : dr.Cells[20].Value.ToString();
                mmmdb.MDB022 = dr.Cells[21].Value == null ? null : dr.Cells[21].Value.ToString();
                mmmdb.MDB023 = dr.Cells[22].Value == null ? null : dr.Cells[22].Value.ToString();
                if (dr.Cells[23].Value == null || dr.Cells[23].Value.ToString().Length == 0)
                {
                    mmmdb.MDB024 = null;
                }
                else
                {
                    mmmdb.MDB024 = decimal.Parse(dr.Cells[23].Value.ToString());
                }
                if (dr.Cells[24].Value == null || dr.Cells[24].Value.ToString().Length == 0)
                {
                    mmmdb.MDB025 = null;
                }
                else
                {
                    mmmdb.MDB025 = decimal.Parse(dr.Cells[24].Value.ToString());
                }
                if (dr.Cells[25].Value == null || dr.Cells[25].Value.ToString().Length == 0)
                {
                    mmmdb.MDB026 = null;
                }
                else
                {
                    mmmdb.MDB026 = decimal.Parse(dr.Cells[25].Value.ToString());
                }
                mmmdb.MDB027 = dr.Cells[26].Value == null ? null : dr.Cells[26].Value.ToString();
                mmmdb.MDB028 = dr.Cells[27].Value == null ? null : dr.Cells[27].Value.ToString();
                mmmdb.MDB029 = dr.Cells[28].Value == null ? null : dr.Cells[28].Value.ToString();
                if (dr.Cells[29].Value == null || dr.Cells[29].Value.ToString().Length == 0)
                {
                    mmmdb.MDB030 = null;
                }
                else
                {
                    mmmdb.MDB030 = decimal.Parse(dr.Cells[29].Value.ToString());
                }
                mmmdb.MDB031 = dr.Cells[30].Value == null ? null : dr.Cells[30].Value.ToString();
                if (dr.Cells[31].Value == null || dr.Cells[31].Value.ToString().Length == 0)
                {
                    mmmdb.MDB035 = null;
                }
                else
                {
                    mmmdb.MDB035 = decimal.Parse(dr.Cells[31].Value.ToString());
                }
                if (dr.Cells[32].Value == null || dr.Cells[32].Value.ToString().Length == 0)
                {
                    mmmdb.MDB036 = null;
                }
                else
                {
                    mmmdb.MDB036 = decimal.Parse(dr.Cells[32].Value.ToString());
                }
                mmmdb.MDB040 = dr.Cells[33].Value == null ? null : dr.Cells[33].Value.ToString();
                mmmdb.MDB041 = int.Parse(dr.Cells[34].Value.ToString());
                mmmdb.MDB042 = int.Parse(dr.Cells[35].Value.ToString());
                mmmdb.MDB043 = int.Parse(dr.Cells[36].Value.ToString());
                mmmdb.MDB044 = int.Parse(dr.Cells[37].Value.ToString());
                mmmdb.MDB051 = dr.Cells[38].Value == null ? null : dr.Cells[38].Value.ToString();
                mmmdb.MDB052 = dr.Cells[39].Value == null ? null : dr.Cells[39].Value.ToString();
                mmmdb.MDB053 = dr.Cells[40].Value == null ? null : dr.Cells[40].Value.ToString();
                mmmdb.MDB054 = dr.Cells[41].Value == null ? null : dr.Cells[41].Value.ToString();
                mmmdb.MDB055 = dr.Cells[42].Value == null ? null : dr.Cells[42].Value.ToString();
                mmmdb.MDB056 = dr.Cells[43].Value == null ? null : dr.Cells[43].Value.ToString();
                mmmdb.MDB057 = dr.Cells[44].Value == null ? null : dr.Cells[44].Value.ToString();
                mmmdb.MDB058 = dr.Cells[45].Value == null ? null : dr.Cells[45].Value.ToString();
                mmmdb.MDB059 = dr.Cells[46].Value == null ? null : dr.Cells[46].Value.ToString();
                mmmdb.MDB060 = dr.Cells[47].Value == null ? null : dr.Cells[47].Value.ToString();
                mmmdb.MDB061 = dr.Cells[48].Value == null ? null : dr.Cells[48].Value.ToString();
                mmmdb.MDB062 = dr.Cells[49].Value == null ? null : dr.Cells[49].Value.ToString();
                mmmdb.MDB063 = dr.Cells[50].Value == null ? null : dr.Cells[50].Value.ToString();
                mmmdb.MDB064 = dr.Cells[51].Value == null ? null : dr.Cells[51].Value.ToString();
                if (dr.Cells[52].Value == null || dr.Cells[52].Value.ToString().Length == 0)
                {
                    mmmdb.CreatedDate = null;
                }
                else
                {
                    mmmdb.CreatedDate = DateTime.Parse(dr.Cells[52].Value.ToString());
                }

                mmmdb.Creator = dr.Cells[53].Value == null ? null : dr.Cells[53].Value.ToString();
                if (dr.Cells[54].Value == null || dr.Cells[54].Value.ToString().Length == 0)
                {
                    mmmdb.ModifiedDate = null;
                }
                else
                {
                    mmmdb.ModifiedDate = DateTime.Parse(dr.Cells[54].Value.ToString());
                }
                mmmdb.Modifier = dr.Cells[55].Value == null ? null : dr.Cells[55].Value.ToString();
                returnValue = mmmdb;
                DialogResult = DialogResult.OK;
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
            btnConfirm_Click(null, null);
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
                    GetAllMaterials(urlFinally);
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

        private async void GetAllMaterials(string urlFinally)
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
                dataGridView1.DataSource = JsonConvert.DeserializeObject<List<MMMDB>>(responseBody);
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
