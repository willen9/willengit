namespace P1618401
{
    partial class FrmScreen09
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rabAll = new System.Windows.Forms.RadioButton();
            this.rabBack = new System.Windows.Forms.RadioButton();
            this.rabGoodsOut = new System.Windows.Forms.RadioButton();
            this.rabGoodsIn = new System.Windows.Forms.RadioButton();
            this.txtDateEnd = new System.Windows.Forms.TextBox();
            this.txtDateStart = new System.Windows.Forms.TextBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(846, 493);
            this.panel1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 179);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(846, 314);
            this.dataGridView1.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rabAll);
            this.groupBox1.Controls.Add(this.rabBack);
            this.groupBox1.Controls.Add(this.rabGoodsOut);
            this.groupBox1.Controls.Add(this.rabGoodsIn);
            this.groupBox1.Controls.Add(this.txtDateEnd);
            this.groupBox1.Controls.Add(this.txtDateStart);
            this.groupBox1.Controls.Add(this.lblCount);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(846, 94);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // rabAll
            // 
            this.rabAll.AutoSize = true;
            this.rabAll.Checked = true;
            this.rabAll.Location = new System.Drawing.Point(42, 55);
            this.rabAll.Name = "rabAll";
            this.rabAll.Size = new System.Drawing.Size(58, 20);
            this.rabAll.TabIndex = 6;
            this.rabAll.TabStop = true;
            this.rabAll.Text = "全部";
            this.rabAll.UseVisualStyleBackColor = true;
            // 
            // rabBack
            // 
            this.rabBack.AutoSize = true;
            this.rabBack.Location = new System.Drawing.Point(363, 55);
            this.rabBack.Name = "rabBack";
            this.rabBack.Size = new System.Drawing.Size(58, 20);
            this.rabBack.TabIndex = 9;
            this.rabBack.TabStop = true;
            this.rabBack.Text = "退回";
            this.rabBack.UseVisualStyleBackColor = true;
            // 
            // rabGoodsOut
            // 
            this.rabGoodsOut.AutoSize = true;
            this.rabGoodsOut.Location = new System.Drawing.Point(256, 55);
            this.rabGoodsOut.Name = "rabGoodsOut";
            this.rabGoodsOut.Size = new System.Drawing.Size(58, 20);
            this.rabGoodsOut.TabIndex = 8;
            this.rabGoodsOut.TabStop = true;
            this.rabGoodsOut.Text = "出貨";
            this.rabGoodsOut.UseVisualStyleBackColor = true;
            // 
            // rabGoodsIn
            // 
            this.rabGoodsIn.AutoSize = true;
            this.rabGoodsIn.Location = new System.Drawing.Point(149, 55);
            this.rabGoodsIn.Name = "rabGoodsIn";
            this.rabGoodsIn.Size = new System.Drawing.Size(58, 20);
            this.rabGoodsIn.TabIndex = 7;
            this.rabGoodsIn.TabStop = true;
            this.rabGoodsIn.Text = "進貨";
            this.rabGoodsIn.UseVisualStyleBackColor = true;
            // 
            // txtDateEnd
            // 
            this.txtDateEnd.Location = new System.Drawing.Point(310, 18);
            this.txtDateEnd.MaxLength = 8;
            this.txtDateEnd.Name = "txtDateEnd";
            this.txtDateEnd.Size = new System.Drawing.Size(151, 27);
            this.txtDateEnd.TabIndex = 5;
            // 
            // txtDateStart
            // 
            this.txtDateStart.Location = new System.Drawing.Point(106, 18);
            this.txtDateStart.MaxLength = 8;
            this.txtDateStart.Name = "txtDateStart";
            this.txtDateStart.Size = new System.Drawing.Size(151, 27);
            this.txtDateStart.TabIndex = 4;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("PMingLiU", 14F);
            this.lblCount.Location = new System.Drawing.Point(632, 21);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(80, 19);
            this.lblCount.TabIndex = 11;
            this.lblCount.Text = "筆數:0筆";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("PMingLiU", 14F);
            this.label5.Location = new System.Drawing.Point(14, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 19);
            this.label5.TabIndex = 11;
            this.label5.Text = "查詢日期:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("PMingLiU", 30F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(255, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 40);
            this.label6.TabIndex = 14;
            this.label6.Text = "—";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnReturn);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnPrint);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(846, 85);
            this.panel2.TabIndex = 0;
            // 
            // btnReturn
            // 
            this.btnReturn.Image = global::P1618401.Properties.Resources._1479735022_Revert;
            this.btnReturn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnReturn.Location = new System.Drawing.Point(246, 12);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(72, 72);
            this.btnReturn.TabIndex = 3;
            this.btnReturn.Text = "返回";
            this.btnReturn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::P1618401.Properties.Resources.bt_serch02_png;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSearch.Location = new System.Drawing.Point(12, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(72, 72);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "搜尋";
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
            // btnPrint
            // 
            this.btnPrint.Image = global::P1618401.Properties.Resources.print_icon;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPrint.Location = new System.Drawing.Point(90, 12);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(72, 72);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "列印";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // FrmScreen09
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 493);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmScreen09";
            this.Text = "轉檔異常查詢";
            this.Load += new System.EventHandler(this.FrmScreen09_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.TextBox txtDateEnd;
        private System.Windows.Forms.TextBox txtDateStart;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rabAll;
        private System.Windows.Forms.RadioButton rabBack;
        private System.Windows.Forms.RadioButton rabGoodsOut;
        private System.Windows.Forms.RadioButton rabGoodsIn;
        private System.Windows.Forms.Label lblCount;
    }
}