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
        
        public ActionResult Index()
        {
            bd.getListAutor();
           
            //db.Persones.Add(new Client(new Adreça("Urgell","Balaguer","25310"),"ggfhgf","xavi",5,"xavisp6@gmail.com","xavi"));
            //db.Teatres.Add(new Teatre(new Funcio(new DateTime(1999, 12, 12), new TimeSpan(4, 56, 34), new TimeSpan(4, 56, 34)), new Adreça("Urgell", "Balaguer", "25310"), "liceu", 6, 8));
            //db.Funcions.Add(new Funcio(new Teatre(new Adreça("Urgell", "Balaguer", "25310"), "liceu", 6, 8), new DateTime(1999,12,12), new TimeSpan(4, 56, 34), new TimeSpan(4, 56, 34)));

            
            return View();
        }

        [Filtratge]
        public ActionResult Main()
        {
            return View();
        }

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