using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentMyWrox.Models;

namespace RentMyWrox.Controllers
{
    public class ShoppingCartController : Controller
    {
        private Guid UserID = Guid.Empty;

        public ActionResult Index()
        {
            using(RentMyWroxContext context = new RentMyWroxContext())
            {
                ShoppingCartSummary summary = GetShoppingCartSummary(context);
                return PartialView("_ShoppingCartSummary", summary);       
            }
        }

        public ActionResult AddToCart(int id)
        {
            using (RentMyWroxContext context = new RentMyWroxContext())
            {
                Item addedItem = context.Items.FirstOrDefault(x => x.Id == id);

                // now that we know it is a valid ID
                if (addedItem != null)
                {
                    // Check to see if this item was already added
                    var sameItemInShoppingCart = context.ShoppingCarts
                         .FirstOrDefault(x => x.Item.Id == id && x.UserId == UserID);
                    if (sameItemInShoppingCart == null)
                    {
                        // if not already in cart then add it
                        ShoppingCart sc = new ShoppingCart
                        {
                            Item = addedItem,
                            UserId = UserID,
                            Quantity = 1,
                            DateAdded = DateTime.Now
                        };
                        context.ShoppingCarts.Add(sc);
                    }
                    else
                    {
                        // increment the quantity of the existing shopping cart item
                        sameItemInShoppingCart.Quantity++;
                    }
                    context.SaveChanges();
                }
                ShoppingCartSummary summary = GetShoppingCartSummary(context);
                return PartialView("_ShoppingCartSummary", summary);
            }
        }

        private ShoppingCartSummary GetShoppingCartSummary(RentMyWroxContext context)
        {
            ShoppingCartSummary summary = new ShoppingCartSummary();
            var cartList = context.ShoppingCarts.Where(x => x.UserId == UserID);
            if (cartList != null && cartList.Count() > 0)
            {
                summary.TotalValue = cartList.Sum(x => x.Quantity * x.Item.Cost);
                summary.Quantity = cartList.Sum(x => x.Quantity);
            }
            return summary;
        }
    }
}