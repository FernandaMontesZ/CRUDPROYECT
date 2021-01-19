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
    public class PersonalsController : Controller
    {
        private EmpleadosEntities db = new EmpleadosEntities();
        private consultasSQL emDB = new consultasSQL();

        // GET: Personals
        public ActionResult Index()
        {
            return View(emDB.ReadAll().ToList());
        }
        //GET: envia lista de todo el personal
        public JsonResult ReadListaPersonal()
        {
            var result = emDB.ReadAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Personals/Create
        //public static ActionResult Create()
        //{
        //}

        // POST: Personals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public ActionResult Create(Personal personal)
        {
             //var result = personal;
           var result = emDB.Create(personal);
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult GetInfID(int? ID_personal )

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
           return Json(new { result = result},JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int? ID_personal)
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
            var result = emDB.Delete((int)ID_personal);
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }

        // GET: Personals/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Personal personal = db.Personal.Find(id);
        //    if (personal == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(personal);
        //}

        // POST: Personals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID_personal,Nombre,ApePaterno,ApeMaterno,Edad,IsActive")] Personal personal)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(personal).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(personal);
        //}

        // GET: Personals/Delete/5
        //public ActionResult Delete(int? ID_personal)
        //{
        //    if (ID_personal == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Personal Empleado = emDB.RealAll().Find(x => x.ID_personal.Equals(ID_personal));
        //    if (Empleado == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(Empleado);
        //}

        // POST: Personals/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int ID_personal)
        //{
        //    Personal Empleado = emDB.RealAll().Find(x => x.ID_personal.Equals(ID_personal));
        //    emDB.Delete(ID_personal);
        //    return RedirectToAction("Index");
        //}
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Personals/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Personal personal = db.Personal.Find(id);
        //    if (personal == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(personal);
        //}

        public ActionResult Reporte ()
        {
            return Redirect("~/Reports/frm_reporte.aspx?tipo=1");
        }
    }
}
