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
        List<Personal> SetUpdatePersonal = new List<Personal>();
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
            if (ID_personal == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SetUpdatePersonal = emDB.Details((int)ID_personal);
            if (SetUpdatePersonal == null)
            {
                return HttpNotFound();
            }
            return Json(SetUpdatePersonal, JsonRequestBehavior.AllowGet);
        }

        //[ValidateAntiForgeryToken]
        public ActionResult Edit(int? ID_personal, Personal personal)
        {
            if (ID_personal == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SetUpdatePersonal = emDB.Details((int)ID_personal);

            if (SetUpdatePersonal == null)
            {
                return HttpNotFound();
            }

            foreach (Personal detPersonal in SetUpdatePersonal)
            {
                if (personal.ID_personal == 0)
                {
                    personal.ID_personal = Convert.ToInt32(detPersonal.ID_personal);
                }
                if (personal.Nombre == null)
                {
                    personal.Nombre = Convert.ToString(detPersonal.Nombre);
                }
                if (personal.ApePaterno == null)
                {
                    personal.ApePaterno = Convert.ToString(detPersonal.ApePaterno);
                }
                if (personal.ApeMaterno == null)
                {
                    personal.ApeMaterno = Convert.ToString(detPersonal.ApeMaterno);
                }
                if (personal.Edad == null)
                {
                    personal.Edad = Convert.ToInt32(detPersonal.Edad);
                }
                if (personal.IsActive == null)
                {
                    personal.IsActive = Convert.ToBoolean(detPersonal.IsActive);
                }
            }

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

        public ActionResult Reporte()
        {
            return Redirect("~/Reports/frm_reporte.aspx?tipo=1");
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
