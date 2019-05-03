using Dades.Context;
using Dades.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApplication3
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            PersonaContext db = new PersonaContext();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Database.Delete("gestioteatres");
            db.Adreces.Add(new Adreça("lleida", "lleida", "2345"));
            //db.Adreces.Select(m => m).First();
            db.SaveChanges();
            db.Persones.Add(new Autor(db.Adreces.Select(m => m).First(), "56677", "xavi", 545456));
            db.Persones.Add(new Director(db.Adreces.Select(m => m).First(), "56546456h", "xavi", 45454));
            db.Persones.Add(new Administrador(db.Adreces.Select(m => m).First(), "56546456hrrr", "xavi", 45454,"xavisp6@gmail.com","xavi",5465,new DateTime(1999,11,11)));
            //db.Espectacles.Add(new Espectacle("EL rei león","eatre",new TimeSpan(4,5,54),"cartell",db.Persones.Select(m =>m).Where(m => m.)))
            db.SaveChanges();
        }
    }
}
