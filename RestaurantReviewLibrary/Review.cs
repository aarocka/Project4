using System.Data;
using System.Data.SqlClient;
using Utilities;

namespace RestaurantReviewLibrary
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int ReviewerId { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewText { get; set; }
        public int FoodQualityRating { get; set; }
        public int PriceLevelRating { get; set; }
        public int ServiceRating { get; set; }
        public int AtmosphereRating { get; set; }
        public float AVGRating { get; set; }
        public string RestaurantName { get; set; }
        public int RestaurantId { get; set; }

        public Review(int reviewID)
        {
            if (reviewID != 0)
            {
                //given a review id, find the review in the database and populate the properties
                DBConnect dbConnect = new DBConnect();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "GetReviewByReviewID";
                sqlCommand.Parameters.AddWithValue("@reviewID", reviewID);

                DataSet ds = dbConnect.GetDataSetUsingCmdObj(sqlCommand);
                DataRow dr = ds.Tables[0].Rows[0];
                ReviewId = reviewID;
                ReviewerName = dr["ReviewerName"].ToString();
                ReviewText = dr["Review"].ToString();
                FoodQualityRating = int.Parse(dr["Food Quality Rating"].ToString());
                PriceLevelRating = int.Parse(dr["Price Level Rating"].ToString());
                ServiceRating = int.Parse(dr["Service Rating"].ToString());
                AtmosphereRating = int.Parse(dr["Atmosphere Rating"].ToString());
                AVGRating = float.Parse(dr["AVGRating"].ToString());
                RestaurantName = dr["RestaurantName"].ToString();
                RestaurantId = int.Parse(dr["RestaurantID"].ToString());
                ReviewerId = int.Parse(dr["ReviewerID"].ToString());
            }

        }

        public Review()
        {

        }

    }
}
