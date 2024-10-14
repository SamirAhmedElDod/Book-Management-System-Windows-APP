using DataAccessLayer;
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
using static BOOKSYSTEM.Form1;

namespace BOOKSYSTEM
{
    public partial class Delete : Form
    {
        // Global Variables
        int Selected_ID;
        OpenFileDialog dialog;

        // Recieve Basic Form ( Main Form ) To This Form
        public Delete()
        {
            InitializeComponent(); 
            _refreshComboBox();
        }

        // Delete Book Button ( That Will Add To Form1 (Basic Form ) );
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            int RowsAffected = clsDataAccessLayer.DeleteBookByID(Selected_ID);
            if (RowsAffected > 0)
            {
                // Show Deleted Successfully Form 
                Dialog_Delete dialog_Delete_Form = new Dialog_Delete();
                dialog_Delete_Form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Failed To Delete This Book", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            this.Close();
        }

  
        // Close Button
        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Upload Button
        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            dialog = new OpenFileDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                guna2PictureBox1.Image = Image.FromFile(dialog.FileName);
            }
        }

        // Search By ID && Fill Data Button
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            _refreshComboBox();
            Selected_ID = Convert.ToInt16(guna2TextBox4.Text);
            // Fill Data With Struct From Database
            clsDataAccessLayer.stBookInfo RecievedBook = clsDataAccessLayer.GetSingleBookByID(Selected_ID);

            guna2TextBox1.Text = RecievedBook.BookTitle;
            guna2TextBox2.Text = RecievedBook.Author;
            guna2TextBox3.Text = RecievedBook.Price.ToString();
            int Index = CategoryCB.FindString(RecievedBook.Category);
            CategoryCB.SelectedIndex = Index;
            if (RecievedBook.ImagePath != "")
            {
                guna2PictureBox1.Image = Image.FromFile(RecievedBook.ImagePath);
            }
            else
            {
                guna2PictureBox1.Image = Properties.Resources.photo;
            }
            //guna2PictureBox1.Image = Image.FromFile(RecievedBook.ImagePath);
            guna2DateTimePicker1.Text = RecievedBook.Date_String;
            guna2RatingStar1.Value = RecievedBook.Rate;
        }


        private void AddCategoryComboBox()
        {
            DataTable CategoryDataTable = clsDataAccessLayer.GetAllCategory();

            foreach (DataRow row in CategoryDataTable.Rows)
            {
                string RecievedCategory = (string)row["CategoryName"];
                CategoryCB.Items.Add(RecievedCategory);
            }
        }
        private void _refreshComboBox()
        {
            CategoryCB.Items.Clear();
            AddCategoryComboBox();
        }
    }
}
