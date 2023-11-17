using System;
using System.Data;
using Utilities;

namespace RestaurantReviewProject3
{
    public partial class RegisterUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        protected void btnShowLabel_Click(object sender, EventArgs e)
        {

            //Initialize dBconnect obj
            DBConnect dBConnect = new DBConnect();
            DataSet theData = new DataSet();
            switch (RadioButtonList1.SelectedValue)
            {
                case "Reviewer":
                    //make a dataset
                    System.Data.SqlClient.SqlCommand userCount = new System.Data.SqlClient.SqlCommand();
                    userCount.CommandText = "UserCount";
                    userCount.CommandType = CommandType.StoredProcedure;
                    userCount.Parameters.AddWithValue("@param1", TextBox2.Text);

                    theData = dBConnect.GetDataSetUsingCmdObj(userCount);

                    //check if user exists
                    if (theData.Tables[0].Rows[0]["count"].ToString() == "0")
                    {
                        //User does not exsist create table
                        //Check if form is fill out
                        if (TextBox1.Text != "" && TextBox2.Text != "")
                        {
                            //Form is filled
                            //Check if username already exists

                            //add the user
                            //create the command
                            System.Data.SqlClient.SqlCommand addUser = new System.Data.SqlClient.SqlCommand();
                            addUser.CommandType = CommandType.StoredProcedure;
                            addUser.CommandText = "RegisterReviewerUser";
                            addUser.Parameters.AddWithValue("@UserName", TextBox2.Text);
                            addUser.Parameters.AddWithValue("@Name", TextBox1.Text);


                            //run the procedure
                            dBConnect.DoUpdateUsingCmdObj(addUser);

                            //display congrats
                            Label4.Visible = true;
                            Label4.Text = "*Congrats! You registered an account, go login.*";
                            Button1.Visible = false;
                        }
                        else
                        {
                            //Display Error fill form
                            Label4.Text = "*Please fill the form completely*";
                        }
                    }
                    else
                    {
                        //Display user already exists message
                        Label4.Text = "User already exists";
                        Label4.Visible = true;
                    }
                    break;


                case "Representative":
                    //make a dataset
                    System.Data.SqlClient.SqlCommand userCount1 = new System.Data.SqlClient.SqlCommand();
                    userCount1.CommandText = "UserCount";
                    userCount1.CommandType = CommandType.StoredProcedure;
                    userCount1.Parameters.AddWithValue("@param1", TextBox2.Text);

                    theData = dBConnect.GetDataSetUsingCmdObj(userCount1);
                    if (theData.Tables[0].Rows[0]["count"].ToString() == "0")
                    {

                        if (TextBox1.Text != "" && TextBox2.Text != "")
                        {
                            System.Data.SqlClient.SqlCommand addUser = new System.Data.SqlClient.SqlCommand();
                            addUser.CommandType = CommandType.StoredProcedure;
                            addUser.CommandText = "RegisterRepresentativeUser";
                            addUser.Parameters.AddWithValue("@UserName", TextBox2.Text);
                            addUser.Parameters.AddWithValue("@Name", TextBox1.Text);
                            addUser.Parameters.AddWithValue("@rest", DropDownList1.SelectedValue);

                            dBConnect.DoUpdateUsingCmdObj(addUser);

                            //display congrats
                            Label4.Text = "*Congrats! You registered an account, go login.*";
                            Label4.Visible = true;
                            Button1.Visible = false;

                        }
                        else
                        {
                            //Display Error fill form
                            Label4.Text = "*Please fill the form completely*";

                        }
                    }
                    else
                    {
                        //Display user already exists message
                        Label4.Text = "User already exists";
                        Label4.Visible = true;
                    }
                    break;

                default:
                    Label4.Text = "*Please pick a user type*";
                    break;
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginPage.aspx");
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedValue == "Representative")
            {
                DropDownList1.Visible = true;
                Button3.Visible = true;
                Label3.Visible = true;
            }
            else
            {
                DropDownList1.Visible = false;
                Button3.Visible = false;
                Label3.Visible = false;
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddRestaurant.aspx");
        }
    }
}