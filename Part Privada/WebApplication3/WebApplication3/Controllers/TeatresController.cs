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
using WebApplication3.Classes;

namespace WebApplication3.Controllers
{
    public class TeatresController : Controller
    {
        private PersonaContext db = new PersonaContext();

        // GET: Teatres
        public ActionResult Index()
        {
            List<TeatreVista> vteatres = new List<TeatreVista>();
            var teatres = db.Teatres.Include(t => t.Adreça);
            foreach(var item in teatres.ToList())
            {
                new TeatreVista();
            }
            return View(teatres.ToList());
        }

        // GET: Teatres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teatre teatre = db.Teatres.Find(id);
            if (teatre == null)
            {
                return HttpNotFound();
            }
            return View(teatre);
        }

        // GET: Teatres/Create
        public ActionResult Create()
        {
            //ViewBag.AdreçaID = new SelectList(db.Adreces, "ID", "Comarca");
            return View();
        }

        // POST: Teatres/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            
            if (ModelState.IsValid)
            {
               
            }

            /*ViewBag.AdreçaID = new SelectList(db.Adreces, "ID", "Comarca", teatre.AdreçaID);*/
            return View();
        }

        // GET: Teatres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teatre teatre = db.Teatres.Find(id);
            if (teatre == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdreçaID = new SelectList(db.Adreces, "ID", "Comarca", teatre.AdreçaID);
            return View(teatre);
        }

        // POST: Teatres/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nom,Files,Columnes,AdreçaID")] Teatre teatre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teatre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdreçaID = new SelectList(db.Adreces, "ID", "Comarca", teatre.AdreçaID);
            return View(teatre);
        }

        // GET: Teatres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teatre teatre = db.Teatres.Find(id);
            if (teatre == null)
            {
                return HttpNotFound();
            }
            return View(teatre);
        }

        // POST: Teatres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teatre teatre = db.Teatres.Find(id);
            db.Teatres.Remove(teatre);
            db.SaveChanges();
            return RedirectToAction("Index");
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
