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
    public class FuncionsController : Controller
    {
        //private PersonaContext db = new PersonaContext();
        private GestorBD bd = new GestorBD();
        // GET: Funcions
        public ActionResult Index()
        {
            //var funcions = db.Funcions.Include(f => f.Espectacle).Include(f => f.Teatre);
           var funcions= bd.getFuncionsInc();
            return View(funcions);
        }

        // GET: Funcions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcio funcio = bd.obtenirFuncioperId(id);
            if (funcio == null)
            {
                return HttpNotFound();
            }
            return View(funcio);
        }

        // GET: Funcions/Create
        public ActionResult Create()
        {
            ViewBag.espectacleID = new SelectList(bd.getEspectacles(), "espectacleID", "titol");
            ViewBag.teatreID = new SelectList(bd.getTeatres(), "ID", "Nom");
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
                bd.afegirFuncio(funcio);
                                return RedirectToAction("Index");
            }

            ViewBag.espectacleID = new SelectList(bd.getEspectacles(), "espectacleID", "titol", funcio.espectacleID);
            ViewBag.teatreID = new SelectList(bd.getTeatres(), "ID", "Nom", funcio.teatreID);
            return View(funcio);
        }

        // GET: Funcions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcio funcio = bd.obtenirFuncioperId(id);
            if (funcio == null)
            {
                return HttpNotFound();
            }
            ViewBag.espectacleID = new SelectList(bd.getEspectacles(), "espectacleID", "titol", funcio.espectacleID);
            ViewBag.teatreID = new SelectList(bd.getTeatres(), "ID", "Nom", funcio.teatreID);
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
                bd.modificarFuncio(funcio);
               
                return RedirectToAction("Index");
            }
            ViewBag.espectacleID = new SelectList(bd.getEspectacles(), "espectacleID", "titol", funcio.espectacleID);
            ViewBag.teatreID = new SelectList(bd.getTeatres(), "ID", "Nom", funcio.teatreID);
            return View(funcio);
        }

        // GET: Funcions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcio funcio = bd.obtenirFuncioperId(id);
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
            Funcio funcio = bd.obtenirFuncioperId(id);
            bd.borrarFuncio(funcio);
            
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
