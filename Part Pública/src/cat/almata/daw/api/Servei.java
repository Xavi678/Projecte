package cat.almata.daw.api;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
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
import javax.xml.bind.ParseConversionEvent;

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
	
	@Path("/getFuncionsbyTeatre")
	@GET
	@Produces(MediaType.APPLICATION_JSON)
	public Response obtenirFuncionsperTeatre(@QueryParam("id") String id ) {
		try {

			// System.out.println();
			
			List<Funcio> funcions = db.obtenirFuncionsperTeatre(Integer.parseInt(id));

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
			if(db.existeixEmail(client.getEmail())==true) {
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
	
	@Path("/obtenirCompresFiltrades")
	@GET
	@Produces(MediaType.APPLICATION_JSON)
	@Consumes(MediaType.APPLICATION_JSON)
	public Response obtenirCompresFiltrades(@QueryParam("clientID") String clientID, @QueryParam("data") String search, @QueryParam("espectacle") String espectacle, @QueryParam("teatre") String teatre   ) {
		try {

			// System.out.println();
			
			//Boolean inserit= db.insert(client);

			// Token t=new Token(token, new Date());

			/*GenericEntity<Boolean> genericEntity = new GenericEntity<Boolean>(inserit) {
			};*/
			DateFormat format = new SimpleDateFormat("yyyy-MM-dd");
			ArrayList<Compra> compres=db.obtenirCompres(clientID,format.parse(search),espectacle,teatre);
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
	public Response searchEspectacles(@QueryParam("search") String search, @QueryParam("data") String data, @QueryParam("datafi") String datafi) {
		try {

			// System.out.println();
			
			//Boolean inserit= db.insert(client);

			// Token t=new Token(token, new Date());

			/*GenericEntity<Boolean> genericEntity = new GenericEntity<Boolean>(inserit) {
			};*/
			
			//ArrayList<String> localitats= db.obtenirLocalitats();
		
		

			DateFormat format = new SimpleDateFormat("yyyy-MM-dd");
		ArrayList<Espectacle> espectacles= new ArrayList<Espectacle>();
		
		if(data==null || data.isEmpty() && datafi==null || datafi.isEmpty() && !search.isEmpty() && search!=null) {
		
espectacles=	db.filtrarEspectacles(search);
		}else if(search==null || search.isEmpty()   && !data.isEmpty() && data!=null && !datafi.isEmpty() && datafi!=null) {
			
			 espectacles=	db.filtrarEspectacles(format.parse(data),format.parse(datafi));
		}else if(search!=null && !search.isEmpty() && data !=null && !data.isEmpty() && !datafi.isEmpty() && datafi!=null) {
			espectacles=db.filtrarEspectacles(search,format.parse(data),format.parse(datafi));
		}
		
			
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
	public Response searchTeatres(@QueryParam("search") String search, @QueryParam("data") String data,@QueryParam("datafi") String datafi) {
		try {

			// System.out.println();
			
			//Boolean inserit= db.insert(client);

			// Token t=new Token(token, new Date());

			/*GenericEntity<Boolean> genericEntity = new GenericEntity<Boolean>(inserit) {
			};*/
			
			/*ArrayList<String> localitats= db.obtenirLocalitats();
			
			GenericEntity<ArrayList<String>> genericEntity = new GenericEntity<ArrayList<String>>(localitats) {
			};*/
			
			DateFormat format = new SimpleDateFormat("yyyy-MM-dd");
			ArrayList<Teatre> teatres= new ArrayList<Teatre>();
			
			if(data==null || data.isEmpty() && datafi.isEmpty() || datafi==null && !search.isEmpty() && search!=null) {
			
 teatres=	db.filtrarTeatres(search);
			}else if(search==null || search.isEmpty()   && !data.isEmpty() && data!=null && !datafi.isEmpty() && datafi!=null) {
				
				 teatres=	db.filtrarTeatres(format.parse(data),format.parse(datafi));
			}else if(search!=null && !search.isEmpty() && data !=null && !data.isEmpty() && !datafi.isEmpty() && datafi!=null) {
				teatres=db.filtrarTeatres(search,format.parse(data),format.parse(datafi));
			}
			
			GenericEntity<ArrayList<Teatre>> genericEntity = new GenericEntity<ArrayList<Teatre>>(teatres) {
			};
			return Response.ok( genericEntity,MediaType.APPLICATION_JSON).build();
		} catch (Exception e) {
			return Response.status(Response.Status.BAD_REQUEST).build();
		}
	}
	
	
	@Path("/getTeatres")
	@GET
	@Produces(MediaType.APPLICATION_JSON)
	@Consumes(MediaType.APPLICATION_JSON)
	public Response searchTeatres() {
		try {

			// System.out.println();
			
			//Boolean inserit= db.insert(client);

			// Token t=new Token(token, new Date());

			/*GenericEntity<Boolean> genericEntity = new GenericEntity<Boolean>(inserit) {
			};*/
			
			/*ArrayList<String> localitats= db.obtenirLocalitats();
			
			GenericEntity<ArrayList<String>> genericEntity = new GenericEntity<ArrayList<String>>(localitats) {
			};*/
			
			
			ArrayList<Teatre> teatres= new ArrayList<Teatre>();
			ArrayList<Teatre> tmp= new ArrayList<Teatre>();
			teatres = db.obtenirTeatres();
			
			for(int i=0;i<teatres.size() && i<=2 ;i++) {
				tmp.add(teatres.get(i));
			}
			
			GenericEntity<ArrayList<Teatre>> genericEntity = new GenericEntity<ArrayList<Teatre>>(tmp) {
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
