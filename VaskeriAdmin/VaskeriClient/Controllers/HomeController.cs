using ModelsAndContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VaskeriClient.Services;

namespace VaskeriClient.Controllers
{
    public class HomeController : Controller
    {
        private DBManager dbm = new DBManager();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            if (dbm.UserExists(user))
            {
                Session["user"] = user;
                return RedirectToAction("Index", "Member");
            }
            else
            {
                return View();
            }
        }

        public async Task<ActionResult> ResetMachines()
        {
            await dbm.Debug_ClearAllMachinesAsync();

            return RedirectToAction("Index", "Home");
        }

    }
}