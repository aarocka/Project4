using RestaurantReviewLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace RestaurantReviewProject3
{
    public partial class AnonymousPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserSession session = (UserSession)Session["theSession"];
                Session["theSession"] = session;
                if (Session["theSession"] == null)
                {
                    Response.Redirect("LoginPage.aspx");
                }

                //Get all restaurants using a GET request
                String webApiUrl = "http://localhost:5054/api/restaurant/all";
                WebRequest request = WebRequest.Create(webApiUrl);
                WebResponse response = request.GetResponse();

                // Read the data from the Web Response, which requires working with streams.
                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);
                String data = reader.ReadToEnd();
                reader.Close();
                response.Close();

                // Deserialize a JSON string into a List<Restaurant>
                JavaScriptSerializer js = new JavaScriptSerializer();
                List<Restaurant> restaurantList = js.Deserialize<List<Restaurant>>(data);
                



                GridView1.DataSource = restaurantList;
                GridView1.DataBind();
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "MyButton1")
            {
                string itemId = e.CommandArgument.ToString();
                //create reservation session
                String makeReservation = (String)Session["ReservationSession"];
                if (Session["ReservaionSession"] == null)
                {
                    makeReservation = itemId.ToString();
                }
                Session["ReservationSession"] = makeReservation;
                Response.Redirect("MakeReservation.aspx");
            }

            if (e.CommandName == "MyButton2")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                String seeAllReviews = (String)Session["SeeAllReviews"];
                if (Session["SeeAllReviews"] == null)
                {
                    seeAllReviews = id.ToString();
                }
                Session["SeeAllReviews"] = seeAllReviews;
                Response.Redirect("RestReviews.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginPage.aspx");
            Session["theSession"] = null;
        }
    }
}