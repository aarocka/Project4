using RestaurantReviewLibrary;
using System;
using System.Data;
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

                DBConnect dBConnect = new DBConnect();
                DataSet myReviews = new DataSet();

                System.Data.SqlClient.SqlCommand getMyReviews = new System.Data.SqlClient.SqlCommand();
                getMyReviews.CommandType = CommandType.StoredProcedure;
                getMyReviews.CommandText = "GetReviewsByReviewer";
                getMyReviews.Parameters.AddWithValue("@id", session.Id);

                myReviews = dBConnect.GetDataSetUsingCmdObj(getMyReviews);

                GridView1.DataSource = myReviews;
                GridView1.DataBind();
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
                DBConnect dBconnect = new DBConnect();

                System.Data.SqlClient.SqlCommand deleteReview = new System.Data.SqlClient.SqlCommand();
                deleteReview.CommandType = CommandType.StoredProcedure;
                deleteReview.CommandText = "DeleteAReview";
                deleteReview.Parameters.AddWithValue("@ID", e.CommandArgument);

                dBconnect.DoUpdateUsingCmdObj(deleteReview);

                Response.Redirect("YourReviews.aspx");
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            String temp = (String)Session["temp"];
            Session["temp"] = temp;

            DBConnect d = new DBConnect();

            System.Data.SqlClient.SqlCommand updateReview = new System.Data.SqlClient.SqlCommand();
            updateReview.CommandType = CommandType.StoredProcedure;
            updateReview.CommandText = "ChangeReview";
            updateReview.Parameters.AddWithValue("@Id", temp);
            updateReview.Parameters.AddWithValue("@review", TextBox1.Text);

            d.DoUpdateUsingCmdObj(updateReview);

            Session["temp"] = null;
            Response.Redirect("YourReviews.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReviewerPage.aspx");
        }
    }
}



