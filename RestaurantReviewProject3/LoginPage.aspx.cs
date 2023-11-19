using RestaurantReviewLibrary;
using System;

namespace RestaurantReviewProject3
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //UserSession test = new UserSession("Kevin", true);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            UserSession session = (UserSession)Session["theSession"];
            Session["theSession"] = session;
            if (session == null)
            {
                session = new UserSession("Guest", true , "1234");
            }
            Session["theSession"] = session;
            Response.Redirect("GuestPage.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            UserSession session = (UserSession)Session["theSession"];
            Session["theSession"] = session;


            switch (RadioButtonList1.SelectedValue)
            {
                case "Representative":
                    if (session == null)
                    {
                        session = new UserSession(TextBox1.Text, false, TextBox2.Text);
                    }
                    Session["theSession"] = session;

                    if (session.Id != 0)
                    {
                        Response.Redirect("RepresentativePage.aspx");
                    }
                    else
                    {
                        Label1.Text = "Invalid user";
                        Label1.Visible = true;
                        Session["theSession"] = null;
                    }

                    break;

                case "Reviewer":
                    if (session == null)
                    {
                        session = new UserSession(TextBox1.Text, true, TextBox2.Text);
                    }
                    Session["theSession"] = session;

                    if (session.Id != 0)
                    {
                        Response.Redirect("ReviewerPage.aspx");
                    }
                    else
                    {
                        Session["theSession"] = null;
                        Label1.Text = "Invalid user";
                        Label1.Visible = true;
                    }
                    break;
            }
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterUser.aspx");
        }
    }
}