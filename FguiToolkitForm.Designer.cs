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
            this.ckbSearchView = new System.Windows.Forms.CheckBox();
            this.ckbSearchAssets = new System.Windows.Forms.CheckBox();
            this.ckbDeleteAfterSearch = new System.Windows.Forms.CheckBox();
            this.btnSwitchExpPath = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.datagridView1 = new System.Windows.Forms.DataGridView();
            this.btnFguiRoot = new System.Windows.Forms.Button();
            this.txtFguiRoot = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.datagridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPurgeProject
            // 
            this.btnPurgeProject.Location = new System.Drawing.Point(150, 65);
            this.btnPurgeProject.Name = "btnPurgeProject";
            this.btnPurgeProject.Size = new System.Drawing.Size(75, 43);
            this.btnPurgeProject.TabIndex = 0;
            this.btnPurgeProject.Text = "開始";
            this.btnPurgeProject.UseVisualStyleBackColor = true;
            this.btnPurgeProject.Click += new System.EventHandler(this.btnPurgeProj_Click);
            // 
            // ckbSearchView
            // 
            this.ckbSearchView.AutoSize = true;
            this.ckbSearchView.Checked = true;
            this.ckbSearchView.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbSearchView.Location = new System.Drawing.Point(12, 60);
            this.ckbSearchView.Name = "ckbSearchView";
            this.ckbSearchView.Size = new System.Drawing.Size(96, 16);
            this.ckbSearchView.TabIndex = 2;
            this.ckbSearchView.Text = "搜尋無用組件";
            this.ckbSearchView.UseVisualStyleBackColor = true;
            // 
            // ckbSearchAssets
            // 
            this.ckbSearchAssets.AutoSize = true;
            this.ckbSearchAssets.Checked = true;
            this.ckbSearchAssets.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbSearchAssets.Location = new System.Drawing.Point(11, 82);
            this.ckbSearchAssets.Name = "ckbSearchAssets";
            this.ckbSearchAssets.Size = new System.Drawing.Size(96, 16);
            this.ckbSearchAssets.TabIndex = 3;
            this.ckbSearchAssets.Text = "搜尋無用圖檔";
            this.ckbSearchAssets.UseVisualStyleBackColor = true;
            // 
            // ckbDeleteAfterSearch
            // 
            this.ckbDeleteAfterSearch.AutoSize = true;
            this.ckbDeleteAfterSearch.Location = new System.Drawing.Point(12, 104);
            this.ckbDeleteAfterSearch.Name = "ckbDeleteAfterSearch";
            this.ckbDeleteAfterSearch.Size = new System.Drawing.Size(132, 16);
            this.ckbDeleteAfterSearch.TabIndex = 4;
            this.ckbDeleteAfterSearch.Text = "搜尋完成後直接刪除";
            this.ckbDeleteAfterSearch.UseVisualStyleBackColor = true;
            // 
            // btnSwitchExpPath
            // 
            this.btnSwitchExpPath.Location = new System.Drawing.Point(425, 56);
            this.btnSwitchExpPath.Name = "btnSwitchExpPath";
            this.btnSwitchExpPath.Size = new System.Drawing.Size(88, 29);
            this.btnSwitchExpPath.TabIndex = 5;
            this.btnSwitchExpPath.Text = "切換";
            this.btnSwitchExpPath.UseVisualStyleBackColor = true;
            this.btnSwitchExpPath.Click += new System.EventHandler(this.btnSwitchExpPath_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(251, 78);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(168, 20);
            this.comboBox1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(252, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "切換預設導出路徑";
            // 
            // datagridView1
            // 
            this.datagridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.colPath});
            this.datagridView1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datagridView1.Location = new System.Drawing.Point(11, 141);
            this.datagridView1.Name = "datagridView1";
            this.datagridView1.RowHeadersVisible = false;
            this.datagridView1.RowTemplate.Height = 24;
            this.datagridView1.Size = new System.Drawing.Size(503, 441);
            this.datagridView1.TabIndex = 26;
            this.datagridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.datagridview1_CellMouseClick);
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
            this.txtFguiRoot.Size = new System.Drawing.Size(348, 22);
            this.txtFguiRoot.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(425, 91);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 29);
            this.button1.TabIndex = 11;
            this.button1.Text = "批次導出";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
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
            // FguiToolkitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 594);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtFguiRoot);
            this.Controls.Add(this.btnFguiRoot);
            this.Controls.Add(this.datagridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnSwitchExpPath);
            this.Controls.Add(this.ckbDeleteAfterSearch);
            this.Controls.Add(this.ckbSearchAssets);
            this.Controls.Add(this.ckbSearchView);
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
        private System.Windows.Forms.CheckBox ckbSearchView;
        private System.Windows.Forms.CheckBox ckbSearchAssets;
        private System.Windows.Forms.CheckBox ckbDeleteAfterSearch;
        private System.Windows.Forms.Button btnSwitchExpPath;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView datagridView1;
        private System.Windows.Forms.Button btnFguiRoot;
        private System.Windows.Forms.TextBox txtFguiRoot;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPath;
    }
}

