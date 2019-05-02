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
            Persona persona = bd.obtenirPersonaperId(id);
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
       public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = bd.obtenirPersonaperId(id);
            
            if(persona is Usuari)
            {
                
               Usuari u= bd.obtenirUsuariperId(id);

                if (u is Administrador)
                {

                    return View(new PersonaVista(u.NIF, u.nom, u.edat, u.email, u.password, u.Adreça.Comarca, u.Adreça.Localitat, u.Adreça.Localitat,TipusPersona.Administrador));
                }
                else
                {
                    return View(new PersonaVista(u.NIF, u.nom, u.edat, u.email, u.password, u.Adreça.Comarca, u.Adreça.Localitat, u.Adreça.Localitat,TipusPersona.Client));
                }
                }
            if (persona == null)
            {
                return HttpNotFound();
            }
            //ViewBag.AdreçaID = new SelectList(db.Adreces, "ID", "Comarca", persona.AdreçaID);
            if (persona is Autor)
            {
                return View(new PersonaVista(persona.NIF, persona.nom, persona.edat, null, null, persona.Adreça.Comarca, persona.Adreça.Localitat, persona.Adreça.Localitat,TipusPersona.Autor));
            }
            else
            {
                return View(new PersonaVista(persona.NIF, persona.nom, persona.edat, null, null, persona.Adreça.Comarca, persona.Adreça.Localitat, persona.Adreça.Localitat, TipusPersona.Director));
            }
            }

        // POST: Personas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NIF,nom,edat,email,password,Comarca,Localitat,Codipostal,tipus")] PersonaVista person)
        {
            //Persona persona = new Persona(new Adreça(p.Comarca,p.Localitat,p.Codipostal),p.NIF,p.nom,p.edat);
           Persona tmpp= db.Persones.Select(p => p).Where(p => p.NIF.Equals(person.NIF)).FirstOrDefault();
            Adreça adreça = db.Adreces.Select(a => a).Where(a => a.ID.Equals(tmpp.AdreçaID)).FirstOrDefault();
            adreça.Comarca = person.Comarca;
            adreça.Codipostal = person.Codipostal;
            adreça.Localitat = person.Localitat;
            tmpp.edat = person.edat;
            tmpp.nom = person.nom;

            
            if (ModelState.IsValid)
            {
                db.Entry(tmpp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.AdreçaID = new SelectList(db.Adreces, "ID", "Comarca", persona.AdreçaID);
            return View();
        }
        
        // GET: Personas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = bd.obtenirPersonaperId(id);
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
            Persona persona = bd.obtenirPersonaperId(id);
            bd.borrarPersona(persona);
           
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
