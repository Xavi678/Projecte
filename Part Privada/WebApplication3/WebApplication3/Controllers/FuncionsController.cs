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
    public class FuncionsController : Controller
    {
        //private PersonaContext db = new PersonaContext();
        private GestorBD bd = new GestorBD();
        // GET: Funcions

            /// <summary>
            /// obté una llista de funcions
            /// </summary>
            /// <returns>una vista amb una llista de funcions</returns>
        public ActionResult Index()
        {
            //var funcions = db.Funcions.Include(f => f.Espectacle).Include(f => f.Teatre);
           var funcions= bd.getFuncionsInc();
            return View(funcions);
        }
        /// <summary>
        /// Obté un id, comprova que no sigui null, obté un objecte funcio per l'id donat i comprova que l'objecte funcio no sigui null
        /// </summary>
        /// <param name="id">Enter que pot ser null</param>
        /// <returns>retorna un error http o retorna la vista amb l'objecte Funcio</returns>
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


        /// <summary>
        /// Crea un selectlist a partir dels espectacles, i un altre a partir dels teatres
        /// </summary>
        /// <returns>retorna una vista</returns>
        // GET: Funcions/Create
        public ActionResult Create()
        {
            ViewBag.espectacleID = new SelectList(bd.getEspectacles(), "espectacleID", "titol");
            ViewBag.teatreID = new SelectList(bd.getTeatres(), "ID", "Nom");
            return View();
        }
        /// <summary>
        /// Obté d'un formulari un objecte funcio, comprova que sigui vàlid, si ho es afegeix la funcio a la base de dades i redirecciona a l'index, sino torna a crear els selectlist i retorna una vista
        /// </summary>
        /// <param name="funcio">Objecte Funcio</param>
        /// <returns>retorna a l'index o a la vista</returns>
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
        /// <summary>
        /// Obté un id, comprova que no sigui null, obté un objecte funcio per l'id donat i comprova que l'objecte funcio no sigui null, després crea dos selectlist
        /// </summary>
        /// <param name="id">Enter que pot ser null</param>
        /// <returns>retorna un error http o retorna la vista amb l'objecte Funcio</returns>
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

        /// <summary>
        /// Obté un objecte funcio per un formulari que comprova si es valid, si ho és, modifica l'objecte i retorna a l'index, si no és vàlid crea els dos selectlist i va a la vista
        /// </summary>
        /// <param name="funcio">Objecte Funcio</param>
        /// <returns>una vista amb l'objecte Funcio</returns>
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

        /// <summary>
        /// Obté un id, comprova que no sigui null, obté un objecte funcio per l'id donat i comprova que l'objecte funcio no sigui null
        /// </summary>
        /// <param name="id">Enter que pot ser null</param>
        /// <returns>retorna un error http o retorna la vista amb l'objecte Funcio</returns>
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

        /// <summary>
        /// obté un id, i per aquest id obté tot l'objecte funció per després borrar-lo de la base de dades
        /// </summary>
        /// <param name="id">Enter</param>
        /// <returns>redirecciona a l'index</returns>
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
