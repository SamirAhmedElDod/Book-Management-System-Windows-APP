using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Policy;
using DataAccessLayer;

namespace BOOKSYSTEM
{
    public partial class Form1 : Form
    {
        // Global Variables
        string url = "";
        int Move;
        int MoveX;
        int MoveY;

        public Form1()
        {
            InitializeComponent();
            _refreshAllBooksDataGridView();
        }

        // Recieve Data Access Layer Here --> ( Connect To Database => Return All Books )
        private void _refreshAllBooksDataGridView()
        {          
            guna2DataGridView2.DataSource = clsDataAccessLayer.GetAllBooks();
        }



        public static class GlobalSettings
        {
            public static int GlobalID = 0;
        }
        //  Pass Data Grid View
        public Guna2DataGridView myDataTable
        {
            get { return guna2DataGridView2; }
        }
        
    
        // All Forms
        private Add Add_Form;
        private Edit Edit_Form;
        private Delete Delete_Form;
        private Information information_Form;

        // Add Button
        private void guna2Button5_Click(object sender, EventArgs e)
        {

            if (Add_Form == null || Add_Form.IsDisposed)
            {

                Add_Form = new Add();
                Guna2Transition myTransition = new Guna2Transition();
                myTransition.ShowSync(Add_Form);
            }
            else
            {
                Add_Form.BringToFront();
            }

            Add_Form.FormClosed += (s , args) => _refreshAllBooksDataGridView();
        }

        // Edit Button
        private void guna2Button6_Click(object sender, EventArgs e)
        {
            if (Edit_Form == null || Edit_Form.IsDisposed)
            {
                Edit_Form = new Edit();
                Guna2Transition myTransition = new Guna2Transition();
                myTransition.ShowSync(Edit_Form);
                Edit_Form.FormClosed += (s, args) => _refreshAllBooksDataGridView();
            }
            else
            {
                Edit_Form.BringToFront();
            }

        }

        // Delete Button
        private void guna2Button7_Click(object sender, EventArgs e)
        {
            if (Delete_Form == null || Delete_Form.IsDisposed) 
            {
                Delete_Form = new Delete();
                Guna2Transition myTransition = new Guna2Transition();
                myTransition.ShowSync(Delete_Form);
                Delete_Form.FormClosed += (s, args) => _refreshAllBooksDataGridView();
            }
            else
            {
                Delete_Form.BringToFront();
            }
        }

        // information Button
        private void guna2Button8_Click(object sender, EventArgs e)
        {
            if (information_Form == null || information_Form.IsDisposed)
            { 
                information_Form = new Information();
                Guna2Transition myTransition = new Guna2Transition();
                myTransition.ShowSync(information_Form);
                information_Form.FormClosed += (s, args) => _refreshAllBooksDataGridView();

            }
            else
            {
                information_Form.BringToFront();
            }
        }

        // Minimize Button
        private void guna2CirclePictureBox3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }


        // Close Button
        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();            
        }

        // Start ( Mouse ) Header Move While Drag 
        private void guna2Panel4_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            MoveX = e.X;
            MoveY = e.Y;
        }

        private void guna2Panel4_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;

        }

        private void guna2Panel4_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move== 1)
            {
                this.SetDesktopLocation(MousePosition.X - MoveX, MousePosition.Y - MoveY);
            }
        }
        // End ( Mouse )


        // Clear DataGridView Selection
        private void Form1_Activated(object sender, EventArgs e)
        {

            guna2DataGridView2.ClearSelection();


        }



        // Start Social Media Process 
        private void Social()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        private void guna2CirclePictureBox4_Click(object sender, EventArgs e)
        {
            url = "https://api.whatsapp.com/send/?phone=01277040276&text&type=phone_number&app_absent=0";
            Social();
        }

        private void guna2CirclePictureBox5_Click(object sender, EventArgs e)
        {
            url = "https://www.facebook.com/samir.elboss.33";
            Social();
        }

        private void guna2CirclePictureBox6_Click(object sender, EventArgs e)
        {
            url = "https://www.linkedin.com/in/samir-ahmed-38943a325";
            Social();
        }

        private void guna2CirclePictureBox7_Click(object sender, EventArgs e)
        {
            url = "mailto:basm.fathy159@gmail.com";
            Social();
        }
        // End Social Media Process 
    }
}
