using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace CarBookingPortal
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string strUsername = txtBoxUsername.Text;//get data entered in txtbox username
            string strPassword = txtBoxPassword.Text;//get data entered in txtbox password

            MySqlConnection connection = new MySqlConnection("Data Source = Localhost; server = localhost; user = root; pwd = root123; database = db_carbooking; port = 3306;");
            MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM logindetails WHERE username='" + strUsername + "' and password='" + strPassword + "';", connection);
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            int iResult = -1;
            if (reader.Read())
            {
                iResult = reader.GetInt32(0);
            }
            connection.Close();
            if (iResult == 1)//valid user
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('you have logged in successfully');", true);
                Session["LoginName"] = strUsername;
                Response.Redirect("~/Home.aspx");//On login move to home page
            }
            else
            {
                return;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtBoxUsername.Text = "";
            txtBoxPassword.Text = "";
        }
    }
}