using Dades.Context;
using Dades.Gestor;
using Dades.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Autenticacio;

namespace GestioTeatres.Controllers
{
    public class HomeController : Controller
    {
        GestorBD bd = new GestorBD();
        
        /// <summary>
        /// Va a la vista index
        /// </summary>
        /// <returns>retorna una vista</returns>
        public ActionResult Index()
        {
            //bd.getListAutor();
           
            //db.Persones.Add(new Client(new Adreça("Urgell","Balaguer","25310"),"ggfhgf","xavi",5,"xavisp6@gmail.com","xavi"));
            //db.Teatres.Add(new Teatre(new Funcio(new DateTime(1999, 12, 12), new TimeSpan(4, 56, 34), new TimeSpan(4, 56, 34)), new Adreça("Urgell", "Balaguer", "25310"), "liceu", 6, 8));
            //db.Funcions.Add(new Funcio(new Teatre(new Adreça("Urgell", "Balaguer", "25310"), "liceu", 6, 8), new DateTime(1999,12,12), new TimeSpan(4, 56, 34), new TimeSpan(4, 56, 34)));

            
            return View();
        }

        /// <summary>
        /// Comprova si un usuari esta loggejat i va a la vista Main
        /// </summary>
        /// <returns>retorna una vista</returns>
        [Filtratge]
        public ActionResult Main()
        {
            return View();
        }

        /// <summary>
        /// Obté un objecte Administrador per mitjà d'un formulari, comrpova si és vàlid, si ho és el posa en sessió, i va a la vista Main, si no ho és crea un missatge d'error i torna al login
        /// </summary>
        /// <param name="administrador">Objecte Administrador</param>
        /// <returns>una vista</returns>
        public ActionResult Login([Bind(Include = "email,password")] Administrador administrador)
        {

            if(bd.validar(administrador.email, administrador.password))
            {
                Session["login"] = administrador.email;
                return View("Main");
               
            }
            else
            {
                ModelState.AddModelError("", "Login failed");
                return View("Index");

                
            }


            
        }
        
        /// <summary>
        /// Posa a null l'objecte que hi havia en sessió
        /// </summary>
        /// <returns>una vista</returns>
        public ActionResult Logout()
        {
            Session["login"] = null;

            return View("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}