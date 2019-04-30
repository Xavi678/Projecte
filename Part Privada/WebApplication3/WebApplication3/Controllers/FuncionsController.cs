using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dades.Context;
using Dades.Models;

namespace WebApplication3.Controllers
{
    public class FuncionsController : Controller
    {
        private PersonaContext db = new PersonaContext();

        // GET: Funcions
        public ActionResult Index()
        {
            var funcions = db.Funcions.Include(f => f.Espectacle).Include(f => f.Teatre);
            return View(funcions.ToList());
        }

        // GET: Funcions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcio funcio = db.Funcions.Find(id);
            if (funcio == null)
            {
                return HttpNotFound();
            }
            return View(funcio);
        }

        // GET: Funcions/Create
        public ActionResult Create()
        {
            ViewBag.espectacleID = new SelectList(db.Espectacles, "ID", "titol");
            ViewBag.teatreID = new SelectList(db.Teatres, "ID", "Nom");
            return View();
        }

        // POST: Funcions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,espectacleID,teatreID,data,horaInici,horaFi")] Funcio funcio)
        {
            if (ModelState.IsValid)
            {
                db.Funcions.Add(funcio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.espectacleID = new SelectList(db.Espectacles, "ID", "titol", funcio.espectacleID);
            ViewBag.teatreID = new SelectList(db.Teatres, "ID", "Nom", funcio.teatreID);
            return View(funcio);
        }

        // GET: Funcions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcio funcio = db.Funcions.Find(id);
            if (funcio == null)
            {
                return HttpNotFound();
            }
            ViewBag.espectacleID = new SelectList(db.Espectacles, "ID", "titol", funcio.espectacleID);
            ViewBag.teatreID = new SelectList(db.Teatres, "ID", "Nom", funcio.teatreID);
            return View(funcio);
        }

        // POST: Funcions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,espectacleID,teatreID,data,horaInici,horaFi")] Funcio funcio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(funcio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.espectacleID = new SelectList(db.Espectacles, "ID", "titol", funcio.espectacleID);
            ViewBag.teatreID = new SelectList(db.Teatres, "ID", "Nom", funcio.teatreID);
            return View(funcio);
        }

        // GET: Funcions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcio funcio = db.Funcions.Find(id);
            if (funcio == null)
            {
                return HttpNotFound();
            }
            return View(funcio);
        }

        // POST: Funcions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Funcio funcio = db.Funcions.Find(id);
            db.Funcions.Remove(funcio);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /*protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
