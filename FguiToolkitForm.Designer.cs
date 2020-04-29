namespace fgui_toolkit
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
            this.combo_exp = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.datagridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnFguiRoot = new System.Windows.Forms.Button();
            this.txtFguiRoot = new System.Windows.Forms.TextBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnAddExpPath = new System.Windows.Forms.Button();
            this.btnDelExpSet = new System.Windows.Forms.Button();
            this.btnModifyExp = new System.Windows.Forms.Button();
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
            // combo_exp
            // 
            this.combo_exp.FormattingEnabled = true;
            this.combo_exp.Location = new System.Drawing.Point(14, 71);
            this.combo_exp.Name = "combo_exp";
            this.combo_exp.Size = new System.Drawing.Size(150, 20);
            this.combo_exp.TabIndex = 6;
            this.combo_exp.SelectedIndexChanged += new System.EventHandler(this.combo_exp_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "導出路徑";
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
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(425, 65);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(88, 30);
            this.btnExport.TabIndex = 11;
            this.btnExport.Text = "批次導出";
            this.btnExport.UseVisualStyleBackColor = true;
            // 
            // btnAddExpPath
            // 
            this.btnAddExpPath.Location = new System.Drawing.Point(174, 65);
            this.btnAddExpPath.Name = "btnAddExpPath";
            this.btnAddExpPath.Size = new System.Drawing.Size(30, 30);
            this.btnAddExpPath.TabIndex = 27;
            this.btnAddExpPath.Text = "+";
            this.btnAddExpPath.UseVisualStyleBackColor = true;
            this.btnAddExpPath.Click += new System.EventHandler(this.btnAddExpPath_Click);
            // 
            // btnDelExpSet
            // 
            this.btnDelExpSet.Location = new System.Drawing.Point(242, 65);
            this.btnDelExpSet.Name = "btnDelExpSet";
            this.btnDelExpSet.Size = new System.Drawing.Size(30, 30);
            this.btnDelExpSet.TabIndex = 28;
            this.btnDelExpSet.Text = "-";
            this.btnDelExpSet.UseVisualStyleBackColor = true;
            this.btnDelExpSet.Click += new System.EventHandler(this.btnDelExpSet_Click);
            // 
            // btnModifyExp
            // 
            this.btnModifyExp.Location = new System.Drawing.Point(208, 65);
            this.btnModifyExp.Name = "btnModifyExp";
            this.btnModifyExp.Size = new System.Drawing.Size(30, 30);
            this.btnModifyExp.TabIndex = 29;
            this.btnModifyExp.Text = " ░";
            this.btnModifyExp.UseVisualStyleBackColor = true;
            this.btnModifyExp.Click += new System.EventHandler(this.btnModifyExp_Click);
            // 
            // FguiToolkitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 594);
            this.Controls.Add(this.btnModifyExp);
            this.Controls.Add(this.btnDelExpSet);
            this.Controls.Add(this.btnAddExpPath);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.txtFguiRoot);
            this.Controls.Add(this.btnFguiRoot);
            this.Controls.Add(this.datagridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.combo_exp);
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
        private System.Windows.Forms.ComboBox combo_exp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView datagridView1;
        private System.Windows.Forms.Button btnFguiRoot;
        private System.Windows.Forms.TextBox txtFguiRoot;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPath;
        private System.Windows.Forms.Button btnAddExpPath;
        private System.Windows.Forms.Button btnDelExpSet;
        private System.Windows.Forms.Button btnModifyExp;
    }
}

