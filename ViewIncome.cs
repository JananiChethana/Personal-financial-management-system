using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient; //connect with SQL server
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IncomeManagement
{
    public partial class ViewIncome : Form
    {
        public ViewIncome()
        {
            InitializeComponent();
            DisplayIncomes();
        }
        DataTable table = new DataTable(); //yt
        int indexRow;
       
        private void label2_Click(object sender, EventArgs e) //go to dashboard
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e) //go to income
        {
            Income obj = new Income();
            obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e) //go to expenses
        {
            Expenses obj = new Expenses();
            obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e) //go to view expenses
        {
            ViewExpenses obj = new ViewExpenses();
            obj.Show();
            this.Hide();
        }
        private void DisplayIncomes() //from this method, we display incomes in data grid view
        {
            con.Open();     //extract data from dataset to data grid view
            string query = "select * from IncomeTbl where IncUser= '" + LogIn.User + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);     //updating database table
            var ds = new DataSet();
            sda.Fill(ds);
            IncomeDGV.DataSource = ds.Tables[0];

            con.Close();
        }
      
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\incomeDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void label19_Click(object sender, EventArgs e) //close the application
        {
            Application.Exit();
        }

        private void IncomeDGV_CellContentClick(object sender, DataGridViewCellEventArgs e) //selecting a row
        {
            indexRow = e.RowIndex;
            DataGridViewRow row = IncomeDGV.Rows[indexRow];

            txtname.Text = IncomeDGV.SelectedRows[0].Cells[0].Value.ToString();
            txtAmt.Text = IncomeDGV.SelectedRows[0].Cells[1].Value.ToString();
            categorybox.SelectedItem = IncomeDGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e) //go to login
        {
            LogIn obj = new LogIn();
            obj.Show();
            this.Hide();
        }

        private void dltbtn_Click(object sender, EventArgs e) //delete button
        {
            if (txtname.Text == "") 
            {
                MessageBox.Show("Select the item to be deleted");
            }
            else
            {
                con.Open();
                string query = "delete from IncomeTbl where IncName = '" + txtname.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item succussfully deleted!");
                con.Close();
                DisplayIncomes();
            }
        }

        private void ViewIncome_Load(object sender, EventArgs e)
        {
            DisplayIncomes();
        }

        private void button6_Click(object sender, EventArgs e) //edit button
        {
            if (txtname.Text == "" || txtAmt.Text == "")
            {
                MessageBox.Show("Fill all the fileds!");
            }
            else
            {
                string query = "update  IncomeTbl set IncName='" + txtname.Text + "' where IncAmt='" + txtAmt.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);

                try
                {
                    con.Open();
                    DataGridViewRow newDataRow = IncomeDGV.Rows[indexRow]; //youtube video code
                    newDataRow.Cells[0].Value = txtname.Text;
                    newDataRow.Cells[1].Value = txtAmt.Text;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item successfully updates");
                    con.Close();
                    DisplayIncomes();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("" + ex);
                }
               
                
               
                

               
            }
        }

        private void rfbtn_Click(object sender, EventArgs e)
        {
            
        }
    }
}
