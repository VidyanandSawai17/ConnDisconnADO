using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Xml.Linq;


namespace ConnDisconnADO
{
    public partial class StudConn : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public StudConn()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "insert into student5 values(@rollno,@fname,@lname,@per)";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@rollno", Convert.ToInt32(textId.Text));
                cmd.Parameters.AddWithValue("@fname", textfName.Text);
                cmd.Parameters.AddWithValue("@lname", textlName.Text);
                cmd.Parameters.AddWithValue("@per", Convert.ToInt32(textPer.Text));
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
                string query = "select * from student5";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@rollno", Convert.ToInt32(textId.Text));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        textfName.Text = dr["fname"].ToString();
                        textlName.Text = dr["lname"].ToString();
                        textPer.Text = dr["per"].ToString();
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
                string query = "update student5 set fname=@fname, lname=@lname, per=@per where rollno=@rollno";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@rollno", Convert.ToInt32(textId.Text));
                cmd.Parameters.AddWithValue("@fname", textfName.Text);
                cmd.Parameters.AddWithValue("@lname", textlName.Text);
                cmd.Parameters.AddWithValue("@per", Convert.ToInt32(textPer.Text));
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
                string query = "delete from student5 where rollno=@rollno ";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@rollno", Convert.ToInt32(textId.Text));
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
