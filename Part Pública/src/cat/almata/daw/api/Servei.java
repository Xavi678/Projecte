package cat.almata.daw.api;

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
import cat.almata.daw.models.Espectacle;
import cat.almata.daw.models.Funcio;



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
	
	@Path("/addUser")
	@POST
	@Produces(MediaType.APPLICATION_JSON)
	@Consumes(MediaType.APPLICATION_JSON)
	public Response inserirUsuari(UsuariClient client ) {
		try {

			// System.out.println();
			
			Boolean inserit= db.insert(client);

			// Token t=new Token(token, new Date());

			GenericEntity<Boolean> genericEntity = new GenericEntity<Boolean>(inserit) {
			};

			
			return Response.ok(genericEntity, MediaType.APPLICATION_JSON).build();
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
