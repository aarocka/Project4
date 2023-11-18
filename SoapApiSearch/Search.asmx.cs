using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Collections;
using SoapApiSearch;
using System.Data.SqlClient;
using RestaurantReviewLibrary;
using Utilities;

namespace SoapApiSearch
{
    /// <summary>
    /// Summary description for CustomerService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    // [System.Web.Script.Services.ScriptService]
    public class SearchRestaurant : System.Web.Services.WebService

    {
        
        // This method receives a name for a customer and returns a Customer object with the field values from the database record.
        // This method returns only the first occurrence of a customer name.
        [WebMethod]
        public Restaurant GetRestaurantByName(String name)
        {


            Restaurant rest = null;
            DBConnect objDB = new DBConnect();
            String strSQL = "SELECT * FROM Restaurants WHERE Name='" + name + "'";
            int count = 0;

            objDB.GetDataSet(strSQL, out count);

            if (count > 0)
            {
                rest = new Restaurant(int.Parse(objDB.GetField("Id",0).ToString()));
                rest.Name = objDB.GetField("Name", 0).ToString();
                rest.Address = objDB.GetField("Address", 0).ToString();
                rest.Category = objDB.GetField("Category", 0).ToString();
                rest.Phone = objDB.GetField("Phone", 0).ToString();
                rest.IMGURL = objDB.GetField("Picture", 0).ToString();
            }
            return rest;
        }
    }
}