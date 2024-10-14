using DataAccessLayer;
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
    public partial class Category : Form
    {
        public Category()
        {
            InitializeComponent();
        }

        // Close Button
        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Add New Category Button
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            int RowsAffected = 0;
            string newCategory = addCategoryTB.Text.Trim();

            if (!string.IsNullOrEmpty(newCategory))
            {
                // Recieve AddNew Category From Data Access Layer
                RowsAffected = clsDataAccessLayer.AddNewCategory(newCategory);
            }
            else
            {
                MessageBox.Show("Please enter a valid category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            if (RowsAffected > 0)
            {
                Dialog_Add dialog_Add_Form = new Dialog_Add();
                dialog_Add_Form.ShowDialog();
            }
            else
            {
                MessageBox.Show($"Faild To Add New Category", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            // Show Added Successfully Form 
           

            this.Close();

        }
    }
}
