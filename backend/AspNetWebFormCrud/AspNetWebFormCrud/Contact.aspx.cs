using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace AspNetWebFormCrud
{
    public partial class Contact1 : System.Web.UI.Page
    {
        SqlConnection sqlConn = new SqlConnection(@"Data source = (localdb)\MSSQLLocalDB; Initial Catalog = TestDb; Integrated Security = true");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnDelete.Enabled = false;
                FillGridView();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void clear()
        {
            txtName.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
            lblSuccess.Text = lblError.Text = "";
            FillGridView();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            SqlCommand sqlCmd = new SqlCommand("sp_ContactCreateOrUpdate", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@Id", string.IsNullOrEmpty(hfContactId.Value) ? 0 : Convert.ToInt32(hfContactId.Value));
            sqlCmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
            sqlCmd.ExecuteNonQuery();
            sqlConn.Close();
            string id = hfContactId.Value;
            clear();
            if (id == "")
            {
                lblSuccess.Text = "Contact saved successfully";
            }
            else
            {
                lblSuccess.Text = "Contact updated successfully";
            }
        }

        protected void lnk_OnClick(object sender, EventArgs e)
        {
            int id = Convert.ToInt32((sender as LinkButton).CommandArgument);

            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            SqlDataAdapter sqlDa = new SqlDataAdapter("sp_ContactViewById", sqlConn);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@Id", id);
            DataTable dt = new DataTable();
            sqlDa.Fill(dt);
            sqlConn.Close();
            gvContact.DataSource = dt;
            gvContact.DataBind();
            hfContactId.Value = id.ToString();
            txtName.Text = dt.Rows[0]["Name"].ToString();
            txtPhone.Text = dt.Rows[0]["Phone"].ToString();
            txtEmail.Text = dt.Rows[0]["Email"].ToString();
            txtAddress.Text = dt.Rows[0]["Address"].ToString();
            btnSave.Text = "Update";
            btnDelete.Enabled = true;
        }

        private void FillGridView()
        {
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            SqlDataAdapter sqlDa = new SqlDataAdapter("sp_ContactViewAll", sqlConn);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            sqlDa.Fill(dt);
            sqlConn.Close();
            gvContact.DataSource = dt;
            gvContact.DataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            SqlCommand sqlCmd = new SqlCommand("sp_ContactDeleteById", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@Id", Convert.ToInt32(hfContactId.Value));
            sqlCmd.ExecuteNonQuery();
            sqlConn.Close(); 
            clear();
            lblSuccess.Text = "Deleted Successfully";
        }
    }
}