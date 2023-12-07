using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviewLibrary;
using CoreSite.Models;
using System.Net;
using System.Text.Json;

namespace CoreSite.Controllers
{
    public class RestaurantController : Controller
    {
        // GET: RestaurantController
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult Id(int id)
        {
            //Initialize model
            RestaurantModel restaurantModel = new RestaurantModel();

            //Make api call to get restaurant info
            string webApiUrl = "http://localhost:5054/api/restaurant/" + id;
            WebRequest request = WebRequest.Create(webApiUrl);
            WebResponse response = request.GetResponse();

            //read the data from the web response, which requires working with streams
            System.IO.Stream theDataStream = response.GetResponseStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(theDataStream);
            string data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            //Deserialize a json string into a restaurant object
            Restaurant restaurant = JsonSerializer.Deserialize<Restaurant>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            restaurantModel.Restaurant = restaurant;
            

            //Make api call to get reviews for restaurant
            webApiUrl = "http://localhost:5054/api/restaurant/" + id +"/review";
            request = WebRequest.Create(webApiUrl);
            response = request.GetResponse();

            //read the data from the web response, which requires working with streams
            theDataStream = response.GetResponseStream();
            reader = new System.IO.StreamReader(theDataStream);
            data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            //Deserialize a json string into a list of reviews
            List<Review> reviews = JsonSerializer.Deserialize<List<Review>>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            restaurantModel.Reviews = reviews;
            
            ViewBag.Id = id;
            return View(restaurantModel);
        }

        // GET: RestaurantController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RestaurantController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RestaurantController/Create
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

        // GET: RestaurantController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RestaurantController/Edit/5
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

        // GET: RestaurantController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RestaurantController/Delete/5
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
