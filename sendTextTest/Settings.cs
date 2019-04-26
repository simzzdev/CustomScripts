using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomScripts
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {

        }

        private void editBtn_Click(object sender, EventArgs e)
        {

        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {

        }

        private void importBtn_Click(object sender, EventArgs e)
        {

        }

        private void exportBtn_Click(object sender, EventArgs e)
        {

        }

        private void radioButtons_CheckedChanged(object sender, EventArgs e)
        {
            windowTitleBox.Visible = radioButton3.Checked;
        }
    }
}
