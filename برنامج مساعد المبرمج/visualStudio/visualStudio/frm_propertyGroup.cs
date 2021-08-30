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
    public partial class frm_propertyGroup : Form
    {
        public frm_propertyGroup()
        {
            InitializeComponent();
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            txt_propertyGroupNo.Text = "ترقيم تلقائي";
            txt_propertyGroupName.Enabled = true;
            txt_propertyGroupName.Text = "";
            txt_propertyGroupDescription.Enabled = true;
            txt_propertyGroupDescription.Text = "";
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if(txt_propertyGroupName.Text=="" || txt_propertyGroupDescription.Text=="")
            {
                MessageBox.Show("رجاء قم بإكمال البيانات");
            }
            else
            {
                if(txt_propertyGroupNo.Text== "ترقيم تلقائي")
                {
                    SqlConnection con = myConnection.con1();
                    SqlCommand com = new SqlCommand("insert into propertyGroup values ('" + txt_propertyGroupName.Text + "','" + txt_propertyGroupDescription.Text + "')", con);
                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();
                    btn_last_Click(null, null);
                }
                else
                {
                    MessageBox.Show("رجاء قم بالضغط على زر جديد");
                }
            }

        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            if(btn_Edit.Text=="تعديل")
            {
                btn_Edit.Text = "حفظ التعديلات";
                txt_propertyGroupName.Enabled = true;
                txt_propertyGroupName.BackColor = Color.Yellow;
                txt_propertyGroupDescription.Enabled = true;
                txt_propertyGroupDescription.BackColor = Color.Yellow;
            }
            else
            {
                SqlConnection con = myConnection.con1();
                SqlCommand com = new SqlCommand("update propertyGroup set propertyGroup_name = '" + txt_propertyGroupName.Text + "',propertyGroup_description = '" + txt_propertyGroupDescription.Text + "' where propertyGroup_no = "+txt_propertyGroupNo.Text, con);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();


                btn_Edit.Text = "تعديل";
                txt_propertyGroupName.Enabled = false;
                txt_propertyGroupName.BackColor = Color.White;
                txt_propertyGroupDescription.Enabled = false;
                txt_propertyGroupDescription.BackColor = Color.White;
                MessageBox.Show("تمت التعديلات بنجاح");
            }

        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_propertyGroupNo.Text != "" && txt_propertyGroupNo.Text != "ترقيم تلقائي")
                {
                    SqlConnection con = myConnection.con1();
                    SqlCommand com = new SqlCommand("delete from propertyGroup  where propertyGroup_no = " + txt_propertyGroupNo.Text, con);
                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("تم الحذف بنجاح");
                    txt_propertyGroupNo.Text = "";
                    txt_propertyGroupName.Text = "";
                    txt_propertyGroupDescription.Text = "";

                    btn_first_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                if(ex.Message.Contains("FK__property__proper__3B75D760") )
                {
                    MessageBox.Show("توجد خصائص مرتبطة بهذه المجموعة قم بحذف الخصائص قبل حذف المجموعة");
                }
                
                //throw;
            }


        }

        int x = 0;
        private void btn_first_Click(object sender, EventArgs e)
        {
            txt_propertyGroupName.Enabled = false;
            txt_propertyGroupDescription.Enabled = false;

            x = 0;
            SqlConnection con = myConnection.con1();
            string comString = "select * from propertyGroup ";
            SqlCommand com = new SqlCommand(comString, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();

            if (dt.Rows.Count > 0)
            {
                //-------1
                txt_propertyGroupNo.Text = dt.Rows[x][0].ToString();
                //-------2
                txt_propertyGroupName.Text = dt.Rows[x][1].ToString();
                //-------3
                txt_propertyGroupDescription.Text = dt.Rows[x][2].ToString();
            }
        }

        private void btn_last_Click(object sender, EventArgs e)
        {
            txt_propertyGroupName.Enabled = false;
            txt_propertyGroupDescription.Enabled = false;

            SqlConnection con = myConnection.con1();
            string comString = "select * from propertyGroup ";
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
                txt_propertyGroupNo.Text = dt.Rows[x][0].ToString();
                //-------2
                txt_propertyGroupName.Text = dt.Rows[x][1].ToString();
                //-------3
                txt_propertyGroupDescription.Text = dt.Rows[x][2].ToString();
            }
        }

        private void btn_previous_Click(object sender, EventArgs e)
        {
            txt_propertyGroupName.Enabled = false;
            txt_propertyGroupDescription.Enabled = false;

            x -= 1;
            if (x < 0)
            {
                x = 0;
                MessageBox.Show("لا توجد حسابات قبل هذا الحساب");
            }

            SqlConnection con = myConnection.con1();
            string comString = "select * from propertyGroup ";
            SqlCommand com = new SqlCommand(comString, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();


            if (dt.Rows.Count > 0)
            {
                //-------1
                txt_propertyGroupNo.Text = dt.Rows[x][0].ToString();
                //-------2
                txt_propertyGroupName.Text = dt.Rows[x][1].ToString();
                //-------3
                txt_propertyGroupDescription.Text = dt.Rows[x][2].ToString();
            }
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            txt_propertyGroupName.Enabled = false;
            txt_propertyGroupDescription.Enabled = false;

            SqlConnection con = myConnection.con1();
            string comString = "select * from propertyGroup ";
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
                txt_propertyGroupNo.Text = dt.Rows[x][0].ToString();
                //-------2
                txt_propertyGroupName.Text = dt.Rows[x][1].ToString();
                //-------3
                txt_propertyGroupDescription.Text = dt.Rows[x][2].ToString();
            }
        }

        private void frm_propertyGroup_Load(object sender, EventArgs e)
        {
            btn_first_Click(null, null);
        }
    }
}
