using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using CRUD;
using CRUD.Models;

namespace CRUD.Controllers
{
    public class HomeController : Controller
    {
        private EmpleadosEntities db = new EmpleadosEntities();
        public ActionResult Index()
        {
            return View();
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

        public ActionResult Persona()
        {
            ViewBag.Message = "Registros del personal de la empresa.";
            return View();
        }
    }
}