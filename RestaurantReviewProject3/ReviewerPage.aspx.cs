﻿using RestaurantReviewLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Web.UI.WebControls;
using Utilities;
using SoapApiSearch;
using System.Collections.Generic;

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

                //Get all restaurants with get request
                String webApiUrl = "http://localhost:5054/api/restaurant/all";
                WebRequest request = WebRequest.Create(webApiUrl);
                WebResponse response = request.GetResponse();

                // Read the data from the Web Response, which requires working with streams.
                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);
                String data = reader.ReadToEnd();
                reader.Close();
                response.Close();

                // Deserialize a JSON string into a List<Restaurant>.
                System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Restaurant> restaurants = js.Deserialize<List<Restaurant>>(data);




                GridView1.DataSource = restaurants;
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

        protected void Button7_Click(object sender, EventArgs e)
        {
            // Get the restaurant name from the TextBox
            string restaurantName = TextBox1.Text;

            // Call the web service method to get the restaurant information
            SearchRestaurant service = new SearchRestaurant();
            Restaurant restaurant = service.GetRestaurantByName(restaurantName);

            // Check if the restaurant exists
            if (restaurant != null)
            {
                // Bind the result to GridView2
                List<Restaurant> restaurantList = new List<Restaurant>();
                restaurantList.Add(restaurant);

                GridView2.DataSource = restaurantList;
                GridView2.DataBind();

                // Hide the error message if it was previously displayed
                lblErrorMessage.Visible = false;
            }
            else
            {
                // Display the error message
                lblErrorMessage.Text = "Sorry, the restaurant doesn't exist.";
                lblErrorMessage.Visible = true;

                // Clear GridView2
                GridView2.DataSource = null;
                GridView2.DataBind();
            }
        }


    }
}