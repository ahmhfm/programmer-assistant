using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace visualStudio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void sizeAndLocation()
        {
            this.Size = new Size(myForm.getSize()[0], myForm.getSize()[1]);
            this.Location = new Point(myForm.getLocation()[0], myForm.getLocation()[1]);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            sizeAndLocation();
        }

        private void btnFormSize_Click(object sender, EventArgs e)
        {
            myForm.formSizeAndLocation();
            sizeAndLocation();
            panel13.Controls.Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMinuBar_Click(object sender, EventArgs e)
        {
            if(panel2.Visible == true)
            {
                panel2.Visible = false;
                panel3.Visible = false;

            }
            else
            {
                panel2.Visible = true;
                panel3.Visible = true;

            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            tableLayoutPanel1.ColumnStyles[0].Width = Convert.ToInt32(trackBar1.Value);
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_element_Click(object sender, EventArgs e)
        {
            frm_element frm = new frm_element();
            frm.TopLevel = false;
            frm.TopMost = false;
            panel13.Controls.Clear();
            panel13.Controls.Add(frm);
            frm.Show();    
        }

        private void btn_property_Click(object sender, EventArgs e)
        {
            frm_property frm = new frm_property();
            frm.TopLevel = false;
            frm.TopMost = false;
            panel13.Controls.Clear();
            panel13.Controls.Add(frm);
            frm.Show();
        }

        private void btn_PropertyGroup_Click(object sender, EventArgs e)
        {
            frm_propertyGroup frm = new frm_propertyGroup();
            frm.TopMost = false;
            frm.TopLevel = false;
            panel13.Controls.Clear();
            panel13.Controls.Add(frm);
            frm.Show();
        }

        private void btn_eventGroup_Click(object sender, EventArgs e)
        {
            frm_eventGroup frm = new frm_eventGroup();
            frm.TopMost = false;
            frm.TopLevel = false;
            panel13.Controls.Clear();
            panel13.Controls.Add(frm);
            frm.Show();
        }

        private void btn_event_Click(object sender, EventArgs e)
        {
            frm_event frm = new frm_event();
            frm.TopLevel = false;
            frm.TopMost = false;
            panel13.Controls.Clear();
            panel13.Controls.Add(frm);
            frm.Show();
        }
    }
}
