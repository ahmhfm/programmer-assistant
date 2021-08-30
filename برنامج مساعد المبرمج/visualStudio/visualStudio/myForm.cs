using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace visualStudio
{
    class myForm
    {
        /*
        in properties write this Variables  : 
        1 form_Width  int
        2 form_Height  int
        3 form_left  int
        4 form_top  int 
        */

        // تغيير قيم الطول والعرض للنموذج في الخصائص
        public static void chingeSize(int width , int height )
        {
            Properties.Settings.Default["form_Width"] = width ;
            Properties.Settings.Default["form_Height"] = height;
            Properties.Settings.Default.Save();
        }


        // تغيير قيم موقع النموذج في الخصائص
        public static void chingeLocation(int left , int top )
        {
            Properties.Settings.Default["form_left"] = left ;
            Properties.Settings.Default["form_top"] = top ;
            Properties.Settings.Default.Save();
        }

        // الحصول على قيم الطول العرض للنموذج
        public static int[] getSize()
        {
            int[] size = new int[2];
            size[0] = int.Parse(Properties.Settings.Default["form_Width"].ToString() );
            size[1] = int.Parse(Properties.Settings.Default["form_Height"].ToString());
            return size;
        }

        // الحصول على موقع النموذج
        public static int[] getLocation()
        {
            int[] size = new int[2];
            size[0] = int.Parse(Properties.Settings.Default["form_left"].ToString());
            size[1] = int.Parse(Properties.Settings.Default["form_top"].ToString());
            return size;
        }

        public static void formSizeAndLocation()
        {
            Form frm = new Form();
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.BackColor = Color.DarkCyan;


            //-------------------------------------------
            Button btn = new Button();
            btn.BackColor = Color.LightBlue;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Location = new Point(0, 0);
            btn.Dock = DockStyle.Top;
            btn.Text = "Save Size And Location";
            btn.Click += delegate
            {
                chingeSize(frm.Width, frm.Height);
                chingeLocation(frm.Left, frm.Top);
                frm.Close();
            };

            //-------------------------------------------
            Button btn1 = new Button();
            btn1.Width = 30;
            btn1.Height = 30;
            btn1.BackColor = Color.Orange;
            btn1.FlatStyle = FlatStyle.Flat;
            btn1.FlatAppearance.BorderSize = 0;
            btn1.Text = "←";
            btn1.Click += delegate
            {
                frm.Left = frm.Left - 1;
            };

            //-------------------------------------------
            Button btn2 = new Button();
            btn2.Width = 30;
            btn2.Height = 30;
            btn2.BackColor = Color.Orange;
            btn2.FlatStyle = FlatStyle.Flat;
            btn2.FlatAppearance.BorderSize = 0;
            btn2.Anchor = (AnchorStyles.Right | AnchorStyles.Top);
            btn2.Text = "→";
            btn2.Click += delegate
            {
                frm.Left = frm.Left + 1;
            };
            //-------------------------------------------
            int topOf_9_1_2_3_4 = btn.Height + 30;
            int leftOf_9_5_6_7_8 = 30;
            //-------------------------------------------
            Button btn9 = new Button();
            btn9.Width = leftOf_9_5_6_7_8 + 28;
            btn9.Height = topOf_9_1_2_3_4 - btn.Height + 28;
            btn9.BackColor = Color.GreenYellow;
            btn9.FlatStyle = FlatStyle.Flat;
            btn9.FlatAppearance.BorderSize = 0;
            btn9.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            btn9.Text = "+";
            bool mouseDown = false;
            int click_x = 0;
            int click_y = 0;
            btn9.MouseDown += delegate (object sender, MouseEventArgs e)
            {
                btn9.Cursor = Cursors.Hand;
                mouseDown = true;
                click_x = e.Location.X;
                click_y = e.Location.Y;
            };
            btn9.MouseMove += delegate (object sender, MouseEventArgs e)
            {
                if (mouseDown)
                {
                    int y = (frm.Location.Y - click_y) + e.Location.Y;
                    int x = (frm.Location.X - click_x) + e.Location.X;

                    frm.Location = new Point(x, y);
                }

            };
            btn9.MouseUp += delegate
            {
                mouseDown = false;
                btn9.Cursor = Cursors.Default;
            };

            //-------------------------------------------
            Button btn3 = new Button();
            btn3.Width = 30;
            btn3.Height = 30;
            btn3.BackColor = Color.Yellow;
            btn3.FlatStyle = FlatStyle.Flat;
            btn3.FlatAppearance.BorderSize = 0;
            btn3.Anchor = (AnchorStyles.Left | AnchorStyles.Top);
            btn3.Text = "←";
            btn3.Click += delegate
            {
                if (frm.Width > btn9.Width + 2 + 120 + 10) frm.Width = frm.Width - 1;
            };

            //-------------------------------------------
            Button btn4 = new Button();
            btn4.Width = 30;
            btn4.Height = 30;
            btn4.BackColor = Color.Yellow;
            btn4.FlatStyle = FlatStyle.Flat;
            btn4.FlatAppearance.BorderSize = 0;
            btn4.Anchor = (AnchorStyles.Right | AnchorStyles.Top);
            btn4.Text = "→";
            btn4.Click += delegate
            {
                    frm.Width = frm.Width + 1;
            };

            //-------------------------------------------
            Button btn5 = new Button();
            btn5.Width = 30;
            btn5.Height = 30;
            btn5.BackColor = Color.Orange;
            btn5.FlatStyle = FlatStyle.Flat;
            btn5.FlatAppearance.BorderSize = 0;
            btn5.Text = "↑";
            btn5.Click += delegate
            {
                frm.Top = frm.Top - 1;
            };

            //-------------------------------------------
            Button btn6 = new Button();
            btn6.Width = 30;
            btn6.Height = 30;
            btn6.BackColor = Color.Orange;
            btn6.FlatStyle = FlatStyle.Flat;
            btn6.FlatAppearance.BorderSize = 0;
            btn6.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            btn6.Text = "↓";
            btn6.Click += delegate
            {
                frm.Top = frm.Top + 1;
            };

            //-------------------------------------------
            Button btn7 = new Button();
            btn7.Width = 30;
            btn7.Height = 30;
            btn7.BackColor = Color.Yellow;
            btn7.FlatStyle = FlatStyle.Flat;
            btn7.FlatAppearance.BorderSize = 0;
            btn7.Text = "↑";
            btn7.Click += delegate
            {
                if(frm.Height > topOf_9_1_2_3_4 + 150) frm.Height = frm.Height - 1;
            };

            //-------------------------------------------
            Button btn8 = new Button();
            btn8.Width = 30;
            btn8.Height = 30;
            btn8.BackColor = Color.Yellow;
            btn8.FlatStyle = FlatStyle.Flat;
            btn8.FlatAppearance.BorderSize = 0;
            btn8.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            btn8.Text = "↓";
            btn8.Click += delegate
            {
                frm.Height = frm.Height + 1;
            };

            

            //-------------------------------------------
            Button btn10 = new Button();
            btn10.ForeColor = Color.White;
            btn10.Width = frm.Width-btn9.Width-2;
            btn10.Height = topOf_9_1_2_3_4 - btn.Height -2;
            btn10.BackColor = Color.DarkMagenta;
            btn10.FlatStyle = FlatStyle.Flat;
            btn10.FlatAppearance.BorderSize = 0;
            btn10.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            btn10.Text = "↔";


            btn10.MouseDown += delegate (object sender, MouseEventArgs e)
            {
                if(e.X < btn10.Width/2)
                {
                    if(frm.Width>btn9.Width+2+120+10)
                    {
                        frm.Width -= 20;
                    }

                }else if(e.X > btn10.Width / 2)
                {
                        frm.Width += 20;
                }
            };

            //-------------------------------------------
            Button btn11 = new Button();
            btn11.ForeColor = Color.White;
            btn11.Width = leftOf_9_5_6_7_8-2 ;
            btn11.Height = frm.Height-topOf_9_1_2_3_4+30 ;
            btn11.BackColor = Color.DarkMagenta;
            btn11.FlatStyle = FlatStyle.Flat;
            btn11.FlatAppearance.BorderSize = 0;
            btn11.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left);
            btn11.Text = "↕";


            btn11.MouseDown += delegate (object sender, MouseEventArgs e)
            {
                if (e.Y < btn11.Height / 2)
                {
                    if(frm.Height> topOf_9_1_2_3_4 + 170)
                    {
                        frm.Height -= 20;
                    }

                }
                else if (e.Y > btn11.Height / 2)
                {
                    frm.Height += 20;
                }
            };

            //-------------------------------------------

            frm.Controls.Add(btn);
            frm.Controls.Add(btn1);
            frm.Controls.Add(btn2);
            frm.Controls.Add(btn3);
            frm.Controls.Add(btn4);
            frm.Controls.Add(btn5);
            frm.Controls.Add(btn6);
            frm.Controls.Add(btn7);
            frm.Controls.Add(btn8);
            frm.Controls.Add(btn9);
            frm.Controls.Add(btn10);
            frm.Controls.Add(btn11);


            //-------------------------------------------
            frm.Load += delegate 
            {
                frm.Size = new Size(myForm.getSize()[0], myForm.getSize()[1]);
                frm.Location = new Point(myForm.getLocation()[0], myForm.getLocation()[1]);


                btn1.Location = new Point(leftOf_9_5_6_7_8+30, topOf_9_1_2_3_4);
                btn2.Location = new Point(frm.Width - btn2.Width , topOf_9_1_2_3_4);
                btn3.Location = new Point(leftOf_9_5_6_7_8 +30+ btn1.Width, topOf_9_1_2_3_4);
                btn4.Location = new Point(frm.Width  - btn4.Width - btn2.Width, topOf_9_1_2_3_4);
                btn5.Location = new Point(leftOf_9_5_6_7_8, topOf_9_1_2_3_4 + btn1.Height );
                btn6.Location = new Point(leftOf_9_5_6_7_8, frm.Height - btn6.Height);
                btn7.Location = new Point(leftOf_9_5_6_7_8, topOf_9_1_2_3_4 + btn1.Height+btn5.Height);
                btn8.Location = new Point(leftOf_9_5_6_7_8, frm.Height-btn6.Height-btn8.Height);
                btn9.Location = new Point(1, btn.Height+1);
                btn10.Location = new Point(btn9.Width+2, btn.Height + 1);
                btn11.Location = new Point(1, btn9.Width + 1);


            };
            frm.Opacity= .99 ;
            frm.ShowDialog();

        }
    }
}


/*
in main form write this methodes :
 
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
        }

*/
