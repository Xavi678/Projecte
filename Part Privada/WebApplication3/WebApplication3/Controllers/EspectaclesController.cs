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
using WebApplication3.Autenticacio;

namespace WebApplication3.Controllers
{
    [Filtratge]
    public class EspectaclesController : Controller
    {
        //private PersonaContext db = new PersonaContext();

        private GestorBD bd = new GestorBD();

        // GET: Espectacles
        /// <summary>
        /// Obté els espectacles de la base de dades i els passa a la vista
        /// </summary>
        /// <returns>retorna una vista amb un model d'espectacles</returns>
        public ActionResult Index()
        {
            //var espectacles = ;
            var espectacles= bd.getEspectaclesInc();
            return View(espectacles);
        }

        /// <summary>
        /// Obté l'espectacle per un id i comprova que no sigui null
        /// </summary>
        /// <param name="id">Enter que pot ser null</param>
        /// <returns>retorna una vista amb un objecte Espectacle</returns>
        // GET: Espectacles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Espectacle espectacle =  bd.obtenirEspectacleperId(id);
            if (espectacle == null)
            {
                return HttpNotFound();
            }
            return View(espectacle);
        }
       /// <summary>
       /// Crea dos llistes, una de director i l'altra d'autor per poder-les obtenir a la vista
       /// </summary>
       /// <returns>retorna una vista</returns>
        public ActionResult Create()
        {
            ViewBag.nifDirector = new SelectList(bd.getListDirector(), "NIF", "nom");
            ViewBag.nifAutor= new SelectList(bd.getListAutor(), "NIF", "nom");
            return View();
        }

        /// <summary>
        /// Obté un element espectacle passat amb un formulari comprovant que no hi hagi errors i l'afegeix a la base de dades, i si no torna a la vista create, tot creant dos llistes per pdoer-hi accedir des de la vista
        /// </summary>
        /// <param name="espectacle"></param>
        /// <returns>redirecciona a l'index o retorna la vista Create amb un objecte espectacle</returns>
        // POST: Espectacles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EspectacleID,titol,sinopsi,durada,cartell,nifAutor,nifDirector")] Espectacle espectacle)
        {
            if (ModelState.IsValid)
            {
                bd.afegirEspectacle(espectacle);
                
                return RedirectToAction("Index");
            }
            ViewBag.nifDirector = new SelectList(bd.getListDirector(), "NIF", "nom");
            ViewBag.nifAutor = new SelectList(bd.getListAutor(), "NIF", "nom");
            return View(espectacle);
        }
        /// <summary>
        /// Obté l'espectacle per un id i comprova que no sigui null
        /// </summary>
        /// <param name="id">Enter que pot ser null</param>
        /// <returns>retorna una vista amb un objecte Espectacle</returns>
        // GET: Espectacles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Espectacle espectacle = bd.obtenirEspectacleperId(id);
            if (espectacle == null)
            {
                return HttpNotFound();
            }
            ViewBag.nifDirector = new SelectList(bd.getListDirector(), "NIF", "nom");
            ViewBag.nifAutor = new SelectList(bd.getListAutor(), "NIF", "nom");
            return View(espectacle);
        }

        /// <summary>
        /// Obté un element espectacle passat amb un formulari comprovant que no hi hagi errors i modifica l'objecte a la base de dades, i si no torna a la vista create, tot creant dos llistes per pdoer-hi accedir des de la vista
        /// </summary>
        /// <param name="espectacle"></param>
        /// <returns>redirecciona a l'index o retorna la vista Create amb un objecte espectacle</returns>
        // POST: Espectacles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EspectacleID,titol,sinopsi,durada,cartell,nifAutor,nifDirector")] Espectacle espectacle)
        {
            if (ModelState.IsValid)
            {
                bd.modificarEspectacle(espectacle);
                
                return RedirectToAction("Index");
            }
            ViewBag.nifDirector = new SelectList(bd.getListDirector(), "NIF", "nom");
            ViewBag.nifAutor = new SelectList(bd.getListAutor(), "NIF", "nom");
            return View(espectacle);
        }
        /// <summary>
        /// Obté l'espectacle per un id i comprova que no sigui null
        /// </summary>
        /// <param name="id">Enter que pot ser null</param>
        /// <returns>retorna una vista amb un objecte Espectacle</returns>
        // GET: Espectacles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Espectacle espectacle = bd.obtenirEspectacleperId(id);
            if (espectacle == null)
            {
                return HttpNotFound();
            }
            return View(espectacle);
        }
        /// <summary>
        /// Obté un id, amb aquest troba tot l'bjecte espectacle i el borra de la base de dades
        /// </summary>
        /// <param name="id">Enter</param>
        /// <returns>redirecciona a l'index</returns>
        // POST: Espectacles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Espectacle espectacle = bd.obtenirEspectacleperId(id);
            bd.borrarEspectacle(espectacle);

            
            return RedirectToAction("Index");
        }

       /* protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
