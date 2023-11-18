using RestaurantReviewLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using Utilities;

namespace RestaurantReviewProject3
{
    public partial class YourReviews : System.Web.UI.Page
    {
        public void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserSession session = (UserSession)Session["theSession"];
                Session["theSession"] = session;

                if (Session["theSession"] == null)
                {
                    Response.Redirect("ReviewerPage.aspx");
                }

                //populate the gridview with the data returned from a get request
                String webApiUrl = "http://localhost:5054/api/user/"+session.Id+"/review";
                WebRequest request = WebRequest.Create(webApiUrl);
                WebResponse response = request.GetResponse();

                // Read the data from the Web Response, which requires working with streams.
                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);
                String data = reader.ReadToEnd();
                reader.Close();
                response.Close();

                // Deserialize a JSON string into a List<Review>.

                JavaScriptSerializer js = new JavaScriptSerializer();
                List<Review> reviews = js.Deserialize<List<Review>>(data);

                GridView1.DataSource = reviews;
                GridView1.DataBind();





                /*
                DBConnect dBConnect = new DBConnect();
                DataSet myReviews = new DataSet();

                System.Data.SqlClient.SqlCommand getMyReviews = new System.Data.SqlClient.SqlCommand();
                getMyReviews.CommandType = CommandType.StoredProcedure;
                getMyReviews.CommandText = "GetReviewsByReviewer";
                getMyReviews.Parameters.AddWithValue("@id", session.Id);

                myReviews = dBConnect.GetDataSetUsingCmdObj(getMyReviews);
                */



                //GridView1.DataSource = myReviews;
                //GridView1.DataBind();
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "MyButton1")
            {
                TextBox1.Visible = true;
                Button3.Visible = true;

                String temp = (String)Session["temp"];
                Session["temp"] = temp;
                if (Session["temp"] == null)
                {
                    temp = e.CommandArgument.ToString();
                }
                Session["temp"] = temp;
            }
            else
            {
                //delete the review using a Delete request
                string webApiUrl = "http://localhost:5054/api/user/review/delete/"+e.CommandArgument;
                WebRequest request = WebRequest.Create(webApiUrl);
                request.Method = "DELETE";
                WebResponse response = request.GetResponse();
                response.Close();

                /*
                DBConnect dBconnect = new DBConnect();

                System.Data.SqlClient.SqlCommand deleteReview = new System.Data.SqlClient.SqlCommand();
                deleteReview.CommandType = CommandType.StoredProcedure;
                deleteReview.CommandText = "DeleteAReview";
                deleteReview.Parameters.AddWithValue("@ID", e.CommandArgument);

                dBconnect.DoUpdateUsingCmdObj(deleteReview);
                */
                Response.Redirect("YourReviews.aspx");
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            String temp = (String)Session["temp"];
            Session["temp"] = temp;


            //update the review using a Put request
            Review review = new Review(int.Parse(temp));
            review.ReviewText = TextBox1.Text;
            string webApiUrl = "http://localhost:5054/api/user/review/update";
            WebRequest request = WebRequest.Create(webApiUrl);
            request.Method = "PUT";
            request.ContentType = "application/json";
            JavaScriptSerializer js = new JavaScriptSerializer();
            String jsonReview = js.Serialize(review);
            StreamWriter writer = new StreamWriter(request.GetRequestStream());
            writer.Write(jsonReview);
            writer.Flush();
            writer.Close();
            WebResponse response = request.GetResponse();
            response.Close();




            /*
            DBConnect d = new DBConnect();

            System.Data.SqlClient.SqlCommand updateReview = new System.Data.SqlClient.SqlCommand();
            updateReview.CommandType = CommandType.StoredProcedure;
            updateReview.CommandText = "ChangeReview";
            updateReview.Parameters.AddWithValue("@Id", temp);
            updateReview.Parameters.AddWithValue("@review", TextBox1.Text);

            d.DoUpdateUsingCmdObj(updateReview);
            */
            Session["temp"] = null;
            Response.Redirect("YourReviews.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReviewerPage.aspx");
        }
    }
}



