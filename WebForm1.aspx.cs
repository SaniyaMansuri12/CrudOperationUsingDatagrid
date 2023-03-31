using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace CrudOperationUsingDataGridBind
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Dbcs"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        public void BindGrid()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_ListInsertInformation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                con.Close();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_InsertInfo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                cmd.ExecuteNonQuery();
                con.Close();
              
                
                Response.Write("Record saved successfully");
                BindGrid();
                
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void btnClear_click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtAddress.Text = "";
            txtEmail.Text = "";
            txtMobile.Text = "";
        }
    }
}