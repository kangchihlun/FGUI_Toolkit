﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Security;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace fgui_toolkit
{
    public partial class FguiToolkitForm : Form
    {
        private string exeLocation = "";

        private string FguiLocation = "";
        private AutoResetEvent UIEvent = new AutoResetEvent(false);

        private string packageDesc = "";

        // All Resources id
        Dictionary<string, FResource> resourceIDDict = new Dictionary<string, FResource>();

        // All export path
        public Dictionary<string, ExportInfo> exportInfoDict = new Dictionary<string, ExportInfo>();

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

            t = Global.GetPrivateProfileString("FGUI", "Exe", "", sb, 255, Application.StartupPath + "\\Config.ini");
            exeLocation = sb.ToString();

            exportInfoDict.Clear();
            if (FguiLocation.Length > 0)
            {
                string expinfopath = FguiLocation + "\\settings\\exportinfo.json";
                if (File.Exists(expinfopath))
                {
                    exportInfoDict = JsonConvert.DeserializeObject<Dictionary<string, ExportInfo>>(File.ReadAllText(expinfopath));

                }
            }
            updateCombItem();
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
                    Console.WriteLine(ex);
                }
            }
            LoadConfig();
        }

        private void fetchFairyExePath()
        {
            if (exeLocation.Length > 0)
            {
                if (File.Exists(exeLocation)) return;
            }

            var process = Process.GetCurrentProcess(); // Or whatever method you are using
            string fullPath = process.MainModule.FileName;

            foreach (Process PPath in Process.GetProcesses())
            {
                if (PPath.ProcessName.ToString() == "FairyGUI-Editor")
                {
                    exeLocation = PPath.MainModule.FileName;
                    break;
                }
            }
            if (exeLocation.Length > 0)
            {
                Global.WritePrivateProfileString("FGUI", "Exe", exeLocation, Application.StartupPath + "\\Config.ini");
            }
            else
            {
                MessageBox.Show("無法找到FairyGUI.exe，請先開啟專案");
            }
        }

        private bool isFairyRunning()
        {
            var process = Process.GetCurrentProcess(); // Or whatever method you are using
            string fullPath = process.MainModule.FileName;
            bool bFound = false;
            foreach (Process PPath in Process.GetProcesses())
            {
                if (PPath.ProcessName.ToString() == "FairyGUI-Editor")
                {
                    bFound = true;
                    break;
                }
            }
            return bFound;
        }

        private void btnPurgeProj_Click(object sender, EventArgs e)
        {
            datagridView1.Rows.Clear();
            datagridView1.Refresh();
            resourceIDDict.Clear();
            if (FguiLocation.Length<1) return;
            List<string> dirs = Directory.GetDirectories(FguiLocation, "*", SearchOption.TopDirectoryOnly).ToList();
            foreach (string dir in dirs)
            {
                string lastfolderName = Path.GetFileName(dir);
                if (lastfolderName != "assets") continue;
                doPurgeProj(dir);
            }
            fetchFairyExePath();
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

                if (rootElement.Name.ToString() == "packageDescription")
                    packageDesc = rootElement.Attribute("id").Value;

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
                                    img.bUsed = res.Attribute("exported").Value == "true" ? true : false;
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
                                if (res.Name.ToString() == "folder") // 這個不知道要幹嘛用的
                                    resource.bUsed = true;

                                if (res.Name.ToString() == "sound") //聲音檔先暫時略過，還得搜索 transition 裡面的
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
            //Console.WriteLine(di.Parent.FullName);
            List<string> assetsdirs = Directory.GetDirectories(di.Parent.FullName, "*", SearchOption.TopDirectoryOnly).ToList();
            foreach (string dir in assetsdirs)
            {
                if (Global.ContainStr(dir, "assets_"))
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
                                        if (resourceIDDict[id].path == path &&
                                            resourceIDDict[id].name == name)
                                        {
                                            if (!resourceIDDict[id].bUsed)
                                            {
                                                string rresid = res.Attribute("id").Value;
                                                FResource resource = new FResource();
                                                resource.id = id;
                                                resource.name = name;
                                                resource.path = Path.GetDirectoryName(psn) + path;
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
                    string fullpath = Path.GetDirectoryName(packageFileLocation) + resourceIDDict[id].path.Replace('/', '\\') + resourceIDDict[id].name;
                    Global.UI(resourceIDDict[id].id, resourceIDDict[id].name, fullpath);
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

        private void checkResInUseRecursive(string fileName, string root)
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
#if DEBUG
                            if (Path.GetFileName(fileName) == "dialogRoad.xml")
                            {
                                Console.WriteLine("2314");
                            }
                            //Console.WriteLine(Path.GetFileName(fileName));
#endif

                            // 發現一個致命的錯誤，直接查找位於xml內 filename 這個屬性有可能是錯的，一定要從src再回去 package 裡面找
                            string extname = Path.GetExtension(res.Attribute("fileName").Value);
                            if (extname == ".xml")
                            {
                                if (null != res.Attribute("src"))
                                {
                                    string src = res.Attribute("src").Value;
                                    if (resourceIDDict.ContainsKey(src))
                                    {
                                        resourceIDDict[src].bUsed = true;
                                    }

                                    string path_ = resourceIDDict[src].path;
                                    string name_ = resourceIDDict[src].name;
                                    checkResInUseRecursive(root + path_ + name_, root);
                                }
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
                        else if ("list" == res.Name)
                        {
                            // list content should be recursive
                            string tgtsid = "";
                            if (null != res.Attribute("defaultItem"))
                            {
                                string strdfitm = res.Attribute("defaultItem").Value.ToString();
                                string dfitm = strdfitm.Substring(5);

                                tgtsid = dfitm.Substring(packageDesc.Length);
                                if (resourceIDDict.ContainsKey(tgtsid))
                                {
                                    resourceIDDict[tgtsid].bUsed = true;
                                    string path_ = resourceIDDict[tgtsid].path;
                                    string name_ = resourceIDDict[tgtsid].name;
                                    checkResInUseRecursive(root + path_ + name_, root);
                                }
                            }

                            foreach (XElement ls in res.Elements())
                            {
                                if (ls.Name.ToString() == "item")
                                {
                                    if (null != ls.Attribute("url"))
                                    {
                                        string strdfitm = ls.Attribute("url").Value.ToString();
                                        string dfitm = strdfitm.Substring(5);

                                        tgtsid = dfitm.Substring(packageDesc.Length);
                                        if (resourceIDDict.ContainsKey(tgtsid))
                                        {
                                            resourceIDDict[tgtsid].bUsed = true;
                                            string path_ = resourceIDDict[tgtsid].path;
                                            string name_ = resourceIDDict[tgtsid].name;
                                            checkResInUseRecursive(root + path_ + name_, root);
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
                else if ("Button" == childElement.Name)
                {
                    if (null != childElement.Attribute("sound"))
                    {
                        string strdfitm = childElement.Attribute("sound").Value.ToString();
                        string dfitm = strdfitm.Substring(5);
                        string tgtsid = dfitm.Substring(packageDesc.Length);
                        if (resourceIDDict.ContainsKey(tgtsid))
                        {
                            resourceIDDict[tgtsid].bUsed = true;
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
                    if (Global.ContainStr(linespl[0], "char"))
                    {
                        string img = linespl[2].Split('=')[1];
                        if (resourceIDDict.ContainsKey(img))
                            resourceIDDict[img].bUsed = true;
                    }
                }
            }
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
                    DebugView(wParam, lParam1, lParam2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("UpdateUI " + ex.ToString());
            }
            UIEvent.Set();
        }

        private void DebugView(string id, string name, string path)
        {
            ListViewItem NListView = new ListViewItem();
            NListView.SubItems.Add(new ListViewItem.ListViewSubItem().Text = id);
            NListView.SubItems.Add(new ListViewItem.ListViewSubItem().Text = name);
            NListView.SubItems.Add(new ListViewItem.ListViewSubItem().Text = path);
            datagridView1.Rows.Add(new string[] { name, path });

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

        private void btnAddExpPath_Click(object sender, EventArgs e)
        {
            newPackageForm pf = new newPackageForm(this.exportInfoDict, this);
            pf.TopMost = true;
            pf.StartPosition = FormStartPosition.Manual;
            pf.Location = new Point(this.Location.X, this.Location.Y + 10);
            pf.ShowDialog();
        }

        private void updateCombItem()
        {
            this.combo_exp.Items.Clear();
            this.combo_exp.ResetText();
            foreach (string ko in exportInfoDict.Keys)
            {
                this.combo_exp.Items.Add(ko);
            }
            this.combo_exp.SelectedIndex = this.combo_exp.Items.Count - 1;
        }

        private void btnDelExpSet_Click(object sender, EventArgs e)
        {
            if (combo_exp.Items.Count < 1) return;
            //Console.WriteLine(combo_exp.Items[combo_exp.SelectedIndex].ToString());
            this.exportInfoDict.Remove(combo_exp.Items[combo_exp.SelectedIndex].ToString());

            updateCombItem();
        }

        public void onAddExpDlgClosing(Dictionary<string, ExportInfo> info)
        {
            this.exportInfoDict = info;
            updateCombItem();
        }

        private void btnModifyExp_Click(object sender, EventArgs e)
        {
            if (this.combo_exp.Items.Count < 1) return;
            string _iName = this.combo_exp.Items[this.combo_exp.SelectedIndex].ToString();
            ExportInfo info = exportInfoDict[_iName];
            modifyPackageForm mf = new modifyPackageForm(_iName, info, this);
            mf.TopMost = true;
            mf.StartPosition = FormStartPosition.Manual;
            mf.Location = new Point(this.Location.X, this.Location.Y + 10);
            mf.ShowDialog();
        }

        public void onModifyExpDlgClosing(string _iName, ExportInfo info)
        {
            if (exportInfoDict.ContainsKey(_iName))
            {
                exportInfoDict[_iName] = info;
                string expinfopath = FguiLocation + "\\settings\\exportinfo.json";
                File.WriteAllText(expinfopath, JsonConvert.SerializeObject(exportInfoDict));
            }
        }

        private void modifyExportPath(ExportInfo info)
        {
            List<string> dirs = Directory.GetDirectories(FguiLocation, "*", SearchOption.TopDirectoryOnly).ToList();
            foreach (string dir in dirs)
            {
                string lastfolderName = Path.GetFileName(dir);
                if (Global.ContainStr(lastfolderName, "assets"))
                {
                    List<string> sdirs = Directory.GetDirectories(dir, "*", SearchOption.TopDirectoryOnly).ToList();
                    if (sdirs.Count < 1) return;
                    string[] fileEntries = Directory.GetFiles(sdirs[0], "*.xml");
                    foreach (string fileName in fileEntries)
                    {
                        string curFile = Path.GetFileName(fileName).Split('.')[0];
                        if (!Global.ContainStr(curFile, "package")) continue;
                        //Console.WriteLine(fileName);
                        XDocument xmlFile = XDocument.Load(fileName);
                        var query = from c in xmlFile.Elements("packageDescription").Elements("publish") select c;
                        foreach (XElement book in query)
                        {
                            book.Attribute("name").Value = info.PackageName;
                        }
                        xmlFile.Save(fileName);
                    }
                }

                if (Global.ContainStr(lastfolderName, "settings"))
                {
                    string settingjsonpath = dir + "/Publish.json";
                    if (!File.Exists(settingjsonpath)) continue;
                    //Console.WriteLine(settingjsonpath);
                    JObject publishjson = JObject.Parse(File.ReadAllText(settingjsonpath));
                    publishjson["path"] = info.ExportPath;
                    publishjson["branchPath"] = info.ExportPath_Branch;
                    using (FileStream fs = File.Open(settingjsonpath, FileMode.OpenOrCreate))
                    using (StreamWriter sw = new StreamWriter(fs))
                    using (JsonTextWriter jw = new JsonTextWriter(sw))
                    {
                        jw.Formatting = Formatting.Indented;
                        //jw.IndentChar = '\t';
                        //jw.Indentation = 1;
                        publishjson.WriteTo(jw);
                    }
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (isFairyRunning())
            {
                MessageBox.Show("請先關閉 FairyGui Editor 以繼續");
                return;
            }

            foreach (var proc in Process.GetProcessesByName("FairyGUI-Editor"))
            {
                proc.Kill();
            }
            if (exeLocation.Length < 1)
            {
                MessageBox.Show("請先執行場景資源掃描以繼續");
                return;
            }
            if (!File.Exists(exeLocation)) return;

            string selstr = combo_exp.Items[combo_exp.SelectedIndex].ToString();
            ExportInfo expinfo = new ExportInfo();
            if (selstr.Length > 0)
                expinfo = exportInfoDict[selstr];

            modifyExportPath(expinfo);

            // open the export target folder
            string tgtdir = retrieveDir(FguiLocation, expinfo.ExportPath_Branch.Replace('\\', '/'));
            if (Directory.Exists(tgtdir))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = tgtdir,
                    FileName = "explorer.exe"
                };
                Process.Start(startInfo);
            }

            // Start Export Thread
            ParameterizedThreadStart starterp = new ParameterizedThreadStart(startexportThread);
            Thread a = new Thread(starterp);
            ExportThreadParm parm = new ExportThreadParm();
            parm.selstr = selstr;
            parm.ExportPath_Branch = expinfo.ExportPath_Branch;
            a.Start(parm);

        }
        
        private string retrieveDir(string root , string inpath)
        {
            DirectoryInfo di = new DirectoryInfo(root);
            string s_path = inpath;
            while (s_path.Substring(0,3)=="../")
            {
                s_path = s_path.Substring(3);
                di = Directory.GetParent(di.FullName);
            }
            return (di.FullName + '/' + s_path).Replace('/','\\');
        }

        private void startexportThread(object parm)
        {
            ExportThreadParm expparm = (ExportThreadParm)parm;
            List<string> dirs = Directory.GetDirectories(FguiLocation, "*", SearchOption.TopDirectoryOnly).ToList();
            // branch exported first
            foreach (string dir in dirs)
            {
                string lastfolderName = Path.GetFileName(dir);
                if (Global.ContainStr(lastfolderName, "assets"))
                {
                    // to check name patter is match ?
                    bool bNamePatternMatch = true;

                    int substart = Math.Min(lastfolderName.Length, "assets_".Length);
                    string gName = lastfolderName.Substring(substart);
                    // if this guy don't write any expression on group name , don't check just export directly.
                    if (Global.ContainStr(expparm.selstr, "?") || Global.ContainStr(expparm.selstr, "*"))
                    {
                        bNamePatternMatch = false;
                        List<string> match_spl = expparm.selstr.Split('_').ToList();
                        List<string> lastfld_spl = gName.Split('_').ToList();

                        if (match_spl.Count == lastfld_spl.Count)
                        {
                            int matchcnt = 0;
                            for (int i = 0; i < lastfld_spl.Count; i++)
                            {
                                if (match_spl[i] == lastfld_spl[i])
                                    matchcnt++;
                                else if (match_spl[i] == "?")
                                    matchcnt++;
                                else if (match_spl[i] == "*")
                                    matchcnt++;
                            }
                            if (matchcnt == lastfld_spl.Count)
                                bNamePatternMatch = true;
                        }
                    }

                    if (bNamePatternMatch)
                    {
                        string execString = " -p ";
                        List<String> fileEntries = Directory.GetFiles(FguiLocation, "*.fairy").ToList();
                        if (fileEntries.Count < 1) continue;
                        execString += fileEntries[0].Replace('\\', '/');
                        execString += " -t " + gName;
                        this.Invoke(new InvokeExpStatus(this.exportThreadMsg), new object[] { lastfolderName + " 導出中..." });

                        ProcessStartInfo commandInfosub = new ProcessStartInfo();
                        commandInfosub.WorkingDirectory = FguiLocation;
                        commandInfosub.UseShellExecute = false;
                        commandInfosub.RedirectStandardInput = true;
                        commandInfosub.RedirectStandardOutput = true;
                        commandInfosub.FileName = exeLocation;
                        commandInfosub.Arguments = execString;
                        Process processsub = Process.Start(commandInfosub);
                        Console.WriteLine(exeLocation + execString);
                        processsub.WaitForExit();
                        Thread.Sleep(100);
                        Console.WriteLine(lastfolderName + " exported ");

                    }
                }
            }

            // then export main branch 
            foreach (string dir in dirs)
            {
                string lastfolderName = Path.GetFileName(dir);
                if (lastfolderName == "assets")
                {
                    string execString = " -p ";
                    List<String> fileEntries = Directory.GetFiles(FguiLocation, "*.fairy").ToList();
                    if (fileEntries.Count < 1) continue;
                    execString += fileEntries[0].Replace('\\', '/');

                    ProcessStartInfo commandInfosub = new ProcessStartInfo();
                    commandInfosub.WorkingDirectory = FguiLocation;
                    commandInfosub.UseShellExecute = false;
                    commandInfosub.RedirectStandardInput = true;
                    commandInfosub.RedirectStandardOutput = true;
                    commandInfosub.FileName = exeLocation;
                    commandInfosub.Arguments = execString;
                    Process processsub = Process.Start(commandInfosub);
                    Console.WriteLine(exeLocation + execString);
                    processsub.WaitForExit();
                    Thread.Sleep(100);

                    Console.WriteLine(lastfolderName + " exported ");
                    this.Invoke(new InvokeExpStatus(this.exportThreadMsg), new object[] { lastfolderName + " 導出完成 " });
                }
            }
            foreach (var proc in Process.GetProcessesByName("FairyGUI-Editor"))
            {
                proc.Kill();
            }

            /// revert all change
            ProcessStartInfo commandInfo = new ProcessStartInfo();
            commandInfo.WorkingDirectory = FguiLocation;
            commandInfo.UseShellExecute = false;
            commandInfo.RedirectStandardInput = true;
            commandInfo.RedirectStandardOutput = true;
            commandInfo.FileName = "git.exe";
            commandInfo.Arguments = "reset --hard";
            Process process = Process.Start(commandInfo);

            this.Invoke(new InvokeExpThDone(this.exportThreadDone), new object[] { expparm.ExportPath_Branch });
        }
        private delegate void InvokeExpThDone(string branchLoc);
        private void exportThreadDone(string branchLoc)
        {
            MessageBox.Show("導出完成!", "FGUI Toolkit", MessageBoxButtons.OK, MessageBoxIcon.None,
            MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
        }

        private delegate void InvokeExpStatus(string msg);
        private void exportThreadMsg(string msg)
        {
            this.lbb_exportText.Text = msg;
        }
    }
}
