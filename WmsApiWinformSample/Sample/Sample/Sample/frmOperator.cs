using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample
{
    public partial class frmOperator : Form
    {
        public frmOperator()
        {
            InitializeComponent();
        }

        private void frmOperator_Load(object sender, EventArgs e)
        {
            cmbCondition.SelectedIndex = 0;
            cmbType.SelectedIndex = 0;
            cmbCondition.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult=DialogResult.Cancel;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {

        }
    }
}
