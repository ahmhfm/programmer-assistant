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
    public partial class frm_eventGroup : Form
    {
        public frm_eventGroup()
        {
            InitializeComponent();
        }
        int x = 0;
        private void btn_first_Click(object sender, EventArgs e)
        {
            txt_eventGroupName.Enabled = false;
            txt_eventGroupDescription.Enabled = false;

            x = 0;
            SqlConnection con = myConnection.con1();
            string comString = "select * from eventGroup ";
            SqlCommand com = new SqlCommand(comString, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();

            if (dt.Rows.Count > 0)
            {
                //-------1
                txt_eventGroupNo.Text = dt.Rows[x][0].ToString();
                //-------2
                txt_eventGroupName.Text = dt.Rows[x][1].ToString();
                //-------3
                txt_eventGroupDescription.Text = dt.Rows[x][2].ToString();
            }
        }

        private void btn_last_Click(object sender, EventArgs e)
        {
            txt_eventGroupName.Enabled = false;
            txt_eventGroupDescription.Enabled = false;

            SqlConnection con = myConnection.con1();
            string comString = "select * from eventGroup ";
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
                txt_eventGroupNo.Text = dt.Rows[x][0].ToString();
                //-------2
                txt_eventGroupName.Text = dt.Rows[x][1].ToString();
                //-------3
                txt_eventGroupDescription.Text = dt.Rows[x][2].ToString();
            }
        }

        private void btn_previous_Click(object sender, EventArgs e)
        {
            txt_eventGroupName.Enabled = false;
            txt_eventGroupDescription.Enabled = false;

            x -= 1;
            if (x < 0)
            {
                x = 0;
                MessageBox.Show("لا توجد حسابات قبل هذا الحساب");
            }

            SqlConnection con = myConnection.con1();
            string comString = "select * from eventGroup ";
            SqlCommand com = new SqlCommand(comString, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();


            if (dt.Rows.Count > 0)
            {
                //-------1
                txt_eventGroupNo.Text = dt.Rows[x][0].ToString();
                //-------2
                txt_eventGroupName.Text = dt.Rows[x][1].ToString();
                //-------3
                txt_eventGroupDescription.Text = dt.Rows[x][2].ToString();
            }
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            txt_eventGroupName.Enabled = false;
            txt_eventGroupDescription.Enabled = false;

            SqlConnection con = myConnection.con1();
            string comString = "select * from eventGroup ";
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
                txt_eventGroupNo.Text = dt.Rows[x][0].ToString();
                //-------2
                txt_eventGroupName.Text = dt.Rows[x][1].ToString();
                //-------3
                txt_eventGroupDescription.Text = dt.Rows[x][2].ToString();
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            txt_eventGroupNo.Text = "ترقيم تلقائي";
            txt_eventGroupName.Enabled = true;
            txt_eventGroupName.Text = "";
            txt_eventGroupDescription.Enabled = true;
            txt_eventGroupDescription.Text = "";
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (txt_eventGroupName.Text == "" || txt_eventGroupDescription.Text == "")
            {
                MessageBox.Show("رجاء قم بإكمال البيانات");
            }
            else
            {
                if (txt_eventGroupNo.Text == "ترقيم تلقائي")
                {
                    SqlConnection con = myConnection.con1();
                    SqlCommand com = new SqlCommand("insert into eventGroup values ('" + txt_eventGroupName.Text + "','" + txt_eventGroupDescription.Text + "')", con);
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
            if (btn_Edit.Text == "تعديل")
            {
                btn_Edit.Text = "حفظ التعديلات";
                txt_eventGroupName.Enabled = true;
                txt_eventGroupName.BackColor = Color.Yellow;
                txt_eventGroupDescription.Enabled = true;
                txt_eventGroupDescription.BackColor = Color.Yellow;
            }
            else
            {
                SqlConnection con = myConnection.con1();
                SqlCommand com = new SqlCommand("update propertyGroup set propertyGroup_name = '" + txt_eventGroupName.Text + "',propertyGroup_description = '" + txt_eventGroupDescription.Text + "' where propertyGroup_no = " + txt_eventGroupNo.Text, con);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();


                btn_Edit.Text = "تعديل";
                txt_eventGroupName.Enabled = false;
                txt_eventGroupName.BackColor = Color.White;
                txt_eventGroupDescription.Enabled = false;
                txt_eventGroupDescription.BackColor = Color.White;
                MessageBox.Show("تمت التعديلات بنجاح");
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_eventGroupNo.Text != "" && txt_eventGroupNo.Text != "ترقيم تلقائي")
                {
                    SqlConnection con = myConnection.con1();
                    SqlCommand com = new SqlCommand("delete from eventGroup  where eventGroup_no = " + txt_eventGroupNo.Text, con);
                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("تم الحذف بنجاح");
                    txt_eventGroupNo.Text = "";
                    txt_eventGroupName.Text = "";
                    txt_eventGroupDescription.Text = "";

                    btn_first_Click(null, null);
                }
            }
            catch (Exception ex)
            {

                if (ex.Message.Contains("FK__eevent__eevent_e__403A"))
                {
                    MessageBox.Show("توجد احداث مرتبطة بهذه المجموعة قم بحذف الاحداث قبل حذف المجموعة");
                }

                //throw;
            }
        }

        private void frm_eventGroup_Load(object sender, EventArgs e)
        {
            btn_first_Click(null, null);
        }
    }
}
