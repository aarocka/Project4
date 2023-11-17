using RestaurantReviewLibrary;
using System;
using System.Data;
using Utilities;

namespace RestaurantReviewProject3
{
    public partial class MakeReservation : System.Web.UI.Page
    {
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

                DBConnect db = new DBConnect();
                System.Data.SqlClient.SqlCommand addReservation = new System.Data.SqlClient.SqlCommand();

                addReservation.CommandType = CommandType.StoredProcedure;
                addReservation.CommandText = "AddReservation";
                addReservation.Parameters.AddWithValue("@Name", TextBox1.Text);
                addReservation.Parameters.AddWithValue("@Date", Calendar1.SelectedDate.ToString("yyyy-MM-dd"));
                addReservation.Parameters.AddWithValue("@Time", DropDownList1.SelectedValue.ToString());
                addReservation.Parameters.AddWithValue("@Restaurant", makeReservation);

                db.DoUpdateUsingCmdObj(addReservation);

                makeReservation = null;

                Label3.Text = "Your reservation has been placed! Go back to home page.";
                Label3.Visible = true;
                Button1.Visible = false;
            }
        }
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