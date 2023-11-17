using System.Data;
using Utilities;


namespace RestaurantReviewLibrary
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int Restaurant { get; set; }

        public Reservation(int id)
        {
            DBConnect dbConnect = new DBConnect();
            DataSet ds = dbConnect.GetDataSet("SELECT * FROM Reservations WHERE Id = " + id.ToString());
            if (ds.Tables[0].Rows.Count == 1)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                Id = int.Parse(dr["Id"].ToString());
                Name = dr["Name"].ToString();
                Date = dr["Date"].ToString();
                Time = dr["Time"].ToString();
                Restaurant = int.Parse(dr["Restaurant"].ToString());
            }
        }

        public Reservation()
        {

        }
    }
}
