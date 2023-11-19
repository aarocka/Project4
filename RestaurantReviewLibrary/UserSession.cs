using System;

using System.Data;
using Utilities;


namespace RestaurantReviewLibrary
{
    public class UserSession
    {
        private string userName;
        private int id;
        private int restaurantID;
        private bool reviewer;
        private string password;
        //make a constructor here

        public UserSession(string name, bool isReviewer, string password)
        {
            reviewer = isReviewer;
            if (isReviewer)
            {
                userName = name;
                DBConnect dbConnect = new DBConnect();

                //make command object
                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "FindRevIDByUserWithPW";
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@PW", password); 

                //setID
                DataSet dataSet = dbConnect.GetDataSetUsingCmdObj(command);
                try
                {   
                    id = int.Parse(dataSet.Tables[0].Rows[0]["Id"].ToString());


                }
                catch (Exception e)
                {
                    id = 0;
                }
                //disconnect
                dbConnect.CloseConnection();
            }
            else
            {
                userName = name;
                DBConnect dbConnect = new DBConnect();

                //make command object
                System.Data.SqlClient.SqlCommand findUserID = new System.Data.SqlClient.SqlCommand();
                findUserID.CommandType = CommandType.StoredProcedure;

                //TODO MAke strored procedurwe
                findUserID.CommandText = "FindRepByUSRNameWithPW";
                findUserID.Parameters.AddWithValue("@username", name);
                findUserID.Parameters.AddWithValue("@PW", password);


                //setID
                DataSet myDataSet = dbConnect.GetDataSetUsingCmdObj(findUserID);
                try
                {
                    id = int.Parse(myDataSet.Tables[0].Rows[0]["Id"].ToString());

                    DBConnect db = new DBConnect();
                    System.Data.SqlClient.SqlCommand myCommand = new System.Data.SqlClient.SqlCommand();
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandText = "RestaurantInformation";
                    myCommand.Parameters.AddWithValue("@ID", id);

                    restaurantID = int.Parse(db.GetDataSetUsingCmdObj(myCommand).Tables[0].Rows[0]["RestaurantID"].ToString());
                }
                catch (Exception e)
                {
                    id = 0;
                }
                //disconnect
                dbConnect.CloseConnection();


            }

            this.password = password;
        }


        public int Id { get => id; set => id = value; }
        public string UserName { get => userName; set => userName = value; }
        public int RestaurantID { get => restaurantID; set => restaurantID = value; }
        public bool Reviewer { get => reviewer; set => reviewer = value; }
    }
}
