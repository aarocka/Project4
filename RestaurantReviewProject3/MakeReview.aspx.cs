using System;
using System.Data;
using Utilities;

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

            Label2.Text = "Your review has been placed! Go back to homepage.";
            Label2.Visible = true;

            Button1.Visible = false;
        }
    }
}