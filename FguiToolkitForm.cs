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

        // All Resources id
        Dictionary<string, FResource> resourceIDDict = new Dictionary<string, FResource>();


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
            datagridView1.Rows.Clear();
            datagridView1.Refresh();
            resourceIDDict.Clear();

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
                    FguiLocation = fproj_loc;
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
            datagridView1.Rows.Clear();
            datagridView1.Refresh();
            resourceIDDict.Clear();

            List<string> dirs = Directory.GetDirectories(FguiLocation, "*", SearchOption.TopDirectoryOnly).ToList();
            foreach (string dir in dirs)
            {
                string lastfolderName = Path.GetFileName(dir);
                if (lastfolderName != "assets") continue;
                doPurgeProj(dir);
            }
        }

        private void doPurgeProj(string assetspath)
        {
            if (!Directory.Exists(assetspath)) return;

            List<string> dirs = Directory.GetDirectories(assetspath, "*", SearchOption.TopDirectoryOnly).ToList();
            if (dirs.Count < 1) return;
            string[] fileEntries = Directory.GetFiles(dirs[0], "*.xml");

            // 特別判斷 font res
            Dictionary<string, FResource> fontDict = new Dictionary<string, FResource>();

            // 單獨輸出的 comp 收集
            Dictionary<string, FResource> exportedComp = new Dictionary<string, FResource>();

            // root 
            string lroot = dirs[0];
            #region read package.xml
            string packageFileLocation = "";
            foreach (string fileName in fileEntries)
            {
                string curFile = Path.GetFileName(fileName).Split('.')[0];
                if (!Global.ContainStr(curFile, "package")) continue;
                packageFileLocation = fileName;
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
                                if (null != res.Attribute("exported"))
                                    img.bUsed = res.Attribute("exported").Value=="true"?true:false;
                                resourceIDDict[id] = img;
                            }
                            else
                            {
                                FResource resource = new FResource();
                                resource.id = id;
                                resource.name = name;
                                resource.path = path;
                                if ("/" == path)
                                    resource.bUsed = true;
                                if(res.Name.ToString() == "folder") // 這個不知道要幹嘛用的
                                    resource.bUsed = true;

                                if (null != res.Attribute("exported"))
                                {
                                    resource.bUsed = res.Attribute("exported").Value == "true" ? true : false;
                                    exportedComp[lroot + path + name] = resource;
                                }
                                resourceIDDict[id] = resource;
                                if (res.Name.ToString() == "font") // 特別判斷 font，拉到外面再循環檢查一次
                                    fontDict[lroot + path + name] = resource;
                            }
                        }
                    }
                }
            }
            #endregion

            #region read main scene xml
            
            // Scan Fonts
            foreach (string fonts in fontDict.Keys)
            {
                checkFont(fonts);
            }
            // Scan Exported Components
            foreach (string xc in exportedComp.Keys)
            {
                checkResInUseRecursive(xc, lroot);
            }

            #endregion

            #region read package_branch 分支

            Dictionary<string, FResource> resdict_branch_unused = new Dictionary<string, FResource>();
            DirectoryInfo di = new DirectoryInfo(assetspath);
            Console.WriteLine(di.Parent.FullName);
            List<string> assetsdirs = Directory.GetDirectories(di.Parent.FullName, "*", SearchOption.TopDirectoryOnly).ToList();
            foreach (string dir in assetsdirs)
            {
                if (Global.ContainStr(dir,"assets_"))
                {
                    List<string> assSubds = Directory.GetDirectories(dir, "*", SearchOption.TopDirectoryOnly).ToList();
                    if (assSubds.Count < 1) return;
                    string[] ffs = Directory.GetFiles(assSubds[0], "*.xml");
                    foreach (string psn in ffs)
                    {
                        string curFile = Path.GetFileName(psn).Split('.')[0];
                        if (!Global.ContainStr(curFile, "package_branch")) continue;
                        XElement rootElement = XElement.Load(psn);
                        foreach (XElement childElement in rootElement.Elements())
                        {
                            if (childElement.Name.ToString() == "resources")
                            {
                                foreach (XElement res in childElement.Elements())
                                {
                                    string name = res.Attribute("name").Value;
                                    string path = res.Attribute("path").Value;
                                    foreach (string id in resourceIDDict.Keys)
                                    {
                                        if( resourceIDDict[id].path == path &&
                                            resourceIDDict[id].name == name )
                                        {
                                            if (!resourceIDDict[id].bUsed)
                                            {
                                                string rresid = res.Attribute("id").Value;
                                                FResource resource = new FResource();
                                                resource.id = id;
                                                resource.name = name;
                                                resource.path = Path.GetDirectoryName(psn)+path;
                                                resdict_branch_unused[rresid] = resource;

                                                //Console.WriteLine(resourceIDDict[id].name);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            #endregion

            // final result
            foreach (string id in resourceIDDict.Keys)
            {
                if (!resourceIDDict[id].bUsed)
                {
                    string fullpath = Path.GetDirectoryName(packageFileLocation)+resourceIDDict[id].path.Replace('/', '\\') + resourceIDDict[id].name;
                    Global.UI(resourceIDDict[id].id,resourceIDDict[id].name,fullpath) ;
                }
            }
            // 分支
            foreach (string id in resdict_branch_unused.Keys)
            {
                if (!resdict_branch_unused[id].bUsed)
                {
                    string fullpath = resdict_branch_unused[id].path.Replace('/', '\\') + resdict_branch_unused[id].name;
                    Global.UI(resdict_branch_unused[id].id, resdict_branch_unused[id].name, fullpath);
                }
            }
        }

        private void checkResInUseRecursive(string fileName,string root)
        {
            if (!File.Exists(fileName)) return;
            string curext = Path.GetExtension(fileName);
            if (curext != ".xml") return;
            XElement rootElement = XElement.Load(fileName);
            foreach (XElement childElement in rootElement.Elements())
            {
                if (childElement.Name.ToString() == "displayList")
                {
                    foreach (XElement res in childElement.Elements())
                    {
                        if (null != res.Attribute("fileName"))
                        {
                            string extname = Path.GetExtension(res.Attribute("fileName").Value);
                            if(extname == ".xml")
                            {
                                if (null != res.Attribute("src"))
                                {
                                    string src = res.Attribute("src").Value;
                                    if (resourceIDDict.ContainsKey(src))
                                    {
                                        resourceIDDict[src].bUsed = true;
                                    }
                                }
                                string subpath = root+"\\"+res.Attribute("fileName").Value.Replace('/','\\');
                                checkResInUseRecursive(subpath,root);
                            }
                            else if (null != res.Attribute("src"))
                            {
                                string src = res.Attribute("src").Value;
                                if (resourceIDDict.ContainsKey(src))
                                {
                                    resourceIDDict[src].bUsed = true;
                                }
                            }
                        }
                        
                    }
                }
            }
        }

        private void checkFont(string fileName)
        {
            if (!File.Exists(fileName)) return;
            string curext = Path.GetExtension(fileName);
            if (curext != ".fnt") return;
            using (var reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    List<string> linespl = line.Split(' ').ToList();
                    if(Global.ContainStr(linespl[0], "char"))
                    {
                        string img = linespl[2].Split('=')[1];
                        if(resourceIDDict.ContainsKey(img))
                            resourceIDDict[img].bUsed = true;
                    }
                }
            }
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
            datagridView1.Rows.Add(new string[] { name,path });

        }

        private void datagridview1_CellMouseDbClick(object sender, System.Windows.Forms.DataGridViewCellMouseEventArgs e)
        {
            DataGridView view = (DataGridView)sender;
            if (e.Button == MouseButtons.Left)
            {
                if (e.RowIndex < 0) return;
                view.Rows[e.RowIndex].Selected = true;
                if (null != view.Rows[e.RowIndex].Cells[0].Value)
                {
                    string path = view.Rows[e.RowIndex].Cells[1].Value.ToString();
                    System.Diagnostics.Process.Start("explorer.exe", string.Format("/select,\"{0}\"", view.Rows[e.RowIndex].Cells[1].Value.ToString()));
                }
            }
        }

        private string curRClickPath = "";
        private void datagridview1_CellMouseClick(object sender, System.Windows.Forms.DataGridViewCellMouseEventArgs e)
        {
            DataGridView view = (DataGridView)sender;
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex < 0) return;
                view.Rows[e.RowIndex].Selected = true;
                if (null != view.Rows[e.RowIndex].Cells[0].Value)
                {
                    string path = view.Rows[e.RowIndex].Cells[1].Value.ToString();
                    System.Drawing.Rectangle r = view.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    curRClickPath = path;
                    MenuItem DelMenu = new MenuItem("複製");
                    DelMenu.Click += new System.EventHandler(this.mi_romenuclick);
                    List<MenuItem> menuItems = new List<MenuItem>() { DelMenu };
                    ContextMenu buttonMenu = new ContextMenu(menuItems.ToArray());
                    buttonMenu.Show((Control)sender, (new System.Drawing.Point(r.Left + e.X, r.Top + e.Y)), LeftRightAlignment.Left);
                }
            }
        }
        private void mi_romenuclick(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(curRClickPath);
        }
    }
}
