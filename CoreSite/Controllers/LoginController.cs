using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviewLibrary;

namespace CoreSite.Controllers
{
    public class LoginController : Controller
    {
        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(IFormCollection collection)
        {
            var username = collection["Username"];
            var password = collection["Password"];

            //check if username and password are correct using a get request
            string uri = "http://localhost:5054/api/login/" + username + "/" + password + "/" + "true";
            System.Net.WebRequest request = System.Net.WebRequest.Create(uri);
            System.Net.WebResponse response = request.GetResponse();

            //read the data from the web response, which requires working with streams
            System.IO.Stream theDataStream = response.GetResponseStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(theDataStream);
            string data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            //Deserialize a json string into a UserSession object
            UserSession userSession = System.Text.Json.JsonSerializer.Deserialize<UserSession>(data, new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IncludeFields = true
            });


            if (userSession.Id!=0&&userSession.Reviewer==true)
            {
                HttpContext.Session.Set("UserSession", System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(userSession));
                return RedirectToAction("Index", "Restaurants");
            }
            else if (userSession.Id != 0 && userSession.Reviewer == false)
            {
                HttpContext.Session.Set("UserSession", System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(userSession));
                return Redirect("http://example.com");
            }
            else
            {
                ViewBag.Error = "Invalid username or password";
                return View();
            }
        }


        // GET: LoginController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginController/Create
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

        // GET: LoginController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginController/Edit/5
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

        // GET: LoginController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginController/Delete/5
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
