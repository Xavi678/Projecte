﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dades.Context;
using Dades.Models;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class TeatresController : Controller
    {
        private PersonaContext db = new PersonaContext();

        // GET: Teatres
        public ActionResult Index()
        {
            var teatres = db.Teatres.Include(t => t.Adreça);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nom,Files,Columnes,Comarca,Localitat,Codipostal")] TeatreVista teatre)
        {
            Adreça e = new Adreça(teatre.Comarca, teatre.Localitat, teatre.Codipostal);
            Teatre t = new Teatre(e, teatre.Nom, teatre.Files, teatre.Columnes);
            if (ModelState.IsValid)
            {
                db.Adreces.Add(e);
                db.SaveChanges();
                db.Teatres.Add(t);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.AdreçaID = new SelectList(db.Adreces, "ID", "Comarca", teatre.AdreçaID);
            return View(teatre);
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
            TeatreVista vista = new TeatreVista(teatre.ID,teatre.Nom, teatre.Files, teatre.Columnes, teatre.Adreça.Comarca, teatre.Adreça.Localitat, teatre.Adreça.Codipostal);
            //ViewBag.AdreçaID = new SelectList(db.Adreces, "ID", "Comarca", teatre.AdreçaID);
            return View(vista);
        }

        // POST: Teatres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
       /* [HttpPost]
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
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nom,Files,Columnes,Comarca,Localitat,Codipostal")] TeatreVista teatre)
        {
            Adreça e = new Adreça(teatre.Comarca, teatre.Localitat, teatre.Codipostal);
            Teatre t = new Teatre(e, teatre.Nom, teatre.Files, teatre.Columnes);
            if (ModelState.IsValid)
            {
                db.Entry(e).State = EntityState.Modified;
                db.Entry(t).State = EntityState.Modified;
                db.SaveChanges();
                

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.AdreçaID = new SelectList(db.Adreces, "ID", "Comarca", teatre.AdreçaID);
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
