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
                return Redirect("http://example.com");
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
    }
}
