using RestaurantReviewLibrary;
using System;
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


                GridView1.DataSource = SqlDataSource1;
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