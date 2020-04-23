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

        private void btnPurgeProj_Click(object sender, EventArgs e)
        {
            bool bSearchViews = ckbSearchView.Checked;
            bool bSearchAssets = ckbSearchAssets.Checked;
            bool bDeleteAfterSearch = ckbDeleteAfterSearch.Checked;

            Global.UI("1", "2", "Test");
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
                    Global.WritePrivateProfileString("FGUI", "Location", fproj_loc , Application.StartupPath + "\\Config.ini");
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show("不是一個 .fairy 檔案 ");
                }
            }

            
        }

        private void btnSwitchExpPath_Click(object sender, EventArgs e)
        {

        }

        private void FguiToolkitForm_Load(object sender, EventArgs e)
        {
            
            #region DataGridView

            
            #endregion
            
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
