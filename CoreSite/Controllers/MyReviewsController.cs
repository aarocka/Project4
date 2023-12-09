using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviewLibrary;
using System.Net;
using System.Text.Json;

namespace CoreSite.Controllers
{
    public class MyReviewsController : Controller
    {
        // GET: MyReviewsController
        public ActionResult Index()
        {
            //grab session and check if user is reviewer
            string session = HttpContext.Session.GetString("UserSession");
            //deserialize session
            UserSession userSession = JsonSerializer.Deserialize<UserSession>(session);

            //Make api call to get restaurant info
            string webApiUrl = "http://localhost:5054/api/user/"+userSession.Id+"/review";
            WebRequest request = WebRequest.Create(webApiUrl);
            WebResponse response = request.GetResponse();

            //read the data from the web response, which requires working with streams
            System.IO.Stream theDataStream = response.GetResponseStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(theDataStream);
            string data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            //Deserialize into a list<review>
            List<Review> reviews = JsonSerializer.Deserialize<List<Review>>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(reviews);
        }

        // GET: MyReviewsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MyReviewsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MyReviewsController/Create
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

        // GET: MyReviewsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MyReviewsController/Edit/5
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

        // GET: MyReviewsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MyReviewsController/Delete/5
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
