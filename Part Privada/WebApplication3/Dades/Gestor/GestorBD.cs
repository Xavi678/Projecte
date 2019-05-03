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

        public IEnumerable getTeatresInc()
        {
            return db.Teatres.Include(t => t.Adreça).ToList();
        }

        public IEnumerable getEspectaclesInc()
        {
            return db.Espectacles.Include(a => a.Autor).Include(d => d.Director).ToList();
        }

        public Teatre obtenirTeatreperId(int? id)
        {
            return db.Teatres.Find(id);
        }

        public Teatre obtenirTeatreperId(int id)
        {
            return db.Teatres.Select(e => e).Where(e => e.ID.Equals(id)).FirstOrDefault();
        }

        public object getPersones()
        {
            return db.Persones.Include(p => p.Adreça).ToList(); 
        }

        public Persona obtenirPersonaperId(string id)
        {
            return db.Persones.Find(id);
        }

        public Funcio obtenirFuncioperId(int? id)
        {
            return db.Funcions.Find(id);
        }

        public Espectacle obtenirEspectacleperId(int? id)
        {
            return db.Espectacles.Find(id);
        }

        public IEnumerable getFuncionsInc()
        {
            return db.Funcions.Include(f => f.Espectacle).Include(f => f.Teatre).ToList();
        }

        public IEnumerable getListDirector()
        {
            return db.Persones.Select(s => s).Where(s => s is Director);
        }

        public void afegirTeatre(Teatre t, Adreça e)
        {
            db.Adreces.Add(e);
            db.SaveChanges();
            db.Teatres.Add(t);

            db.SaveChanges();
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

        public Adreça obtenirAdreçaperId(int adreçaID)
        {
            return db.Adreces.Select(a => a).Where(a => a.ID.Equals(adreçaID)).FirstOrDefault();
        }

        public void modificarEspectacle(Espectacle espectacle)
        {
            db.Entry(espectacle).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void modificarFuncio(Funcio funcio)
        {
            db.Entry(funcio).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void borrarEspectacle(Espectacle espectacle)
        {
            db.Espectacles.Remove(espectacle);
            db.SaveChanges();
        }

        public void borrarFuncio(Funcio funcio)
        {
            db.Funcions.Remove(funcio);
            db.SaveChanges();
        }

        public void editar(Teatre t, Adreça adreça)
        {
           // Teatre t = db.Teatres.Select(e => e).Where(e => e.ID.Equals(teatre.ID)).FirstOrDefault();
            //Adreça adreça = db.Adreces.Select(a => a).Where(a => a.ID.Equals(t.AdreçaID)).FirstOrDefault();

            
            db.Entry(adreça).State = EntityState.Modified;
            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void editar( Persona persona,Adreça adreça)
        {
            // Teatre t = db.Teatres.Select(e => e).Where(e => e.ID.Equals(teatre.ID)).FirstOrDefault();
            //Adreça adreça = db.Adreces.Select(a => a).Where(a => a.ID.Equals(t.AdreçaID)).FirstOrDefault();


            db.Entry(adreça).State = EntityState.Modified;
            db.Entry(persona).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void borrarPersona(Persona persona)
        {
            db.Persones.Remove(persona);
            db.SaveChanges();
        }

        public Usuari obtenirUsuariperId(string id)
        {
            return db.Usuaris.Find(id);
        }

        public void borrarTeatre(Teatre teatre)
        {
            db.Teatres.Remove(teatre);
            db.SaveChanges();
        }

        public Persona obtenirPersonaperNIF(string nIF)
        {
            return db.Persones.Select(p => p).Where(p => p.NIF.Equals(nIF)).FirstOrDefault();
        }
    }
}
