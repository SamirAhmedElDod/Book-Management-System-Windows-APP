using DataAccessLayer;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BOOKSYSTEM
{
    public partial class Edit: Form
    {
        // Global Variables
        int Selected_ID;        
        OpenFileDialog dialog;
        string RecievedImagePath = "";


        // Recieve Basic Form ( Main Form ) To This Form
        public Edit()
        {
            InitializeComponent();
        }

        // Edit Book Button ( That Will Add To Form1 (Basic Form ) );
        private void guna2Button5_Click(object sender, EventArgs e)
        {
           
            // Add Section
            string BookTitle = guna2TextBox1.Text;
            string Author = guna2TextBox2.Text;
            decimal Price;
            bool FloatSuccess = decimal.TryParse(guna2TextBox3.Text , out Price);
            if (!FloatSuccess)
            {
                MessageBox.Show("Invalid price format. Please enter a valid number.", "Price Error" , MessageBoxButtons.OK , MessageBoxIcon.Error);
                return; 
            }
            string BookCategory = CategoryCB.Text;
            //Image BookCover = guna2PictureBox1.Image;
            string ImagePath = "";
            if (dialog != null)
            {
                ImagePath = dialog.FileName;

            }
            else
            {
                ImagePath = RecievedImagePath;
            }
            string Date = guna2DateTimePicker1.Text;
            float ratingValue = guna2RatingStar1.Value;

            // Editing Cell With DataAccessLayer
            int RowsAffected = clsDataAccessLayer.UpdateBookByID
                (Selected_ID , BookTitle , Author , Price, ImagePath , BookCategory, ratingValue , Date);

            if (RowsAffected > 0)
            {
                // Show Added Successfully Form 
                Dialog_Edit dialog_Edit_Form = new Dialog_Edit();
                dialog_Edit_Form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Failed To Update | Edit This Book", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            guna2TextBox1.Clear();
            guna2TextBox2.Clear();
            guna2TextBox3.Clear();
            guna2RatingStar1.Value= 0;

            this.Close();
        }

      
        // Close Button
        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Upload Image
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
            RecievedImagePath = RecievedBook.ImagePath;
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
