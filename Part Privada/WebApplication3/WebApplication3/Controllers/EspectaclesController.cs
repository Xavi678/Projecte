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

namespace WebApplication3.Controllers
{
    public class EspectaclesController : Controller
    {
        //private PersonaContext db = new PersonaContext();

        private GestorBD bd = new GestorBD();

        // GET: Espectacles
        public ActionResult Index()
        {
            //var espectacles = ;
            var espectacles= bd.getEspectaclesInc();
            return View(espectacles);
        }

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

        // GET: Espectacles/Create
        public ActionResult Create()
        {
            ViewBag.nifDirector = new SelectList(bd.getListDirector(), "NIF", "nom");
            ViewBag.nifAutor= new SelectList(bd.getListAutor(), "NIF", "nom");
            return View();
        }

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
            return View(espectacle);
        }

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
            return View(espectacle);
        }

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
