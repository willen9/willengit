namespace P1618401
{
    partial class FrmScreen08
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rabAll = new System.Windows.Forms.RadioButton();
            this.rabBack = new System.Windows.Forms.RadioButton();
            this.rabGoodsOut = new System.Windows.Forms.RadioButton();
            this.rabGoodsIn = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblStyle = new System.Windows.Forms.Label();
            this.txtLocalEnd = new System.Windows.Forms.TextBox();
            this.txtLocalStart = new System.Windows.Forms.TextBox();
            this.txtDateEnd = new System.Windows.Forms.TextBox();
            this.txtGlobalEnd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCusNo = new System.Windows.Forms.TextBox();
            this.txtDateStart = new System.Windows.Forms.TextBox();
            this.lblCusNo = new System.Windows.Forms.Label();
            this.txtGlobalStart = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(970, 533);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(970, 533);
            this.panel2.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.dataGridView1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 214);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(970, 319);
            this.panel5.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(970, 319);
            this.dataGridView1.TabIndex = 18;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 85);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(970, 129);
            this.panel4.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rabAll);
            this.groupBox2.Controls.Add(this.rabBack);
            this.groupBox2.Controls.Add(this.rabGoodsOut);
            this.groupBox2.Controls.Add(this.rabGoodsIn);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(833, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(134, 129);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // rabAll
            // 
            this.rabAll.AutoSize = true;
            this.rabAll.Checked = true;
            this.rabAll.Location = new System.Drawing.Point(18, 96);
            this.rabAll.Name = "rabAll";
            this.rabAll.Size = new System.Drawing.Size(58, 20);
            this.rabAll.TabIndex = 17;
            this.rabAll.TabStop = true;
            this.rabAll.Text = "全部";
            this.rabAll.UseVisualStyleBackColor = true;
            // 
            // rabBack
            // 
            this.rabBack.AutoSize = true;
            this.rabBack.Location = new System.Drawing.Point(18, 70);
            this.rabBack.Name = "rabBack";
            this.rabBack.Size = new System.Drawing.Size(58, 20);
            this.rabBack.TabIndex = 16;
            this.rabBack.TabStop = true;
            this.rabBack.Text = "退回";
            this.rabBack.UseVisualStyleBackColor = true;
            // 
            // rabGoodsOut
            // 
            this.rabGoodsOut.AutoSize = true;
            this.rabGoodsOut.Location = new System.Drawing.Point(18, 44);
            this.rabGoodsOut.Name = "rabGoodsOut";
            this.rabGoodsOut.Size = new System.Drawing.Size(58, 20);
            this.rabGoodsOut.TabIndex = 15;
            this.rabGoodsOut.TabStop = true;
            this.rabGoodsOut.Text = "出貨";
            this.rabGoodsOut.UseVisualStyleBackColor = true;
            // 
            // rabGoodsIn
            // 
            this.rabGoodsIn.AutoSize = true;
            this.rabGoodsIn.Location = new System.Drawing.Point(18, 18);
            this.rabGoodsIn.Name = "rabGoodsIn";
            this.rabGoodsIn.Size = new System.Drawing.Size(58, 20);
            this.rabGoodsIn.TabIndex = 14;
            this.rabGoodsIn.TabStop = true;
            this.rabGoodsIn.Text = "進貨";
            this.rabGoodsIn.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTotal);
            this.groupBox1.Controls.Add(this.lblStyle);
            this.groupBox1.Controls.Add(this.txtLocalEnd);
            this.groupBox1.Controls.Add(this.txtLocalStart);
            this.groupBox1.Controls.Add(this.txtDateEnd);
            this.groupBox1.Controls.Add(this.txtGlobalEnd);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtCusNo);
            this.groupBox1.Controls.Add(this.txtDateStart);
            this.groupBox1.Controls.Add(this.lblCusNo);
            this.groupBox1.Controls.Add(this.txtGlobalStart);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(833, 129);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblTotal.Location = new System.Drawing.Point(714, 110);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(112, 16);
            this.lblTotal.TabIndex = 15;
            this.lblTotal.Text = "筆數：19468筆";
            // 
            // lblStyle
            // 
            this.lblStyle.AutoSize = true;
            this.lblStyle.Font = new System.Drawing.Font("PMingLiU", 14F);
            this.lblStyle.ForeColor = System.Drawing.Color.Firebrick;
            this.lblStyle.Location = new System.Drawing.Point(732, 18);
            this.lblStyle.Name = "lblStyle";
            this.lblStyle.Size = new System.Drawing.Size(57, 19);
            this.lblStyle.TabIndex = 14;
            this.lblStyle.Text = "瀏  覽";
            // 
            // txtLocalEnd
            // 
            this.txtLocalEnd.Location = new System.Drawing.Point(323, 91);
            this.txtLocalEnd.MaxLength = 5;
            this.txtLocalEnd.Name = "txtLocalEnd";
            this.txtLocalEnd.Size = new System.Drawing.Size(151, 27);
            this.txtLocalEnd.TabIndex = 13;
            // 
            // txtLocalStart
            // 
            this.txtLocalStart.Location = new System.Drawing.Point(119, 91);
            this.txtLocalStart.MaxLength = 5;
            this.txtLocalStart.Name = "txtLocalStart";
            this.txtLocalStart.Size = new System.Drawing.Size(151, 27);
            this.txtLocalStart.TabIndex = 12;
            // 
            // txtDateEnd
            // 
            this.txtDateEnd.Location = new System.Drawing.Point(323, 55);
            this.txtDateEnd.MaxLength = 15;
            this.txtDateEnd.Name = "txtDateEnd";
            this.txtDateEnd.Size = new System.Drawing.Size(151, 27);
            this.txtDateEnd.TabIndex = 10;
            // 
            // txtGlobalEnd
            // 
            this.txtGlobalEnd.Location = new System.Drawing.Point(369, 19);
            this.txtGlobalEnd.MaxLength = 15;
            this.txtGlobalEnd.Name = "txtGlobalEnd";
            this.txtGlobalEnd.Size = new System.Drawing.Size(199, 27);
            this.txtGlobalEnd.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("PMingLiU", 30F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(268, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 40);
            this.label4.TabIndex = 10;
            this.label4.Text = "—";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtCusNo
            // 
            this.txtCusNo.Location = new System.Drawing.Point(551, 55);
            this.txtCusNo.MaxLength = 6;
            this.txtCusNo.Name = "txtCusNo";
            this.txtCusNo.Size = new System.Drawing.Size(151, 27);
            this.txtCusNo.TabIndex = 11;
            // 
            // txtDateStart
            // 
            this.txtDateStart.Location = new System.Drawing.Point(119, 55);
            this.txtDateStart.MaxLength = 15;
            this.txtDateStart.Name = "txtDateStart";
            this.txtDateStart.Size = new System.Drawing.Size(151, 27);
            this.txtDateStart.TabIndex = 9;
            // 
            // lblCusNo
            // 
            this.lblCusNo.AutoSize = true;
            this.lblCusNo.Font = new System.Drawing.Font("PMingLiU", 14F);
            this.lblCusNo.Location = new System.Drawing.Point(499, 58);
            this.lblCusNo.Name = "lblCusNo";
            this.lblCusNo.Size = new System.Drawing.Size(52, 19);
            this.lblCusNo.TabIndex = 8;
            this.lblCusNo.Text = "客編:";
            // 
            // txtGlobalStart
            // 
            this.txtGlobalStart.Location = new System.Drawing.Point(119, 19);
            this.txtGlobalStart.MaxLength = 15;
            this.txtGlobalStart.Name = "txtGlobalStart";
            this.txtGlobalStart.Size = new System.Drawing.Size(199, 27);
            this.txtGlobalStart.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("PMingLiU", 14F);
            this.label5.Location = new System.Drawing.Point(67, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 19);
            this.label5.TabIndex = 8;
            this.label5.Text = "日期:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("PMingLiU", 14F);
            this.label3.Location = new System.Drawing.Point(21, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 19);
            this.label3.TabIndex = 8;
            this.label3.Text = "Local Code:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("PMingLiU", 30F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(268, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 40);
            this.label6.TabIndex = 10;
            this.label6.Text = "—";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("PMingLiU", 30F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(315, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 40);
            this.label2.TabIndex = 10;
            this.label2.Text = "—";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("PMingLiU", 14F);
            this.label1.Location = new System.Drawing.Point(13, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 19);
            this.label1.TabIndex = 8;
            this.label1.Text = "Global Code:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnImport);
            this.panel3.Controls.Add(this.btnReturn);
            this.panel3.Controls.Add(this.btnDel);
            this.panel3.Controls.Add(this.btnSearch);
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Controls.Add(this.btnModify);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(970, 85);
            this.panel3.TabIndex = 0;
            // 
            // btnImport
            // 
            this.btnImport.Image = global::P1618401.Properties.Resources.bt_import02;
            this.btnImport.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnImport.Location = new System.Drawing.Point(324, 13);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(72, 72);
            this.btnImport.TabIndex = 4;
            this.btnImport.Text = "匯入";
            this.btnImport.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Image = global::P1618401.Properties.Resources._1479735022_Revert;
            this.btnReturn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnReturn.Location = new System.Drawing.Point(480, 12);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(72, 72);
            this.btnReturn.TabIndex = 6;
            this.btnReturn.Text = "返回";
            this.btnReturn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnDel
            // 
            this.btnDel.Enabled = false;
            this.btnDel.Image = global::P1618401.Properties.Resources.bt_delete02;
            this.btnDel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDel.Location = new System.Drawing.Point(402, 12);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(72, 72);
            this.btnDel.TabIndex = 5;
            this.btnDel.Text = "刪除";
            this.btnDel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::P1618401.Properties.Resources.bt_serch02_png;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSearch.Location = new System.Drawing.Point(246, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(72, 72);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "查詢";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::P1618401.Properties.Resources.bt_cancelt02;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCancel.Location = new System.Drawing.Point(168, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 72);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Image = global::P1618401.Properties.Resources.bt_save02;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSave.Location = new System.Drawing.Point(90, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(72, 72);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "儲存";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnModify
            // 
            this.btnModify.Image = global::P1618401.Properties.Resources.bt_edit02;
            this.btnModify.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnModify.Location = new System.Drawing.Point(12, 12);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(72, 72);
            this.btnModify.TabIndex = 0;
            this.btnModify.Text = "修改";
            this.btnModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // FrmScreen08
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 533);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmScreen08";
            this.Text = "轉檔異常查詢";
            this.Load += new System.EventHandler(this.FrmScreen08_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rabAll;
        private System.Windows.Forms.RadioButton rabBack;
        private System.Windows.Forms.RadioButton rabGoodsOut;
        private System.Windows.Forms.RadioButton rabGoodsIn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtLocalEnd;
        private System.Windows.Forms.TextBox txtLocalStart;
        private System.Windows.Forms.TextBox txtGlobalEnd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDateStart;
        private System.Windows.Forms.TextBox txtGlobalStart;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDateEnd;
        private System.Windows.Forms.TextBox txtCusNo;
        private System.Windows.Forms.Label lblCusNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblStyle;
        private System.Windows.Forms.Label lblTotal;
    }
}