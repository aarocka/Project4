using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetaurantReviewLibrary
{
    class Session
    {
        private string name;
        private int userID;

        public Session(int id)
        {
            userID = id;
        }

        public string Name { get => name; set => name = value; }
        public int UserID { get => userID; set => userID = value; }
    }
}
