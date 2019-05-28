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
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Filtratge]
    public class TeatresController : Controller
    {
        //private PersonaContext db = new PersonaContext();
        private GestorBD bd = new GestorBD();
        // GET: Teatres

        /// <summary>
        /// Obté una llista de teatres per pasar-la a la vista
        /// </summary>
        /// <returns>una vista amb un objecte llista de teatres</returns>
        public ActionResult Index()
        {
            var teatres = bd.getTeatresInc();

            return View(teatres);
        }

        /// <summary>
        /// Per l'id passat comprova que no sigui null, i obté el teatre corresponent per l'id passat, comprova que l'objecte obtingut no sigui null i retorna la vista
        /// </summary>
        /// <param name="id">Enter que pot ser null</param>
        /// <returns>Vista amb un objecte Teatre</returns>
        // GET: Teatres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teatre teatre = bd.obtenirTeatreperId(id);
            if (teatre == null)
            {
                return HttpNotFound();
            }
            return View(teatre);
        }

        /// <summary>
        /// Crea un selectlist dels municipis per passar-la a la vista
        /// </summary>
        /// <returns>una vista</returns>
        // GET: Teatres/Create
        public ActionResult Create()
        {
            //ViewBag.AdreçaID = new SelectList(db.Adreces, "ID", "Comarca");
            ViewBag.Municipis = new SelectList(bd.obtenirMunicipis());
            return View();
        }


        /// <summary>
        /// obté un objecte teatrevista per un formulari i obté el municipi corresponent a la localitat, instancia un objecte adreça i un teatre, i si el model és correcte els afegeix a la base de dades i torna ala ction index, si no és vàlid el formualri torna a crear el selectlist i retorna la vista
        /// </summary>
        /// <param name="teatre"></param>
        /// <returns></returns>
        // POST: Teatres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nom,Files,Columnes,Localitat,Codipostal")] TeatreVista teatre)
        {
            mpiscatalunya municipi = bd.obtenirMunicipi(teatre.Localitat);
            Adreça e = new Adreça(municipi.Nomcomarca, teatre.Localitat, teatre.Codipostal);
            Teatre t = new Teatre(e, teatre.Nom, teatre.Files, teatre.Columnes);
            if (ModelState.IsValid)
            {
                bd.afegirTeatre(t,e);
               
                return RedirectToAction("Index");
            }
            ViewBag.Municipis = new SelectList(bd.obtenirMunicipis());
            //ViewBag.AdreçaID = new SelectList(db.Adreces, "ID", "Comarca", teatre.AdreçaID);
            return View(teatre);
        }

        /// <summary>
        /// Per l'id passat comprova que no sigui null, i obté el teatre corresponent per l'id passat, comprova que l'objecte obtingut no sigui null i retorna la vista, crea el selectlist de municipis i instancia un Objecte TeatreVista per pasar-lo a la vista
        /// </summary>
        /// <param name="id">Enter que pot ser null</param>
        /// <returns>Vista amb un objecte TeatreVista</returns>ET: Teatres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teatre teatre = bd.obtenirTeatreperId(id);

            if (teatre == null)
            {
                return HttpNotFound();
            }
            TeatreVista vista = new TeatreVista(teatre.ID,teatre.Nom, teatre.Files, teatre.Columnes, teatre.Adreça.Comarca, teatre.Adreça.Localitat,teatre.Adreça.Codipostal);
            //ViewBag.AdreçaID = new SelectList(db.Adreces, "ID", "Comarca", teatre.AdreçaID);
            ViewBag.Municipis = new SelectList(bd.obtenirMunicipis());
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

        /// <summary>
        /// Obté el teatreVista des d'un formulari, per la localitat obté el municipi i edita l'adreça i el teatre corresponent, si no és correct el formulari torna a la vista creant el selectlist dels municipis
        /// </summary>
        /// <param name="teatre">Objecte TeatreVista</param>
        /// <returns>Una vista o una redirecció a l'index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nom,Files,Columnes,Localitat,Codipostal")] TeatreVista teatre)
        {
            //Adreça e = new Adreça(teatre.Comarca, teatre.Localitat, teatre.Codipostal);
            //Teatre t = new Teatre(e, teatre.Nom, teatre.Files, teatre.Columnes);
            mpiscatalunya municipi = bd.obtenirMunicipi(teatre.Localitat);
            Teatre t=   bd.obtenirTeatreperId(teatre.ID);
            Adreça adreça = bd.obtenirAdreçaperId(t.AdreçaID);
            adreça.editarAdreça(municipi,teatre.Codipostal);
            t.Files = teatre.Files;
            t.Columnes = teatre.Columnes;
            t.Nom = teatre.Nom;
            



            if (ModelState.IsValid)
            {
                bd.editar(t, adreça);
                
                

                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Municipis = new SelectList(bd.obtenirMunicipis());
            //ViewBag.AdreçaID = new SelectList(db.Adreces, "ID", "Comarca", teatre.AdreçaID);
            return View(teatre);
        }
        /// <summary>
        /// Per l'id passat comprova que no sigui null, i obté el teatre corresponent per l'id passat, comprova que l'objecte obtingut no sigui null i retorna la vista
        /// </summary>
        /// <param name="id">Enter que pot ser null</param>
        /// <returns>Vista amb un objecte Teatre</returns>
        // GET: Teatres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teatre teatre = bd.obtenirTeatreperId(id);
            if (teatre == null)
            {
                return HttpNotFound();
            }
            return View(teatre);
        }


        /// <summary>
        /// Obté un id amb el que busca el teatre corresponent per després borrar-lo de la base de dades
        /// </summary>
        /// <param name="id">Enter</param>
        /// <returns>retorna una redirecció</returns>
        // POST: Teatres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teatre teatre = bd.obtenirTeatreperId(id);
            bd.borrarTeatre(teatre);
            
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
