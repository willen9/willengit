namespace P1618401
{
    partial class FrmScreen02
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.GCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOCATION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REAL_QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INVE_QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ADJUST_QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.GlobalCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocalCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.USystem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnPrintInv = new System.Windows.Forms.Button();
            this.btnPrintMul = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnMain = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(879, 533);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel4);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(879, 448);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dataGridView2);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 252);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(873, 193);
            this.panel4.TabIndex = 4;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GCODE,
            this.LOCATION,
            this.REAL_QTY,
            this.INVE_QTY,
            this.ADJUST_QTY});
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(0, 29);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.Size = new System.Drawing.Size(873, 164);
            this.dataGridView2.TabIndex = 6;
            // 
            // GCODE
            // 
            this.GCODE.DataPropertyName = "GCODE";
            this.GCODE.HeaderText = "GCODE";
            this.GCODE.Name = "GCODE";
            this.GCODE.ReadOnly = true;
            this.GCODE.Width = 150;
            // 
            // LOCATION
            // 
            this.LOCATION.DataPropertyName = "LOCATION";
            this.LOCATION.HeaderText = "LOCATION";
            this.LOCATION.Name = "LOCATION";
            this.LOCATION.ReadOnly = true;
            // 
            // REAL_QTY
            // 
            this.REAL_QTY.DataPropertyName = "REAL_QTY";
            this.REAL_QTY.HeaderText = "REAL_QTY";
            this.REAL_QTY.Name = "REAL_QTY";
            this.REAL_QTY.ReadOnly = true;
            // 
            // INVE_QTY
            // 
            this.INVE_QTY.DataPropertyName = "INVE_QTY";
            this.INVE_QTY.HeaderText = "INVE_QTY";
            this.INVE_QTY.Name = "INVE_QTY";
            this.INVE_QTY.ReadOnly = true;
            // 
            // ADJUST_QTY
            // 
            this.ADJUST_QTY.DataPropertyName = "ADJUST_QTY";
            this.ADJUST_QTY.HeaderText = "ADJUST_QTY";
            this.ADJUST_QTY.Name = "ADJUST_QTY";
            this.ADJUST_QTY.ReadOnly = true;
            this.ADJUST_QTY.Width = 120;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(873, 29);
            this.panel6.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("PMingLiU", 14F);
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(12, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "物料盤點記錄";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 23);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(873, 229);
            this.panel3.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GlobalCode,
            this.LocalCode,
            this.Description,
            this.USystem});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 29);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(873, 200);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // GlobalCode
            // 
            this.GlobalCode.DataPropertyName = "GlobalCode";
            this.GlobalCode.HeaderText = "GlobalCode";
            this.GlobalCode.Name = "GlobalCode";
            this.GlobalCode.ReadOnly = true;
            this.GlobalCode.Width = 150;
            // 
            // LocalCode
            // 
            this.LocalCode.DataPropertyName = "LocalCode";
            this.LocalCode.HeaderText = "LocalCode";
            this.LocalCode.Name = "LocalCode";
            this.LocalCode.ReadOnly = true;
            this.LocalCode.Width = 150;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 200;
            // 
            // USystem
            // 
            this.USystem.DataPropertyName = "USystem";
            this.USystem.HeaderText = "USystem";
            this.USystem.Name = "USystem";
            this.USystem.ReadOnly = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Font = new System.Drawing.Font("PMingLiU", 14F);
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(873, 29);
            this.panel5.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("PMingLiU", 14F);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "物料瀏覽";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnReturn);
            this.panel2.Controls.Add(this.btnPrintInv);
            this.panel2.Controls.Add(this.btnPrintMul);
            this.panel2.Controls.Add(this.btnEnd);
            this.panel2.Controls.Add(this.btnMain);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(879, 85);
            this.panel2.TabIndex = 0;
            // 
            // btnReturn
            // 
            this.btnReturn.Image = global::P1618401.Properties.Resources._1479735022_Revert;
            this.btnReturn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnReturn.Location = new System.Drawing.Point(416, 12);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(96, 72);
            this.btnReturn.TabIndex = 4;
            this.btnReturn.Text = "返回";
            this.btnReturn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnPrintInv
            // 
            this.btnPrintInv.Image = global::P1618401.Properties.Resources.bt_cancelt02;
            this.btnPrintInv.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPrintInv.Location = new System.Drawing.Point(315, 12);
            this.btnPrintInv.Name = "btnPrintInv";
            this.btnPrintInv.Size = new System.Drawing.Size(96, 72);
            this.btnPrintInv.TabIndex = 3;
            this.btnPrintInv.Text = "列印盤點表";
            this.btnPrintInv.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPrintInv.UseVisualStyleBackColor = true;
            this.btnPrintInv.Click += new System.EventHandler(this.btnPrintInv_Click);
            // 
            // btnPrintMul
            // 
            this.btnPrintMul.Image = global::P1618401.Properties.Resources.bt_save02;
            this.btnPrintMul.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPrintMul.Location = new System.Drawing.Point(214, 12);
            this.btnPrintMul.Name = "btnPrintMul";
            this.btnPrintMul.Size = new System.Drawing.Size(96, 72);
            this.btnPrintMul.TabIndex = 2;
            this.btnPrintMul.Text = "列印盤差表";
            this.btnPrintMul.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPrintMul.UseVisualStyleBackColor = true;
            this.btnPrintMul.Click += new System.EventHandler(this.btnPrintMul_Click);
            // 
            // btnEnd
            // 
            this.btnEnd.Image = global::P1618401.Properties.Resources.bt_edit02;
            this.btnEnd.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEnd.Location = new System.Drawing.Point(113, 12);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(96, 72);
            this.btnEnd.TabIndex = 1;
            this.btnEnd.Text = "盤點結轉";
            this.btnEnd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnMain
            // 
            this.btnMain.Image = global::P1618401.Properties.Resources.bt_add02;
            this.btnMain.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMain.Location = new System.Drawing.Point(12, 12);
            this.btnMain.Name = "btnMain";
            this.btnMain.Size = new System.Drawing.Size(96, 72);
            this.btnMain.TabIndex = 0;
            this.btnMain.Text = "產生主檔";
            this.btnMain.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMain.UseVisualStyleBackColor = true;
            this.btnMain.Click += new System.EventHandler(this.btnMain_Click);
            // 
            // FrmScreen02
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 533);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmScreen02";
            this.Text = "物料盤點";
            this.Load += new System.EventHandler(this.FrmScreen02_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnPrintInv;
        private System.Windows.Forms.Button btnPrintMul;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnMain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn GlobalCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocalCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn USystem;
        private System.Windows.Forms.DataGridViewTextBoxColumn GCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOCATION;
        private System.Windows.Forms.DataGridViewTextBoxColumn REAL_QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn INVE_QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn ADJUST_QTY;
    }
}