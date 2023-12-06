using RestaurantReviewLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using Utilities;


namespace RestaurantReviewProject3
{
    public partial class RepresentativePage : System.Web.UI.Page
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

                //Get reservations for a given restaurant using a GET request
                String webApiUrl = "http://localhost:5054/api/management/"+session.RestaurantID+"/reservation";
                WebRequest request = WebRequest.Create(webApiUrl);
                WebResponse response = request.GetResponse();

                // Read the data from the Web Response, which requires working with streams.
                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);
                String data = reader.ReadToEnd();
                reader.Close();
                response.Close();

                // Deserialize a JSON string into a List<Reservations>
                JavaScriptSerializer js = new JavaScriptSerializer();
                List<Reservation> reservations = js.Deserialize<List<Reservation>>(data);




                /*
                DBConnect dBConnect = new DBConnect();
                DataSet myDataSet = new DataSet();
                myDataSet = dBConnect.GetDataSet("Select * FROM Reservations WHERE Restaurant = '" + session.RestaurantID + "';");
                */
                GridView1.DataSource = reservations;
                GridView1.DataBind();
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {

        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            UserSession session = (UserSession)Session["theSession"];
            Session["theSession"] = session;
            String makeReservation = (String)Session["ReservationSession"];
            if (Session["ReservaionSession"] == null)
            {
                makeReservation = session.RestaurantID.ToString();
            }
            Session["ReservationSession"] = makeReservation;
            Response.Redirect("MakeReservation.aspx");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["theSession"] = null;
            Response.Redirect("LoginPage.aspx");
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "MyButton2")
            {
                string itemId = e.CommandArgument.ToString();
                //create reservation session
                String update = (String)Session["update"];
                if (Session["update"] == null)
                {
                    update = e.CommandArgument.ToString();
                }
                Session["update"] = update;
                Response.Redirect("EditReservation.aspx"); //TODO MAKE THAT PAGE
            }

            if (e.CommandName == "MyButton1")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                DBConnect test = new DBConnect();
                SqlCommand delete = new SqlCommand();
                delete.CommandText = "DeleteAReservation";
                delete.CommandType = CommandType.StoredProcedure;
                delete.Parameters.AddWithValue("@id", id);

                test.DoUpdateUsingCmdObj(delete);
                Response.Redirect("RepresentativePage.aspx");
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            //Take us to the edit restaurant page
            Response.Redirect("EditRestrauntInfo.aspx");
        }
    }
}