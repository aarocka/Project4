using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviewLibrary;
using System.Net;
using System.IO;
using System.Text.Json;

namespace CoreSite.Controllers
{
    public class RestaurantsController : Controller
    {
        // GET: RestaurantsController
        public ActionResult Index()
        {
            /*
            List<Restaurant> restaurants = new List<Restaurant>();

            restaurants.Add(new Restaurant(1, "a", "cat1", "123 st", "123-233-4444", "example.com"));
            restaurants.Add(new Restaurant(2, "b", "cat2", "123 st", "123-233-4444", "example.com"));
            restaurants.Add(new Restaurant(3, "c", "cat3", "123 st", "123-233-4444", "example.com"));
            */

            //make a get request to the api
            string webApiUrl = "http://localhost:5054/api/restaurant/all";
            WebRequest request = WebRequest.Create(webApiUrl);
            WebResponse response = request.GetResponse();

            //read the data from the web response, which requires working with streams
            System.IO.Stream theDataStream = response.GetResponseStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(theDataStream);
            string data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            //deserialize a json string into a list<restaurant>
            Console.WriteLine(data);


            List<Restaurant> restaurants = JsonSerializer.Deserialize<List<Restaurant>>(data,new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            

            return View(restaurants);
        }

        // GET: RestaurantsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RestaurantsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RestaurantsController/Create
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

        // GET: RestaurantsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RestaurantsController/Edit/5
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

        // GET: RestaurantsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RestaurantsController/Delete/5
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
