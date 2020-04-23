using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Globalization;

namespace fgui_toolkit
{
    /// <summary>
    /// 全域类别
    /// </summary>
    public class Global
    {
        public static CultureInfo culterInfo = CultureInfo.CurrentCulture;

        public static InvokeUI UI;

        public delegate void InvokeUI(string  wParam, string lParam1, string lParam2);

        #region 导入原生类别库

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern long WritePrivateProfileString(string section, string key, string value, string filePath);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern int GetPrivateProfileInt(string section, string key, int def,string filePath);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern void OutputDebugString(string message);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint RegisterWindowMessage(string lpString);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        #endregion
    }
}
