using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarBookingPortal
{
    public partial class Availability : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginName"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            GetDataFromDatabase();
        }

        private void GetDataFromDatabase()
        {
            //Get data from db and display it

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable dt = new DataTable();

            MySqlConnection connection = new MySqlConnection("Data Source = Localhost; server = localhost; user = root; pwd = root123; database = db_carbooking; port = 3306;");
            MySqlCommand command = new MySqlCommand("SELECT * FROM bookingdata WHERE bookingFromDT > NOW() AND username='"+Session["LoginName"]+"';", connection);
            connection.Open();
            adapter = new MySqlDataAdapter(command);
            adapter.Fill(dt);
            connection.Close();
            gdViewBookingData.DataSource = dt;
            gdViewBookingData.DataBind();
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            string strFromTime = txtBoxFromTime.Text;   //HH:mm:ss
            string strToTime = txtBoxToTime.Text;       //HH:mm:ss

            /*
            string currentDateDay = DateTime.Now.Day.ToString("00");
            string currentDateMonth = DateTime.Now.Month.ToString("00");
            string currentDateYear = DateTime.Now.Year.ToString("0000");

            string strDay = currentDateDay +"-"+ currentDateMonth + "-" + currentDateYear;
            string strFromDay_Time = strFromTime + " " + strDay;
            string strToDay_Time = strToTime + " " + strDay;

            DateTime dtTimeFrom = DateTime.ParseExact(strFromDay_Time, "HH:mm:ss dd-MM-yyyy",null);//HH:mm:ss dd-MM-yyyy
            DateTime dtTimeTo = DateTime.ParseExact(strToDay_Time, "HH:mm:ss dd-MM-yyyy", null);//HH:mm:ss dd-MM-yyyy

            DateTime dtBookedFrom= DateTime.ParseExact("19:00:00 15-01-2016", "HH:mm:ss dd-MM-yyyy", null);//HH:mm:ss dd-MM-yyyy
            DateTime dtBookedTo = DateTime.ParseExact("20:00:00 15-01-2016", "HH:mm:ss dd-MM-yyyy", null);//HH:mm:ss dd-MM-yyyy

            /*string strDay = DateTime.Now.Day.ToString("00") + "-" + DateTime.Now.Month.ToString("00") + "-" + DateTime.Now.Year.ToString("0000");
            */


            string strDay = DateTime.Now.ToString("dd-MM-yyyy");
            string strFromDay_Time = strFromTime + " " + strDay;
            string strToDay_Time = strToTime + " " + strDay;

            DateTime dtTimeFrom = DateTime.ParseExact(strFromDay_Time, "HH:mm:ss dd-MM-yyyy", null);//HH:mm:ss dd-MM-yyyy
            DateTime dtTimeTo = DateTime.ParseExact(strToDay_Time, "HH:mm:ss dd-MM-yyyy", null);//HH:mm:ss dd-MM-yyyy

            DateTime dtBookedFrom = DateTime.Now;
            DateTime dtBookedTo = DateTime.Now;


            #region GetFromDB
            MySqlConnection connection = new MySqlConnection("Data Source = Localhost; server = localhost; user = root; pwd = root123; database = db_carbooking; port = 3306;");
            MySqlCommand command = new MySqlCommand("SELECT * FROM bookingdata WHERE bookingFromDT > NOW();", connection);
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string temp = reader.GetString(3);
                dtBookedFrom = DateTime.ParseExact(temp, "dd-MM-yyyy HH:mm:ss", null);//HH:mm:ss dd-MM-yyyy
                temp = reader.GetString(4);
                dtBookedTo = DateTime.ParseExact(temp, "dd-MM-yyyy HH:mm:ss", null);//HH:mm:ss dd-MM-yyyy
            }
            connection.Close(); 
            #endregion

            if (dtTimeFrom >= dtBookedFrom && dtTimeTo <= dtBookedTo)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Not available, already booked');", true);
                return;
            }

            if (dtTimeFrom <= dtBookedFrom && dtTimeTo >= dtBookedTo)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Not available, already booked');", true);
                return;
            }

            //continue with booking
            btnBook.Visible = true;
            pnlBookingDetails.Visible = true;
        }

        protected void btnBook_Click(object sender, EventArgs e)
        {
            if (txtBoxPurpose.Text == "") 
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Pls fill the purpose field');", true);
                return;
            }
            Book();
        }

        public bool Book()
        {
            bool bReturn = false;

            string strFromTime = txtBoxFromTime.Text;   //HH:mm:ss
            string strToTime = txtBoxToTime.Text;       //HH:mm:ss

            string strDay = DateTime.Now.ToString("yyyy-MM-dd");
            string strFromDay_Time = strDay+" "+ strFromTime;
            string strToDay_Time = strDay + " " + strToTime;
            //2016-01-20 21:31:42
            //yyyy-MM-dd HH:mm:ss
            MySqlConnection connection = new MySqlConnection("Data Source = Localhost; server = localhost; user = root; pwd = root123; database = db_carbooking; port = 3306;");
            MySqlCommand command = new MySqlCommand("INSERT INTO bookingdata(username,bookingDT,bookingFromDT,bookingToDT,Purpose) VALUE('"+ Session["LoginName"].ToString()+ "','"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"','"+ strFromDay_Time + "','"+ strToDay_Time + "','"+txtBoxPurpose.Text+"');", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            return bReturn;
        }

        protected void gdViewBookingData_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // string bookingID = gdViewBookingData.DataKeys[e.RowIndex].Values["bookingID"].ToString();
            string bookingID = gdViewBookingData.Rows[e.RowIndex].Cells[1].Text;

            MySqlConnection connection = new MySqlConnection("Data Source = Localhost; server = localhost; user = root; pwd = root123; database = db_carbooking; port = 3306;");
            MySqlCommand command = new MySqlCommand("DELETE FROM bookingdata WHERE username='"+ Session["LoginName"] + "' AND bookingID="+ bookingID + ";", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            GetDataFromDatabase();
        }
    }
}