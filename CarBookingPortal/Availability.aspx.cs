using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
            MySqlCommand command = new MySqlCommand("SELECT * FROM bookingdata;", connection);
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                dtBookedFrom = DateTime.ParseExact(reader.GetString(3), "HH:mm:ss dd-MM-yyyy", null);//HH:mm:ss dd-MM-yyyy
                dtBookedTo = DateTime.ParseExact(reader.GetString(4), "HH:mm:ss dd-MM-yyyy", null);//HH:mm:ss dd-MM-yyyy
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

            string strDay = DateTime.Now.ToString("dd-MM-yyyy");
            string strFromDay_Time = strFromTime + " " + strDay;
            string strToDay_Time = strToTime + " " + strDay;
            
            MySqlConnection connection = new MySqlConnection("Data Source = Localhost; server = localhost; user = root; pwd = root123; database = db_carbooking; port = 3306;");
            MySqlCommand command = new MySqlCommand("INSERT INTO bookingdata(username,bookingDT,bookingFromDT,bookingToDT,Purpose) VALUE('"+ Session["LoginName"].ToString()+ "','"+ DateTime.Now.ToString("HH:mm:ss dd-MM-yyyy") +"','"+ strFromDay_Time + "','"+ strToDay_Time + "','"+txtBoxPurpose.Text+"');", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            return bReturn;
        }
    }
}