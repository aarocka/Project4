using RestaurantReviewLibrary;
using System;
using System.Data;
using System.Data.SqlClient;
using Utilities;
using System.Web.Script.Serialization;
using System.IO;
using System.Net;


namespace RestaurantReviewProject3
{
    public partial class EditRestrauntInfo : System.Web.UI.Page
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

                //populate the text boxes with the current restaurant information using a get request
                String webApiUrl = "http://localhost:5054/api/restaurant/";
                WebRequest request = WebRequest.Create(webApiUrl + session.RestaurantID +"/");
                WebResponse response = request.GetResponse();

                // Read the data from the Web Response, which requires working with streams.
                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);
                String data = reader.ReadToEnd();
                reader.Close();
                response.Close();

                // Deserialize a JSON string into a Restaurant object.
                JavaScriptSerializer js = new JavaScriptSerializer();
                Restaurant restaurant = js.Deserialize<Restaurant>(data);

                //populate the text boxes with the current restaurant information
                TextBox1.Text = restaurant.Name;
                TextBox2.Text = restaurant.Address;
                TextBox3.Text = restaurant.Category;
                TextBox4.Text = restaurant.Phone;
                TextBox5.Text = restaurant.IMGURL;



                /*
                DBConnect dBConnect = new DBConnect();
                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "RestaurantInformation";
                myCommand.Parameters.AddWithValue("@ID", session.RestaurantID);

                Label1.Text = dBConnect.GetDataSetUsingCmdObj(myCommand).Tables[0].Rows[0]["Name"].ToString();
                Label2.Text = dBConnect.GetDataSetUsingCmdObj(myCommand).Tables[0].Rows[0]["Address"].ToString();
                Label3.Text = dBConnect.GetDataSetUsingCmdObj(myCommand).Tables[0].Rows[0]["Category"].ToString();
                Label4.Text = dBConnect.GetDataSetUsingCmdObj(myCommand).Tables[0].Rows[0]["Phone"].ToString();
                Label5.Text = dBConnect.GetDataSetUsingCmdObj(myCommand).Tables[0].Rows[0]["Picture"].ToString();
                */


            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            UserSession session = (UserSession)Session["theSession"];
            Session["theSession"] = session;

            //Make new restaurant object
            Restaurant restaurant = new Restaurant();
            restaurant.Name = TextBox1.Text;
            restaurant.Address = TextBox2.Text;
            restaurant.Category = TextBox3.Text;
            restaurant.Phone = TextBox4.Text;
            restaurant.IMGURL = TextBox5.Text;
            restaurant.Id = session.RestaurantID;

            //Serialize the restaurant object to JSON format
            JavaScriptSerializer js = new JavaScriptSerializer();
            String jsonRestaurant = js.Serialize(restaurant);
            String webApiUrl = "http://localhost:5054/api/management/update/";

            try
            {
                // Send the Customer object to the Web API that will be used to store a new customer record in the database.
                // Setup an HTTP POST Web Request and get the HTTP Web Response from the server.
                WebRequest request = WebRequest.Create(webApiUrl + "restaurant/");
                request.Method = "PUT";
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
                    //Label3.Text = "The customer was successfully saved to the database.";
                    //Label3.Visible = true;
                    //Button1.Visible = false;
                    //Response.Redirect("RepresentativePage.aspx");
                }
                else
                {
                    //Label3.Text = "A problem occurred while adding the customer to the database. The data wasn't recorded.";
                }
            }
            catch (Exception ex)
            {
                //Label3.Text = "Error: " + ex.Message;
            }


            /*
            DBConnect dBConnect = new DBConnect();
            SqlCommand myCommand = new SqlCommand();
            
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "UpdateRestaurantInformation";
            myCommand.Parameters.AddWithValue("@RestaurantId", session.RestaurantID);
            myCommand.Parameters.AddWithValue("@Name", TextBox1.Text);
            myCommand.Parameters.AddWithValue("@Address", TextBox2.Text);
            myCommand.Parameters.AddWithValue("@Category", TextBox3.Text);
            myCommand.Parameters.AddWithValue("@Phone", TextBox4.Text);
            myCommand.Parameters.AddWithValue("@Picture", TextBox5.Text);
            */
        }
    }
}