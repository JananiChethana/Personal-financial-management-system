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
    public partial class ViewExpenses : Form
    {
        public ViewExpenses()
        {
            InitializeComponent();
            DisplayExpenses();
        }
        DataTable table = new DataTable();
        int indexRow;
       
        private void DisplayExpenses() //to display the expenses details
        {
            con.Open();
            string query = "select * from ExpensesTbl where ExpUser= '" + LogIn.User + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ExpensesDGV.DataSource = ds.Tables[0];

            con.Close();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\incomeDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void label19_Click(object sender, EventArgs e) //exit button
        {
            Application.Exit();
        }

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

        private void label5_Click(object sender, EventArgs e) //go to view income
        {
            ViewIncome obj = new ViewIncome();
            obj.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e) //go to login
        {
            LogIn obj = new LogIn();
            obj.Show();
            this.Hide();
        }

        private void ExpensesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            DataGridViewRow row = ExpensesDGV.Rows[indexRow];

            txtname.Text = ExpensesDGV.SelectedRows[0].Cells[0].Value.ToString();
            txtAmt.Text = ExpensesDGV.SelectedRows[0].Cells[1].Value.ToString();
            categorybox.SelectedItem = ExpensesDGV.SelectedRows[0].Cells[2].Value.ToString();
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
                string query = "delete from ExpensesTbl where ExpName = '" + txtname.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item succussfully deleted!");
                con.Close();
                DisplayExpenses();
            }
        }

        private void ViewExpenses_Load(object sender, EventArgs e)
        {
            DisplayExpenses();
        }

        private void button6_Click(object sender, EventArgs e) //edit button
        {
            if (txtname.Text == "" || txtAmt.Text == "") //if these details are empty, show the below messege
            {
                MessageBox.Show("Fill all the fileds!");
            }
            else 
            {
                
                string query = "update  ExpensesTbl set ExpName='" + txtname.Text + "' where ExpAmt='" + txtAmt.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item successfully updates");
                    con.Close();
                    DisplayExpenses();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("" + ex);
                }
                
                
            }
        }
    }
}
