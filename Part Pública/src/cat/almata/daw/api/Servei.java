package cat.almata.daw.api;

import javax.ws.rs.GET;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.QueryParam;
import javax.ws.rs.core.GenericEntity;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;



@Path("/Servei")
public class Servei {
	
	GestorBd db = new GestorBd(Constant.dbserver, Constant.database, Constant.user, Constant.password);
	
	@Path("/Autenticar")
	@GET
	@Produces(MediaType.APPLICATION_JSON)
	public Response Autenticar(@QueryParam("email") String email, @QueryParam("password") String passwd) {
		try {

			// System.out.println();

			Client client = db.autenticar(email, passwd);

			// Token t=new Token(token, new Date());

			GenericEntity<Client> genericEntity = new GenericEntity<Client>(client) {
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
