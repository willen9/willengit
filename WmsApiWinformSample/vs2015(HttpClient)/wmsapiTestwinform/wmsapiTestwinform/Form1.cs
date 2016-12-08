using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace wmsapiTestwinform
{
    public partial class Form1 : Form
    {
        private string mode = "";
        private int editIndex = -1;
        private string url = "http://192.168.10.109/wmsapi/customers";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                CMMDG cmmdg = new CMMDG();
                cmmdg.CreatedDate = DateTime.Now;
                List<CMMDG> lst = dataGridView1.DataSource as List<CMMDG>;
                if (lst == null)
                {
                    lst = new List<CMMDG>();
                }
                lst.Add(cmmdg);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = lst;
                dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;

                foreach (DataGridViewRow dgv in dataGridView1.Rows)
                {
                    if (dgv.Index == dataGridView1.RowCount - 1)
                    {
                        dgv.ReadOnly = false;
                        dgv.Cells[42].ReadOnly=dgv.Cells[44].ReadOnly = true;
                    }
                    else
                    {
                        dgv.ReadOnly = true;
                    }
                }
                dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
                mode = "N";
                btnNew.Enabled = btnDelete.Enabled = btnSearch.Enabled = btnModify.Enabled = false;
                btnSave.Enabled = btnCancel.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (dataGridView1.CurrentCell == null)
                {
                    MessageBox.Show("Please select item");
                    return;
                }
                DeleteCustomersById(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("Please select item");
                return;
            }
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
                editIndex = dataGridView1.CurrentCell.RowIndex;
                foreach (DataGridViewRow dgv in dataGridView1.Rows)
                {
                    if (dgv.Index == editIndex)
                    {
                        dgv.ReadOnly = false;
                        dgv.Cells[44].Value=DateTime.Now;
                        dgv.Cells[0].ReadOnly = dgv.Cells[1].ReadOnly=dgv.Cells[42].ReadOnly= dgv.Cells[44].ReadOnly = true;
                    }
                    else
                    {
                        dgv.ReadOnly = true;
                    }
                }
                dataGridView1.Rows[editIndex].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                mode = "M";
                btnNew.Enabled = btnDelete.Enabled = btnSearch.Enabled = btnModify.Enabled = false;
                btnSave.Enabled = btnCancel.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string urlFinally = url;
            if (!string.IsNullOrEmpty(txtID.Text.Trim()))
            {
                urlFinally += "/" + txtID.Text.Trim();
            }
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                GetAllCustemers(urlFinally);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (mode == "")
            {
                return;
            }
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (mode == "M")
                {
                    string id = dataGridView1.Rows[editIndex].Cells[1].Value.ToString();
                    CMMDG cmmdg = new CMMDG();
                    cmmdg.SYSID = dataGridView1.Rows[editIndex].Cells[0].Value.ToString();
                    cmmdg.MDG001 = id;
                    cmmdg.MDG002 = dataGridView1.Rows[editIndex].Cells[2].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[2].Value.ToString();
                    cmmdg.MDG003 = dataGridView1.Rows[editIndex].Cells[3].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[3].Value.ToString();
                    cmmdg.MDG004 = dataGridView1.Rows[editIndex].Cells[4].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[4].Value.ToString();
                    cmmdg.MDG005 = dataGridView1.Rows[editIndex].Cells[5].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[5].Value.ToString();
                    cmmdg.MDG006 = dataGridView1.Rows[editIndex].Cells[6].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[6].Value.ToString();
                    cmmdg.MDG007 = dataGridView1.Rows[editIndex].Cells[7].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[7].Value.ToString();
                    cmmdg.MDG008 = dataGridView1.Rows[editIndex].Cells[8].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[8].Value.ToString();
                    cmmdg.MDG009 = dataGridView1.Rows[editIndex].Cells[9].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[9].Value.ToString();
                    cmmdg.MDG010 = dataGridView1.Rows[editIndex].Cells[10].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[10].Value.ToString();
                    cmmdg.MDG011 = dataGridView1.Rows[editIndex].Cells[11].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[11].Value.ToString();
                    cmmdg.MDG012 = dataGridView1.Rows[editIndex].Cells[12].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[12].Value.ToString();
                    cmmdg.MDG013 = dataGridView1.Rows[editIndex].Cells[13].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[13].Value.ToString();
                    cmmdg.MDG014 = dataGridView1.Rows[editIndex].Cells[14].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[14].Value.ToString();
                    cmmdg.MDG015 = dataGridView1.Rows[editIndex].Cells[15].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[15].Value.ToString();
                    cmmdg.MDG016 = dataGridView1.Rows[editIndex].Cells[16].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[16].Value.ToString();
                    cmmdg.MDG017 = dataGridView1.Rows[editIndex].Cells[17].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[17].Value.ToString();
                    cmmdg.MDG018 = dataGridView1.Rows[editIndex].Cells[18].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[18].Value.ToString();
                    cmmdg.MDG021 = dataGridView1.Rows[editIndex].Cells[19].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[19].Value.ToString();
                    cmmdg.MDG022 = dataGridView1.Rows[editIndex].Cells[20].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[20].Value.ToString();
                    cmmdg.MDG023 = dataGridView1.Rows[editIndex].Cells[21].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[21].Value.ToString();
                    cmmdg.MDG024 = dataGridView1.Rows[editIndex].Cells[22].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[22].Value.ToString();
                    cmmdg.MDG025 = dataGridView1.Rows[editIndex].Cells[23].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[23].Value.ToString();
                    cmmdg.MDG026 = dataGridView1.Rows[editIndex].Cells[24].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[24].Value.ToString();
                    cmmdg.MDG027 = dataGridView1.Rows[editIndex].Cells[25].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[25].Value.ToString();
                    cmmdg.MDG028 = dataGridView1.Rows[editIndex].Cells[26].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[26].Value.ToString();
                    cmmdg.MDG029 = dataGridView1.Rows[editIndex].Cells[27].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[27].Value.ToString();
                    cmmdg.MDG030 = dataGridView1.Rows[editIndex].Cells[28].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[28].Value.ToString();
                    cmmdg.MDG031 = dataGridView1.Rows[editIndex].Cells[29].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[29].Value.ToString();
                    cmmdg.MDG032 = dataGridView1.Rows[editIndex].Cells[30].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[30].Value.ToString();
                    cmmdg.MDG033 = dataGridView1.Rows[editIndex].Cells[31].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[31].Value.ToString();
                    cmmdg.MDG034 = dataGridView1.Rows[editIndex].Cells[32].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[32].Value.ToString();
                    cmmdg.MDG035 = dataGridView1.Rows[editIndex].Cells[33].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[33].Value.ToString();
                    cmmdg.MDG036 = dataGridView1.Rows[editIndex].Cells[34].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[34].Value.ToString();
                    cmmdg.MDG037 = dataGridView1.Rows[editIndex].Cells[35].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[35].Value.ToString();
                    cmmdg.MDG038 = dataGridView1.Rows[editIndex].Cells[36].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[36].Value.ToString();
                    cmmdg.MDG039 = dataGridView1.Rows[editIndex].Cells[37].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[37].Value.ToString();
                    cmmdg.MDG040 = dataGridView1.Rows[editIndex].Cells[38].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[28].Value.ToString();
                    cmmdg.MDG051 = dataGridView1.Rows[editIndex].Cells[39].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[39].Value.ToString();
                    cmmdg.MDG052 = dataGridView1.Rows[editIndex].Cells[40].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[40].Value.ToString();
                    cmmdg.Creator = dataGridView1.Rows[editIndex].Cells[41].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[41].Value.ToString();
                    if (string.IsNullOrEmpty(dataGridView1.Rows[editIndex].Cells[42].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[42].Value.ToString()))
                    {
                        cmmdg.CreatedDate = null;
                    }
                    else
                    {
                        cmmdg.CreatedDate = DateTime.Parse(dataGridView1.Rows[editIndex].Cells[42].Value.ToString());
                    }

                    cmmdg.Modifier = dataGridView1.Rows[editIndex].Cells[43].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[43].Value.ToString();
                    if (string.IsNullOrEmpty(dataGridView1.Rows[editIndex].Cells[44].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[44].Value.ToString()))
                    {
                        cmmdg.ModifiedDate = null;
                    }
                    else
                    {
                        cmmdg.ModifiedDate = DateTime.Parse(dataGridView1.Rows[editIndex].Cells[44].Value.ToString());
                    }
                    PutCustomers(id, cmmdg);
                }
                else
                {
                    int index = dataGridView1.RowCount - 1;
                    CMMDG cmmdg = new CMMDG();
                    cmmdg.SYSID = dataGridView1.Rows[index].Cells[0].Value == null ? "" : dataGridView1.Rows[index].Cells[0].Value.ToString();
                    cmmdg.MDG001 = dataGridView1.Rows[index].Cells[1].Value == null ? "" : dataGridView1.Rows[index].Cells[1].Value.ToString();
                    cmmdg.MDG002 = dataGridView1.Rows[index].Cells[2].Value == null ? "" : dataGridView1.Rows[index].Cells[2].Value.ToString();
                    cmmdg.MDG003 = dataGridView1.Rows[index].Cells[3].Value == null ? "" : dataGridView1.Rows[index].Cells[3].Value.ToString();
                    cmmdg.MDG004 = dataGridView1.Rows[index].Cells[4].Value == null ? "" : dataGridView1.Rows[index].Cells[4].Value.ToString();
                    cmmdg.MDG005 = dataGridView1.Rows[index].Cells[5].Value == null ? "" : dataGridView1.Rows[index].Cells[5].Value.ToString();
                    cmmdg.MDG006 = dataGridView1.Rows[index].Cells[6].Value == null ? "" : dataGridView1.Rows[index].Cells[6].Value.ToString();
                    cmmdg.MDG007 = dataGridView1.Rows[index].Cells[7].Value == null ? "" : dataGridView1.Rows[index].Cells[7].Value.ToString();
                    cmmdg.MDG008 = dataGridView1.Rows[index].Cells[8].Value == null ? "" : dataGridView1.Rows[index].Cells[8].Value.ToString();
                    cmmdg.MDG009 = dataGridView1.Rows[index].Cells[9].Value == null ? "" : dataGridView1.Rows[index].Cells[9].Value.ToString();
                    cmmdg.MDG010 = dataGridView1.Rows[index].Cells[10].Value == null ? "" : dataGridView1.Rows[index].Cells[10].Value.ToString();
                    cmmdg.MDG011 = dataGridView1.Rows[index].Cells[11].Value == null ? "" : dataGridView1.Rows[index].Cells[11].Value.ToString();
                    cmmdg.MDG012 = dataGridView1.Rows[index].Cells[12].Value == null ? "" : dataGridView1.Rows[index].Cells[12].Value.ToString();
                    cmmdg.MDG013 = dataGridView1.Rows[index].Cells[13].Value == null ? "" : dataGridView1.Rows[index].Cells[13].Value.ToString();
                    cmmdg.MDG014 = dataGridView1.Rows[index].Cells[14].Value == null ? "" : dataGridView1.Rows[index].Cells[14].Value.ToString();
                    cmmdg.MDG015 = dataGridView1.Rows[index].Cells[15].Value == null ? "" : dataGridView1.Rows[index].Cells[15].Value.ToString();
                    cmmdg.MDG016 = dataGridView1.Rows[index].Cells[16].Value == null ? "" : dataGridView1.Rows[index].Cells[16].Value.ToString();
                    cmmdg.MDG017 = dataGridView1.Rows[index].Cells[17].Value == null ? "" : dataGridView1.Rows[index].Cells[17].Value.ToString();
                    cmmdg.MDG018 = dataGridView1.Rows[index].Cells[18].Value == null ? "" : dataGridView1.Rows[index].Cells[18].Value.ToString();
                    cmmdg.MDG021 = dataGridView1.Rows[index].Cells[19].Value == null ? "" : dataGridView1.Rows[index].Cells[19].Value.ToString();
                    cmmdg.MDG022 = dataGridView1.Rows[index].Cells[20].Value == null ? "" : dataGridView1.Rows[index].Cells[20].Value.ToString();
                    cmmdg.MDG023 = dataGridView1.Rows[index].Cells[21].Value == null ? "" : dataGridView1.Rows[index].Cells[21].Value.ToString();
                    cmmdg.MDG024 = dataGridView1.Rows[index].Cells[22].Value == null ? "" : dataGridView1.Rows[index].Cells[22].Value.ToString();
                    cmmdg.MDG025 = dataGridView1.Rows[index].Cells[23].Value == null ? "" : dataGridView1.Rows[index].Cells[23].Value.ToString();
                    cmmdg.MDG026 = dataGridView1.Rows[index].Cells[24].Value == null ? "" : dataGridView1.Rows[index].Cells[24].Value.ToString();
                    cmmdg.MDG027 = dataGridView1.Rows[index].Cells[25].Value == null ? "" : dataGridView1.Rows[index].Cells[25].Value.ToString();
                    cmmdg.MDG028 = dataGridView1.Rows[index].Cells[26].Value == null ? "" : dataGridView1.Rows[index].Cells[26].Value.ToString();
                    cmmdg.MDG029 = dataGridView1.Rows[index].Cells[27].Value == null ? "" : dataGridView1.Rows[index].Cells[27].Value.ToString();
                    cmmdg.MDG030 = dataGridView1.Rows[index].Cells[28].Value == null ? "" : dataGridView1.Rows[index].Cells[28].Value.ToString();
                    cmmdg.MDG031 = dataGridView1.Rows[index].Cells[29].Value == null ? "" : dataGridView1.Rows[index].Cells[29].Value.ToString();
                    cmmdg.MDG032 = dataGridView1.Rows[index].Cells[30].Value == null ? "" : dataGridView1.Rows[index].Cells[30].Value.ToString();
                    cmmdg.MDG033 = dataGridView1.Rows[index].Cells[31].Value == null ? "" : dataGridView1.Rows[index].Cells[31].Value.ToString();
                    cmmdg.MDG034 = dataGridView1.Rows[index].Cells[32].Value == null ? "" : dataGridView1.Rows[index].Cells[32].Value.ToString();
                    cmmdg.MDG035 = dataGridView1.Rows[index].Cells[33].Value == null ? "" : dataGridView1.Rows[index].Cells[33].Value.ToString();
                    cmmdg.MDG036 = dataGridView1.Rows[index].Cells[34].Value == null ? "" : dataGridView1.Rows[index].Cells[34].Value.ToString();
                    cmmdg.MDG037 = dataGridView1.Rows[index].Cells[35].Value == null ? "" : dataGridView1.Rows[index].Cells[35].Value.ToString();
                    cmmdg.MDG038 = dataGridView1.Rows[index].Cells[36].Value == null ? "" : dataGridView1.Rows[index].Cells[36].Value.ToString();
                    cmmdg.MDG039 = dataGridView1.Rows[index].Cells[37].Value == null ? "" : dataGridView1.Rows[index].Cells[37].Value.ToString();
                    cmmdg.MDG040 = dataGridView1.Rows[index].Cells[38].Value == null ? "" : dataGridView1.Rows[index].Cells[28].Value.ToString();
                    cmmdg.MDG051 = dataGridView1.Rows[index].Cells[39].Value == null ? "" : dataGridView1.Rows[index].Cells[39].Value.ToString();
                    cmmdg.MDG052 = dataGridView1.Rows[index].Cells[40].Value == null ? "" : dataGridView1.Rows[index].Cells[40].Value.ToString();
                    cmmdg.Creator = dataGridView1.Rows[index].Cells[41].Value == null ? "" : dataGridView1.Rows[index].Cells[41].Value.ToString();
                    if (string.IsNullOrEmpty(dataGridView1.Rows[index].Cells[42].Value == null ? "" : dataGridView1.Rows[index].Cells[42].Value.ToString()))
                    {
                        cmmdg.CreatedDate = null;
                    }
                    else
                    {
                        cmmdg.CreatedDate = DateTime.Parse(dataGridView1.Rows[index].Cells[42].Value.ToString());
                    }

                    cmmdg.Modifier = dataGridView1.Rows[index].Cells[43].Value == null ? "" : dataGridView1.Rows[index].Cells[43].Value.ToString();
                    if (string.IsNullOrEmpty(dataGridView1.Rows[index].Cells[44].Value == null ? "" : dataGridView1.Rows[index].Cells[44].Value.ToString()))
                    {
                        cmmdg.ModifiedDate = null;
                    }
                    else
                    {
                        cmmdg.ModifiedDate = DateTime.Parse(dataGridView1.Rows[index].Cells[44].Value.ToString());
                    }
                    PostCustomers(cmmdg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                mode = "";
                editIndex = -1;
                dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
                btnNew.Enabled = btnDelete.Enabled = btnSearch.Enabled = btnModify.Enabled = true;
                btnSave.Enabled = btnCancel.Enabled = false;
                btnSearch_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private async void GetAllCustemers(string urlFinally)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("system_id", "123");
            //client.DefaultRequestHeaders.Add("fields", "MDG001,MDG002,MDG003");
            //client.DefaultRequestHeaders.Add("sort", "MDG001 desc");
            //client.DefaultRequestHeaders.Add("page", "1");
            //client.DefaultRequestHeaders.Add("limit", "2");
            HttpResponseMessage response = await client.GetAsync(urlFinally);
            //response.EnsureSuccessStatusCode();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                if (!responseBody.StartsWith("["))
                {
                    responseBody = "[" + responseBody + "]"; 
                }
                dataGridView1.DataSource = JsonConvert.DeserializeObject<List<CMMDG>>(responseBody);
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

        private async void PutCustomers(string id, CMMDG cmmdg)
        {
            HttpClient client = new HttpClient();
            string sendBody = JsonConvert.SerializeObject(cmmdg);
            using (HttpContent httpContent = new StringContent(sendBody))
            {
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await client.PutAsync(url + "/" + id, httpContent);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    btnSearch_Click(null, null);
                    mode = "";
                    editIndex = -1;
                    dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
                    btnNew.Enabled = btnDelete.Enabled = btnSearch.Enabled = btnModify.Enabled = true;
                    btnSave.Enabled = btnCancel.Enabled = false;
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    responseBody = responseBody.Replace("\\", "").Replace("\"error\",", "").Trim(new char[] { '"' });
                    MessageBox.Show(JsonConvert.DeserializeObject<List<Error>>(responseBody)[0].message);
                }
                else
                {
                    MessageBox.Show("Exception");
                }
            }
        }

        private async void DeleteCustomersById(string id)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("system_id", "123");
            HttpResponseMessage response = await client.DeleteAsync(url + "/" + id);
            if (response.StatusCode== HttpStatusCode.OK)
            {
                btnSearch_Click(null, null);
            }
            else if (response.StatusCode == HttpStatusCode.NoContent)
            {
                MessageBox.Show("No Object");
            }
            else
            {
                MessageBox.Show("Exception");
            }
        }

        private async void PostCustomers(CMMDG cmmdg)
        {
            HttpClient client = new HttpClient();
            string sendBody = JsonConvert.SerializeObject(cmmdg);
            using (HttpContent httpContent = new StringContent(sendBody))
            {
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await client.PostAsync(url, httpContent);
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    btnSearch_Click(null, null);
                    mode = "";
                    editIndex = -1;
                    dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
                    btnNew.Enabled = btnDelete.Enabled = btnSearch.Enabled = btnModify.Enabled = true;
                    btnSave.Enabled = btnCancel.Enabled = false;
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    responseBody = responseBody.Replace("\\", "").Replace("\"error\",", "").Trim(new char[] { '"' });
                    MessageBox.Show(JsonConvert.DeserializeObject<List<Error>>(responseBody)[0].message);
                }
                else
                {
                    MessageBox.Show("Exception");
                }
            }
        }
    }

    public class Error
    {
        public string code { get; set; }
        public string message { get; set; }
    }
}
