using System;
using System.Data;
using System.Data.SqlClient;
using Utilities;

namespace RestaurantReviewProject3
{
    public partial class AddRestaurant : System.Web.UI.Page
    {
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

                Label6.Visible = true;
                Label6.Text = "Your restaurant has been added! Go back home!";
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReviewerPage.aspx");
        }
    }
}