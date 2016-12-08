using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WMSApiWinForm
{
    public partial class Form1 : Form
    {
        private string mode = "";
        private int editIndex = -1;
        private string url = "http://192.168.10.109/wmsapi/departments";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                CMMDC cmmdc = new CMMDC();
                cmmdc.CreatedDate = DateTime.Now;
                List<CMMDC> lst = dataGridView1.DataSource as List<CMMDC>;
                if (lst == null)
                {
                    lst = new List<CMMDC>();
                }
                lst.Add(cmmdc);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = lst;
                dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;

                foreach (DataGridViewRow dgv in dataGridView1.Rows)
                {
                    if (dgv.Index == dataGridView1.RowCount - 1)
                    {
                        dgv.ReadOnly = false;
                        dgv.Cells[6].ReadOnly = dgv.Cells[8].ReadOnly = true;
                    }
                    else
                    {
                        dgv.ReadOnly = true;
                    }
                }
                dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
                mode = "N";
                btnAdd.Enabled = btnDelete.Enabled = btnSearch.Enabled = btnModify.Enabled = false;
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
                string sendUrl = url + "/" +
                                 dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString();
                HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(sendUrl);
                webRequest.Method = "DELETE";
                webRequest.Headers["system_id"] = "123";
                webRequest.ContentType = "application/json";
                using (StreamWriter sw = new StreamWriter(webRequest.GetRequestStream()))
                {
                    sw.Write(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString());
                    //sw.Write("dfdfdfdf");
                    sw.Close();
                }
                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                using (StreamReader sr = new StreamReader(webResponse.GetResponseStream()))
                {
                    var result = sr.ReadToEnd();
                    btnSearch_Click(null,null);
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
                        dgv.Cells[8].Value = DateTime.Now;
                        dgv.Cells[0].ReadOnly = dgv.Cells[1].ReadOnly = dgv.Cells[6].ReadOnly = dgv.Cells[8].ReadOnly = true;
                    }
                    else
                    {
                        dgv.ReadOnly = true;
                    }
                }
                dataGridView1.Rows[editIndex].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                mode = "M";
                btnAdd.Enabled = btnDelete.Enabled = btnSearch.Enabled = btnModify.Enabled = false;
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
            //using (WebClient webClient = new WebClient())
            //{
            //    string sendUrl = url;
            //    if (txtId.Text.Trim().Length > 0)
            //    {
            //        sendUrl += "/" + txtId.Text.Trim();
            //    }
            //    webClient.Headers["system_id"] = "a123456";
            //    webClient.Encoding = Encoding.UTF8;
            //    webClient.DownloadStringAsync(new Uri(sendUrl));
            //    webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
            //}
            try
            {
                string sendUrl = url;
                if (txtId.Text.Trim().Length > 0)
                {
                    sendUrl += "/" + txtId.Text.Trim();
                }
                HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(sendUrl);
                webRequest.Method = "GET";
                webRequest.Headers["system_id"] = "123";
                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                using (StreamReader sr = new StreamReader(webResponse.GetResponseStream()))
                {
                    var result = sr.ReadToEnd();
                    if (!result.StartsWith("["))
                    {
                        result = "[" + result + "]";
                    }
                    dataGridView1.DataSource = JsonConvert.DeserializeObject<List<CMMDC>>(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        //{
        //    if (e.Error != null)
        //    {
        //        MessageBox.Show(e.Error.Message);
        //    }
        //    else
        //    {
        //        string msg = e.Result;

        //        if (!msg.StartsWith("["))
        //        {
        //            msg="["+msg+"]";
        //        }
        //        dataGridView1.DataSource = JsonConvert.DeserializeObject<List<CMMDC>>(msg);
        //    }
        //}

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
                    CMMDC cmmdc = new CMMDC();
                    cmmdc.SYSID = dataGridView1.Rows[editIndex].Cells[0].Value.ToString();
                    cmmdc.MDC001 = id;
                    cmmdc.MDC002 = dataGridView1.Rows[editIndex].Cells[2].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[2].Value.ToString();
                    cmmdc.MDC003 = dataGridView1.Rows[editIndex].Cells[3].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[3].Value.ToString();
                    cmmdc.MDC004 = dataGridView1.Rows[editIndex].Cells[4].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[4].Value.ToString();
                    cmmdc.Creator = dataGridView1.Rows[editIndex].Cells[5].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[5].Value.ToString();
                    if (string.IsNullOrEmpty(dataGridView1.Rows[editIndex].Cells[6].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[6].Value.ToString()))
                    {
                        cmmdc.CreatedDate = null;
                    }
                    else
                    {
                        cmmdc.CreatedDate = DateTime.Parse(dataGridView1.Rows[editIndex].Cells[6].Value.ToString());
                    }

                    cmmdc.Modifier = dataGridView1.Rows[editIndex].Cells[7].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[7].Value.ToString();
                    if (string.IsNullOrEmpty(dataGridView1.Rows[editIndex].Cells[8].Value == null ? "" : dataGridView1.Rows[editIndex].Cells[8].Value.ToString()))
                    {
                        cmmdc.ModifiedDate = null;
                    }
                    else
                    {
                        cmmdc.ModifiedDate = DateTime.Parse(dataGridView1.Rows[editIndex].Cells[8].Value.ToString());
                    }
                    HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(url+"/"+id);
                    webRequest.Method = "PUT";
                    webRequest.Headers["system_id"] = "123";
                    webRequest.ContentType = "application/json";
                    using (StreamWriter sw = new StreamWriter(webRequest.GetRequestStream()))
                    {
                        sw.Write(JsonConvert.SerializeObject(cmmdc));
                        sw.Close();
                    }
                    HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                    using (StreamReader sr = new StreamReader(webResponse.GetResponseStream()))
                    {
                        var result = sr.ReadToEnd();
                        btnSearch_Click(null, null);
                    }
                }
                else
                {
                    int index = dataGridView1.RowCount - 1;
                    CMMDC cmmdc = new CMMDC();
                    cmmdc.SYSID = dataGridView1.Rows[index].Cells[0].Value == null ? "" : dataGridView1.Rows[index].Cells[0].Value.ToString();
                    cmmdc.MDC001 = dataGridView1.Rows[index].Cells[1].Value == null ? "" : dataGridView1.Rows[index].Cells[1].Value.ToString();
                    cmmdc.MDC002 = dataGridView1.Rows[index].Cells[2].Value == null ? "" : dataGridView1.Rows[index].Cells[2].Value.ToString();
                    cmmdc.MDC003 = dataGridView1.Rows[index].Cells[3].Value == null ? "" : dataGridView1.Rows[index].Cells[3].Value.ToString();
                    cmmdc.MDC004 = dataGridView1.Rows[index].Cells[4].Value == null ? "" : dataGridView1.Rows[index].Cells[4].Value.ToString();
                    cmmdc.Creator = dataGridView1.Rows[index].Cells[5].Value == null ? "" : dataGridView1.Rows[index].Cells[5].Value.ToString();
                    if (string.IsNullOrEmpty(dataGridView1.Rows[index].Cells[6].Value == null ? "" : dataGridView1.Rows[index].Cells[6].Value.ToString()))
                    {
                        cmmdc.CreatedDate = null;
                    }
                    else
                    {
                        cmmdc.CreatedDate = DateTime.Parse(dataGridView1.Rows[index].Cells[6].Value.ToString());
                    }

                    cmmdc.Modifier = dataGridView1.Rows[index].Cells[7].Value == null ? "" : dataGridView1.Rows[index].Cells[7].Value.ToString();
                    if (string.IsNullOrEmpty(dataGridView1.Rows[index].Cells[8].Value == null ? "" : dataGridView1.Rows[index].Cells[8].Value.ToString()))
                    {
                        cmmdc.ModifiedDate = null;
                    }
                    else
                    {
                        cmmdc.ModifiedDate = DateTime.Parse(dataGridView1.Rows[index].Cells[8].Value.ToString());
                    }
                    HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                    webRequest.Method = "POST";
                    webRequest.Headers["system_id"] = "123";
                    webRequest.ContentType = "application/json";
                    using (StreamWriter sw = new StreamWriter(webRequest.GetRequestStream()))
                    {
                        sw.Write(JsonConvert.SerializeObject(cmmdc));
                        //sw.Write("dfdfdfdf");
                        sw.Close();
                    }
                    HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                    using (StreamReader sr = new StreamReader(webResponse.GetResponseStream()))
                    {
                        var result = sr.ReadToEnd();
                        btnSearch_Click(null, null);
                    }
                }
                mode = "";
                editIndex = -1;
                dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
                btnAdd.Enabled = btnDelete.Enabled = btnSearch.Enabled = btnModify.Enabled = true;
                btnSave.Enabled = btnCancel.Enabled = false;
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
                btnAdd.Enabled = btnDelete.Enabled = btnSearch.Enabled = btnModify.Enabled = true;
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
    }
}
