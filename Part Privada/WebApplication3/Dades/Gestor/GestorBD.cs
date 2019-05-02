using Dades.Context;
using Dades.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Dades.Gestor
{
    public class GestorBD
    {
       private PersonaContext db = new PersonaContext();



        public bool validar(string Email, string Password)
        {
            if(db.Usuaris.Select(a => a).Where(a => a is Administrador && a.email.Equals(Email) && a.password.Equals(Password)).Count() > 0)
            {

                return true;
            }
            else
            {
                return false;
            }
              
            
           

        }

        public object getPersones()
        {
            return db.Persones.Include(p => p.Adreça).ToList(); 
        }

        public IEnumerable getFuncionsInc()
        {
            return db.Funcions.Include(f => f.Espectacle).Include(f => f.Teatre).ToList();
        }

        public IEnumerable getListDirector()
        {
            return db.Persones.Select(s => s).Where(s => s is Director);
        }

        public IEnumerable getListAutor()
        {
            return db.Persones.Select(s => s).Where(s => s is Autor);
        }

        public IEnumerable getListClient()
        {
            return db.Persones.Select(s => s).Where(s => s is Client);
        }

        public IEnumerable getEspectacles()
        {
            return db.Espectacles;
        }

        public IEnumerable getTeatres()
        {
            return db.Teatres;
        }

        public IEnumerable getListAdministrador()
        {
            return db.Persones.Select(s => s).Where(s => s is Administrador);
        }

        public void afegirEspectacle(Espectacle espectacle)
        {
            db.Espectacles.Add(espectacle);
            db.SaveChanges();
        }

        

        public void afegirFuncio(Funcio funcio)
        {
            db.Funcions.Add(funcio);
            db.SaveChanges();

        }

        public void afegirClient(Client client, Adreça e)
        {
            db.Adreces.Add(e);
            db.SaveChanges();
            db.Persones.Add(client);
            db.SaveChanges();
        }

        public void afegirAdministrador(Administrador admin, Adreça e)
        {
            db.Adreces.Add(e);
            db.SaveChanges();
            db.Persones.Add(admin);
            db.SaveChanges();
        }

        public void afegirDirector(Director director, Adreça e)
        {
            db.Adreces.Add(e);
            db.SaveChanges();
            db.Persones.Add(director);
            db.SaveChanges();
        }

        public void afegirAutor(Autor autor, Adreça e)
        {
            db.Adreces.Add(e);
            db.SaveChanges();
            db.Persones.Add(autor);
            db.SaveChanges();
        }
    }
}
