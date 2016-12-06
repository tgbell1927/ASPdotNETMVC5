using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentMyWrox.Models;

namespace RentMyWrox.Controllers
{
    public class StoreOpenController : Controller
    {
        // GET: StoreOpen
        public ActionResult Index()
        {
            StoreOpen results = new StoreOpen();
            DateTime now = DateTime.Now;
            if (now.DayOfWeek == DayOfWeek.Sunday ||
               (now.DayOfWeek == DayOfWeek.Saturday &&
                       now.TimeOfDay > new TimeSpan(18, 0, 0)))
            {
                results.IsStoreOpenNow = false;
                results.Message = "We open Monday at 9:00 am";
            }
            else if (now.TimeOfDay >= new TimeSpan(9, 0, 0) &&
                       now.TimeOfDay <= new TimeSpan(18, 0, 0))
            {
                results.IsStoreOpenNow = true;
                TimeSpan difference = new TimeSpan(18, 0, 0) - now.TimeOfDay;
                results.Message = string.Format("We close in {0} hours and {1} minutes",
                     difference.Hours, difference.Minutes);
            }
            else if (now.TimeOfDay <= new TimeSpan(9, 0, 0))
            {
                results.IsStoreOpenNow = false;
                results.Message = "We will open at 9:00 am";
            }
            else
            {
                results.IsStoreOpenNow = false;
                results.Message = "We will open tomorrow at 9:00 am";
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}