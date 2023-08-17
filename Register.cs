using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; //connect with SQL server

namespace IncomeManagement
{
    public partial class Register : Form
    {
       
        public Register()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\incomeDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void Clear() //clear method
        {
            txtname.Text = "";
            txtpass.Text = "";
            txtconf.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtname.Text == "" || txtpass.Text == "" || txtconf.Text == "") //if the three text boxes are empty display this message
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try //if not, insert the details in tot he UserTbl
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into UserTbl values('"+txtname.Text+ "','"+txtpass.Text+"')",con);
                    cmd.Parameters.AddWithValue("@UN", txtname.Text);
                    cmd.Parameters.AddWithValue("@UP", txtpass.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User added!!");
                    con.Close();
                    Clear();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void showpass_CheckedChanged(object sender, EventArgs e)
        {
            if (showpass.Checked) //change the password text style into this symbol without displaying the real characters.
            {
                txtpass.PasswordChar = '\0';
                txtconf.PasswordChar = '\0';
            }
            else
            {
                txtpass.PasswordChar = '•';
                txtconf.PasswordChar = '•';
            }

        }

        private void label19_Click(object sender, EventArgs e)
        {
            Application.Exit(); //close the application
        }

        private void button3_Click(object sender, EventArgs e) //clear button
        {
            txtname.Clear();
            txtpass.Clear();
            txtconf.Clear();

            txtname.Focus();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            LogIn obj = new LogIn(); //open the login page
            obj.Show();
        }

        private void button4_Click(object sender, EventArgs e) //please avoid
        {
            
        }

        private void button5_Click(object sender, EventArgs e) 
        {
           
        }
    }
}
