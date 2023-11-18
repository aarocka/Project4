using System.Data;
using Utilities;

namespace RestaurantReviewLibrary
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Category { get; set; }
        public string Phone { get; set; }
        public string IMGURL { get; set; }

        public Restaurant(int id)
        {
            //sql find the restaurant by id
            if (id != 0)
            {
                DBConnect dbConnect = new DBConnect();
                DataSet ds = dbConnect.GetDataSet("SELECT * FROM Restaurants WHERE Id = " + id.ToString());
                DataRow dr = ds.Tables[0].Rows[0];

                Id = id;
                Name = dr["Name"].ToString();
                Address = dr["Address"].ToString();
                Category = dr["Category"].ToString();
                Phone = dr["Phone"].ToString();
                IMGURL = dr["Picture"].ToString();
            }

        }

        public Restaurant() { }
    }
}
