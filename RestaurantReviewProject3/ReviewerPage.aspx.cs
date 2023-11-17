using RestaurantReviewLibrary;
using System;
using System.Data;
using System.Web.UI.WebControls;
using Utilities;

namespace RestaurantReviewProject3
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserSession session = (UserSession)Session["theSession"];
                if (Session["theSession"] == null)
                {
                    Response.Redirect("LoginPage.aspx");
                }
                Session["theSession"] = session;

                Session["SeeAllReviews"] = null;
                Session["ReservaionSession"] = null;
                Session["makeReviewSession"] = null;

                //DBConnect db = new DBConnect();

                // Bind the data from the database to your DropDownLists
                DropDownList2.DataSource = SqlDataSource2;
                DropDownList2.DataTextField = "Category";
                DropDownList2.DataValueField = "Category";
                DropDownList2.DataBind();

                DropDownList3.DataSource = SqlDataSource2;
                DropDownList3.DataTextField = "Category";
                DropDownList3.DataValueField = "Category";
                DropDownList3.DataBind();

                // Set the default value for DropDownList2
                DropDownList2.Items.Insert(0, new ListItem("Select a Category", "0"));
                DropDownList2.SelectedIndex = 0;

                // Set the default value for DropDownList3
                DropDownList3.Items.Insert(0, new ListItem("Select a Category", "0"));
                DropDownList3.SelectedIndex = 0;

                GridView1.DataSource = SqlDataSource1;
                GridView1.DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "MyButton1")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                //Create new session and populate it with rest id and user

                String seeAllReviews = (String)Session["SeeAllReviews"];
                if (Session["SeeAllReviews"] == null)
                {
                    seeAllReviews = id.ToString();
                }
                Session["SeeAllReviews"] = seeAllReviews;

                Response.Redirect("RestReviews.aspx");
            }

            if (e.CommandName == "MyButton2")
            {
                UserSession session = (UserSession)Session["theSession"];
                if (Session["theSession"] == null)
                {
                    Response.Redirect("LoginPage.aspx");
                }
                Session["theSession"] = session;

                int id = Convert.ToInt32(e.CommandArgument);


                int[] thing = (int[])Session["makeReviewSession"];
                if (Session["makeReviewSession"] == null)
                {
                    thing = new int[] { id, session.Id };
                }
                Session["makeReviewSession"] = thing;
                Response.Redirect("MakeReview.aspx");
            }

            if (e.CommandName == "MyButton3")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                String makeReservation = (String)Session["ReservationSession"];
                if (Session["ReservaionSession"] == null)
                {
                    makeReservation = id.ToString();
                }
                Session["ReservationSession"] = makeReservation;
                Response.Redirect("MakeReservation.aspx");
            }
        }
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            DBConnect db = new DBConnect();
            //setup DataSet
            DataSet dataSet = new DataSet();

            if (DropDownList2.SelectedIndex == 0 && DropDownList3.SelectedIndex == 0)
            {
                //Do noting
            }
            else if (DropDownList2.SelectedIndex != 0 ^ DropDownList3.SelectedIndex != 0)
            {
                //Find the changed item and do the thing
                if (DropDownList2.SelectedIndex != 0)
                {
                    //search based on first ddl
                    System.Data.SqlClient.SqlCommand getRestaurants = new System.Data.SqlClient.SqlCommand();

                    getRestaurants.CommandType = CommandType.StoredProcedure;
                    getRestaurants.CommandText = "GetRestaurantsByCategories";
                    getRestaurants.Parameters.AddWithValue("@Category1", DropDownList2.SelectedValue.ToString());

                    //run the procedure
                    dataSet = db.GetDataSetUsingCmdObj(getRestaurants);

                    //bind data to grid view
                    GridView1.DataSource = dataSet;
                    GridView1.DataBind();
                }
                else
                {
                    //search based on second ddl
                    System.Data.SqlClient.SqlCommand getRestaurants = new System.Data.SqlClient.SqlCommand();

                    getRestaurants.CommandType = CommandType.StoredProcedure;
                    getRestaurants.CommandText = "GetRestaurantsByCategories";
                    getRestaurants.Parameters.AddWithValue("@Category1", DropDownList3.SelectedValue.ToString());

                    //run the procedure
                    dataSet = db.GetDataSetUsingCmdObj(getRestaurants);

                    //bind data to grid view
                    GridView1.DataSource = dataSet;
                    GridView1.DataBind();
                    //bind data to grid view
                }
            }
            else
            {
                //Find both cats
                System.Data.SqlClient.SqlCommand getRestaurants = new System.Data.SqlClient.SqlCommand();

                getRestaurants.CommandType = CommandType.StoredProcedure;
                getRestaurants.CommandText = "GetRestaurantsByCategories";
                getRestaurants.Parameters.AddWithValue("@Category1", DropDownList2.SelectedValue.ToString());
                getRestaurants.Parameters.AddWithValue("@Category2", DropDownList3.SelectedValue.ToString());
                //run the procedure
                dataSet = db.GetDataSetUsingCmdObj(getRestaurants);

                //bind data to grid view
                GridView1.DataSource = dataSet;
                GridView1.DataBind();
            }

            DropDownList2.SelectedValue.ToString();
            DropDownList3.SelectedValue.ToString();
        }

        //protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}
        protected void Button4_Click(object sender, EventArgs e)
        {
            Session["theSession"] = null;
            Response.Redirect("LoginPage.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("YourReviews.aspx");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddRestaurant.aspx");
        }


    }
}