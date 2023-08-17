using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient; //to connect with SQL server
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IncomeManagement
{
    public partial class Expenses : Form
    {
        public Expenses()
        {
            InitializeComponent();
            GetToExp();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\incomeDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void Clear() //clear method
        {
            ExName.Text = "";
            ExAmt.Text = "";
            ExDesc.Text = "";
            ExCat.SelectedIndex = 0;
        }
        private void label19_Click(object sender, EventArgs e) //close the application
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

        private void label5_Click(object sender, EventArgs e) //go to view income
        {
            ViewIncome obj = new ViewIncome();
            obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e) //go to view expenses
        {
            ViewExpenses obj = new ViewExpenses();
            obj.Show();
            this.Hide();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //add expenses
        {
            if (ExName.Text == "" || ExAmt.Text == "" || ExCat.SelectedIndex == -1 || ExDesc.Text == "") //if these details are missing, display the below messege
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try           //from this query we are saying to insert these details into the ExpensesTbl. 
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ExpensesTbl(ExpName,ExpAmt,ExpCat,ExpDate,ExpDesc,ExpUser)values(@EN,@EA,@EC,@ED,@EDe,@EU)", con);
                    cmd.Parameters.AddWithValue("@EN", ExName.Text);
                    cmd.Parameters.AddWithValue("@EA", ExAmt.Text);
                    cmd.Parameters.AddWithValue("@EC", ExCat.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@ED", ExDate.Value.Date);
                    cmd.Parameters.AddWithValue("@EDe", ExDesc.Text);
                    cmd.Parameters.AddWithValue("@EU", LogIn.User);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Expense added!!");
                    con.Close();
                    GetToExp();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) //go to login
        {
            LogIn obj = new LogIn();
            obj.Show();
            this.Hide();
        }
        private void GetToExp() //extract data from dataset to data grid view
        {
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select Sum(ExpAmt) from ExpensesTbl where ExpUser= '" + LogIn.User + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                //Exp = Convert.ToInt32(dt.Rows[0][0].ToString());
                totexpense.Text = "Rs " + dt.Rows[0][0].ToString();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
        }
    }
}
