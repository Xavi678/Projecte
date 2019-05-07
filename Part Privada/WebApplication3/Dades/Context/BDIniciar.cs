using Dades.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dades.Context
{
    public class BDIniciar : DropCreateDatabaseIfModelChanges<PersonaContext>
    {
        protected override void Seed(PersonaContext context)
        {
            List<Persona> persones = new List<Persona>();
            persones.Add(new Administrador(new Adreça("El segrià","lleida","25310"),"4353454234Z","xavi",45,"xavi@hotmail.com","xavi",973396578,new DateTime(1999,02,28)));
            persones.Add(new Client(new Adreça("El segrià", "lleida", "25310"), "454656546F", "xavi", 45, "xavi56@hotmail.com", "xavi", 973396578, new DateTime(1999, 02, 28),"Manel Gutierrez"));
            persones.Add(new Autor(new Adreça("El segrià", "lleida", "25310"), "4546565464", "Manolo", 45));
            persones.Add(new Director(new Adreça("El segrià", "lleida", "25310"), "4546565446F", "Martin", 45));
            List<Espectacle> espectacles = new List<Espectacle>();

            espectacles.Add(new Espectacle("Star Wars", "s una franquicia compuesta primordialmente de una serie de películas concebidas por el cineasta estadounidense George Lucas, y producidas y distribuidas por The Walt Disney Company a partir de 2012. Su trama describe las vivencias de un grupo de personajes que habitan en una galaxia ficticia e interactúan con elementos como «la Fuerza», un campo de energía metafísico y omnipresente4​ que posee un «lado oscuro» provocado por la ira, el miedo y el odio.",new TimeSpan(2,3,4), "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6c/Star_Wars_Logo.svg/275px-Star_Wars_Logo.svg.png", new Director(new Adreça("El segrià", "lleida", "25310"), "4546565464546gfgf", "Lucas", 45), new Autor(new Adreça("El segrià", "lleida", "25310"), "454656546423S", "James", 45)));
            espectacles.Add(new Espectacle("Star Wars", "s una franquicia compuesta primordialmente de una serie de películas concebidas por el cineasta estadounidense George Lucas, y producidas y distribuidas por The Walt Disney Company a partir de 2012. Su trama describe las vivencias de un grupo de personajes que habitan en una galaxia ficticia e interactúan con elementos como «la Fuerza», un campo de energía metafísico y omnipresente4​ que posee un «lado oscuro» provocado por la ira, el miedo y el odio.", new TimeSpan(2, 3, 4), "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6c/Star_Wars_Logo.svg/275px-Star_Wars_Logo.svg.png", new Director(new Adreça("El segrià", "lleida", "25310"), "1230883D", "Lucas", 45), new Autor(new Adreça("El segrià", "lleida", "25310"), "8454444A", "James", 45)));
            espectacles.Add(new Espectacle("Star Wars", "s una franquicia compuesta primordialmente de una serie de películas concebidas por el cineasta estadounidense George Lucas, y producidas y distribuidas por The Walt Disney Company a partir de 2012. Su trama describe las vivencias de un grupo de personajes que habitan en una galaxia ficticia e interactúan con elementos como «la Fuerza», un campo de energía metafísico y omnipresente4​ que posee un «lado oscuro» provocado por la ira, el miedo y el odio.", new TimeSpan(2, 3, 4), "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6c/Star_Wars_Logo.svg/275px-Star_Wars_Logo.svg.png", new Director(new Adreça("El segrià", "lleida", "25310"), "45465654656564546gfgf", "Lucas", 45), new Autor(new Adreça("El segrià", "lleida", "25310"), "4546565465663S", "James", 45)));
            espectacles.Add(new Espectacle("Star Wars", "s una franquicia compuesta primordialmente de una serie de películas concebidas por el cineasta estadounidense George Lucas, y producidas y distribuidas por The Walt Disney Company a partir de 2012. Su trama describe las vivencias de un grupo de personajes que habitan en una galaxia ficticia e interactúan con elementos como «la Fuerza», un campo de energía metafísico y omnipresente4​ que posee un «lado oscuro» provocado por la ira, el miedo y el odio.", new TimeSpan(2, 3, 4), "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6c/Star_Wars_Logo.svg/275px-Star_Wars_Logo.svg.png", new Director(new Adreça("El segrià", "lleida", "25310"), "45465654645TTY5676f", "Lucas", 45), new Autor(new Adreça("El segrià", "lleida", "25310"), "454656544543534S", "James", 45)));
            List<Funcio> funcions = new List<Funcio>();

            


            context.Teatres.Add(new Teatre(new Adreça("El segrià", "Alcarràs", "25310"), "liceu", 5, 6));
            context.Espectacles.AddRange(espectacles);
            context.Persones.AddRange(persones);
            base.Seed(context);

            //funcions.Add(new Funcio(context.Espectacles.FirstOrDefault(), context.Teatres.FirstOrDefault(), new DateTime(2018, 2, 3), new TimeSpan(2, 30, 30), new TimeSpan(1, 56, 45)));
            //funcions.Add(new Funcio(context.Espectacles.FirstOrDefault(), context.Teatres.FirstOrDefault(), new DateTime(2018, 2, 3), new TimeSpan(2, 30, 30), new TimeSpan(1, 56, 45)));
            ///context.Funcions.AddRange(funcions);
            //base.Seed(context);
        }

    }
}
