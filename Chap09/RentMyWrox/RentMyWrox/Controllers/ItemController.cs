using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentMyWrox.Models;

namespace RentMyWrox.Controllers
{
    public class ItemController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            using (RentMyWroxContext context = new RentMyWroxContext())
            {
                List<Item> itemList = context.Items.Where(x => x.IsAvailable).Take(5).ToList();
                return View(itemList);
            }
        }

        public ActionResult Details(int id)
        {
            using (RentMyWroxContext context = new RentMyWroxContext())
            {
                Item item = context.Items.FirstOrDefault(x => x.Id == id);
                return View(item);
            }
        }

    }
}