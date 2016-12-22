﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace P1618401.ReportFrm
{
    public partial class frm04 : Form
    {
        public DataTable dtSource = null;
        public frm04()
        {
            InitializeComponent();
        }

        private void frm03_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                reportViewer1.ShowRefreshButton = false;
                reportViewer1.ShowFindControls = false;
                reportViewer1.ShowStopButton = false;
                reportViewer1.ShowDocumentMapButton = false;
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1_ds04", dtSource));
                this.reportViewer1.RefreshReport();
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
}
