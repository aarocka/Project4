using System;
using System.Data;
using System.Data.SqlClient;
using Utilities;

namespace RestaurantReviewProject3
{
    public partial class EditReservation : System.Web.UI.Page
    {
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

            DBConnect dB = new DBConnect();
            SqlCommand sql = new SqlCommand();
            sql.CommandText = "ChangeReservation";
            sql.CommandType = CommandType.StoredProcedure;
            sql.Parameters.AddWithValue("@reservation", update);
            sql.Parameters.AddWithValue("@name", TextBox1.Text);
            sql.Parameters.AddWithValue("@Date", Calendar1.SelectedDate.ToString("yyyy-MM-dd"));
            sql.Parameters.AddWithValue("@Time", DropDownList1.SelectedValue);
            dB.DoUpdateUsingCmdObj(sql);

            Response.Redirect("RepresentativePage.aspx");
        }
    }
}