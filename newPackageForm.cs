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

    public partial class newPackageForm : Form
    {
                // All export path
        public Dictionary<string,ExportInfo> exportInfoDict = new Dictionary<string,ExportInfo>();
        private FguiToolkitForm _parentform;

        public newPackageForm(Dictionary<string,ExportInfo> info,FguiToolkitForm parentform)
        {
            InitializeComponent();
            this.FormClosing += Form1_Closing;
            this.exportInfoDict = info;
            _parentform = parentform;
        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            StringBuilder sb = new StringBuilder(255);
            int t = Global.GetPrivateProfileString("FGUI", "Location", "", sb, 255, Application.StartupPath + "\\Config.ini");
            string FguiLocation = sb.ToString();
            if (FguiLocation.Length > 0)
            {
                ExportInfo expinfo = new ExportInfo();

                expinfo.PackageName = txtPkgName.Text;
                expinfo.ExportPath = txtExpPath.Text;
                expinfo.ExportPath_Branch = txtBrExpPath.Text;
                exportInfoDict[txtGroupName.Text] = expinfo;
                string expinfopath = FguiLocation + "\\exportinfo.json";
                File.WriteAllText(expinfopath, JsonConvert.SerializeObject(exportInfoDict));
                _parentform.onAddExpDlgClosing(exportInfoDict);
            }
        }
    }
}
