using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dades.Context;
using Dades.Gestor;
using Dades.Models;
using WebApplication3.Models;
using static WebApplication3.Models.PersonaVista;

namespace WebApplication3.Controllers
{
    public class PersonasController : Controller
    {
        private PersonaContext db = new PersonaContext();
        private GestorBD bd = new GestorBD();

        // GET: Personas
       
        public ActionResult Index()
        {
             
            var persones = bd.getPersones();
            return View(persones);
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
                            Client client = new Client(e, persona.NIF, persona.nom, persona.edat, persona.email, persona.password);
                            bd.afegirClient(client,e);
                            
                            return RedirectToAction("Index");
                        }
                        
                        break;
                    }
                case TipusPersona.Administrador:
                    {
                        if (ModelState.IsValid)
                        {
                            Administrador admin = new Administrador(e, persona.NIF, persona.nom, persona.edat, persona.email, persona.password);

                            bd.afegirAdministrador(admin,e);
                            
                            return RedirectToAction("Index");
                        }
                        break;
                    }
                case TipusPersona.Director:
                    {
                        if (ModelState.IsValid)
                        {
                            Director director = new Director(e, persona.NIF, persona.nom, persona.edat);
                            bd.afegirDirector(director,e);
                            
                            return RedirectToAction("Index");
                        }
                        break;
                    }
                case TipusPersona.Autor:
                    {
                        if (ModelState.IsValid)
                        {
                            Autor autor = new Autor(e, persona.NIF, persona.nom, persona.edat);
                            bd.afegirAutor(autor,e);
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
