using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSite.Controllers
{
    public class CreateAccountController : Controller
    {
        // GET: CreateAccountController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CreateAccountController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CreateAccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CreateAccountController/Create
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

        // GET: CreateAccountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CreateAccountController/Edit/5
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

        // GET: CreateAccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CreateAccountController/Delete/5
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
