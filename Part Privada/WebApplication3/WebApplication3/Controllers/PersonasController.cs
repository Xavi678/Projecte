using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Dades.Context;
using Dades.Gestor;
using Dades.Models;
using MySql.Data.MySqlClient;
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
       /// <summary>
       /// Obté una llista de persones i redirecciona a la vista
       /// </summary>
       /// <returns>una vista amb una llista de persones</returns>
        public ActionResult Index()
        {
             
            var persones = bd.getPersones();
            return View(persones);
        }
        /// <summary>
        /// Obté un id, comprova que no sigui null, obté un objecte persona per l'id donat i comprova que l'objecte Persona no sigui null
        /// </summary>
        /// <param name="id">Enter que pot ser null</param>
        /// <returns>retorna un error http o retorna la vista amb l'objecte Persona</returns>
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

        /// <summary>
        /// Crea un selectlist d'una llista de municipis
        /// </summary>
        /// <returns>una vista</returns>
        // GET: Personas/Create
        public ActionResult Create()
        {
            //ViewBag.Tipus = new SelectList(db.Adreces, "ID", "Comarca");

            ViewBag.Municipis = new SelectList(bd.obtenirMunicipis());
            return View();
        }

        /// <summary>
        /// Obté un objecte PersonaVista, per la localitat, obté un objecte municipi i instancia una adreça, depenent del tipus de persona que sigui, entra dins d'un switch en cada un d'ells comprova que els elemnts siguin vàlids, i en el cas del client o l'administrador anteriorment comprova que no existeixi l'email passat, si existeix reidreccciona a la vista i abans crea un missatge d'error, si tot és correcte afegeix la persona a la base de dades i torna a l'index, si surt del case retorna a la vista creant una altra llista, el mateix que si surt algfun error controlat pel try catch, sortirà un missatge d'error a més
        /// </summary>
        /// <param name="persona">Objecte Persona</param>
        /// <returns></returns>
        // POST: Personas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    
        public ActionResult Create([Bind(Include = "NIF,nom,edat,email,password,Localitat,dataNaixement,telefon,tipus,cognoms,Codipostal")] PersonaVista persona)
        {
            try
            {
                //Validation validation = new Validation();
                mpiscatalunya municipi = bd.obtenirMunicipi(persona.Localitat);
                Adreça e = new Adreça(municipi.Nomcomarca, persona.Localitat, persona.Codipostal);

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
                            if  (ModelState.IsValid) {
                                Administrador admin = new Administrador(e, persona.NIF, persona.nom, persona.edat, persona.email, persona.password, persona.telefon.GetValueOrDefault(), persona.dataNaixement.HasValue ? persona.dataNaixement.Value : DateTime.Now);

                                bd.afegirAdministrador(admin, e);

                                return RedirectToAction("Index");
                            }
                            
                            break;
                        }
                    case TipusPersona.Director:
                        {

                            if (ModelState.IsValid)
                            {
                                Director director = new Director(e, persona.NIF, persona.nom, persona.edat);
                                bd.afegirDirector(director, e);

                                return RedirectToAction("Index");
                            }
                            break;
                        }
                    case TipusPersona.Autor:
                        {
                            if (ModelState.IsValid)
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
            }catch(DbUpdateException e)
            {
               
                
                ViewBag.Municipis = new SelectList(bd.obtenirMunicipis());
                ModelState.AddModelError("NIF", "Aquest NIF ja existeix");
                return View();
            }
        }

        /// <summary>
        /// Obté un id, comprova que no sigui null, obté un objecte persona per l'id donat i comprova que l'objecte Persona no sigui null, depenent del tipus de Persona que sigui instanciarà uns atributs o uns altres
        /// </summary>
        /// <param name="id">String</param>
        /// <returns>retorna un error http o retorna la vista amb l'objecte PersonaVista</returns>
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

                    return View(new PersonaVista(u.NIF, u.nom, u.edat, u.email, u.password, u.Adreça.Comarca, u.Adreça.Localitat, u.Adreça.Codipostal,TipusPersona.Administrador,u.telefon,u.dataNaixement));
                }
                else
                {
                    Client c = bd.obtenirClientperId(id);
                    return View(new PersonaVista(u.NIF, u.nom, u.edat, u.email, u.password, u.Adreça.Comarca, u.Adreça.Localitat, u.Adreça.Codipostal,TipusPersona.Client,c.telefon,c.dataNaixement,c.Cognoms));
                }
                }
           
            //ViewBag.AdreçaID = new SelectList(db.Adreces, "ID", "Comarca", persona.AdreçaID);
            if (persona is Autor)
            {
                return View(new PersonaVista(persona.NIF, persona.nom, persona.edat, null, null, persona.Adreça.Comarca, persona.Adreça.Localitat, persona.Adreça.Codipostal,TipusPersona.Autor));
            }
            else
            {
                return View(new PersonaVista(persona.NIF, persona.nom, persona.edat, null, null, persona.Adreça.Comarca, persona.Adreça.Localitat, persona.Adreça.Codipostal, TipusPersona.Director));
            }
            }

        /// <summary>
        /// Obté un objecte PersonaVista d'un formulari, obté el municipi depenent de la localitat, i depenent del tipus de persona que sigui editarà els seus valors a la base de dades, si el formulari no és valid tornarà a la vista creant un selectlist dels municipis
        /// </summary>
        /// <param name="person">Objecte Persona</param>
        /// <returns>una vista o redireccionarà a l'index</returns>
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
                    adreça.editarAdreça(municipi,person.Codipostal);

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
                    adreça.editarAdreça(municipi, person.Codipostal);
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
                    adreça.editarAdreça(municipi, person.Codipostal);
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
        /// <summary>
        /// Obté un id, comprova que no sigui null, obté un objecte persona per l'id donat i comprova que l'objecte Persona no sigui null
        /// </summary>
        /// <param name="id">String</param>
        /// <returns>retorna un error http o retorna la vista amb l'objecte Persona</returns>
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
        /// <summary>
        /// Obté un id i per aquest busca la persona dins la base de dades per després borrar-la de la base de dades i tornar a l'index
        /// </summary>
        /// <param name="id">String</param>
        /// <returns>redirecció a l'Index</returns>
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
