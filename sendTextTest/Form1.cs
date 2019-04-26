using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace CustomScripts
{
    public partial class Form1 : Form
    {
        public ScriptSettings scriptSettings;

        public void InitializeScriptsList()
        {
            using (FileStream f = new FileStream("customscripts.xml", FileMode.Open))
            {
                XmlSerializer s = new XmlSerializer(typeof(ScriptSettings));
                try
                {
                    scriptSettings = (ScriptSettings)s.Deserialize(f);
                }
                catch (Exception ex)
                {
                    scriptSettings = ScriptSettings.InitTest();
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
            InitializeScriptsList();
            //
            this.LostFocus += Form1_LostFocus;
            this.Shown += Form1_Shown;
            foreach (MenuItem m in this.contextMenu1.MenuItems)
                m.Click += ContextMenu_ItemClicked;
            //
            GlobalKeyboardHook hook = new GlobalKeyboardHook();
            hook.KeyboardPressed += Hook_KeyboardPressed;
        }

        private void ContextMenu_ItemClicked(object sender, EventArgs e)
        {
            MenuItem senderItem = (MenuItem)sender;
            if (senderItem.Text == "Open")
                this.Show();
            else if (senderItem.Text == "Exit")
                this.Close();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Hide();
            textBox1.Text = "";
        }

        private void Form1_LostFocus(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Hook_KeyboardPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown
                && e.KeyboardData.VirtualCode == (int)Keys.F7)
            {
                this.Show();
                this.Activate();
            }
        }

        /*private const int WM_SETTEXT = 0x000C;
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(
            string lpClassName,
            string lpWindowName);

        [DllImport("User32.dll")]
        private static extern IntPtr FindWindowEx(
            IntPtr hwndParent,
            IntPtr hwndChildAfter,
            string lpszClass,
        string lpszWindows);
        [DllImport("User32.dll")]
        private static extern Int32 SendMessage(
            IntPtr hWnd,
            int Msg,
            IntPtr wParam,
        StringBuilder lParam);*/

        private void goBtn_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                this.Hide();
                //3 methods. Pick your poison:

                //Method 1 = SendKeys. Very slow.
                //System.Windows.Forms.SendKeys.Send(((Script)listBox1.SelectedItem).Text);

                //Method 2 - clipboard - fast but with a sacrifice
                Clipboard.Clear();
                Clipboard.SetText(((Script)listBox1.SelectedItem).Text);
                SendKeys.SendWait("^v");

                //Method 3 - Must know exact window name AND it replaces all text... 
                /*IntPtr hWnd = FindWindow("Notepad", "*Untitled - Notepad");
                if (!hWnd.Equals(IntPtr.Zero))
                {
                    // retrieve Edit window handle of Notepad
                    IntPtr edithWnd = FindWindowEx(hWnd, IntPtr.Zero, "Edit", null);
                    if (!edithWnd.Equals(IntPtr.Zero))
                        // send WM_SETTEXT message with "Hello World!"
                        SendMessage(edithWnd, WM_SETTEXT, IntPtr.Zero, new StringBuilder(((Script)listBox1.SelectedItem).Text));
                }*/

                textBox1.Text = "";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            for (int i = 0; i < scriptSettings.Scripts.Count; i++)
            {
                string searchStr = textBox1.Text;
                Script el = scriptSettings.Scripts.ElementAt(i);
                if (el.Search(searchStr))
                {
                    listBox1.Items.Add(el);
                }
            }
            if (listBox1.Items.Count > 0)
                listBox1.SelectedIndex = 0;
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
        }

        private void settingsBtn_Click(object sender, EventArgs e)
        {
            Settings s = new Settings();
            s.ShowDialog();
        }

        private void hideBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        
    }
}
