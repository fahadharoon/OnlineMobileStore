using MobileStore.Models;
using MobileStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MobileStore.Controllers
{
    public class HomeController : Controller
    {
        MobileStoreContext db = new MobileStoreContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Slider()
        {
            return PartialView();
        }
      

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}