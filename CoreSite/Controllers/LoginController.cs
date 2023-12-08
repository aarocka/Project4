using CoreSite.Models;
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
        public ActionResult Index(LoginModel model)
        {
            var username = model.Username;
            var password = model.Password;
            var reviewer = model.IsReviewer;
            
            //this should work , but it doesn't.  I don't know why. The form return both true and false
            //var reviewer = collection["IsReviewer"].ToString().ToLower() == "true" ? true : false;
            
            
            //check if username and password are correct using a get request
            string uri = "http://localhost:5054/api/login/" + username + "/" + password + "/" + reviewer;
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
                //user is a reviewer
                HttpContext.Session.Set("UserSession", System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(userSession));
                return RedirectToAction("Index", "Restaurants");
            }
            else if (userSession.Id != 0 && userSession.Reviewer == false)
            {
                //user is a restaurant owner
                HttpContext.Session.Set("UserSession", System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(userSession));
                return RedirectToAction("Index", "Representative");
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
