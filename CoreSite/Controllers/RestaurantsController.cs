﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviewLibrary;
using System.Net;
using System.IO;
using System.Text.Json;
using Humanizer;
using Microsoft.CodeAnalysis.CodeActions;

namespace CoreSite.Controllers
{
    public class RestaurantsController : Controller
    {
        // GET: RestaurantsController
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("UserSession") == null)
            {
                //user is not logged in
                ViewBag.UserName = "Guest";
                ViewBag.HideStuff = "visibility: collapse;";
                //hide the logout button
                //hide the add restaurant button
                //hide my reviews button
            }
            else
            {
                //user is logged in

                //Deserialize HttpContext.Session.GetString("UserSession");
                UserSession userSession = JsonSerializer.Deserialize<UserSession>(HttpContext.Session.GetString("UserSession"), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    IncludeFields = true
                });

                ViewBag.UserName = userSession.UserName;
                //show the logout button
                //show the add restaurant button
                //show my reviews button
                ViewBag.HideStuff = "visibility: visable;";
            }
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
            List<Restaurant> restaurants = JsonSerializer.Deserialize<List<Restaurant>>(data,new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            

            return View(restaurants);
        }

        public ActionResult MyReviews() { 
            return RedirectToAction("Index","MyReviews");
        }

        // GET: RestaurantsController/Details/5
        public ActionResult Details(int id)
        {
            return RedirectToAction("Id", "Restaurant", new { id });
        }

        // GET: RestaurantsController/Create
        public ActionResult Create()
        {
            return View();
        }



        public ActionResult MakeReservation(int id)
        {
            return RedirectToAction("Id", "Reservation", new { id });
        }

        public ActionResult MakeReview(int id)
        {
            return RedirectToAction("Id", "Review", new { id });
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
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
