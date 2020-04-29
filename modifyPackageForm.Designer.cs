namespace fgui_toolkit
{
    partial class modifyPackageForm
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
            this.txtExpPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBrExpPath = new System.Windows.Forms.TextBox();
            this.txtPkgName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtExpPath
            // 
            this.txtExpPath.Location = new System.Drawing.Point(101, 60);
            this.txtExpPath.Name = "txtExpPath";
            this.txtExpPath.Size = new System.Drawing.Size(330, 22);
            this.txtExpPath.TabIndex = 11;
            this.txtExpPath.Text = "..\\niuniu_laya\\bin\\NIU\\fgui\\NIU\\master";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "發布路徑";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "分支發布路徑";
            // 
            // txtBrExpPath
            // 
            this.txtBrExpPath.Location = new System.Drawing.Point(101, 87);
            this.txtBrExpPath.Name = "txtBrExpPath";
            this.txtBrExpPath.Size = new System.Drawing.Size(330, 22);
            this.txtBrExpPath.TabIndex = 13;
            this.txtBrExpPath.Text = "..\\niuniu_laya\\bin\\NIU\\fgui\\NIU";
            // 
            // txtPkgName
            // 
            this.txtPkgName.Location = new System.Drawing.Point(101, 33);
            this.txtPkgName.Name = "txtPkgName";
            this.txtPkgName.Size = new System.Drawing.Size(330, 22);
            this.txtPkgName.TabIndex = 15;
            this.txtPkgName.Text = "NIU";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "包名稱";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "組名";
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(101, 6);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(89, 22);
            this.txtGroupName.TabIndex = 17;
            this.txtGroupName.Text = "S001_*_*";
            // 
            // modifyPackageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 123);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtGroupName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPkgName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBrExpPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtExpPath);
            this.Name = "modifyPackageForm";
            this.Text = "修改";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtExpPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBrExpPath;
        private System.Windows.Forms.TextBox txtPkgName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtGroupName;
    }
}