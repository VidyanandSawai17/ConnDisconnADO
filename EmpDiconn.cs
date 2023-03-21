using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace ConnDisconnADO
{
    public partial class EmpDiconn : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;
        public EmpDiconn()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
        }
        private DataSet GetAllProducts()
        {
            da = new SqlDataAdapter("select * from Emp5", con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "emp");
            return ds;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = GetAllProducts();
                DataRow row = ds.Tables["emp"].NewRow();
                row["empid"] = textId.Text;
                row["fname"] = textfName.Text;
                row["lname"] = textlName.Text;
                row["sal"] = textPer.Text;
                // add new row in the dataset table
                ds.Tables["emp"].Rows.Add(row);
                da.Update(ds.Tables["emp"]);
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
                DataRow row = ds.Tables["emp"].Rows.Find(textId.Text);
                if (row != null)
                {
                    row["fname"] = textfName.Text;
                    row["lname"] = textlName.Text;
                    row["sal"] = textPer.Text;
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
                DataRow row = ds.Tables["emp"].Rows.Find(textId.Text);
                if (row != null)
                {
                    row["fname"] = textfName.Text;
                    row["lname"] = textlName.Text;
                    row["sal"] = textPer.Text;
                    int res = da.Update(ds.Tables["emp"]);
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

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = GetAllProducts();
                DataRow row = ds.Tables["emp"].Rows.Find(textId.Text);
                if (row != null)
                {
                    row.Delete();
                    int res = da.Update(ds.Tables["emp"]);
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
