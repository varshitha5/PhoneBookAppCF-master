using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBookAppCF.Controllers
{
    public class DropdownController : Controller
    {
        // GET: Dropdown
        public ActionResult Index()
        {
            List<SelectListItem> search = new List<SelectListItem>();
            search.Add(new SelectListItem { Text = "City", Value = "1" });
            search.Add(new SelectListItem { Text = "State", Value = "2" });
            search.Add(new SelectListItem { Text = "Country", Value = "3" });
            search.Add(new SelectListItem { Text = "Pincode", Value = "4" });
            ViewBag.Search = search;
            return View();
        }
    }
}