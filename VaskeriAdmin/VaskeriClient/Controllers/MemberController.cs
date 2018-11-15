using ModelsAndContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using VaskeriClient.Models;
using VaskeriClient.Services;

namespace VaskeriClient.Controllers
{
    public class MemberController : Controller
    {
        private DBManager dbm = new DBManager();

        public ActionResult Index()
        {
            User user = (User) Session["user"];

            if (user != null)
            {
                UserFront front = dbm.GetUserFrontpageInfo(user);
                Session["user"] = front.User;
                return View(front);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult CreateReservation()
        {
            User user = (User) Session["user"];

            if (user != null)
            {
                if (dbm.UserCanMakeReservation(user))
                {
                    return View(dbm.GetUnusedMachines(user.ServiceID));
                } else
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult CreateReservation(FormCollection formCollection)
        {
            User user = (User)Session["user"];

            if (user != null)
            {
                var reserve = formCollection["reserve"];
                var date = formCollection["date"];
                var time = formCollection["WashTimes"];

                reserve = Regex.Replace(reserve, @",+", ",");


                dbm.CreateReservation(reserve, date, time, user);

                return RedirectToAction("Index");
            } else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult StartMachine()
        {
            User user = (User)Session["user"];

            if (user != null)
            {
                return View(dbm.GetAvailableMachines(user));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult StartMachine(FormCollection formCollection)
        {
            User user = (User)Session["user"];

            if (user != null)
            {
                string type = formCollection["type"];
                string mid = "-1";
                string pid = "-1";

                if(type == "Washer")
                {
                    mid = formCollection["WashingMachines"];
                    pid = formCollection["WashingPrograms"];
                } else if (type == "Dryer")
                {
                    mid = formCollection["DryerMachines"];
                    pid = formCollection["DryerPrograms"];
                }

                dbm.StartMachine(mid, pid, type, user);

                return View("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}