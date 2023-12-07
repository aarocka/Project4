using RestaurantReviewLibrary;

namespace CoreSite.Models
{
    public class RestaurantModel
    {
        public Restaurant Restaurant { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
