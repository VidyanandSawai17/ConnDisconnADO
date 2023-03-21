using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Xml.Linq;

namespace ConnDisconnADO
{
    public partial class EmpConn : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public EmpConn()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);

        }

        private void textId_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string query = "insert into Emp5 values(@empid,@fname,@lname,@sal)";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@empid", Convert.ToInt32(textId.Text));
                cmd.Parameters.AddWithValue("@fname", textfName.Text);
                cmd.Parameters.AddWithValue("@lname", textlName.Text);
                cmd.Parameters.AddWithValue("@sal", Convert.ToInt32(textSal.Text));
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record inserted..");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from Emp5";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@empid", Convert.ToInt32(textId.Text));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        textfName.Text = dr["fname"].ToString();
                        textlName.Text = dr["lname"].ToString();
                        textSal.Text = dr["sal"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found..");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "update Emp5 set fname=@fname, lname=@lname, sal=@sal where empid=@empid";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@empid", Convert.ToInt32(textId.Text));
                cmd.Parameters.AddWithValue("@fname", textfName.Text);
                cmd.Parameters.AddWithValue("@lname", textlName.Text);
                cmd.Parameters.AddWithValue("@sal", Convert.ToInt32(textSal.Text));
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record updated..");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "delete from Emp5 where empid=@empid ";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@empid", Convert.ToInt32(textId.Text));
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record deleted..");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
