package cat.almata.daw.api;

import java.util.ArrayList;
import java.util.List;

import javax.ws.rs.Consumes;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.QueryParam;
import javax.ws.rs.core.GenericEntity;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;

import cat.almata.daw.models.UsuariClient;
import cat.almata.daw.models.Compra;
import cat.almata.daw.models.Espectacle;
import cat.almata.daw.models.Funcio;
import cat.almata.daw.models.Teatre;



@Path("/Servei")
public class Servei {
	
	GestorBd db = new GestorBd(Constant.dbserver, Constant.database, Constant.user, Constant.password);
	
	@Path("/Autenticar")
	@GET
	@Produces(MediaType.APPLICATION_JSON)
	public Response autenticar(@QueryParam("email") String email, @QueryParam("password") String passwd) {
		try {

			// System.out.println();

			UsuariClient client = db.autenticar(email, passwd);

			// Token t=new Token(token, new Date());

			GenericEntity<UsuariClient> genericEntity = new GenericEntity<UsuariClient>(client) {
			};

			
			return Response.ok(genericEntity, MediaType.APPLICATION_JSON).build();
		}catch (Exception e) {
			return Response.status(Response.Status.BAD_REQUEST).build();
		}
	}
	
	
	@Path("/getEspectacles")
	@GET
	@Produces(MediaType.APPLICATION_JSON)
	public Response obtenirEspectacles() {
		try {

			// System.out.println();

			List<Espectacle> espectacles = db.obtenirEspectacles();

			// Token t=new Token(token, new Date());

			GenericEntity<List<Espectacle>> genericEntity = new GenericEntity<List<Espectacle>>(espectacles) {
			};

			
			return Response.ok(genericEntity, MediaType.APPLICATION_JSON).build();
		} catch (Exception e) {
			return Response.status(Response.Status.BAD_REQUEST).build();
		}
	}
	
	@Path("/getFuncions")
	@GET
	@Produces(MediaType.APPLICATION_JSON)
	public Response obtenirFuncions(@QueryParam("id") String id ) {
		try {

			// System.out.println();
			
			List<Funcio> funcions = db.obtenirFuncions(Integer.parseInt(id));

			// Token t=new Token(token, new Date());

			GenericEntity<List<Funcio>> genericEntity = new GenericEntity<List<Funcio>>(funcions) {
			};

			
			return Response.ok(genericEntity, MediaType.APPLICATION_JSON).build();
		} catch (Exception e) {
			return Response.status(Response.Status.BAD_REQUEST).build();
		}
	}
	
	
	@Path("/getFuncio")
	@GET
	@Produces(MediaType.APPLICATION_JSON)
	public Response obtenirFuncio(@QueryParam("id") String id ) {
		try {

			// System.out.println();
			
			Funcio funcions = db.obtenirFuncio(Integer.parseInt(id));

			// Token t=new Token(token, new Date());
			
			db.obtenirOcupades(funcions);

			GenericEntity<Funcio> genericEntity = new GenericEntity<Funcio>(funcions) {
			};

			
			return Response.ok(genericEntity, MediaType.APPLICATION_JSON).build();
		} catch (Exception e) {
			return Response.status(Response.Status.BAD_REQUEST).build();
		}
	}
	
	@Path("/addUser")
	@POST
	@Produces(MediaType.APPLICATION_JSON)
	@Consumes(MediaType.APPLICATION_JSON)
	public Response inserirUsuari(UsuariClient client ) {
		try {

			// System.out.println();
			if(!db.existeixEmail(client.getEmail())) {
				return Response.status(Response.Status.BAD_REQUEST).build();
			}
			
			Boolean inserit= db.insert(client);

			// Token t=new Token(token, new Date());

			GenericEntity<Boolean> genericEntity = new GenericEntity<Boolean>(inserit) {
			};

			
			return Response.ok(genericEntity, MediaType.APPLICATION_JSON).build();
		} catch (Exception e) {
			return Response.status(Response.Status.BAD_REQUEST).build();
		}
	}
	
	
	@Path("/comprar")
	@POST
	@Produces(MediaType.APPLICATION_JSON)
	@Consumes(MediaType.APPLICATION_JSON)
	public Response comprar(@QueryParam("funcioID") String funcioID,@QueryParam("clientID") String clientID,ArrayList<Integer> llistafilescolumnes ) {
		try {

			// System.out.println();
			
			//Boolean inserit= db.insert(client);

			// Token t=new Token(token, new Date());

			/*GenericEntity<Boolean> genericEntity = new GenericEntity<Boolean>(inserit) {
			};*/
			
			for( int fc: llistafilescolumnes) {
				
				int fila= Integer.parseInt(String.valueOf(fc).substring(0, 1));
				int columna= Integer.parseInt(String.valueOf(fc).substring(1, 2));
				
				db.insertCompra(funcioID,fila,columna,clientID);

				
			}
			
			return Response.ok( MediaType.APPLICATION_JSON).build();
		} catch (Exception e) {
			return Response.status(Response.Status.BAD_REQUEST).build();
		}
	}
	
	@Path("/obtenirCompres")
	@GET
	@Produces(MediaType.APPLICATION_JSON)
	@Consumes(MediaType.APPLICATION_JSON)
	public Response obtenirCompres(@QueryParam("clientID") String clientID ) {
		try {

			// System.out.println();
			
			//Boolean inserit= db.insert(client);

			// Token t=new Token(token, new Date());

			/*GenericEntity<Boolean> genericEntity = new GenericEntity<Boolean>(inserit) {
			};*/
			
			ArrayList<Compra> compres=db.obtenirCompres(clientID);
			GenericEntity<ArrayList<Compra>> genericEntity = new GenericEntity<ArrayList<Compra>>(compres) {
			};
			return Response.ok( genericEntity,MediaType.APPLICATION_JSON).build();
		} catch (Exception e) {
			return Response.status(Response.Status.BAD_REQUEST).build();
		}
	}
	
	
	@Path("/obtenirLocalitats")
	@GET
	@Produces(MediaType.APPLICATION_JSON)
	@Consumes(MediaType.APPLICATION_JSON)
	public Response obtenirLocalitats( ) {
		try {

			// System.out.println();
			
			//Boolean inserit= db.insert(client);

			// Token t=new Token(token, new Date());

			/*GenericEntity<Boolean> genericEntity = new GenericEntity<Boolean>(inserit) {
			};*/
			
			ArrayList<String> localitats= db.obtenirLocalitats();
			
			GenericEntity<ArrayList<String>> genericEntity = new GenericEntity<ArrayList<String>>(localitats) {
			};
			
			return Response.ok( genericEntity,MediaType.APPLICATION_JSON).build();
		} catch (Exception e) {
			return Response.status(Response.Status.BAD_REQUEST).build();
		}
	}
	
	@Path("/searchEspectacles")
	@GET
	@Produces(MediaType.APPLICATION_JSON)
	@Consumes(MediaType.APPLICATION_JSON)
	public Response searchEspectacles(@QueryParam("search") String search) {
		try {

			// System.out.println();
			
			//Boolean inserit= db.insert(client);

			// Token t=new Token(token, new Date());

			/*GenericEntity<Boolean> genericEntity = new GenericEntity<Boolean>(inserit) {
			};*/
			
			//ArrayList<String> localitats= db.obtenirLocalitats();
		ArrayList<Espectacle> espectacles=	db.filtrarEspectacles(search);
			
			GenericEntity<ArrayList<Espectacle>> genericEntity = new GenericEntity<ArrayList<Espectacle>>(espectacles) {
			};
			
			return Response.ok( genericEntity,MediaType.APPLICATION_JSON).build();
		} catch (Exception e) {
			return Response.status(Response.Status.BAD_REQUEST).build();
		}
	}
	
	@Path("/searchTeatres")
	@GET
	@Produces(MediaType.APPLICATION_JSON)
	@Consumes(MediaType.APPLICATION_JSON)
	public Response searchTeatres(@QueryParam("search") String search) {
		try {

			// System.out.println();
			
			//Boolean inserit= db.insert(client);

			// Token t=new Token(token, new Date());

			/*GenericEntity<Boolean> genericEntity = new GenericEntity<Boolean>(inserit) {
			};*/
			
			/*ArrayList<String> localitats= db.obtenirLocalitats();
			
			GenericEntity<ArrayList<String>> genericEntity = new GenericEntity<ArrayList<String>>(localitats) {
			};*/
			
			
ArrayList<Teatre> teatres=	db.filtrarTeatres(search);
			
			GenericEntity<ArrayList<Teatre>> genericEntity = new GenericEntity<ArrayList<Teatre>>(teatres) {
			};
			return Response.ok( genericEntity,MediaType.APPLICATION_JSON).build();
		} catch (Exception e) {
			return Response.status(Response.Status.BAD_REQUEST).build();
		}
	}
	
	
	/*@Path("/A")
	@GET
	@Produces(MediaType.APPLICATION_JSON)
	public Response a() {
		try {

			// System.out.println();

			String retorn="ghghj";

			// Token t=new Token(token, new Date());

			GenericEntity<String> genericEntity = new GenericEntity<String>(retorn) {
			};

			
			return Response.ok(genericEntity, MediaType.APPLICATION_JSON).build();
		} catch (Exception e) {
			return Response.status(Response.Status.BAD_REQUEST).build();
		}
	}
*/
}
