using RestaurantReviewLibrary;
using System;
using System.Data;
using System.Data.SqlClient;
using Utilities;


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



            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DBConnect dBConnect = new DBConnect();
            SqlCommand myCommand = new SqlCommand();
            UserSession session = (UserSession)Session["theSession"];
            Session["theSession"] = session;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "UpdateRestaurantInformation";
            myCommand.Parameters.AddWithValue("@RestaurantId", session.RestaurantID);
            myCommand.Parameters.AddWithValue("@Name", TextBox1.Text);
            myCommand.Parameters.AddWithValue("@Address", TextBox2.Text);
            myCommand.Parameters.AddWithValue("@Category", TextBox3.Text);
            myCommand.Parameters.AddWithValue("@Phone", TextBox4.Text);
            myCommand.Parameters.AddWithValue("@Picture", TextBox5.Text);
        }
    }
}