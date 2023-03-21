using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Xml.Linq;

namespace ConnDisconnADO
{
    public partial class BookDisConn : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;
        public BookDisConn()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
        }
        private DataSet GetAllProducts()
        {
            da = new SqlDataAdapter("select * from Book5", con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "book");
            return ds;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = GetAllProducts();
                DataRow row = ds.Tables["book"].NewRow();
                row["bookid"] = textId.Text;
                row["bookname"] = textName.Text;
                row["authorname"] = textAuthor.Text;
                row["price"] = textPrice.Text;
                // add new row in the dataset table
                ds.Tables["book"].Rows.Add(row);
                da.Update(ds.Tables["book"]);
                MessageBox.Show("Record inserted");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = GetAllProducts();
                DataRow row = ds.Tables["book"].Rows.Find(textId.Text);
                if (row != null)
                {
                    row["bookname"] = textName.Text;
                    row["autorname"] = textAuthor.Text;
                    row["price"] = textPrice.Text;
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = GetAllProducts();
                DataRow row = ds.Tables["book"].Rows.Find(textId.Text);
                if (row != null)
                {
                    row["bookname"] = textName.Text;
                    row["authorname"] = textAuthor.Text;
                    row["price"] = textPrice.Text;
                    int res = da.Update(ds.Tables["book"]);
                    if (res >= 1)
                    {
                        MessageBox.Show("Record updated");
                    }
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = GetAllProducts();
                DataRow row = ds.Tables["book"].Rows.Find(textId.Text);
                if (row != null)
                {
                    row.Delete();
                    int res = da.Update(ds.Tables["book"]);
                    if (res >= 1)
                    {
                        MessageBox.Show("Record deleted");
                    }

                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
