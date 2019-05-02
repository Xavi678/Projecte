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
using WebApplication3.Models;
using static WebApplication3.Models.PersonaVista;

namespace WebApplication3.Controllers
{
    public class PersonasController : Controller
    {
        private PersonaContext db = new PersonaContext();

       

        // GET: Personas
        public ActionResult Index()
        {
            var persones = db.Persones.Include(p => p.Adreça);
            return View(persones.ToList());
        }

        // GET: Personas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persones.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // GET: Personas/Create
        public ActionResult Create()
        {
            //ViewBag.Tipus = new SelectList(db.Adreces, "ID", "Comarca");
            return View();
        }

        // POST: Personas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NIF,nom,edat,email,password,Comarca,Localitat,Codipostal,tipus")] PersonaVista persona)
        {
            Adreça e = new Adreça(persona.Comarca, persona.Localitat, persona.Codipostal);
            // t = new Persona();
            //int i = (int)persona.tipus;
            switch (persona.tipus)
            {
                case TipusPersona.Client:
                    {
                        if (ModelState.IsValid)
                        {
                            db.Adreces.Add(e);
                            db.SaveChanges();
                            db.Persones.Add(new Client(e, persona.NIF, persona.nom, persona.edat, persona.email, persona.password));
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        
                        break;
                    }
                case TipusPersona.Administrador:
                    {
                        if (ModelState.IsValid)
                        {
                            db.Adreces.Add(e);
                            db.SaveChanges();
                            db.Persones.Add(new Administrador(e, persona.NIF, persona.nom, persona.edat, persona.email, persona.password));
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        break;
                    }
                case TipusPersona.Director:
                    {
                        if (ModelState.IsValid)
                        {
                            db.Adreces.Add(e);
                            db.SaveChanges();
                            db.Persones.Add(new Director(e, persona.NIF, persona.nom, persona.edat));
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        break;
                    }
                case TipusPersona.Autor:
                    {
                        if (ModelState.IsValid)
                        {
                            db.Adreces.Add(e);
                            db.SaveChanges();
                            db.Persones.Add(new Autor(e, persona.NIF, persona.nom, persona.edat));
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        break;
                    }
                default: break;
            }
            /*if (ModelState.IsValid)
            {
                
                db.Persones.Add(persona);
                db.SaveChanges();
                return RedirectToAction("Index");
            }*/

            //ViewBag.AdreçaID = new SelectList(db.Adreces, "ID", "Comarca", persona.AdreçaID);
            return View();
        }

        // GET: Personas/Edit/5
       /* public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persones.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdreçaID = new SelectList(db.Adreces, "ID", "Comarca", persona.AdreçaID);
            return View(persona);
        }

        // POST: Personas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NIF,nom,edat,AdreçaID")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(persona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdreçaID = new SelectList(db.Adreces, "ID", "Comarca", persona.AdreçaID);
            return View(persona);
        }
        */
        // GET: Personas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persones.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Persona persona = db.Persones.Find(id);
            db.Persones.Remove(persona);
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
