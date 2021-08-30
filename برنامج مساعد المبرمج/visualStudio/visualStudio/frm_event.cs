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

namespace visualStudio
{
    public partial class frm_event : Form
    {
        public frm_event()
        {
            InitializeComponent();
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            txt_eventNo.Text = "ترقيم تلقائي";
            txt_eventName.Text = "";
            txt_eventDescription.Text = "";
            txt_eventName.Enabled = true;
            txt_eventDescription.Enabled = true;
            comboBox1.Enabled = true;
            comboBox1.SelectedIndex = -1;
            label5.Text = "";
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (txt_eventName.Text == "" || txt_eventDescription.Text == "" || comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("رجاء قم بتعبئة جميع الحقول");
            }
            else
            {
                if (txt_eventNo.Text != "ترقيم تلقائي")
                {
                    MessageBox.Show("رجاء قم بالضغط على الزر جديد");
                }
                else
                {
                    SqlConnection con = myConnection.con1();
                    SqlCommand com = new SqlCommand("insert into eevent values ('" + txt_eventName.Text + "','" + txt_eventDescription.Text + "','" + comboBox1.SelectedValue + "')", con);
                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();
                    btn_last_Click(null, null);
                }
            }
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            if (btn_Edit.Text == "تعديل")
            {
                txt_eventName.BackColor = Color.Yellow;
                txt_eventDescription.BackColor = Color.Yellow;
                comboBox1.BackColor = Color.Yellow;

                txt_eventName.Enabled = true;
                txt_eventDescription.Enabled = true;
                comboBox1.Enabled = true;
                btn_Edit.Text = "حفظ التعديلات";
            }
            else
            {
                SqlConnection con = myConnection.con1();
                SqlCommand com = new SqlCommand("update eevent set  eevent_Name = '" + txt_eventName.Text + "',eevent_Description = '" + txt_eventDescription.Text + "', eevent_eventGroupNo = '" + comboBox1.SelectedValue + "' where eevent_no =" + txt_eventNo.Text, con);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();

                txt_eventName.BackColor = Color.White;
                txt_eventDescription.BackColor = Color.White;
                comboBox1.BackColor = Color.White;
                txt_eventName.Enabled = false;
                txt_eventDescription.Enabled = false;
                comboBox1.Enabled = false;

                btn_Edit.Text = "تعديل";
                MessageBox.Show("تمت التعديلات بنجاح");
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            txt_eventName.Enabled = false;
            txt_eventDescription.Enabled = false;
            comboBox1.Enabled = false;

            if (txt_eventNo.Text != "" && txt_eventNo.Text != "ترقيم تلقائي")
            {
                SqlConnection con = myConnection.con1();
                SqlCommand com = new SqlCommand("delete from eevent  where eevent_no =" + txt_eventNo.Text, con);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("تمت عملية الحذف بنجاح");
                txt_eventNo.Text = "";
                txt_eventName.Text = "";
                txt_eventDescription.Text = "";

                btn_first_Click(null, null);
            }
        }
        int x = 0;
        private void btn_first_Click(object sender, EventArgs e)
        {
            txt_eventName.Enabled = false;
            txt_eventDescription.Enabled = false;
            comboBox1.Enabled = false;

            x = 0;
            SqlConnection con = myConnection.con1();
            string comString = "select * from eevent ";
            SqlCommand com = new SqlCommand(comString, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();

            if (dt.Rows.Count > 0)
            {
                //-------1
                txt_eventNo.Text = dt.Rows[x][0].ToString();
                //-------2
                txt_eventName.Text = dt.Rows[x][1].ToString();
                //-------3
                txt_eventDescription.Text = dt.Rows[x][2].ToString();
                //-------4
                foreach (DataRowView xxx in comboBox1.Items)
                {
                    if (xxx[0].ToString() == dt.Rows[x][3].ToString())
                    {
                        comboBox1.Text = xxx[1].ToString();
                        break;
                    }

                }
            }
        }

        private void btn_previous_Click(object sender, EventArgs e)
        {
            txt_eventName.Enabled = false;
            txt_eventDescription.Enabled = false;
            comboBox1.Enabled = false;

            x -= 1;
            if (x < 0)
            {
                x = 0;
                MessageBox.Show("لا توجد حسابات قبل هذا الحساب");
            }

            SqlConnection con = myConnection.con1();
            string comString = "select * from eevent ";
            SqlCommand com = new SqlCommand(comString, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();


            if (dt.Rows.Count > 0)
            {
                //-------1
                txt_eventNo.Text = dt.Rows[x][0].ToString();
                //-------2
                txt_eventName.Text = dt.Rows[x][1].ToString();
                //-------3
                txt_eventDescription.Text = dt.Rows[x][2].ToString();
                //-------4
                foreach (DataRowView xxx in comboBox1.Items)
                {
                    if (xxx[0].ToString() == dt.Rows[x][3].ToString())
                    {
                        comboBox1.Text = xxx[1].ToString();
                        break;
                    }

                }
            }
        }

        private void btn_next_Click(object sender, EventArgs e)
        {

            txt_eventName.Enabled = false;
            txt_eventDescription.Enabled = false;
            comboBox1.Enabled = false;


            SqlConnection con = myConnection.con1();
            string comString = "select * from eevent ";
            SqlCommand com = new SqlCommand(comString, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();


            x += 1;
            if (x > dt.Rows.Count - 1)
            {
                x = dt.Rows.Count - 1;
                MessageBox.Show("لا توجد حسابات بعد هذا الحساب");
            }


            if (dt.Rows.Count > 0)
            {
                //-------1
                txt_eventNo.Text = dt.Rows[x][0].ToString();
                //-------2
                txt_eventName.Text = dt.Rows[x][1].ToString();
                //-------3
                txt_eventDescription.Text = dt.Rows[x][2].ToString();
                //-------4
                foreach (DataRowView xxx in comboBox1.Items)
                {
                    if (xxx[0].ToString() == dt.Rows[x][3].ToString())
                    {
                        comboBox1.Text = xxx[1].ToString();
                        break;
                    }

                }
            }
        }

        private void btn_last_Click(object sender, EventArgs e)
        {

            txt_eventName.Enabled = false;
            txt_eventDescription.Enabled = false;
            comboBox1.Enabled = false;

            SqlConnection con = myConnection.con1();
            string comString = "select * from eevent ";
            SqlCommand com = new SqlCommand(comString, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();

            x = dt.Rows.Count - 1;


            if (dt.Rows.Count > 0)
            {
                //-------1
                txt_eventNo.Text = dt.Rows[x][0].ToString();
                //-------2
                txt_eventName.Text = dt.Rows[x][1].ToString();
                //-------3
                txt_eventDescription.Text = dt.Rows[x][2].ToString();
                //-------4
                foreach (DataRowView xxx in comboBox1.Items)
                {
                    if (xxx[0].ToString() == dt.Rows[x][3].ToString())
                    {
                        comboBox1.Text = xxx[1].ToString();
                        break;
                    }

                }
            }
        }

        private void frm_event_Load(object sender, EventArgs e)
        {
            SqlConnection con = myConnection.con1();
            SqlCommand com = new SqlCommand("select * from eventGroup ", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();
            comboBox1.DataSource = dt;
            comboBox1.ValueMember = dt.Columns[0].ColumnName;
            comboBox1.DisplayMember = dt.Columns[1].ColumnName;
            comboBox1.SelectedIndex = -1;

            btn_first_Click(null, null);
        }
    }
}
