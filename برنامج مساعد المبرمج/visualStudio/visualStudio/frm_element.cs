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
    public partial class frm_element : Form
    {
        public frm_element()
        {
            InitializeComponent();
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            txt_elementNo.Text = "ترقيم تلقائي";
            txt_elementName.Text = "";
            txt_elementName.Enabled = true;
            txt_elementDescription.Text = "";
            txt_elementDescription.Enabled = true;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if( txt_elementName.Text != "" && txt_elementDescription.Text != "" )
            {
                if (txt_elementNo.Text != "ترقيم تلقائي")
                {
                    MessageBox.Show("قم بضغط جديد قبل عملية الاضافة");
                }
                else
                {
                    SqlConnection con = myConnection.con1();
                    SqlCommand com = new SqlCommand("insert into element values ('" + txt_elementName.Text + "','" + txt_elementDescription.Text + "')", con);
                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();
                    txt_elementName.Text = "";
                    txt_elementDescription.Text = "";
                    txt_elementName.Enabled = false;
                    txt_elementDescription.Enabled = false;
                    btn_last_Click(null, null);
                }

            }
            else
            {
                MessageBox.Show("قم بتعبئة جميع الحقول");
            }

        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            if(btn_Edit.Text == "تعديل")
            {
                txt_elementName.BackColor = Color.Yellow;
                txt_elementDescription.BackColor = Color.Yellow;
                txt_elementName.Enabled = true;
                txt_elementDescription.Enabled = true;
                btn_Edit.Text = "حفظ التعديلات";
            }
            else
            {
                SqlConnection con = myConnection.con1();
                SqlCommand com = new SqlCommand("update element set element_name = '" + txt_elementName.Text + "' , element_description = '" + txt_elementDescription.Text + "' where element_no ="+txt_elementNo.Text, con);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                txt_elementName.BackColor = Color.White;
                txt_elementDescription.BackColor = Color.White;
                btn_Edit.Text = "تعديل";
                txt_elementName.Enabled = false;
                txt_elementDescription.Enabled = false;
            }



        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                txt_elementName.Enabled = false;
                txt_elementDescription.Enabled = false;
                if (btn_Delete.Text == "حذف")
                {
                    btn_Delete.Text = "تأكيد الحذف";
                }
                else
                {
                    SqlConnection con = myConnection.con1();
                    SqlCommand com = new SqlCommand("delete from element where element_no =" + txt_elementNo.Text, con);
                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();
                    txt_elementName.BackColor = Color.White;
                    txt_elementDescription.BackColor = Color.White;

                    txt_elementNo.Text = "";
                    txt_elementName.Text = "";
                    txt_elementDescription.Text = "";

                    btn_first_Click(null, null);
                    btn_Delete.Text = "حذف";
                    MessageBox.Show("تمت عملية الحذف بنجاح");
                }
            }
            catch (Exception ex) 
            {
                if(ex.Message.Contains("FK__linkEleme__linkE__4316F928"))
                {
                    MessageBox.Show("قم بحذف جميع الخصائص المرتبطة بهذا العنصر ");
                    btn_Delete.Text = "حذف";

                }
                else if(ex.Message.Contains("FK__linkEleme__linkE__46E78A0C"))
                {
                    MessageBox.Show("قم بحذف جميع الأحداث المرتبطة بهذا العنصر ");
                    btn_Delete.Text = "حذف";


                }
                else if(ex.Message.Contains("FK__code__code_eleme__5CD6CB2B"))
                {
                    MessageBox.Show("قم بحذف جميع الأكواد المرتبطة بهذا العنصر ");
                    btn_Delete.Text = "حذف";

                }
                //throw;
            }
            
        }
        int x = 0;

        private void btn_first_Click(object sender, EventArgs e)
        {
            txt_elementName.Enabled = false;
            txt_elementDescription.Enabled = false;

            x = 0;
            SqlConnection con = myConnection.con1();
            string comString = "select * from element ";
            SqlCommand com = new SqlCommand(comString, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();

            if(dt.Rows.Count>0)
            {
                //-------1
                txt_elementNo.Text = dt.Rows[x][0].ToString();
                //-------2
                txt_elementName.Text = dt.Rows[x][1].ToString();
                //-------3
                txt_elementDescription.Text = dt.Rows[x][2].ToString();
            }

            fillPropertyDataGrid();
            fillEventDataGrid();


            txt_codeCode.Text = "";
            txt_codeDescription.Text = "";
            txt_codeNo.Text = "";
            
            btn_codefirst_Click(null,null);
            btn_ExplanationFrist_Click(null, null);
        }

        private void btn_last_Click(object sender, EventArgs e)
        {
            txt_elementName.Enabled = false;
            txt_elementDescription.Enabled = false;

            SqlConnection con = myConnection.con1();
            string comString = "select * from element ";
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
                txt_elementNo.Text = dt.Rows[x][0].ToString();
                //-------2
                txt_elementName.Text = dt.Rows[x][1].ToString();
                //-------3
                txt_elementDescription.Text = dt.Rows[x][2].ToString();
            }

            fillPropertyDataGrid();
            fillEventDataGrid();

            txt_codeCode.Text = "";
            txt_codeDescription.Text = "";
            txt_codeNo.Text = "";
            btn_codefirst_Click(null, null);
            btn_ExplanationFrist_Click(null, null);
        }

        private void btn_previous_Click(object sender, EventArgs e)
        {
            txt_elementName.Enabled = false;
            txt_elementDescription.Enabled = false;

            x -= 1;
            if (x < 0)
            {
                x = 0;
                MessageBox.Show("لا توجد حسابات قبل هذا الحساب");
            }

            SqlConnection con = myConnection.con1();
            string comString = "select * from element ";
            SqlCommand com = new SqlCommand(comString, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();


            if (dt.Rows.Count > 0)
            {
                //-------1
                txt_elementNo.Text = dt.Rows[x][0].ToString();
                //-------2
                txt_elementName.Text = dt.Rows[x][1].ToString();
                //-------3
                txt_elementDescription.Text = dt.Rows[x][2].ToString();
            }

            fillPropertyDataGrid();
            fillEventDataGrid();

            txt_codeCode.Text = "";
            txt_codeDescription.Text = "";
            txt_codeNo.Text = "";
            btn_codefirst_Click(null, null);
            btn_ExplanationFrist_Click(null, null);
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            txt_elementName.Enabled = false;
            txt_elementDescription.Enabled = false;

            SqlConnection con = myConnection.con1();
            string comString = "select * from element ";
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
                txt_elementNo.Text = dt.Rows[x][0].ToString();
                //-------2
                txt_elementName.Text = dt.Rows[x][1].ToString();
                //-------3
                txt_elementDescription.Text = dt.Rows[x][2].ToString();
            }
            fillPropertyDataGrid();
            fillEventDataGrid();

            txt_codeCode.Text = "";
            txt_codeDescription.Text = "";
            txt_codeNo.Text = "";
            btn_codefirst_Click(null, null);
            btn_ExplanationFrist_Click(null, null);
        }

        private void fillPropertyDataGrid()
        {
            dataGridView1.DataSource = null;

            string elementNo = txt_elementNo.Text;
            if(elementNo=="")
            {
                elementNo = "0";
            }
            SqlConnection con = myConnection.con1();
            SqlCommand com = new SqlCommand("select * from ElementWithproperty where element_no = " + elementNo , con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();
            dataGridView1.DataSource = dt;

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false; // اخفاء احد الاعمدة
            dataGridView1.Columns[2].Visible = false;

            dataGridView1.Columns[3].HeaderText = "رقم الخاصية"; // صف العناوين تسمية احد الخلايا
            dataGridView1.Columns[4].HeaderText = "إسم الخاصية";
            dataGridView1.Columns[5].HeaderText = "وصف الخاصية";

            dataGridView1.Columns[6].Visible = false;

            dataGridView1.Columns[7].HeaderText = "رقم المجموعة";
            dataGridView1.Columns[8].HeaderText = "إسم المجموعة";
            dataGridView1.Columns[9].HeaderText = "وصف المجموعة";

            dataGridView1.Columns[3].Width = 70;
            dataGridView1.Columns[4].Width = 300;
            dataGridView1.Columns[5].Width = ((tabPage1.Width-(70+300+70+150+150))); // عرض هذا العمود متغير بحيث يغطي المساحة المتبقية   
            dataGridView1.Columns[7].Width = 70;
            dataGridView1.Columns[8].Width = 150;
            dataGridView1.Columns[9].Width = 150; 


            ////****************************************************************************************************************************************************
            // العنصر كامل

            dataGridView1.GridColor = Color.FromArgb(233, 236, 250); // لون السطور بين الخلايا
            dataGridView1.BackgroundColor = Color.FromArgb(203, 216, 230); // لون الخلفية
            dataGridView1.BorderStyle = BorderStyle.None; // نمط حدود العنصر بدون حدود
            dataGridView1.AllowUserToAddRows = false; // السماح للمستخدم باضافة صف
            dataGridView1.ReadOnly = false; // للقراءة فقط
            ////****************************************************************************************************************************************************
            // الخلايا

            dataGridView1.DefaultCellStyle.ForeColor = Color.FromArgb(83, 155, 203); // لون الخط في الخلايا
            //dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft; // توسيط ومحاذاة الخط للخلايا
            //dataGridView1.DefaultCellStyle.BackColor = Color.Yellow;// خلفية الخلايا
            //dataGridView1.RowsDefaultCellStyle.BackColor = Color.Blue; // خلفية الخلايا
            //dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True; // السماح بسطور متعددة لجميع الحقول
            //dataGridView1.Columns["property_name"].DefaultCellStyle.WrapMode = DataGridViewTriState.True; // السماح بسطور متعددة لعمود واحد فقط
            dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(203, 216, 230) ; // لون خلفية الخلايا
            //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Green; // تليون صف وترك صف
            //dataGridView1.Columns["property_name"].DefaultCellStyle.BackColor = Color.Red; // تلوين خلفية عمود من الخلايا
            //dataGridView1.Columns["property_name"].DefaultCellStyle.BackColor = Color.FromArgb(83, 155, 203); // تغيير لون خلفية عمود من الخلايا
            //dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None; // بدون سطور بين الخلايا
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; //  سطور عرضية بين الصفوف ودون اعمدة بين الخلايا
            //dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical; //  سطور قائمة بين الاعمدة ودون سطور بين الصفوف
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //  التحديد للصف كامل 
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect; //  التحديد لكل خلية على حدى  

            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(203, 216, 230); // لون خلفية الخلية عند تحديدها
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.FromArgb(216, 100, 239); // لون الكتابة في الخلية عند تحديدها
            dataGridView1.RowTemplate.Height = 30; // تغيير ارتفاع الصف 

            dataGridView1.Sort(this.dataGridView1.Columns["propertyGroup_no"], ListSortDirection.Ascending); // ترتيب البيانات تصاعديا او تنازليا


            ////****************************************************************************************************************************************************
            //// عناوين الاعدمة 

            //dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // توسيط عناوين الاعمدة
            //dataGridView1.ColumnHeadersVisible = false;  // اخفاء صف العناوين
            //dataGridView1.Columns[5].HeaderCell.Style.Font = new Font("Arial", 75); // تغيير حجم الخط لاحد الخلايا بصف العناوين
            //dataGridView1.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomLeft; // محاذاة الخط بأحد الخلايا في صف العناوين
            //dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 100); // الخط لصف العناوين
            dataGridView1.EnableHeadersVisualStyles = false; // السماح بتغيير صف العناوين
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(233, 236, 250); // خلفية صف عناوين الاعمدة
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(233, 236, 250); // لون خلفية خلية عنوان العمود عند تحديده


            //dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;  // لون الخط لصف عناوين الاعمدة 
            //dataGridView1.Columns["property_name"].HeaderCell.Style.BackColor = Color.Red; // تلوين خلفية عنوان عمود معين
            //dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing; // السماح بتغيير حجم حقول عناوين الاعمدة
            //dataGridView1.ColumnHeadersHeight = 100; // تغيير ارتفاع حقول عناوين الاعمدة 

            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None; // بدون حدود بين خلايا عناوين الاعمدة
            //dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised; //  حدود بين خلايا عناوين الاعمدة تسمح بتغيير العرض
            //dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single; //  حدود بين خلايا عناوين الاعمدة عمودية ولونها نفس لون الحدود بين بقية الخلايا


            ////****************************************************************************************************************************************************
            ////   تصميم ستايل معين ومن ثم تم تطبيقه على صف العنوان

            //DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            //dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //dataGridViewCellStyle1.BackColor = SystemColors.Control;
            //dataGridViewCellStyle1.Font = new Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            //dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            //dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            //dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            //dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True; // السماح بسطور متعددة في نفس الحلق 
            //dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;

            ////****************************************************************************************************************************************************
            ////  عناوين الصفوف

            //dataGridView1.EnableHeadersVisualStyles = false; // السماح بتحرير عناوين الصفوف
            //dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Red; // لون خلفية عناوين الصفوف
            //dataGridView1.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Yellow; // لون خلفية عناوين الصفوف عند تحديدها 
            dataGridView1.RowHeadersVisible = false; // اخفاء واظهار عناوين الصفوف
            ////****************************************************************************************************************************************************


        }

        private void fillEventDataGrid()
        {
            string elementNo = txt_elementNo.Text;
            if (elementNo == "")
            {
                elementNo = "0";
            }

            SqlConnection con = myConnection.con1();
            SqlCommand com = new SqlCommand("select * from ElementWithEvent where element_no = " + elementNo , con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();
            dataGridView2.DataSource = dt;

            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[1].Visible = false;
            dataGridView2.Columns[2].Visible = false;

            dataGridView2.Columns[3].HeaderText = "رقم الحدث";
            dataGridView2.Columns[4].HeaderText = "إسم الحدث";
            dataGridView2.Columns[5].HeaderText = "وصف الحدث";

            dataGridView2.Columns[6].Visible = false;

            dataGridView2.Columns[7].HeaderText = "رقم المجموعة";
            dataGridView2.Columns[8].HeaderText = "إسم المجموعة";
            dataGridView2.Columns[9].HeaderText = "وصف المجموعة";

            dataGridView2.Columns[3].Width = 100;
            dataGridView2.Columns[4].Width = 150;
            dataGridView2.Columns[5].Width = ((tabPage2.Width - (100 + 150 + 100 + 100 )) / 2);
            dataGridView2.Columns[7].Width = 100;
            dataGridView2.Columns[8].Width = 100;
            dataGridView2.Columns[9].Width = dataGridView2.Columns[5].Width;

            ////****************************************************************************************************************************************************
            // العنصر كامل

            dataGridView2.GridColor = Color.FromArgb(233, 236, 250); // لون السطور بين الخلايا
            dataGridView2.BackgroundColor = Color.FromArgb(203, 216, 230); // لون الخلفية
            dataGridView2.BorderStyle = BorderStyle.None; // نمط حدود العنصر بدون حدود
            dataGridView2.AllowUserToAddRows = false; // السماح للمستخدم باضافة صف
            dataGridView2.ReadOnly = false; // للقراءة فقط
            ////****************************************************************************************************************************************************
            // الخلايا

            dataGridView2.DefaultCellStyle.ForeColor = Color.FromArgb(83, 155, 203); // لون الخط في الخلايا
            //dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft; // توسيط ومحاذاة الخط للخلايا
            //dataGridView2.DefaultCellStyle.BackColor = Color.Yellow;// خلفية الخلايا
            //dataGridView2.RowsDefaultCellStyle.BackColor = Color.Blue; // خلفية الخلايا
            //dataGridView2.DefaultCellStyle.WrapMode = DataGridViewTriState.True; // السماح بسطور متعددة لجميع الحقول
            //dataGridView2.Columns["property_name"].DefaultCellStyle.WrapMode = DataGridViewTriState.True; // السماح بسطور متعددة لعمود واحد فقط
            dataGridView2.DefaultCellStyle.BackColor = Color.FromArgb(203, 216, 230); // لون خلفية الخلايا
            //dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.Green; // تليون صف وترك صف
            //dataGridView2.Columns["property_name"].DefaultCellStyle.BackColor = Color.Red; // تلوين خلفية عمود من الخلايا
            //dataGridView2.Columns["property_name"].DefaultCellStyle.BackColor = Color.FromArgb(83, 155, 203); // تغيير لون خلفية عمود من الخلايا
            //dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.None; // بدون سطور بين الخلايا
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; //  سطور عرضية بين الصفوف ودون اعمدة بين الخلايا
            //dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical; //  سطور قائمة بين الاعمدة ودون سطور بين الصفوف
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //  التحديد للصف كامل 
            //dataGridView2.SelectionMode = DataGridViewSelectionMode.CellSelect; //  التحديد لكل خلية على حدى  

            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.FromArgb(203, 216, 230); // لون خلفية الخلية عند تحديدها
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.FromArgb(216, 100, 239); // لون الكتابة في الخلية عند تحديدها
            dataGridView2.RowTemplate.Height = 30; // تغيير ارتفاع الصف 

            dataGridView2.Sort(this.dataGridView2.Columns["eventGroup_no"], ListSortDirection.Ascending); // ترتيب البيانات تصاعديا او تنازليا


            ////****************************************************************************************************************************************************
            //// عناوين الاعدمة 

            //dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // توسيط عناوين الاعمدة
            //dataGridView2.ColumnHeadersVisible = false;  // اخفاء صف العناوين
            //dataGridView2.Columns[5].HeaderCell.Style.Font = new Font("Arial", 75); // تغيير حجم الخط لاحد الخلايا بصف العناوين
            //dataGridView2.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomLeft; // محاذاة الخط بأحد الخلايا في صف العناوين
            //dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 100); // الخط لصف العناوين
            dataGridView2.EnableHeadersVisualStyles = false; // السماح بتغيير صف العناوين
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(233, 236, 250); // خلفية صف عناوين الاعمدة
            dataGridView2.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(233, 236, 250); // لون خلفية خلية عنوان العمود عند تحديده


            //dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;  // لون الخط لصف عناوين الاعمدة 
            //dataGridView2.Columns["property_name"].HeaderCell.Style.BackColor = Color.Red; // تلوين خلفية عنوان عمود معين
            //dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing; // السماح بتغيير حجم حقول عناوين الاعمدة
            //dataGridView2.ColumnHeadersHeight = 100; // تغيير ارتفاع حقول عناوين الاعمدة 

            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None; // بدون حدود بين خلايا عناوين الاعمدة
            //dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised; //  حدود بين خلايا عناوين الاعمدة تسمح بتغيير العرض
            //dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single; //  حدود بين خلايا عناوين الاعمدة عمودية ولونها نفس لون الحدود بين بقية الخلايا


            ////****************************************************************************************************************************************************
            ////   تصميم ستايل معين ومن ثم تم تطبيقه على صف العنوان

            //DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            //dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //dataGridViewCellStyle1.BackColor = SystemColors.Control;
            //dataGridViewCellStyle1.Font = new Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            //dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            //dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            //dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            //dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True; // السماح بسطور متعددة في نفس الحلق 
            //dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;

            ////****************************************************************************************************************************************************
            ////  عناوين الصفوف

            //dataGridView2.EnableHeadersVisualStyles = false; // السماح بتحرير عناوين الصفوف
            //dataGridView2.RowHeadersDefaultCellStyle.BackColor = Color.Red; // لون خلفية عناوين الصفوف
            //dataGridView2.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Yellow; // لون خلفية عناوين الصفوف عند تحديدها 
            dataGridView2.RowHeadersVisible = false; // اخفاء واظهار عناوين الصفوف
            ////****************************************************************************************************************************************************

        }


        private void fillComboboxProperty()
        {
            SqlConnection con = myConnection.con1();
            SqlCommand com = new SqlCommand("select * from property", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();
            cb_property.DataSource = dt;
            cb_property.ValueMember = dt.Columns[0].ColumnName;
            cb_property.DisplayMember = dt.Columns[1].ColumnName;
        }

        private void fillComboboxEvent()
        {
            SqlConnection con = myConnection.con1();
            SqlCommand com = new SqlCommand("select * from eevent", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();
            cb_event.DataSource = dt;
            cb_event.ValueMember = dt.Columns[0].ColumnName;
            cb_event.DisplayMember = dt.Columns[1].ColumnName;

            
        }
        private void frm_element_Load(object sender, EventArgs e)
        {
            btn_first_Click(null,null);

            fillComboboxProperty();
            fillComboboxEvent();

            fillPropertyDataGrid();
            fillEventDataGrid();
        }

        private void btn_addProperty_Click(object sender, EventArgs e)
        {
            try
            {
             
                SqlConnection con = myConnection.con1();
                SqlCommand com = new SqlCommand("insert into linkElementWithProperty values (" + txt_elementNo.Text + "," + cb_property.SelectedValue + ",' ') ", con);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }


            fillPropertyDataGrid();
        }

        private void btn_DeleteProperty_Click(object sender, EventArgs e)
        {
            try
            {

                SqlConnection con = myConnection.con1();
                SqlCommand com = new SqlCommand("delete from linkElementWithProperty where  linkElementWithProperty_elementNo = " + txt_elementNo.Text + " and  linkElementWithProperty_propertyNo = " + cb_property.SelectedValue , con);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }


            fillPropertyDataGrid();
        }

        private void btn_addEvent_Click(object sender, EventArgs e)
        {
            try
            {

                SqlConnection con = myConnection.con1();
                SqlCommand com = new SqlCommand("insert into linkElementWithEvent values (" + txt_elementNo.Text + "," + cb_event.SelectedValue + ",' ') ", con);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }

            fillEventDataGrid();
        }

        private void btn_DeleteEvent_Click(object sender, EventArgs e)
        {
            try
            {

                SqlConnection con = myConnection.con1();
                SqlCommand com = new SqlCommand("delete from linkElementWithEvent where  linkElementWithEvent_elementNo = " + txt_elementNo.Text + " and  linkElementWithEvent_eeventNo = " + cb_event.SelectedValue, con);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }

            fillEventDataGrid();
        }

        private void btn_addProperty_Click_1(object sender, EventArgs e)
        {
            btn_addProperty_Click(null, null);
        }

        private void btn_DeleteProperty_Click_1(object sender, EventArgs e)
        {
            btn_DeleteProperty_Click(null, null);
        }

        private void btn_addEvent_Click_1(object sender, EventArgs e)
        {
            btn_addEvent_Click(null, null);
        }

        private void btn_DeleteEvent_Click_1(object sender, EventArgs e)
        {
            btn_DeleteEvent_Click(null, null);
        }



        private void btn_codeNew_Click(object sender, EventArgs e)
        {
            txt_codeNo.Text = "ترقيم تلقائي";
            txt_codeCode.Text = "";
            txt_codeDescription.Text = "";
            txt_codeCode.Enabled = true;
            txt_codeDescription.Enabled = true;
        }

        private void btn_codeSave_Click(object sender, EventArgs e)
        {
            //if (txt_codeCode.Text == "" || txt_codeDescription.Text == "")
            //{
            //    MessageBox.Show("رجاء قم بتعبئة جميع الحقول");
            //}
            //else
            //{
            //    if (txt_codeNo.Text != "ترقيم تلقائي")
            //    {
            //        MessageBox.Show("رجاء قم بالضغط على الزر جديد");
            //    }
            //    else
            //    {
            //        SqlConnection con = myConnection.con1();
            //        SqlCommand com = new SqlCommand("insert into code values (@elementNo,@codeCode,@Description)", con);
            //        com.Parameters.AddWithValue("@elementNo" , txt_elementNo.Text);
            //        com.Parameters.AddWithValue("@codeCode", txt_codeCode.Text);
            //        com.Parameters.AddWithValue("@Description", txt_codeDescription.Text);

            //        con.Open();
            //        com.ExecuteNonQuery();
            //        con.Close();
            //        btn_codeLast_Click(null, null);
            //    }
            //}
            if (txt_codeNo.Text != "ترقيم تلقائي")
            {
                MessageBox.Show("رجاء قم بالضغط على الزر جديد");
            }
            else
            {
                if (txt_codeCode.Text == "" || txt_codeDescription.Text == "")
                {
                    MessageBox.Show("رجاء قم بتعبئة جميع الحقول"); 
                }
                else
                {
                    SqlConnection con = myConnection.con1();
                    SqlCommand com = new SqlCommand("insert into code values (@elementNo,@codeCode,@Description)", con);
                    com.Parameters.AddWithValue("@elementNo", txt_elementNo.Text);
                    com.Parameters.AddWithValue("@codeCode", txt_codeCode.Text);
                    com.Parameters.AddWithValue("@Description", txt_codeDescription.Text);

                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();
                    btn_codeLast_Click(null, null);
                }
            }

        }

        private void btn_codeEdit_Click(object sender, EventArgs e)
        {
            if (btn_codeEdit.Text == "تعديل")
            {
                txt_codeCode.BackColor = Color.Yellow;
                txt_codeDescription.BackColor = Color.Yellow;

                txt_codeCode.Enabled = true;
                txt_codeDescription.Enabled = true;
                btn_codeEdit.Text = "حفظ التعديلات";
            }
            else
            {
                SqlConnection con = myConnection.con1();
                SqlCommand com = new SqlCommand("update code set  code_code = '" + txt_codeCode.Text + "',code_description = '" + txt_codeDescription.Text + " ' where code_no =" + txt_codeNo.Text, con);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();

                txt_codeCode.BackColor = Color.White;
                txt_codeDescription.BackColor = Color.White;

                txt_codeCode.Enabled = false;
                txt_codeDescription.Enabled = false;


                btn_codeEdit.Text = "تعديل";
                MessageBox.Show("تمت التعديلات بنجاح");
            }
        }

        private void btn_codeDelete_Click(object sender, EventArgs e)
        {
            txt_codeCode.Enabled = false;
            txt_codeDescription.Enabled = false;

            if (txt_codeNo.Text != "" && txt_codeNo.Text != "ترقيم تلقائي")
            {
                SqlConnection con = myConnection.con1();
                SqlCommand com = new SqlCommand("delete from code  where code_no =" + txt_codeNo.Text, con);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("تمت عملية الحذف بنجاح");

                txt_codeNo.Text = "";
                txt_codeCode.Text = "";
                txt_codeDescription.Text = "";


                btn_codefirst_Click(null, null);
            }
        }

        int y = 0;
        private void btn_codefirst_Click(object sender, EventArgs e)
        {


            txt_codeCode.Enabled = false;
            txt_codeDescription.Enabled = false;

            string elementNo = txt_elementNo.Text;
            if (elementNo == "")
            {
                elementNo = "0";
            }


            y = 0;
            SqlConnection con = myConnection.con1();
            string comString = "select * from code where code_elementNo = "+elementNo;
            SqlCommand com = new SqlCommand(comString, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();

            if (dt.Rows.Count > 0)
            {
                //-------1
                txt_codeNo.Text = dt.Rows[y][0].ToString();
                //-------2
                txt_codeCode.Text = dt.Rows[y][2].ToString();
                //-------3
                txt_codeDescription.Text = dt.Rows[y][3].ToString();

            }
        }

        private void btn_codePrevious_Click(object sender, EventArgs e)//*********************-------------------->>>>>>>>>>>>>>>>>??????????
        {
            txt_codeCode.Enabled = false;
            txt_codeDescription.Enabled = false;

            y -= 1;
            if (y < 0)
            {
                y = 0;
                MessageBox.Show("لا توجد حسابات قبل هذا الحساب");
            }

            SqlConnection con = myConnection.con1();
            string comString = "select * from code where code_elementNo = " + txt_elementNo.Text;
            SqlCommand com = new SqlCommand(comString, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();


            if (dt.Rows.Count > 0)
            {
                //-------1
                txt_codeNo.Text = dt.Rows[y][0].ToString();
                //-------2
                txt_codeCode.Text = dt.Rows[y][2].ToString();
                //-------3
                txt_codeDescription.Text = dt.Rows[y][3].ToString();
                //-------4

            }
        }

        private void btn_codeNext_Click(object sender, EventArgs e)
        {

            txt_codeCode.Enabled = false;
            txt_codeDescription.Enabled = false;

            SqlConnection con = myConnection.con1();
            string comString = "select * from code where code_elementNo = " + txt_elementNo.Text;
            SqlCommand com = new SqlCommand(comString, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();


            y += 1;
            if (y > dt.Rows.Count - 1)
            {
                y = dt.Rows.Count - 1;
                MessageBox.Show("لا توجد حسابات بعد هذا الحساب");
            }


            if (dt.Rows.Count > 0)
            {
                //-------1
                txt_codeNo.Text = dt.Rows[y][0].ToString();
                //-------2
                txt_codeCode.Text = dt.Rows[y][2].ToString();
                //-------3
                txt_codeDescription.Text = dt.Rows[y][3].ToString();

            }
        }

        private void btn_codeLast_Click(object sender, EventArgs e)
        {

            txt_codeCode.Enabled = false;
            txt_codeDescription.Enabled = false;

            SqlConnection con = myConnection.con1();
            string comString = "select * from code where code_elementNo = " + txt_elementNo.Text;
            SqlCommand com = new SqlCommand(comString, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();

            y = dt.Rows.Count - 1;


            if (dt.Rows.Count > 0)
            {
                //-------1
                txt_codeNo.Text = dt.Rows[y][0].ToString();
                //-------2
                txt_codeCode.Text = dt.Rows[y][2].ToString();
                //-------3
                txt_codeDescription.Text = dt.Rows[y][3].ToString();

            }
        }

        private void btn_ExplanationNew_Click(object sender, EventArgs e)
        {
            txt_ExplanationNo.Text = "ترقيم تلقائي";
            txt_ExplanationTitle.Text = "";
            txt_ExplanationExplanation.Text = "";
            txt_ExplanationTitle.Enabled = true;
            txt_ExplanationExplanation.Enabled = true;
        }

        private void btn_ExplanationSave_Click(object sender, EventArgs e)
        {
            if (txt_ExplanationNo.Text != "ترقيم تلقائي")
            {
                MessageBox.Show("رجاء قم بالضغط على الزر جديد");
            }
            else
            {
                if (txt_ExplanationTitle.Text == "" || txt_ExplanationExplanation.Text == "")
                {
                    MessageBox.Show("رجاء قم بتعبئة جميع الحقول");
                }
                else
                {
                    SqlConnection con = myConnection.con1();
                    SqlCommand com = new SqlCommand("insert into Explanation values (@elementNo,@codeCode,@Description)", con);
                    com.Parameters.AddWithValue("@elementNo", txt_elementNo.Text);
                    com.Parameters.AddWithValue("@codeCode", txt_ExplanationTitle.Text);
                    com.Parameters.AddWithValue("@Description", txt_ExplanationExplanation.Text);

                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();
                    btn_codeLast_Click(null, null);
                }
            }

            txt_ExplanationTitle.Enabled = false;
            txt_ExplanationExplanation.Enabled = false;
        }

        private void btn_ExplanationEdit_Click(object sender, EventArgs e)
        {
            if (btn_ExplanationEdit.Text == "تعديل")
            {
                txt_ExplanationTitle.BackColor = Color.Yellow;
                txt_ExplanationExplanation.BackColor = Color.Yellow;

                txt_ExplanationTitle.Enabled = true;
                txt_ExplanationExplanation.Enabled = true;
                btn_ExplanationEdit.Text = "حفظ التعديلات";
            }
            else
            {
                SqlConnection con = myConnection.con1();
                SqlCommand com = new SqlCommand("update Explanation set  Explanation_Title = '" + txt_ExplanationTitle.Text + "',Explanation_Explanation = '" + txt_ExplanationExplanation.Text + " ' where Explanation_no =" + txt_ExplanationNo.Text, con);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();

                txt_ExplanationTitle.BackColor = Color.White;
                txt_ExplanationExplanation.BackColor = Color.White;

                txt_ExplanationTitle.Enabled = false;
                txt_ExplanationExplanation.Enabled = false;


                btn_ExplanationEdit.Text = "تعديل";
                MessageBox.Show("تمت التعديلات بنجاح");
            }
        }

        private void btn_ExplanationDelete_Click(object sender, EventArgs e)
        {
            txt_ExplanationTitle.Enabled = false;
            txt_ExplanationExplanation.Enabled = false;

            if (txt_ExplanationNo.Text != "" && txt_ExplanationNo.Text != "ترقيم تلقائي")
            {
                SqlConnection con = myConnection.con1();
                SqlCommand com = new SqlCommand("delete from Explanation  where Explanation_no =" + txt_ExplanationNo.Text, con);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("تمت عملية الحذف بنجاح");

                txt_ExplanationNo.Text = "";
                txt_ExplanationTitle.Text = "";
                txt_ExplanationExplanation.Text = "";


                btn_ExplanationFrist_Click(null,null);
            }
        }

        int yyy=0;
        private void btn_ExplanationFrist_Click(object sender, EventArgs e)
        {


            txt_ExplanationTitle.Enabled = false;
            txt_ExplanationExplanation.Enabled = false;

            string elementNo = txt_elementNo.Text;
            if (elementNo == "")
            {
                elementNo = "0";
            }


            yyy = 0;
            SqlConnection con = myConnection.con1();
            string comString = "select * from Explanation where Explanation_elementNo = " + elementNo;
            SqlCommand com = new SqlCommand(comString, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();

            if (dt.Rows.Count > 0)
            {
                //-------1
                txt_ExplanationNo.Text = dt.Rows[yyy][0].ToString();
                //-------2
                txt_ExplanationTitle.Text = dt.Rows[yyy][2].ToString();
                //-------3
                txt_ExplanationExplanation.Text = dt.Rows[yyy][3].ToString();

            }
            else
            {
                txt_ExplanationNo.Text = "";
                txt_ExplanationTitle.Text = "";
                txt_ExplanationExplanation.Text = "";
            }
        }

        private void btn_ExplanationPrevious_Click(object sender, EventArgs e)
        {
            txt_ExplanationTitle.Enabled = false;
            txt_ExplanationExplanation.Enabled = false;

            yyy -= 1;
            if (yyy < 0)
            {
                yyy = 0;
                MessageBox.Show("لا توجد حسابات قبل هذا الحساب");
            }


            string elementNo = txt_elementNo.Text;
            if (elementNo == "")
            {
                elementNo = "0";
            }

            SqlConnection con = myConnection.con1();
            string comString = "select * from Explanation where Explanation_elementNo = " + elementNo;
            SqlCommand com = new SqlCommand(comString, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();


            if (dt.Rows.Count > 0)
            {
                //-------1
                txt_ExplanationNo.Text = dt.Rows[yyy][0].ToString();
                //-------2
                txt_ExplanationTitle.Text = dt.Rows[yyy][2].ToString();
                //-------3
                txt_ExplanationExplanation.Text = dt.Rows[yyy][3].ToString();
            }

        }
        

        private void btn_ExplanationNext_Click(object sender, EventArgs e)
        {
            string elementNo = txt_elementNo.Text;
            if (elementNo == "")
            {
                elementNo = "0";
            }
            SqlConnection con = myConnection.con1();
            string comString = "select * from Explanation where Explanation_elementNo = " + elementNo;
            SqlCommand com = new SqlCommand(comString, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();

            txt_ExplanationTitle.Enabled = false;
            txt_ExplanationExplanation.Enabled = false;


            yyy += 1;
            if (yyy > dt.Rows.Count - 1)
            {
                yyy = dt.Rows.Count - 1;
                MessageBox.Show("لا توجد حسابات بعد هذا الحساب");
            }


            if (dt.Rows.Count > 0)
            {
                //-------1
                txt_ExplanationNo.Text = dt.Rows[yyy][0].ToString();
                //-------2
                txt_ExplanationTitle.Text = dt.Rows[yyy][2].ToString();
                //-------3
                txt_ExplanationExplanation.Text = dt.Rows[yyy][3].ToString();
            }

        }

        private void btn_ExplanationLast_Click(object sender, EventArgs e)
        {
            string elementNo = txt_elementNo.Text;
            if (elementNo == "")
            {
                elementNo = "0";
            }
            SqlConnection con = myConnection.con1();
            string comString = "select * from Explanation where Explanation_elementNo = " + elementNo;
            SqlCommand com = new SqlCommand(comString, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            dt.Load(dr);
            con.Close();

            txt_ExplanationTitle.Enabled = false;
            txt_ExplanationExplanation.Enabled = false;


            yyy = dt.Rows.Count - 1;

            if (dt.Rows.Count > 0)
            {
                //-------1
                txt_ExplanationNo.Text = dt.Rows[yyy][0].ToString();
                //-------2
                txt_ExplanationTitle.Text = dt.Rows[yyy][2].ToString();
                //-------3
                txt_ExplanationExplanation.Text = dt.Rows[yyy][3].ToString();
            }
        }
    }
}
