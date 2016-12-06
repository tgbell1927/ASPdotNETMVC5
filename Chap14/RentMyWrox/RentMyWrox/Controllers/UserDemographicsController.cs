using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.Entity.Validation;
using System.Web.Mvc;
using RentMyWrox.Models;

namespace RentMyWrox.Controllers
{
    public class UserDemographicsController : Controller
    {
        // GET: UserDemographics
        public ActionResult Index()
        {
            using (RentMyWroxContext context = new RentMyWroxContext())
            {
                var list = context.UserDemographics.OrderBy(x => x.Birthdate).ToList();
                list.Add(new UserDemographics { Birthdate = new DateTime(2000, 6, 8) });
                return View(list);
            }
        }

        // GET: UserDemographics/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserDemographics/Create
        public ActionResult Create()
        {
            using (RentMyWroxContext context = new RentMyWroxContext())
            {
                ViewBag.Hobbies = context.Hobbies.Where(x => x.IsActive)
                        .OrderBy(x => x.Name).ToList();
            }
            return View("Manage", new UserDemographics());

        }

        // POST: UserDemographics/Create
        [HttpPost]
        public ActionResult Create(UserDemographics obj)
        {
            using (RentMyWroxContext context = new RentMyWroxContext())
            {
                var ids = Request.Form.GetValues("HobbyIds");
                if (ids != null)
                {
                    obj.Hobbies = context.Hobbies.Where(x => ids.Contains(x.Id.ToString())).ToList();
                }
                context.UserDemographics.Add(obj);
                var validationErrors = context.GetValidationErrors();
                if (validationErrors.Count() == 0)
                {
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ServerValidationErrors = ConvertValidationErrorsToString(validationErrors);
                return View("Manage", obj);
            }
        }

        // GET: UserDemographics/Edit/5
        public ActionResult Edit(int id)
        {
            var model = new UserDemographics();
            return View("Manage", model);

        }

        // POST: UserDemographics/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            using (RentMyWroxContext context = new RentMyWroxContext())
            {
                var item = context.UserDemographics.FirstOrDefault(x => x.Id == id);
                TryUpdateModel(item);
                var ids = Request.Form.GetValues("HobbyIds");
                item.Hobbies = context.Hobbies.Where(x => ids.Contains(x.Id.ToString())).ToList();
                var validationErrors = context.GetValidationErrors();
                if (validationErrors.Count() == 0)
                {
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ServerValidationErrors = ConvertValidationErrorsToString(validationErrors);
                return View("Manage", item);
            }
        }

        // GET: UserDemographics/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserDemographics/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ResidencyReport()
        {
            using (RentMyWroxContext context = new RentMyWroxContext())
            {
                var list = context.Database.SqlQuery<ResidencyReportItem>("exec UserDemographicsTimeInArea").ToList();
                return View(list);
            }
        }

        public ActionResult HobbyReport()
        {
            string query = @"select 
	                        h.Name,
	                        brud.BirthRange,
	                        Count(*) as Total
                        from UserDemographicsHobbies udh
                        inner join Hobbies h on h.Id=udh.Hobby_Id
                        inner join UserDemographics ud on ud.Id=udh.UserDemographics_Id
                        inner join (select Id, 
	                            case 
		                        when Birthdate between  DATEADD(YEAR, -20, getdate()) and GetDate() then ' < 20 '
		                        when birthdate between DATEADD(YEAR, -40, getdate()) and DATEADD(YEAR, -20, getdate()) then '20-40'
		                        when birthdate between DATEADD(YEAR, -60, getdate()) and DATEADD(YEAR, -40, getdate()) then '40-60'		
		                        else ' >60 ' 
	                        end as BirthRange
	                        from UserDemographics) brud on brud.Id = udh.UserDemographics_Id
                        group by brud.BirthRange, h.Name";
            using (RentMyWroxContext context = new RentMyWroxContext())
            {
                var list = context.Database.SqlQuery<HobbyReportItem>(query).ToList();
                return View(list);
            }
        }

        private string ConvertValidationErrorsToString(IEnumerable<DbEntityValidationResult> list)
        {
            StringBuilder results = new StringBuilder();
            results.Append("You had the following validation errors: ");
            foreach (var item in list)
            {
                foreach (var failure in item.ValidationErrors)
                {
                    results.Append(failure.ErrorMessage);
                    results.Append(" ");
                }
            }
            return results.ToString();
        }

    }
}
