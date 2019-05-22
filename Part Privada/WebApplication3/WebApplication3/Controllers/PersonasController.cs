using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Dades.Context;
using Dades.Gestor;
using Dades.Models;
using WebApplication3.Autenticacio;
using WebApplication3.Models;
using WebApplication3.Validador;
using static WebApplication3.Models.PersonaVista;

namespace WebApplication3.Controllers
{
    [Filtratge]
    public class PersonasController : Controller
    {
        //private PersonaContext db = new PersonaContext();
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

            ViewBag.Municipis = new SelectList(bd.obtenirMunicipis());
            return View();
        }

        // POST: Personas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    
        public ActionResult Create([Bind(Include = "NIF,nom,edat,email,password,Localitat,dataNaixement,telefon,tipus,cognoms")] PersonaVista persona)
        {
            try
            {
                //Validation validation = new Validation();
                mpiscatalunya municipi = bd.obtenirMunicipi(persona.Localitat);
                Adreça e = new Adreça(municipi.Nomcomarca, persona.Localitat, municipi.Codi);

                // t = new Persona();
                //int i = (int)persona.tipus;


                switch (persona.tipus)
                {
                    case TipusPersona.Client:
                        {
                            if (bd.ExisteixEmail(persona.email))
                            {
                                ModelState.AddModelError("email", "L'email ha de ser únic");
                                ViewBag.Municipis = new SelectList(bd.obtenirMunicipis());
                                return View();
                            }

                            if (ModelState.IsValid)
                            {

                                Client client = new Client(e, persona.NIF, persona.nom, persona.edat, persona.email, persona.password, persona.telefon.GetValueOrDefault(), persona.dataNaixement.HasValue ? persona.dataNaixement.Value : DateTime.Now, persona.Cognoms);
                                bd.afegirClient(client, e);

                                return RedirectToAction("Index");
                            }

                            break;
                        }
                    case TipusPersona.Administrador:
                        {
                            if (bd.ExisteixEmail(persona.email))
                            {
                                ModelState.AddModelError("email", "L'email ha de ser únic");
                                ViewBag.Municipis = new SelectList(bd.obtenirMunicipis());
                                return View();
                            }


                            //camps.Select(e => e);
                            if  (!String.IsNullOrEmpty( persona.NIF) && !String.IsNullOrEmpty(persona.nom) && persona.edat!=0 && !String.IsNullOrEmpty( persona.email) && String.IsNullOrEmpty(persona.password) && persona.telefon!=0 && persona.dataNaixement!= default(DateTime)) {
                                Administrador admin = new Administrador(e, persona.NIF, persona.nom, persona.edat, persona.email, persona.password, persona.telefon.GetValueOrDefault(), persona.dataNaixement.HasValue ? persona.dataNaixement.Value : DateTime.Now);

                                bd.afegirAdministrador(admin, e);

                                return RedirectToAction("Index");
                            }
                            
                            break;
                        }
                    case TipusPersona.Director:
                        {

                            if (!String.IsNullOrEmpty(persona.NIF) && !String.IsNullOrEmpty(persona.nom) && persona.edat != 0)
                            {
                                Director director = new Director(e, persona.NIF, persona.nom, persona.edat);
                                bd.afegirDirector(director, e);

                                return RedirectToAction("Index");
                            }
                            break;
                        }
                    case TipusPersona.Autor:
                        {
                            if (!String.IsNullOrEmpty(persona.NIF) && !String.IsNullOrEmpty(persona.nom) && persona.edat != 0)
                            {
                                Autor autor = new Autor(e, persona.NIF, persona.nom, persona.edat);
                                bd.afegirAutor(autor, e);
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
                ViewBag.Municipis = new SelectList(bd.obtenirMunicipis());
                //ModelState.AddModelError("", "Omple tots els camps");
                return View();
            }catch(Exception e)
            {
                ViewBag.Municipis = new SelectList(bd.obtenirMunicipis());
                ModelState.AddModelError("", "Hi ha hagut un error");
                return View();
            }
        }

        // GET: Personas/Edit/5
       public ActionResult Edit(string id)
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

            ViewBag.Municipis = new SelectList(bd.obtenirMunicipis());
            if (persona is Usuari)
            {
                
               Usuari u= bd.obtenirUsuariperId(id);

                if (u is Administrador)
                {

                    return View(new PersonaVista(u.NIF, u.nom, u.edat, u.email, u.password, u.Adreça.Comarca, u.Adreça.Localitat, u.Adreça.Localitat,TipusPersona.Administrador,u.telefon,u.dataNaixement));
                }
                else
                {
                    Client c = bd.obtenirClientperId(id);
                    return View(new PersonaVista(u.NIF, u.nom, u.edat, u.email, u.password, u.Adreça.Comarca, u.Adreça.Localitat, u.Adreça.Localitat,TipusPersona.Client,c.telefon,c.dataNaixement,c.Cognoms));
                }
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
        public ActionResult Edit([Bind(Include = "NIF,nom,edat,email,password,Comarca,Localitat,Codipostal,tipus,telefon,dataNaixement,Cognoms")] PersonaVista person)
        {
            if (ModelState.IsValid)
            {
                Adreça adreça = null;
                mpiscatalunya municipi = bd.obtenirMunicipi(person.Localitat);
                if (person.tipus == TipusPersona.Autor || person.tipus == TipusPersona.Director)
                {
                    //Persona persona = new Persona(new Adreça(p.Comarca,p.Localitat,p.Codipostal),p.NIF,p.nom,p.edat);
                    Persona tmpp = bd.obtenirPersonaperNIF(person.NIF);

                    tmpp.edat = person.edat;
                    tmpp.nom = person.nom;
                    adreça = bd.obtenirAdreçaperId(tmpp.AdreçaID);
                    adreça.editarAdreça(municipi);

                    bd.editar(tmpp, adreça);

                    return RedirectToAction("Index");

                }
                else if (person.tipus == TipusPersona.Administrador)
                {
                    if (bd.ExisteixEmail(person.NIF, person.email))
                    {
                        ModelState.AddModelError("email", "L'email ha de ser únic");
                        return View();
                    }
                    Administrador tmpp = bd.obtenirAdminperId(person.NIF);
                    adreça = bd.obtenirAdreçaperId(tmpp.AdreçaID);
                    adreça.editarAdreça(municipi);
                    tmpp.edat = person.edat;
                    tmpp.nom = person.nom;
                    tmpp.email = person.email;
                    tmpp.password = person.password;
                    tmpp.telefon = person.telefon.GetValueOrDefault();
                    tmpp.dataNaixement = person.dataNaixement.HasValue ? person.dataNaixement.Value : DateTime.Now;

                    
                        bd.editar(tmpp, adreça);

                        return RedirectToAction("Index");
                    

                }
                else
                {
                    if (bd.ExisteixEmail(person.NIF, person.email))
                    {
                        ModelState.AddModelError("email", "L'email ha de ser únic");
                        return View();
                    }
                    Client tmpp = bd.obtenirClientperId(person.NIF);
                    adreça = bd.obtenirAdreçaperId(tmpp.AdreçaID);
                    adreça.editarAdreça(municipi);
                    tmpp.edat = person.edat;
                    tmpp.nom = person.nom;
                    tmpp.email = person.email;
                    tmpp.password = person.password;
                    tmpp.telefon = person.telefon.GetValueOrDefault();
                    tmpp.dataNaixement = person.dataNaixement.HasValue? person.dataNaixement.Value : DateTime.Now;
                    tmpp.Cognoms = person.Cognoms;

                    
                        bd.editar(tmpp, adreça);

                        return RedirectToAction("Index");
                    

                }
            }
            //ViewBag.AdreçaID = new SelectList(db.Adreces, "ID", "Comarca", persona.AdreçaID);
            ViewBag.Municipis = new SelectList(bd.obtenirMunicipis());
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
