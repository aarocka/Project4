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
    public partial class EditReservation : System.Web.UI.Page
    {
        String webApiUrl = "http://localhost:5054/api/management/update/";
        //reservation

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            String update = (String)Session["update"];
            Session["update"] = update;

            //Create a new reservation object
            Reservation reservation = new Reservation();
            reservation.Name = TextBox1.Text;
            reservation.Date = Calendar1.SelectedDate.ToString("yyyy-MM-dd");
            reservation.Time = DropDownList1.SelectedValue.ToString();
            reservation.Id = int.Parse(update);

            //Serialize the reservation object to JSON format
            JavaScriptSerializer js = new JavaScriptSerializer();
            String jsonReservation = js.Serialize(reservation);


            /*
            DBConnect dB = new DBConnect();
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "ChangeReservation";
            sql.CommandType = CommandType.StoredProcedure;
            sql.Parameters.AddWithValue("@reservation", update);
            sql.Parameters.AddWithValue("@name", TextBox1.Text);
            sql.Parameters.AddWithValue("@Date", Calendar1.SelectedDate.ToString("yyyy-MM-dd"));
            sql.Parameters.AddWithValue("@Time", DropDownList1.SelectedValue);
            dB.DoUpdateUsingCmdObj(sql);
            */

            try
            {
                // Send the Customer object to the Web API that will be used to store a new customer record in the database.
                // Setup an HTTP POST Web Request and get the HTTP Web Response from the server.
                WebRequest request = WebRequest.Create(webApiUrl + "reservation/");
                request.Method = "PUT";
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
                    //Label3.Text = "The customer was successfully saved to the database.";
                    //Label3.Visible = true;
                    //Button1.Visible = false;
                    Response.Redirect("RepresentativePage.aspx");
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

            
        }
    }
}