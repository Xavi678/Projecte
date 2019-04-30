using Dades.Context;
using Dades.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace GestioTeatres.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {

           
            //db.Persones.Add(new Client(new Adreça("Urgell","Balaguer","25310"),"ggfhgf","xavi",5,"xavisp6@gmail.com","xavi"));
            //db.Teatres.Add(new Teatre(new Funcio(new DateTime(1999, 12, 12), new TimeSpan(4, 56, 34), new TimeSpan(4, 56, 34)), new Adreça("Urgell", "Balaguer", "25310"), "liceu", 6, 8));
            //db.Funcions.Add(new Funcio(new Teatre(new Adreça("Urgell", "Balaguer", "25310"), "liceu", 6, 8), new DateTime(1999,12,12), new TimeSpan(4, 56, 34), new TimeSpan(4, 56, 34)));

            
            return View();
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