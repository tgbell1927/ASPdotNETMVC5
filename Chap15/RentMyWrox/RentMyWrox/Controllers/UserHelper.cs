using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using RentMyWrox.Models;

namespace RentMyWrox.Controllers
{
    public class UserHelper
    {
        private const string coookieName = "RentMyWroxTemporaryUserCookie";

        public static Guid GetUserId()
        {
            Guid userId;
            if (HttpContext.Current.User != null)
            {
                string userid = HttpContext.Current.User.Identity.GetUserId();
                if (Guid.TryParse(userid, out userId))
                {
                    return userId;
                }
            }

            if (HttpContext.Current.Request != null && HttpContext.Current.Request.Cookies != null)
            {
                HttpCookie tempUserCookie = HttpContext.Current.Request.Cookies.Get(coookieName);
                if (tempUserCookie != null && Guid.TryParse(tempUserCookie.Value, out userId))
                {
                    return userId;
                }
            }

            userId = Guid.NewGuid();
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(coookieName, userId.ToString()));
            HttpContext.Current.Request.Cookies.Add(new HttpCookie(coookieName, userId.ToString()));
            return userId;
        }

        public static ApplicationUser GetApplicationUser()
        {
            string userId = HttpContext.Current.User.Identity.GetUserId();
            ApplicationUserManager aum = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            return aum.FindById(userId);
        }

        public static void TransferTemporaryUserToRealUser(Guid tempId, string userId)
        {
            using (RentMyWroxContext context = new RentMyWroxContext())
            {
                if (context.ShoppingCarts.Any(x => x.UserId == tempId))
                {
                    Guid newUserId = Guid.Parse(userId);
                    var list = context.ShoppingCarts.Include("Item").Where(x => x.UserId == tempId);
                    foreach (var tempCart in list)
                    {
                        var sameItemInShoppingCart = context.ShoppingCarts.FirstOrDefault(x => x.Item.Id == tempCart.Item.Id && x.UserId == newUserId);
                        if (sameItemInShoppingCart == null)
                        {
                            tempCart.UserId = newUserId;
                        }
                        else
                        {
                            sameItemInShoppingCart.Quantity++;
                            context.ShoppingCarts.Remove(tempCart);
                        }
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}