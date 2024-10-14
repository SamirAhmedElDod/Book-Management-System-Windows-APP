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
    public partial class Add : Form
    {
        // Global Variables
        OpenFileDialog dialog;
        
        private Category Category_Form;
        public Add()
        {
            InitializeComponent();
            AddCategoryComboBox();
        }



        // Pass Combo Box 
        public Guna2ComboBox ComboBoxCategories
        {
            get { return CategoryCB; }
        }

        // Add New Book Button ( That Will Add To Form1 (Basic Form ) );
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

            // Here I Will Get The Dialog File Name == (Image Path)
            //Image BookCover = guna2PictureBox1.Image;
            string ImageCover = "";
            if (dialog.FileName != string.Empty)
            {
                ImageCover = dialog.FileName; 
            } 
            else
            {
                ImageCover = "Empty Path";
            }


            string Date = guna2DateTimePicker1.Text;
            float ratingValue = guna2RatingStar1.Value;


            GlobalSettings.GlobalID++;


            int RowsAffected = clsDataAccessLayer.AddNewBook(BookTitle , Author , Price , ImageCover, BookCategory , ratingValue , Date);

            //basicForm.myDataTable.Rows.Add(GlobalSettings.GlobalID, BookTitle, Author, Price, BookCover,BookCategory, ratingValue, Date);

            guna2TextBox1.Clear();
            guna2TextBox2.Clear();
            guna2TextBox3.Clear();
            guna2RatingStar1.Value= 0;

            // Show Added Successfully Form 
            Dialog_Add dialog_Add_Form = new Dialog_Add();
            dialog_Add_Form.ShowDialog();

            this.Close();
        }

        
        // Close Button
        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Open ( Add Category ) Button
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (Category_Form == null || Category_Form.IsDisposed)
            {
                Category_Form = new Category();
                Guna2Transition transition = new Guna2Transition();
                transition.ShowSync(Category_Form);
                Category_Form.FormClosed += (s, args) => _refreshComboBox();
            }
            else
            {
                Category_Form.BringToFront();
            }

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

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddCategoryComboBox()
        {
            DataTable CategoryDataTable = clsDataAccessLayer.GetAllCategory();
            
            foreach(DataRow row in CategoryDataTable.Rows)
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
