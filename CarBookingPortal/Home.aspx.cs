using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarBookingPortal
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginName"] == null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Login required');", true);
                Response.Redirect("~/Login.aspx");
            }
        }
    }
}
