using RestaurantReviewLibrary;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using Utilities;
using RestaurantReviewLibrary;


namespace RestaurantReviewProject3
{
    public partial class MakeReservation : System.Web.UI.Page
    {

        String webApiUrl = "http://localhost:5054/api/reservation/add";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String makeReservation = (String)Session["ReservationSession"];
                if (Session["ReservationSession"] == null || Session["theSession"] == null)
                {
                    Response.Redirect("ReviewerPage.aspx");
                }
                Session["ReservationSession"] = makeReservation;
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text != "")
            {
                String makeReservation = (String)Session["ReservationSession"];
                Session["ReservationSession"] = makeReservation;


                //Create a new reservation object
                Reservation reservation = new Reservation();

                //Set the properties of the reservation object
                reservation.Name = TextBox1.Text;
                reservation.Date = Calendar1.SelectedDate.ToString("yyyy-MM-dd");
                reservation.Time = DropDownList1.SelectedValue.ToString();
                reservation.Restaurant = int.Parse(makeReservation);

                //Serialize the reservation object to JSON format
                JavaScriptSerializer js = new JavaScriptSerializer();
                String jsonReservation = js.Serialize(reservation);

                try
                {
                    // Send the Customer object to the Web API that will be used to store a new customer record in the database.
                    // Setup an HTTP POST Web Request and get the HTTP Web Response from the server.
                    WebRequest request = WebRequest.Create(webApiUrl + "AddCustomer/");
                    request.Method = "POST";
                    request.ContentLength = jsonReservation.Length;
                    request.ContentType = "application/json";

                    // Write the JSON data to the Web Request
                    StreamWriter writer = new StreamWriter(request.GetRequestStream());
                    writer.Write(jsonReservation);
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
                        Label3.Text = "The customer was successfully saved to the database.";
                        Label3.Visible = true;
                        Button1.Visible = false;
                    }
                    else
                    {
                        Label3.Text = "A problem occurred while adding the customer to the database. The data wasn't recorded.";
                    }
                }
                catch (Exception ex)
                {
                    Label3.Text = "Error: " + ex.Message;
                }


                /*
                DBConnect db = new DBConnect();
                System.Data.SqlClient.SqlCommand addReservation = new System.Data.SqlClient.SqlCommand();

                addReservation.CommandType = CommandType.StoredProcedure;
                addReservation.CommandText = "AddReservation";
                addReservation.Parameters.AddWithValue("@Name", TextBox1.Text);
                addReservation.Parameters.AddWithValue("@Date", Calendar1.SelectedDate.ToString("yyyy-MM-dd"));
                addReservation.Parameters.AddWithValue("@Time", DropDownList1.SelectedValue.ToString());
                addReservation.Parameters.AddWithValue("@Restaurant", makeReservation);

                db.DoUpdateUsingCmdObj(addReservation);
                */




                makeReservation = null;

                //Label3.Text = "Your reservation has been placed! Go back to home page.";
                
            }
        }

        /*
        private double ExecuteCallToWebAPI(string operation, double value1, double value2)
        {
            String url = "http://cis-iis2.temple.edu/users/pascucci/CIS3342/CoreWebAPI/api/Calculator/" + operation;
            url = url + "/" + value1 + "/" + value2;


            // Create an HTTP Web Request and get the HTTP Web Response from the server.
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();

            // Read the data from the Web Response, which requires working with streams.
            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            String data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            // Deserialize a JSON string into a double.
            JavaScriptSerializer js = new JavaScriptSerializer();
            double result = js.Deserialize<double>(data);

            return result;
        }
        */
        protected void Button2_Click(object sender, EventArgs e)
        {
            UserSession session = (UserSession)Session["theSession"];
            Session["theSession"] = session;

            if (session.UserName == "Guest")
            {
                Session["ReservationSession"] = null;
                Response.Redirect("GuestPage.aspx");
            }
            else if (session.Reviewer == false)
            {
                Session["ReservationSession"] = null;
                Response.Redirect("RepresentativePage.aspx");
            }
            else
            {
                Session["SeeAllReviews"] = null;
                Response.Redirect("ReviewerPage.aspx");
            }
        }
    }
}