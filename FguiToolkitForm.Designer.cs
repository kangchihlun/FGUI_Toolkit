﻿namespace fgui_toolkit
{
    partial class FguiToolkitForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPurgeProject = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.datagridView1 = new System.Windows.Forms.DataGridView();
            this.btnFguiRoot = new System.Windows.Forms.Button();
            this.txtFguiRoot = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddExpPath = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.datagridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPurgeProject
            // 
            this.btnPurgeProject.Location = new System.Drawing.Point(421, 9);
            this.btnPurgeProject.Name = "btnPurgeProject";
            this.btnPurgeProject.Size = new System.Drawing.Size(92, 28);
            this.btnPurgeProject.TabIndex = 0;
            this.btnPurgeProject.Text = "搜尋無用組件";
            this.btnPurgeProject.UseVisualStyleBackColor = true;
            this.btnPurgeProject.Click += new System.EventHandler(this.btnPurgeProj_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(11, 70);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(150, 20);
            this.comboBox1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "切換預設導出路徑";
            // 
            // datagridView1
            // 
            this.datagridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.colPath});
            this.datagridView1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datagridView1.Location = new System.Drawing.Point(11, 108);
            this.datagridView1.Name = "datagridView1";
            this.datagridView1.RowHeadersVisible = false;
            this.datagridView1.RowTemplate.Height = 24;
            this.datagridView1.Size = new System.Drawing.Size(503, 474);
            this.datagridView1.TabIndex = 26;
            this.datagridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.datagridview1_CellMouseClick);
            this.datagridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.datagridview1_CellMouseDbClick);

            // 
            // btnFguiRoot
            // 
            this.btnFguiRoot.Location = new System.Drawing.Point(11, 9);
            this.btnFguiRoot.Name = "btnFguiRoot";
            this.btnFguiRoot.Size = new System.Drawing.Size(68, 27);
            this.btnFguiRoot.TabIndex = 9;
            this.btnFguiRoot.Text = "fgui專案";
            this.btnFguiRoot.UseVisualStyleBackColor = true;
            this.btnFguiRoot.Click += new System.EventHandler(this.btnFguiRoot_Click);
            // 
            // txtFguiRoot
            // 
            this.txtFguiRoot.Location = new System.Drawing.Point(85, 12);
            this.txtFguiRoot.Name = "txtFguiRoot";
            this.txtFguiRoot.Size = new System.Drawing.Size(330, 22);
            this.txtFguiRoot.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(277, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 30);
            this.button1.TabIndex = 11;
            this.button1.Text = "批次導出";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "名稱";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // colPath
            // 
            this.colPath.HeaderText = "路徑";
            this.colPath.Name = "colPath";
            this.colPath.Width = 500;
            // 
            // btnAddExpPath
            // 
            this.btnAddExpPath.Location = new System.Drawing.Point(167, 62);
            this.btnAddExpPath.Name = "btnAddExpPath";
            this.btnAddExpPath.Size = new System.Drawing.Size(49, 29);
            this.btnAddExpPath.TabIndex = 27;
            this.btnAddExpPath.Text = "+";
            this.btnAddExpPath.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(222, 62);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(49, 29);
            this.button2.TabIndex = 28;
            this.button2.Text = "+";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // FguiToolkitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 594);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnAddExpPath);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtFguiRoot);
            this.Controls.Add(this.btnFguiRoot);
            this.Controls.Add(this.datagridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnPurgeProject);
            this.MaximumSize = new System.Drawing.Size(541, 633);
            this.MinimumSize = new System.Drawing.Size(541, 633);
            this.Name = "FguiToolkitForm";
            this.Text = "FGui_Toolkit    Made by Kang";
            ((System.ComponentModel.ISupportInitialize)(this.datagridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPurgeProject;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView datagridView1;
        private System.Windows.Forms.Button btnFguiRoot;
        private System.Windows.Forms.TextBox txtFguiRoot;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPath;
        private System.Windows.Forms.Button btnAddExpPath;
        private System.Windows.Forms.Button button2;
    }
}

