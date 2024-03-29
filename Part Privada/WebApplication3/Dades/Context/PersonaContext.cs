﻿using Dades.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace Dades.Context
{
    public class PersonaContext : DbContext
    {
        public PersonaContext() : base("PersonaContext")
        {
            Database.SetInitializer<PersonaContext>(new BDIniciar());
        }

        

        public DbSet<Persona> Persones { get; set; }
        public DbSet<Adreça> Adreces { get; set; }
        public DbSet<Teatre> Teatres { get; set; }
        public DbSet<Funcio> Funcions { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Espectacle> Espectacles { get; set; }
        public DbSet<Compra> Compres { get; set; }
        public DbSet<Usuari> Usuaris { get; set; }
        public DbSet<Administrador> administradors { get; set; }
        public DbSet<mpiscatalunya> mpiscatalunya { get; set; }
        // public DbSet<Client> Clients { get; set; }

        /* public System.Data.Entity.DbSet<Dades.Models.Autor> Autors { get; set; }
         public System.Data.Entity.DbSet<Dades.Models.Director> Directors { get; set; }*/
    }
          
}