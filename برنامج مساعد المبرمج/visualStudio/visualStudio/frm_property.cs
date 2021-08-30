using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace visualStudio
{
    public partial class frm_property : Form
    {
        public frm_property()
        {
            InitializeComponent();
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            txt_propertyNo.Text = "ترقيم تلقائي";
            txt_propertyName.Text = "";
            txt_propertyDescription.Text = "";
            txt_propertyName.Enabled = true;
            txt_propertyDescription.Enabled = true;
            comboBox1.Enabled = true;
            comboBox1.SelectedIndex = -1;
            label5.Text = "";
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (txt_propertyName.Text==""  ||  txt_propertyDescription.Text=="" || comboBox1.SelectedIndex==-1)
            {
                MessageBox.Show("رجاء قم بتعبئة جميع الحقول");
            }
            else
            {
                if(txt_propertyNo.Text != "ترقيم تلقائي")
                {
                    MessageBox.Show("رجاء قم بالضغط على الزر جديد");
                }
                else
                {
                    SqlConnection con = myConnection.con1();
                    SqlCommand com = new SqlCommand("insert into property values ('" + txt_propertyName.Text + "','" + txt_propertyDescription.Text + "','" + comboBox1.SelectedValue + "')", con);
                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();
                    btn_last_Click(null, null);
                }
            }

        }

        private void frm_property_Load(object sender, EventArgs e)
        {
            SqlConnection con = myConnection.con1();
            SqlCommand com = new SqlCommand("select * from propertyGroup ", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();
            comboBox1.DataSource = dt;
            comboBox1.ValueMember = dt.Columns[0].ColumnName;
            comboBox1.DisplayMember = dt.Columns[1].ColumnName;

            btn_first_Click(null, null);

        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            if(btn_Edit.Text=="تعديل")
            {
                txt_propertyName.BackColor = Color.Yellow;
                txt_propertyDescription.BackColor = Color.Yellow;
                comboBox1.BackColor = Color.Yellow;

                txt_propertyName.Enabled = true;
                txt_propertyDescription.Enabled = true;
                comboBox1.Enabled = true;
                btn_Edit.Text = "حفظ التعديلات";
            }
            else
            {
                SqlConnection con = myConnection.con1();
                SqlCommand com = new SqlCommand("update property set  property_Name = '" + txt_propertyName.Text + "',property_Description = '" + txt_propertyDescription.Text + "', property_propertyGroupNo = '" + comboBox1.SelectedValue + "' where property_no ="+txt_propertyNo.Text, con);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();

                txt_propertyName.BackColor = Color.White;
                txt_propertyDescription.BackColor = Color.White;
                comboBox1.BackColor = Color.White;
                txt_propertyName.Enabled = false;
                txt_propertyDescription.Enabled = false;
                comboBox1.Enabled = false;

                btn_Edit.Text = "تعديل";
                MessageBox.Show("تمت التعديلات بنجاح");
            }
        }
        int x = 0;
        private void btn_first_Click(object sender, EventArgs e)
        {
            txt_propertyName.Enabled = false;
            txt_propertyDescription.Enabled = false;
            comboBox1.Enabled = false;

            x = 0;
            SqlConnection con = myConnection.con1();
            string comString = "select * from property ";
            SqlCommand com = new SqlCommand(comString, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();

            if (dt.Rows.Count > 0)
            {
                //-------1
                txt_propertyNo.Text = dt.Rows[x][0].ToString();
                //-------2
                txt_propertyName.Text = dt.Rows[x][1].ToString();
                //-------3
                txt_propertyDescription.Text = dt.Rows[x][2].ToString();
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

            txt_propertyName.Enabled = false;
            txt_propertyDescription.Enabled = false;
            comboBox1.Enabled = false;

            SqlConnection con = myConnection.con1();
            string comString = "select * from property ";
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
                txt_propertyNo.Text = dt.Rows[x][0].ToString();
                //-------2
                txt_propertyName.Text = dt.Rows[x][1].ToString();
                //-------3
                txt_propertyDescription.Text = dt.Rows[x][2].ToString();
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
            txt_propertyName.Enabled = false;
            txt_propertyDescription.Enabled = false;
            comboBox1.Enabled = false;

            x -= 1;
            if (x < 0)
            {
                x = 0;
                MessageBox.Show("لا توجد حسابات قبل هذا الحساب");
            }

            SqlConnection con = myConnection.con1();
            string comString = "select * from property ";
            SqlCommand com = new SqlCommand(comString, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();


            if (dt.Rows.Count > 0)
            {
                //-------1
                txt_propertyNo.Text = dt.Rows[x][0].ToString();
                //-------2
                txt_propertyName.Text = dt.Rows[x][1].ToString();
                //-------3
                txt_propertyDescription.Text = dt.Rows[x][2].ToString();
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

            txt_propertyName.Enabled = false;
            txt_propertyDescription.Enabled = false;
            comboBox1.Enabled = false;


            SqlConnection con = myConnection.con1();
            string comString = "select * from property ";
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
                txt_propertyNo.Text = dt.Rows[x][0].ToString();
                //-------2
                txt_propertyName.Text = dt.Rows[x][1].ToString();
                //-------3
                txt_propertyDescription.Text = dt.Rows[x][2].ToString();
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

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            txt_propertyName.Enabled = false;
            txt_propertyDescription.Enabled = false;
            comboBox1.Enabled = false;

            if (txt_propertyNo.Text!="" && txt_propertyNo.Text!="ترقيم تلقائي")
            {
                SqlConnection con = myConnection.con1();
                SqlCommand com = new SqlCommand("delete from property  where property_no =" + txt_propertyNo.Text, con);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("تمت عملية الحذف بنجاح");
                txt_propertyNo.Text = "";
                txt_propertyName.Text="";
                txt_propertyDescription.Text = "";
                btn_first_Click(null, null);
            }
        }
    }
}
