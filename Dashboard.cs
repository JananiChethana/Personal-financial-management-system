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
    public partial class Dashboard : Form
    {
        
        public Dashboard()
        {
            InitializeComponent();
            GetToInc();
            GetToExp();
            GetNumExp();
            GetNumInc();
            GetIncDate();
            GetExpDate();
            GetMaxInc();
            GetMinInc();
            GetMaxExp();
            GetMinExp();
            GetBalance();
            GetMaxExpCat();
            GetMaxIncCat();

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }
        //SqlConnection is an inbuild class. It combines front end and back end. con is an object.
        //connection string is the location of database. 
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\incomeDb.mdf;Integrated Security=True;Connect Timeout=30"); 
        private void GetToInc() //getting the total income
        {
            try 
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select Sum(IncAmt) from IncomeTbl where IncUser= '" + LogIn.User + "'", con); //query to get the sum
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Inc = Convert.ToInt32(dt.Rows[0][0].ToString());
                ToInclbl.Text = "Rs " + dt.Rows[0][0].ToString();
                con.Close();
            }
            catch(Exception ex)
            {
                con.Close();
            }
            
        }
        int Inc, Exp;
        private void GetToExp() //to get the total expenses
        {
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select Sum(ExpAmt) from ExpensesTbl where ExpUser= '" + LogIn.User + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Exp = Convert.ToInt32(dt.Rows[0][0].ToString());
                ToExplbl.Text = "Rs " + dt.Rows[0][0].ToString();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
        }
        private void GetBalance() //get the balance
        {
            double Bal = Inc - Exp;         
            Balancelbl.Text = "Rs "+Bal; 
        }
        private void GetMaxExpCat() //to get the maximum expenses category
        {
            try
            {
                con.Open(); //extarct data from dataset to data grid view
                string InnerQuery = "select Max(ExpAmt) from ExpensesTbl";
                DataTable dt1 = new DataTable();
                SqlDataAdapter sda1 = new SqlDataAdapter(InnerQuery, con);
                sda1.Fill(dt1);
                string Query = "select ExpCat from ExpensesTbl where ExpAmt = '" + dt1.Rows[0][0].ToString() + "'";
                SqlDataAdapter sda = new SqlDataAdapter(Query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                BestExplbl.Text = dt.Rows[0][0].ToString();
                con.Close();
            }
            catch(Exception ex)
            {
                con.Close();
            }
            
        }
        private void GetMaxIncCat() //to get the maximum income category
        {
            try
            {
                con.Open();
                string InnerQuery = "select Max(IncAmt) from IncomeTbl";
                DataTable dt1 = new DataTable();
                SqlDataAdapter sda1 = new SqlDataAdapter(InnerQuery, con);
                sda1.Fill(dt1);
                string Query = "select IncCat from IncomeTbl where IncAmt = '" + dt1.Rows[0][0].ToString() + "'";
                SqlDataAdapter sda = new SqlDataAdapter(Query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                BestInclbl.Text = dt.Rows[0][0].ToString();
                con.Close();
            }
            catch(Exception ex) 
            {
                con.Close();
            }
           
        }
        private void GetNumExp() //to get the trasaction count of expenses
        {
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select Count(*) from ExpensesTbl where ExpUser= '" + LogIn.User + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                NumExplbl.Text = dt.Rows[0][0].ToString();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            
        }
        private void GetNumInc() //to get the transaction count of incomes
        {
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select Count(*) from IncomeTbl where IncUser= '" + LogIn.User + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                NumInclbl.Text = dt.Rows[0][0].ToString();
                con.Close();
            }
            catch(Exception ex)
            {
                con.Close();
            }
            
        }
        private void GetIncDate() //to get the last income date
        {
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select Max(IncDate) from IncomeTbl where IncUser= '" + LogIn.User + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                DateInclbl.Text = dt.Rows[0][0].ToString();
                LastInclbl.Text = dt.Rows[0][0].ToString();
                con.Close();
            }
            catch(Exception ex)
            {
                con.Close();
            }
            
        }
        private void GetMaxInc() //to get the maximum income
        {
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select Max(IncAmt) from IncomeTbl where IncUser= '" + LogIn.User + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                MaxInclbl.Text = "Rs " + dt.Rows[0][0].ToString();
                con.Close();
            }
            catch(Exception ex)
            {
                con.Close();
            }
            
        }
        private void GetMinInc() //to get the minimum income 
        {
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select Min(IncAmt) from IncomeTbl where IncUser= '" + LogIn.User + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                MinInclbl.Text = "Rs " + dt.Rows[0][0].ToString();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            
        }
        private void GetMaxExp() //to get the maximum expense
        {
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select Max(ExpAmt) from ExpensesTbl where ExpUser= '" + LogIn.User + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                MaxExplbl.Text = "Rs " + dt.Rows[0][0].ToString();
                con.Close();
            }
            catch(Exception ex)
            {
                con.Close();
            }
           
        }
        private void GetMinExp() //to get the minimum expense
        {
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select Min(ExpAmt) from ExpensesTbl where ExpUser= '" + LogIn.User + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                MinExplbl.Text = "Rs " + dt.Rows[0][0].ToString();
                con.Close();
            }
            catch(Exception ex)
            {
                con.Close();
            }
            
        }
        private void GetExpDate() //to get the last expense date
        {
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select Max(ExpDate) from ExpensesTbl where ExpUser= '" + LogIn.User + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                DateExplbl.Text = dt.Rows[0][0].ToString();
                LastExplbl.Text = dt.Rows[0][0].ToString();
                con.Close();
            }
            catch(Exception ex)
            {
                con.Close();
            }
            
        }
        private void label19_Click(object sender, EventArgs e) //close the application
        {
            Application.Exit();
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

        private void label6_Click(object sender, EventArgs e) //go to view expenses
        {
            ViewExpenses obj = new ViewExpenses();
            obj.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e) //go to login
        {
            LogIn obj = new LogIn();
            obj.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MaxInclbl_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) 
        {
            
        }

        private void button3_Click(object sender, EventArgs e) 
        {
            
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void ToInclbl_Click(object sender, EventArgs e)
        {

        }
    }
}
