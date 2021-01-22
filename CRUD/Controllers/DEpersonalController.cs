using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRUD.Models;

namespace CRUD.Controllers
{
    public class DEpersonalController : Controller
    {
        private EmpleadosEntities db = new EmpleadosEntities();
        private consultasSQL emDB = new consultasSQL();

        // GET: DEpersonal
        public ActionResult Index()
        {
            return View(db.Personal.ToList());
        }
        public JsonResult ReadListaPersonal()
        {
            var result = emDB.ReadAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public ActionResult Create(Personal personal)
        {
            var result = emDB.Create(personal);
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetInfID(int? ID_personal)
        {
            List<Personal> personal = new List<Personal>();
            if (ID_personal == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            personal = emDB.Details((int)ID_personal);
            if (personal == null)
            {
                return HttpNotFound();
            }
            return Json(personal, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Personal personal)
        {            
            var result = emDB.Update(personal);
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int? ID)
        {
            List<Personal> personal = new List<Personal>();

            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            personal = emDB.Details((int)ID);
            if (personal == null)
            {
                return HttpNotFound();
            }
            var result = emDB.Delete((int)ID);
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
