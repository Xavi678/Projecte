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


        /// <summary>
        /// Comprova que que l'email i la password existeixen a la base de dades, i és un Administrador
        /// </summary>
        /// <param name="Email">Cadena de caracters</param>
        /// <param name="Password">Cadena de caracters</param>
        /// <returns>retorna un booleà depenent si l'usuari ha estat autentificat correctament o no</returns>
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
        /// <summary>
        /// Obté els teatres amb l'adreça inclosa
        /// </summary>
        /// <returns>retorna una llista del teatres</returns>
        public IEnumerable getTeatresInc()
        {
            return db.Teatres.Include(t => t.Adreça).ToList();
        }
        /// <summary>
        /// Obté tots els espectacles amb l'autor i el director inclosos
        /// </summary>
        /// <returns>retorna una llista d'espectacles</returns>
        public IEnumerable getEspectaclesInc()
        {
            return db.Espectacles.Include(a => a.Autor).Include(d => d.Director).ToList();
        }
        /// <summary>
        /// Busca a la base de dades un objecte que tingui el mateix id que li he passat
        /// </summary>
        /// <param name="id">Enter</param>
        /// <returns>retorna un objecte Teatre</returns>
        public Teatre obtenirTeatreperId(int? id)
        {
            return db.Teatres.Find(id);
        }

        public int getCP(string localitat)
        {
            return db.mpiscatalunya.Where(e => e.Nom.Equals(localitat)).Select(e => e.Codi).FirstOrDefault();
        }

        /// <summary>
        /// Busca a la base de dades un objecte que tingui el mateix id que li he passat
        /// </summary>
        /// <param name="id">Enter</param>
        /// <returns>retorna un objecte Teatre</returns>
        public Teatre obtenirTeatreperId(int id)
        {
            return db.Teatres.Select(e => e).Where(e => e.ID.Equals(id)).FirstOrDefault();
        }
        /// <summary>
        /// obté totes les persones de la base de dades amb l'adreça inclosa
        /// </summary>
        /// <returns>retorna una llista de persones</returns>
        public List<Persona> getPersones()
        {
            return db.Persones.Include(p => p.Adreça).ToList(); 
        }

        /// <summary>
        /// obté el una llista amb els noms dels municipis
        /// </summary>
        /// <returns>retorna una llista de </returns>
        public IEnumerable obtenirMunicipis()
        {
            return db.mpiscatalunya.Select(m => m.Nom);
        }
        /// <summary>
        /// Obté un objecte persona per l'id passat com a parametre
        /// </summary>
        /// <param name="id">String</param>
        /// <returns>retorna un objecte person</returns>
        public Persona obtenirPersonaperId(string id)
        {
            return db.Persones.Find(id);
        }
        /// <summary>
        /// Obté un objecte Funcio per l'id passat com a parametre
        /// </summary>
        /// <param name="id">Enter que pot ser null</param>
        /// <returns>retorna un objecte Funcio</returns>
        public Funcio obtenirFuncioperId(int? id)
        {
            return db.Funcions.Find(id);
        }

        /// <summary>
        /// Obté un objecte espectacle per l'id passat com a parametre
        /// </summary>
        /// <param name="id">Enter que pot ser null</param>
        /// <returns>un objecte espectacle</returns>
        public Espectacle obtenirEspectacleperId(int? id)
        {
            return db.Espectacles.Find(id);
        }
        /// <summary>
        /// Obté totes les funcions incloeint-hi l'espectacle i el teatre
        /// </summary>
        /// <returns>una llista de funcions</returns>
        public IEnumerable getFuncionsInc()
        {
            return db.Funcions.Include(f => f.Espectacle).Include(f => f.Teatre).ToList();
        }
        /// <summary>
        /// Obté un Objecte mpiscatalunya que el nom sigui igual que la localitat passada com a parametre
        /// </summary>
        /// <param name="localitat">String</param>
        /// <returns>retorna un objecte mpiscatalunya</returns>
        public mpiscatalunya obtenirMunicipi(string localitat)
        {
            return db.mpiscatalunya.Select(l => l).Where(l =>l.Nom.Equals(localitat)).FirstOrDefault();
        }

        /// <summary>
        /// Comprova que l'Email passat com a parametre no existeixi dins la Base de Dades
        /// </summary>
        /// <param name="Email">String</param>
        /// <returns>Retorna un booleà</returns>
        public bool ExisteixEmail(string Email)
        {
            return db.Usuaris.Where(e => e.email.Equals(Email)).Count() > 0 ? true : false;
        }
        /// <summary>
        /// Comprova que l'Email passat com a parametre no existeixi dins la Base de Dades, excepte si el nif és el mateix
        /// </summary>
        /// <param name="Nif">String</param>
        /// <param name="Email">String</param>
        /// <returns>retorna un booleà</returns>
        public bool ExisteixEmail(string Nif,string Email)
        {
            
            return db.Usuaris.Where(e => e.email.Equals(Email)).AsEnumerable().SkipWhile(e => e.NIF.Equals(Nif)).Count() > 0 ? true : false;
        }

        /// <summary>
        /// Filtra i obté les persones que són Directors
        /// </summary>
        /// <returns>retorna una llista</returns>
        public IEnumerable getListDirector()
        {
            return db.Persones.Select(s => s).Where(s => s is Director);
        }

        /// <summary>
        /// Afegeix un teatre i una adreça a la base de dades
        /// </summary>
        /// <param name="t">Teatre</param>
        /// <param name="e">Adreça</param>
        public void afegirTeatre(Teatre t, Adreça e)
        {
            db.Adreces.Add(e);
            db.SaveChanges();
            db.Teatres.Add(t);

            db.SaveChanges();
        }
        /// <summary>
        /// Filtra i obté les persones que són Autors
        /// </summary>
        /// <returns>retorna una llista</returns>
        public IEnumerable getListAutor()
        {
            return db.Persones.Select(s => s).Where(s => s is Autor);
        }
        /// <summary>
        /// Filtra i obté les persones que són Clients
        /// </summary>
        /// <returns>retorna una llista</returns>
        public IEnumerable getListClient()
        {
            return db.Persones.Select(s => s).Where(s => s is Client);
        }
        /// <summary>
        /// Filtra i obté les persones que són Espectacles
        /// </summary>
        /// <returns>retorna una llista</returns>
        public IEnumerable getEspectacles()
        {
            return db.Espectacles;
        }
        /// <summary>
        /// Obté tots els teatres
        /// </summary>
        /// <returns>Una llista</returns>
        public IEnumerable getTeatres()
        {
            return db.Teatres;
        }
        /// <summary>
        /// Filtra i obté les persones que són Administradors
        /// </summary>
        /// <returns>retorna una llista</returns>
        public IEnumerable getListAdministrador()
        {
            return db.Persones.Select(s => s).Where(s => s is Administrador);
        }
        /// <summary>
        /// Afegeix l'espectacle a la base de dades passat com a parametre
        /// </summary>
        /// <param name="espectacle">Espectacle</param>
        public void afegirEspectacle(Espectacle espectacle)
        {
            db.Espectacles.Add(espectacle);
            db.SaveChanges();
        }


        /// <summary>
        /// Afegeix la funcio a la base de dades passat com a parametre
        /// </summary>
        /// <param name="funcio">Funcio</param>
        public void afegirFuncio(Funcio funcio)
        {
            db.Funcions.Add(funcio);
            db.SaveChanges();

        }
        /// <summary>
        /// Afegeix una funció i una adreça a la base de dades
        /// </summary>
        /// <param name="client">Client</param>
        /// <param name="e">Adreça</param>
        public void afegirClient(Client client, Adreça e)
        {
            db.Adreces.Add(e);
            db.SaveChanges();
            db.Persones.Add(client);
            db.SaveChanges();
        }

        /// <summary>
        /// Afegeix una administrador i una adreça a la base de dades
        /// </summary>
        /// <param name="admin">Administrador</param>
        /// <param name="e">Adreça</param>
        public void afegirAdministrador(Administrador admin, Adreça e)
        {
            db.Adreces.Add(e);
            db.SaveChanges();
            db.Persones.Add(admin);
            db.SaveChanges();
        }
        /// <summary>
        /// Afegeix un Director i una adreça a la base de dades
        /// </summary>
        /// <param name="director">Director</param>
        /// <param name="e">Adreça</param>
        public void afegirDirector(Director director, Adreça e)
        {
            db.Adreces.Add(e);
            db.SaveChanges();
            db.Persones.Add(director);
            db.SaveChanges();
        }
        /// <summary>
        /// Afegeix un Autor i una adreça a la base de dades
        /// </summary>
        /// <param name="autor">Autor</param>
        /// <param name="e">Adreça</param>
        public void afegirAutor(Autor autor, Adreça e)
        {
            db.Adreces.Add(e);
            db.SaveChanges();
            db.Persones.Add(autor);
            db.SaveChanges();
        }

        /// <summary>
        /// Filtra i obté les adreces que el seu id sigui igual que el parametre passat
        /// </summary>
        /// <param name="adreçaID">Enter</param>
        /// <returns>retorna un objecte Adreça</returns>
        public Adreça obtenirAdreçaperId(int adreçaID)
        {
            return db.Adreces.Select(a => a).Where(a => a.ID.Equals(adreçaID)).FirstOrDefault();
        }

        /// <summary>
        /// Modifica un espectacle a la base de dades
        /// </summary>
        /// <param name="espectacle">Espectacle</param>
        public void modificarEspectacle(Espectacle espectacle)
        {
            db.Entry(espectacle).State = EntityState.Modified;
            db.SaveChanges();
        }

        /// <summary>
        /// Modifica una funcio a la base de dades
        /// </summary>
        /// <param name="funcio">Funcio</param>
        public void modificarFuncio(Funcio funcio)
        {
            db.Entry(funcio).State = EntityState.Modified;
            db.SaveChanges();
        }

        /// <summary>
        /// Borra un espectacle passat per parametre
        /// </summary>
        /// <param name="espectacle">Espectacle</param>
        public void borrarEspectacle(Espectacle espectacle)
        {
            db.Espectacles.Remove(espectacle);
            db.SaveChanges();
        }
        /// <summary>
        /// Borra una funcio passada per parametre
        /// </summary>
        /// <param name="funcio">Funcio</param>
        public void borrarFuncio(Funcio funcio)
        {
            db.Funcions.Remove(funcio);
            db.SaveChanges();
        }

        /// <summary>
        /// Modifica un teatre i una adreça passats per parametre
        /// </summary>
        /// <param name="t">Teatre</param>
        /// <param name="adreça">Adreça</param>
        public void editar(Teatre t, Adreça adreça)
        {
           // Teatre t = db.Teatres.Select(e => e).Where(e => e.ID.Equals(teatre.ID)).FirstOrDefault();
            //Adreça adreça = db.Adreces.Select(a => a).Where(a => a.ID.Equals(t.AdreçaID)).FirstOrDefault();

            
            db.Entry(adreça).State = EntityState.Modified;
            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
        }

        /// <summary>
        /// Editar una persona i una adreça passats per parametre
        /// </summary>
        /// <param name="persona">Persona</param>
        /// <param name="adreça">Adreça</param>
        public void editar( Persona persona,Adreça adreça)
        {
            // Teatre t = db.Teatres.Select(e => e).Where(e => e.ID.Equals(teatre.ID)).FirstOrDefault();
            //Adreça adreça = db.Adreces.Select(a => a).Where(a => a.ID.Equals(t.AdreçaID)).FirstOrDefault();


            db.Entry(adreça).State = EntityState.Modified;
            db.Entry(persona).State = EntityState.Modified;
            db.SaveChanges();
        }
        /// <summary>
        /// Borra una persona pasada per parametre 
        /// </summary>
        /// <param name="persona">Persona</param>
        public void borrarPersona(Persona persona)
        {
            db.Persones.Remove(persona);
            db.SaveChanges();
        }

        /// <summary>
        /// Busca l'id passat per parametre dins la llista d'usuaris
        /// </summary>
        /// <param name="id">string</param>
        /// <returns>retorna un usuari</returns>
        public Usuari obtenirUsuariperId(string id)
        {
            return db.Usuaris.Find(id);
        }

        /// <summary>
        /// Busca l'id passat per parametre dins la llista de clients
        /// </summary>
        /// <param name="id">String</param>
        /// <returns>retorna un Client</returns>
        public Client obtenirClientperId(string id)
        {
            return db.Clients.Find(id);
        }

        /// <summary>
        /// Borra el tetare passat com a parametre a la base de dades
        /// </summary>
        /// <param name="teatre">Teatre</param>
        public void borrarTeatre(Teatre teatre)
        {
            db.Teatres.Remove(teatre);
            db.SaveChanges();
        }
        /// <summary>
        /// Obté una persona que contingui el mateix NIF
        /// </summary>
        /// <param name="nIF">string</param>
        /// <returns>retorna una persona</returns>
        public Persona obtenirPersonaperNIF(string nIF)
        {
            return db.Persones.Select(p => p).Where(p => p.NIF.Equals(nIF)).FirstOrDefault();
        }

        /// <summary>
        /// Obté una persona que contingui el mateix NIF
        /// </summary>
        /// <param name="nIF">string</param>
        /// <returns>retorna un administrador</returns>
        public Administrador obtenirAdminperId(string nIF)
        {
            return db.administradors.Find(nIF);
        }
    }
}
