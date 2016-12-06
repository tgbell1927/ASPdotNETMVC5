using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using RentMyWrox.Models;

namespace RentMyWrox.Controllers
{
    public class ItemController : Controller
    {
        [Route("")]
        public ActionResult Index(int pageNumber = 1, int pageQty = 5, string sortExp = "name_asc")
        {
            using (RentMyWroxContext context = new RentMyWroxContext())
            {
                // set most of the items needed on the client-side
                ViewBag.PageSize = pageQty;
                ViewBag.PageNumber = pageNumber;
                ViewBag.SortExpression = sortExp;

                var items = from i in context.Items
                            where i.IsAvailable
                            select i;

                // setting this here to get the count of the filtered list
                ViewBag.ItemCount = items.Count();

                switch (sortExp)
                {
                    case "name_asc":
                        items = items.OrderBy(i => i.Name);
                        break;
                    case "name_desc":
                        items = items.OrderByDescending(i => i.Name);
                        break;
                    case "cost_asc":
                        items = items.OrderBy(i => i.Cost);
                        break;
                    case "cost_desc":
                        items = items.OrderByDescending(i => i.Cost);
                        break;
                }

                items = items.Skip((pageNumber - 1) * pageQty).Take(pageQty);
                return View(items.ToList());
            }

        }

        [OutputCache(Duration = 1200, Location = OutputCacheLocation.ServerAndClient)]
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