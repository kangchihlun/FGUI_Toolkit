using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace fgui_toolkit
{

    public partial class modifyPackageForm : Form
    {
        // All export path
        private string _iName = "";
        private ExportInfo exportInfo;
        private FguiToolkitForm _parentform;

        public modifyPackageForm(string iName , ExportInfo info ,FguiToolkitForm parentform)
        {
            InitializeComponent();
            this.FormClosing += Form1_Closing;
            this.exportInfo = info;
            this._iName = iName;
            _parentform = parentform;
            txtGroupName.Text = iName;
            txtPkgName.Text = this.exportInfo.PackageName;
            txtExpPath.Text = this.exportInfo.ExportPath;
            txtBrExpPath.Text = this.exportInfo.ExportPath_Branch;
            txtGroupName.Enabled = false;
        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            StringBuilder sb = new StringBuilder(255);
            int t = Global.GetPrivateProfileString("FGUI", "Location", "", sb, 255, Application.StartupPath + "\\Config.ini");
            string FguiLocation = sb.ToString();
            if (FguiLocation.Length > 0)
            {
                this.exportInfo.PackageName = txtPkgName.Text;
                this.exportInfo.ExportPath = txtExpPath.Text;
                this.exportInfo.ExportPath_Branch = txtBrExpPath.Text;   
                string expinfopath = FguiLocation + "\\exportinfo.json";
                
                _parentform.onModifyExpDlgClosing(this._iName, this.exportInfo);
            }
        }
    }
}
