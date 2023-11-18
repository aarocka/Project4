using RestaurantReviewLibrary;
using System;
using System.Data;
using System.Net;
using Utilities;

namespace RestaurantReviewProject3
{
    public partial class RestReviews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String seeAllReviews = (String)Session["SeeAllReviews"];

                if (Session["SeeAllReviews"] == null || Session["theSession"] == null)
                {
                    Response.Redirect("ReviewerPage.aspx");
                }
                Session["SeeAllReviews"] = seeAllReviews;

                //Make a call to the API to get all for a given restaurant
                string webApiUrl = "http://localhost:5054/api/restaurant/" + seeAllReviews + "/review";
                WebRequest request = WebRequest.Create(webApiUrl);
                WebResponse response = request.GetResponse();

                // Read the data from the Web Response, which requires working with streams.
                System.IO.Stream theDataStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(theDataStream);
                String data = reader.ReadToEnd();
                reader.Close();
                response.Close();

                // Deserialize a JSON string into a List<Review>
                System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
                System.Collections.Generic.List<Review> restReviews = js.Deserialize<System.Collections.Generic.List<Review>>(data);



                /*
                DBConnect db = new DBConnect();
                DataSet restReviews = new DataSet();
                System.Data.SqlClient.SqlCommand getRestrauntsByID = new System.Data.SqlClient.SqlCommand();

                getRestrauntsByID.CommandType = CommandType.StoredProcedure;
                getRestrauntsByID.CommandText = "GetRestaurantReviewsByID";
                getRestrauntsByID.Parameters.AddWithValue("@RestaurantId", seeAllReviews);

                restReviews = db.GetDataSetUsingCmdObj(getRestrauntsByID);
                */

                GridView1.DataSource = restReviews;
                GridView1.DataBind();
            }

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //Handle guest user
            UserSession session = (UserSession)Session["theSession"];
            Session["theSession"] = session;

            if (session.UserName == "Guest")
            {
                Session["SeeAllReviews"] = null;
                Response.Redirect("GuestPage.aspx");
            }
            else
            {
                Response.Redirect("ReviewerPage.aspx");
                Session["SeeAllReviews"] = null;
            }
        }
    }
}

