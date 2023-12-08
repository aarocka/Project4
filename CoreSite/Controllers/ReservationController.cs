using Microsoft.AspNetCore.Mvc;
using RestaurantReviewLibrary;
using System.Net;
using System.Text.Json;

namespace CoreSite.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Id(int id)
        {
            Reservation reservation = new Reservation();
            reservation.Restaurant = id;
            return View(reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Id(IFormCollection collection) {
            //create reservation object
            Reservation reservation = new Reservation();
            reservation.Restaurant = int.Parse(collection["Restaurant"]);
            reservation.Name = collection["Name"];
            reservation.Date = collection["Date"];
            reservation.Time = collection["Time"];
            reservation.Id = 0;

            //add reservation to database
            
            //Serialize the reservation object to JSON format
            string json = JsonSerializer.Serialize(reservation);

            //make api call to add reservation
            string webApiUrl = "http://localhost:5054/api/reservation/add";
            WebRequest request = WebRequest.Create(webApiUrl);
            request.Method = "POST";
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

            if (data == "true")
            {
                

                //grab session and check if user is reviewer
                string session = HttpContext.Session.GetString("UserSession");
                //deserialize session
                UserSession userSession = JsonSerializer.Deserialize<UserSession>(session);

                //if user is reviewer, redirect to reviewer dashboard
                if (userSession.Reviewer)
                {
                    return RedirectToAction("Index", "Restaurants");
                }
                else
                {
                    //redirect to representative dashboard
                    return RedirectToAction("Index", "Representative");
                }
            }
            else
            {
                //Display Failure Message
                return Redirect("https://example.com");
            }
        }
    }
}