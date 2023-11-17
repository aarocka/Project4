using System;
using System.Data;
using Utilities;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using RestaurantReviewLibrary;
using System.IO;
using System.Net;

namespace RestaurantReviewProject3
{
    public partial class MakeReview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int[] thing = (int[])Session["makeReviewSession"];
                if (Session["makeReviewSession"] == null || Session["theSession"] == null)
                {
                    Response.Redirect("ReviewerPage.aspx");
                }
                Session["makeReviewSession"] = thing;

            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReviewerPage.aspx");
            Session["makeReviewSession"] = null;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int[] thing = (int[])Session["makeReviewSession"];
            Session["makeReviewSession"] = thing;

            //Create a new Review object
            Review review = new Review();
            review.ReviewerId = thing[1];
            review.RestaurantId = thing[0];
            review.FoodQualityRating = int.Parse(Request["rating"]);
            review.PriceLevelRating = int.Parse(Request["rating0"]);
            review.ServiceRating = int.Parse(Request["rating1"]);
            review.AtmosphereRating = int.Parse(Request["rating2"]);
            review.ReviewText = TextBox1.Text;

            //Serialize the Review object to JSON format
            JavaScriptSerializer js = new JavaScriptSerializer();
            String jsonReview = js.Serialize(review);


            String webApiUrl = "http://localhost:5054/api/user/review/add";
            try
            {
                // Send the Customer object to the Web API that will be used to store a new customer record in the database.
                // Setup an HTTP POST Web Request and get the HTTP Web Response from the server.
                WebRequest request = WebRequest.Create(webApiUrl);
                request.Method = "POST";
                request.ContentLength = jsonReview.Length;
                request.ContentType = "application/json";

                // Write the JSON data to the Web Request
                StreamWriter writer = new StreamWriter(request.GetRequestStream());
                writer.Write(jsonReview);
                writer.Flush();
                writer.Close();

                // Read the data from the Web Response, which requires working with streams
                WebResponse response = request.GetResponse();
                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);
                String data = reader.ReadToEnd();
                reader.Close();
                response.Close();

                if (data == "true")
                {
                    Label2.Text = "Your review has been placed! Go back to homepage.";
                    Label2.Visible = true;

                    Button1.Visible = false;
                }
                else
                {
                    Label2.Visible = true;
                    Label2.Text = "A problem occurred while adding the customer to the database. The data wasn't recorded.";
                }
            }
            catch (Exception ex)
            {
                Label2.Visible = true;
                Label2.Text = "Error: " + ex.Message;
            }




            /*
            DBConnect db = new DBConnect();
            System.Data.SqlClient.SqlCommand addReview = new System.Data.SqlClient.SqlCommand();
            addReview.CommandType = CommandType.StoredProcedure;
            addReview.CommandText = "AddReview";
            addReview.Parameters.AddWithValue("@UserID", thing[1].ToString());
            addReview.Parameters.AddWithValue("@RestrauntID", thing[0].ToString());
            addReview.Parameters.AddWithValue("@Food", Request["rating"]);
            addReview.Parameters.AddWithValue("@Price", Request["rating0"]);
            addReview.Parameters.AddWithValue("@Service", Request["rating1"]);
            addReview.Parameters.AddWithValue("@Atmosphere", Request["rating2"]);
            addReview.Parameters.AddWithValue("@Review", TextBox1.Text);

            db.DoUpdateUsingCmdObj(addReview);
            */




            
        }
    }
}