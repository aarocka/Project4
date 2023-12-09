using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviewLibrary;
using System.Net;
using System.Text.Json;

namespace CoreSite.Controllers
{
    public class RepresentativeController : Controller
    {
        // GET: RepresentativeController
        public ActionResult Index()
        {
            //check if session is null
            if (HttpContext.Session.GetString("UserSession") == null)
            {
                //user is not logged in
                //Redirect to login page
                ViewBag.UserName = "Guest";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                //User is logged in
                //Deserialize HttpContext.Session.GetString("UserSession");
                UserSession userSession = JsonSerializer.Deserialize<UserSession>(HttpContext.Session.GetString("UserSession"), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    IncludeFields = true
                });

                ViewBag.UserName = userSession.UserName;

                //get reservations from api
                //make a get request to the api
                string webApiUrl = "http://localhost:5054/api/management/"+userSession.RestaurantID+"/reservation";

                //make a get request to the api
                WebRequest request = WebRequest.Create(webApiUrl);
                WebResponse response = request.GetResponse();

                //read the data from the web response, which requires working with streams
                System.IO.Stream theDataStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(theDataStream);
                string data = reader.ReadToEnd();
                reader.Close();
                response.Close();

                //deserialize the json data into a list of reservations
                List<Reservation> reservations = JsonSerializer.Deserialize<List<Reservation>>(data, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    IncludeFields = true
                });


                return View(reservations);
            }
        }

        // GET: RepresentativeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RepresentativeController/Create
        public ActionResult Create()
        {
            //grab the restaurant id from the session
            UserSession userSession = JsonSerializer.Deserialize<UserSession>(HttpContext.Session.GetString("UserSession"), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IncludeFields = true
            });

            int id = int.Parse(userSession.RestaurantID.ToString());

            return RedirectToAction("Id", "Reservation", new { id });
        }

        public ActionResult Reservation(int id)
        {
            //create reservation object
            Reservation reservation = new Reservation();
            reservation.Restaurant = id;
            return View(reservation);
        }

        // POST: RepresentativeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RepresentativeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RepresentativeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RepresentativeController/Delete/5
        public ActionResult Delete(int id)
        { 
            //call the api to delete the reservation
            //make a delete request to the api
            string webApiUrl = "http://localhost:5054/api/management/delete/reservation/"+id;

            //make a delete request to the api
            WebRequest request = WebRequest.Create(webApiUrl);
            request.Method = "DELETE";
            WebResponse response = request.GetResponse();

            //read the data from the web response, which requires working with streams
            System.IO.Stream theDataStream = response.GetResponseStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(theDataStream);
            string data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            //Redirect to index
            return RedirectToAction(nameof(Index));
        }

        // POST: RepresentativeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RepresentativeController/UpdadeInfo
        public ActionResult UpdateInfo()
        {
            //check if session is null
            if (HttpContext.Session.GetString("UserSession") == null)
            {
                //user is not logged in
                //Redirect to login page
                return RedirectToAction("Index", "Login");
            }
            else
            {
                //User is logged in
                //Deserialize HttpContext.Session.GetString("UserSession");
                UserSession userSession = JsonSerializer.Deserialize<UserSession>(HttpContext.Session.GetString("UserSession"), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    IncludeFields = true
                });

                //get restaurant from api
                //make a get request to the api
                string webApiUrl = "http://localhost:5054/api/restaurant/"+userSession.RestaurantID;

                //make a get request to the api
                WebRequest request = WebRequest.Create(webApiUrl);
                WebResponse response = request.GetResponse();

                //read the data from the web response, which requires working with streams
                System.IO.Stream theDataStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(theDataStream);
                string data = reader.ReadToEnd();
                reader.Close();
                response.Close();

                //deserialize the json data
                Restaurant restaurant = JsonSerializer.Deserialize<Restaurant>(data, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    IncludeFields = true
                });

                return View(restaurant);
            }
        }

        // POST: RepresentativeController/UpdadeInfo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateInfo(Restaurant restaurant)
        {
            //check if session is null
            if (HttpContext.Session.GetString("UserSession") == null)
            {
                //user is not logged in
                //Redirect to login page
                return RedirectToAction("Index", "Login");
            }
            else
            {
                //User is logged in
                //Deserialize HttpContext.Session.GetString("UserSession");
                UserSession userSession = JsonSerializer.Deserialize<UserSession>(HttpContext.Session.GetString("UserSession"), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    IncludeFields = true
                });

                //update restaurant in api
                //Serialize the restaurant object to JSON format
                string json = JsonSerializer.Serialize(restaurant);
                 
                //make api call to update restaurant
                string webApiUrl = "http://localhost:5054/api/management/update/restaurant";
                WebRequest request = WebRequest.Create(webApiUrl);
                request.Method = "PUT";
                request.ContentLength = json.Length;
                request.ContentType = "application/json";

                // Write the JSON data to the Web Request
                StreamWriter writer = new StreamWriter(request.GetRequestStream());
                writer.Write(json);
                writer.Flush();
                writer.Close();

                WebResponse response = request.GetResponse();
                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);
                string data = reader.ReadToEnd();
                reader.Close();
                response.Close();

                //Redirect to index
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: RepresentativeController/Logout
        public ActionResult Logout()
        {
            //check if session is null
            if (HttpContext.Session.GetString("UserSession") == null)
            {
                //user is not logged in
                //Redirect to login page
                return RedirectToAction("Index", "Login");
            }
            else
            {
                //delete session
                HttpContext.Session.Remove("UserSession");

                //Redirect to login page
                return RedirectToAction("Index","Login");
            }
        }
    }
}
