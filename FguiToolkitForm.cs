using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security;
using System.Xml.Linq;

namespace fgui_toolkit
{
    public partial class FguiToolkitForm : Form
    {
        private string FguiLocation = "";
        private AutoResetEvent UIEvent = new AutoResetEvent(false);

        public FguiToolkitForm()
        {
            InitializeComponent();
            LoadConfig();
            Global.UI = new Global.InvokeUI(UpdateUI);
        }

        protected void LoadConfig()
        {
            StringBuilder sb = new StringBuilder(255);
            int t = Global.GetPrivateProfileString("FGUI", "Location", "", sb, 255, Application.StartupPath + "\\Config.ini");
            FguiLocation = sb.ToString();
            txtFguiRoot.Text = FguiLocation;
        }

        private void btnFguiRoot_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog()
            {
                FileName = "Select a fairy file",
                Filter = "FGUI files (*.fairy)|*.fairy",
                Title = "Open fairy file"
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fproj_loc = System.IO.Path.GetDirectoryName(openFileDialog1.FileName);
                    txtFguiRoot.Text = fproj_loc;
                    Global.WritePrivateProfileString("FGUI", "Location", fproj_loc, Application.StartupPath + "\\Config.ini");
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show("不是一個 .fairy 檔案 ");
                }
            }


        }

        private void btnPurgeProj_Click(object sender, EventArgs e)
        {
            
            string assetspath = FguiLocation + "\\assets";
            doPurgeProj(assetspath);

        }

        private void doPurgeProj(string assetspath)
        {
            bool bSearchViews = ckbSearchView.Checked;
            bool bSearchAssets = ckbSearchAssets.Checked;
            bool bDeleteAfterSearch = ckbDeleteAfterSearch.Checked;

            if (!Directory.Exists(assetspath)) return;

            List<string> dirs = Directory.GetDirectories(assetspath, "*", SearchOption.TopDirectoryOnly).ToList();
            if (dirs.Count < 1) return;
            string[] fileEntries = Directory.GetFiles(dirs[0], "*.xml");

            // All Resources id
            Dictionary<string, FResource> resourceIDDict = new Dictionary<string, FResource>();

            #region read package.xml
            foreach (string fileName in fileEntries)
            {
                string curFile = Path.GetFileName(fileName).Split('.')[0];
                if (!Global.ContainStr(curFile, "package")) continue;

                XElement rootElement = XElement.Load(fileName);
                foreach (XElement childElement in rootElement.Elements())
                {
                    if (childElement.Name.ToString() == "resources")
                    {
                        foreach (XElement res in childElement.Elements())
                        {
                            string id = res.Attribute("id").Value;
                            string name = res.Attribute("name").Value;
                            string path = res.Attribute("path").Value;
                            if (res.Name.ToString() == "image")
                            {
                                FImage img = new FImage();
                                img.id = id;
                                img.name = name;
                                img.path = path;
                                if (null != res.Attribute("qualityOption"))
                                    img.qualityOption = res.Attribute("qualityOption").Value;
                                if (null != res.Attribute("quality"))
                                    img.quality = res.Attribute("quality").Value;
                                if (null != res.Attribute("atlas"))
                                    img.atlas = res.Attribute("atlas").Value;
                                resourceIDDict[id] = img;
                            }
                            else
                            {
                                FResource resource = new FResource();
                                resource.id = id;
                                resource.name = name;
                                resource.path = path;
                                resourceIDDict[id] = resource;
                            }
                        }
                    }
                }
            }
            #endregion

            #region read other xml
            foreach (string fileName in fileEntries)
            {
                string curFile = Path.GetFileName(fileName).Split('.')[0];
                if (Global.ContainStr(curFile, "package")) continue;

                XElement rootElement = XElement.Load(fileName);
                foreach (XElement childElement in rootElement.Elements())
                {
                    if (childElement.Name.ToString() == "displayList")
                    {
                        foreach (XElement res in childElement.Elements())
                        {
                            string id = res.Attribute("id").Value;
                            string name = res.Attribute("name").Value;
                            Console.WriteLine(id + " -- " + name);
                        }
                    }
                }
            }
            #endregion
            //Global.UI("", "", res.Attribute("name").ToString());
        }

        private void btnSwitchExpPath_Click(object sender, EventArgs e)
        {

        }

        public void UpdateUI(string wParam, string lParam1, string lParam2)
        {
            UIEvent.WaitOne(1000);
            try
            {
                if (this.InvokeRequired)
                {
                    Global.InvokeUI Update = new Global.InvokeUI(UpdateUI);
                    this.BeginInvoke(Update, wParam, lParam1, lParam2);
                }
                else
                {
                    DebugView(wParam,lParam1, lParam2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("UpdateUI " + ex.ToString());
            }
            UIEvent.Set();
        }

        private void DebugView(string id, string name,string path)
        {
            ListViewItem NListView = new ListViewItem();
            NListView.SubItems.Add(new ListViewItem.ListViewSubItem().Text = id);
            NListView.SubItems.Add(new ListViewItem.ListViewSubItem().Text = name);
            NListView.SubItems.Add(new ListViewItem.ListViewSubItem().Text = path);
            datagridView1.Rows.Add(new string[] { id,name,path });

        }

        private void datagridview1_CellMouseClick(object sender, System.Windows.Forms.DataGridViewCellMouseEventArgs e)
        {
            DataGridView view = (DataGridView)sender;
            if (e.Button == MouseButtons.Left)
            {
                if (e.RowIndex < 0) return;
                view.Rows[e.RowIndex].Selected = true;
            }
        }

    }
}
