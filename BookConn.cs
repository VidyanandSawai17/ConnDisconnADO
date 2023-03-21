using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnDisconnADO
{
    public partial class BookConn : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public BookConn()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "insert into Book5 values(@bookid,@bookname,@authorname,@price)";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@bookid", Convert.ToInt32(textId.Text));
                cmd.Parameters.AddWithValue("@bookname", textfName.Text);
                cmd.Parameters.AddWithValue("@authorname", textlName.Text);
                cmd.Parameters.AddWithValue("@price", Convert.ToInt32(textPer.Text));
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
                string query = "select * from Book5";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@bookid", Convert.ToInt32(textId.Text));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        textfName.Text = dr["bookname"].ToString();
                        textlName.Text = dr["authorname"].ToString();
                        textPer.Text = dr["price"].ToString();
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
                string query = "update Book5 set bookname=@bookname, authorname=@authorname, price=@price where bookid=@bookid";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@bookid", Convert.ToInt32(textId.Text));
                cmd.Parameters.AddWithValue("@bookname", textfName.Text);
                cmd.Parameters.AddWithValue("@authorname", textlName.Text);
                cmd.Parameters.AddWithValue("@price", Convert.ToInt32(textPer.Text));
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
                string query = "delete from Book5 where bookid=@bookid ";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@bookid", Convert.ToInt32(textId.Text));
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
