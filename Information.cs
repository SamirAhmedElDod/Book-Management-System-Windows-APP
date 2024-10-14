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

namespace BOOKSYSTEM
{
    public partial class Information : Form
    {
        // Global Variables
        int Selected_ID;
        public Information()
        {
            InitializeComponent();
        }
        
        // Close Button
        private void guna2CirclePictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        // Search By ID && Fill Data Button
        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            _refreshComboBox();

            Selected_ID = Convert.ToInt16(guna2TextBox3.Text);

            clsDataAccessLayer.stBookInfo RecievedBook = clsDataAccessLayer.GetSingleBookByID(Selected_ID);

            guna2TextBox1.Text = RecievedBook.BookTitle;
            guna2TextBox5.Text = RecievedBook.Author;
            guna2TextBox4.Text = RecievedBook.Price.ToString();
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
