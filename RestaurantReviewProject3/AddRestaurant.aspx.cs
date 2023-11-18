using System;
using System.Data;
using System.Data.SqlClient;
using Utilities;
using RestaurantReviewLibrary;
using System.Web.Script.Serialization;
using System.IO;
using System.Net;

namespace RestaurantReviewProject3
{
    public partial class AddRestaurant : System.Web.UI.Page
    {
        String webApiUrl = "http://localhost:5054/api/restaurant/";
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    int[] thing = (int[])Session["addRestSession"];
            //    if (Session["addRestSession"] == null || Session["theSession"] == null)
            //    {
            //        //Response.Redirect("ReviewerPage.aspx");
            //    }
            //    Session["addRestSession"] = thing;
            //}
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox1.Text) || string.IsNullOrEmpty(TextBox2.Text) ||
                string.IsNullOrEmpty(TextBox3.Text) || string.IsNullOrEmpty(TextBox4.Text) ||
                string.IsNullOrEmpty(TextBox5.Text))
            {
                Label6.Text = "Please fill out the form completely.";
                Label6.Visible = true;
            }
            else
            {
                //Create a new restaurant object
                Restaurant restaurant = new Restaurant();
                restaurant.Name = TextBox1.Text;
                restaurant.Address = TextBox2.Text;
                restaurant.Category = TextBox3.Text;
                restaurant.Phone = TextBox4.Text;
                restaurant.IMGURL = TextBox5.Text;

                //Serialize the restaurant object to JSON format
                JavaScriptSerializer js = new JavaScriptSerializer();
                String jsonRestaurant = js.Serialize(restaurant);

                try
                {
                    // Send the Customer object to the Web API that will be used to store a new customer record in the database.
                    // Setup an HTTP POST Web Request and get the HTTP Web Response from the server.
                    WebRequest request = WebRequest.Create(webApiUrl + "add/");
                    request.Method = "POST";
                    request.ContentLength = jsonRestaurant.Length;
                    request.ContentType = "application/json";

                    // Write the JSON data to the Web Request
                    StreamWriter writer = new StreamWriter(request.GetRequestStream());
                    writer.Write(jsonRestaurant);
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
                        Label6.Visible = true;
                        Label6.Text = "Your restaurant has been added! Go back home!";
                        Button1.Visible = false;
                    }
                    else
                    {
                        Label6.Visible = true;
                        Label6.Text = "A problem occurred. The data wasn't recorded.";
                    }
                }
                catch (Exception ex)
                {
                    Label6.Visible = true;
                    Label6.Text = "Error: " + ex.Message;
                }


                /*
                DBConnect d = new DBConnect();
                SqlCommand addNewRestaurant = new SqlCommand();
                addNewRestaurant.CommandType = CommandType.StoredProcedure;
                addNewRestaurant.CommandText = "AddNewRestaurant";
                addNewRestaurant.Parameters.AddWithValue("@Name", TextBox1.Text);
                addNewRestaurant.Parameters.AddWithValue("@Address", TextBox2.Text);
                addNewRestaurant.Parameters.AddWithValue("@Category", TextBox3.Text);
                addNewRestaurant.Parameters.AddWithValue("@Phone", TextBox4.Text);
                addNewRestaurant.Parameters.AddWithValue("@Picture", TextBox5.Text);

                d.DoUpdateUsingCmdObj(addNewRestaurant);
                */
                
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReviewerPage.aspx");
        }
    }
}