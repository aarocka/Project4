using Microsoft.AspNetCore.Mvc;
using RestaurantReviewLibrary;
using System.Data;
using System.Data.SqlClient;
using Utilities;

namespace RestService.Controllers
{
    [Route("api")]
    public class RestaurantController : Controller
    {

        [HttpGet("login/{name}/{password}/{isReviewer}")]
        public UserSession Login(string name, string password, bool isReviewer)
        {
            UserSession userSession = new UserSession(name, isReviewer, password);
            return userSession;
        }
        /*
        public UserSession Get(string name, bool isReviewer, string password)
        {

            UserSession userSession = new UserSession(name, isReviewer, password);
            //create user session
            int userID = 0;
            if (HttpContext.Session.GetString("userSession") != null)
            {
                userID = (int)HttpContext.Session.GetInt32("userSession");

            }

            userID = userSession.Id;
            HttpContext.Session.SetInt32("userSession", userID);

            return userSession;
        }*/


        ///////////////////////////////Restaurant/////////////////////////////////////////


        //given a restaurant id, return all info for that restaurant
        [HttpGet("restaurant/{id}")]
        public Restaurant Get(int id)
        {
            return new Restaurant(id);
        }


        //get all restaurants in the database and return a List<Restaurant>
        [HttpGet("restaurant/all")]
        public List<Restaurant> GetAll()
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            DBConnect dbConnect = new DBConnect();
            DataSet ds = dbConnect.GetDataSet("SELECT * FROM Restaurants");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Restaurant restaurant = new Restaurant(int.Parse(dr["Id"].ToString()));
                restaurants.Add(restaurant);
            }
            return restaurants;
        }


        //given a restaurant id get all reviews and return a List<Review> for that restaurant
        [HttpGet("restaurant/{id}/review")]
        public List<Review> GetReviews(int id)
        {
            List<Review> reviews = new List<Review>();
            DBConnect dbConnect = new DBConnect();
            DataSet ds = dbConnect.GetDataSet("SELECT Id FROM Reviews WHERE Restaurant = " + id.ToString());
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Review review = new(int.Parse(dr["Id"].ToString()));
                reviews.Add(review);
            }
            return reviews;
        }

        //Shared with user and management
        //Given @Name,@Address,@Category,@Phone,@Picture add a restaurant using the stored procedure AddNewRestaurant
        [HttpPost("restaurant/add")]
        public bool AddRestaurant([FromBody] Restaurant restaurant)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddNewRestaurant";
            cmd.Parameters.AddWithValue("@Name", restaurant.Name);
            cmd.Parameters.AddWithValue("@Address", restaurant.Address);
            cmd.Parameters.AddWithValue("@Category", restaurant.Category);
            cmd.Parameters.AddWithValue("@Phone", restaurant.Phone);
            cmd.Parameters.AddWithValue("@Picture", restaurant.IMGURL);
            DBConnect dbConnect = new DBConnect();
            int result = dbConnect.DoUpdateUsingCmdObj(cmd);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //FIX THIS
        //Given @Category1 and or @Category2 return a list of restaurants that match the category using the stored procedure GetRestaurantsByCategories
        [HttpGet("restaurant/category/{category1}/{category2?}")]
        public List<Restaurant> GetRestaurantsByCategories(string category1, string category2 = null)
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            DBConnect dbConnect = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetRestaurantsByCategories";
            cmd.Parameters.AddWithValue("@Category1", category1);
            cmd.Parameters.AddWithValue("@Category2", category2);
            DataSet ds = dbConnect.GetDataSetUsingCmdObj(cmd);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Restaurant restaurant = new Restaurant(int.Parse(dr["Id"].ToString()));
                restaurants.Add(restaurant);
            }
            return restaurants;
        }







        ///////////////////////////////user/////////////////////////////////////////

        //given a user id, return all reviews for that user
        [HttpGet("user/{id}/review")]
        public List<Review> GetUserReviews(int id)
        {
            List<Review> reviews = new List<Review>();
            DBConnect dbConnect = new DBConnect();
            DataSet ds = dbConnect.GetDataSet("SELECT Id FROM Reviews WHERE Reviewer = " + id.ToString());
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Review review = new(int.Parse(dr["Id"].ToString()));
                reviews.Add(review);
            }
            return reviews;
        }

        //Given a @ID delete the review using the stored procedure DeleteAReview
        [HttpDelete("user/review/delete/{id}")]
        public bool DeleteReview(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteAReview";
            cmd.Parameters.AddWithValue("@ID", id);
            DBConnect dbConnect = new DBConnect();
            int result = dbConnect.DoUpdateUsingCmdObj(cmd);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //Fix this
        //Given @UserID,@RestaurantID,@Food,@Price,@Service,@Atmosphere, @Review add a review using the stored procedure AddReview
        [HttpPost("user/review/add")]
        public bool AddReview([FromBody] Review review)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddReview";
            cmd.Parameters.AddWithValue("@UserID", review.ReviewerId);
            cmd.Parameters.AddWithValue("@RestrauntID", review.RestaurantId);
            cmd.Parameters.AddWithValue("@Food", review.FoodQualityRating);
            cmd.Parameters.AddWithValue("@Price", review.PriceLevelRating);
            cmd.Parameters.AddWithValue("@Service", review.ServiceRating);
            cmd.Parameters.AddWithValue("@Atmosphere", review.AtmosphereRating);
            cmd.Parameters.AddWithValue("@Review", review.ReviewText);
            DBConnect dbConnect = new DBConnect();
            int result = dbConnect.DoUpdateUsingCmdObj(cmd);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Given @Id, @review update a review using the stored procedure ChangeReview
        [HttpPut("user/review/update")]
        public bool ChangeReview([FromBody] Review review)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ChangeReview";
            cmd.Parameters.AddWithValue("@Id", review.ReviewId);
            cmd.Parameters.AddWithValue("@review", review.ReviewText);
            DBConnect dbConnect = new DBConnect();
            int result = dbConnect.DoUpdateUsingCmdObj(cmd);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }





        ///////////////////////////////MANAGEMENT/////////////////////////////////////////

        //given a restaurant id return all reservations for that restaurant
        [HttpGet("management/{id}/reservation")]
        public List<Reservation> GetReservations(int id)
        {
            List<Reservation> reservations = new List<Reservation>();
            DBConnect dbConnect = new DBConnect();
            DataSet ds = dbConnect.GetDataSet("SELECT Id FROM Reservations WHERE Restaurant = " + id.ToString());
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Reservation reservation = new(int.Parse(dr["Id"].ToString()));
                reservations.Add(reservation);
            }
            return reservations;
        }

        //given a @RestaurantId,@Name,@Address,@Category,@Phone,@Picture update the restaurant using the stored procedure UpdateRestaurantInformation
        [HttpPut("management/update/restaurant")]
        public bool UpdateRestaurantInformation([FromBody] Restaurant restaurant)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UpdateRestaurantInformation";
            cmd.Parameters.AddWithValue("@RestaurantId", restaurant.Id);
            cmd.Parameters.AddWithValue("@Name", restaurant.Name);
            cmd.Parameters.AddWithValue("@Address", restaurant.Address);
            cmd.Parameters.AddWithValue("@Category", restaurant.Category);
            cmd.Parameters.AddWithValue("@Phone", restaurant.Phone);
            cmd.Parameters.AddWithValue("@Picture", restaurant.IMGURL);
            DBConnect dbConnect = new DBConnect();
            int result = dbConnect.DoUpdateUsingCmdObj(cmd);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Given @reservation,@Name,@Date,@Time update a reservation using the stored procedure ChangeReservation
        [HttpPut("management/update/reservation")]
        public bool ChangeReservation([FromBody] Reservation reservation)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ChangeReservation";
            cmd.Parameters.AddWithValue("@reservation", reservation.Id);
            cmd.Parameters.AddWithValue("@Name", reservation.Name);
            cmd.Parameters.AddWithValue("@Date", reservation.Date);
            cmd.Parameters.AddWithValue("@Time", reservation.Time);
            DBConnect dbConnect = new DBConnect();
            int result = dbConnect.DoUpdateUsingCmdObj(cmd);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //Given @id delete a reservation using the stored procedure DeleteAReservation
        [HttpDelete("management/delete/reservation/{id}")]
        public bool DeleteReservation(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteAReservation";
            cmd.Parameters.AddWithValue("@id", id);
            DBConnect dbConnect = new DBConnect();
            int result = dbConnect.DoUpdateUsingCmdObj(cmd);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //This is shared with user and management
        //given a @Name,@Date,@Time,@Restaurant create a reservation using the stored procedure AddReservation
        [HttpPost("reservation/add")]
        public bool AddReservation([FromBody] Reservation reservation)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddReservation";
            cmd.Parameters.AddWithValue("@Name", reservation.Name);
            cmd.Parameters.AddWithValue("@Date", reservation.Date);
            cmd.Parameters.AddWithValue("@Time", reservation.Time);
            cmd.Parameters.AddWithValue("@Restaurant", reservation.Restaurant);
            DBConnect dbConnect = new DBConnect();
            int result = dbConnect.DoUpdateUsingCmdObj(cmd);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
