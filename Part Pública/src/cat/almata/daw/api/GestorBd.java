package cat.almata.daw.api;

import java.sql.Connection;

import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Date;
import java.util.List;
import java.util.UUID;

import cat.almata.daw.models.UsuariClient;
import cat.almata.daw.models.Adreca;
import cat.almata.daw.models.Compra;
import cat.almata.daw.models.Espectacle;
import cat.almata.daw.models.Funcio;
import cat.almata.daw.models.Teatre;




public class GestorBd {
	
	private String hostname;
	private String database;
	private String port;
	private String userLogin;
	private String userPasswd;
	private String temps;
	private Connection conn;
	
	public GestorBd(){
		super();
		try {
			Class.forName("com.mysql.jdbc.Driver");
		}catch(ClassNotFoundException ex) {
			System.out.println("Error: unable to load driver class!");
		}
		this.temps="?useUnicode=true&useJDBCCompliantTimezoneShift=true&useLegacyDatetimeCode=false&serverTimezone=UTC&autoReconnect=true&useSSL=false";
	}
	
	public GestorBd(String hostname, String database, String port, String user, String passwd) {
		this(hostname,database,user,passwd);
		this.port=port;
	}


	public GestorBd(String hostname, String database, String user, String passwd) {
		this();
		this.hostname = hostname;
		this.database = database;
		this.userLogin = user;
		this.userPasswd = passwd;
		this.temps="?useUnicode=true&useJDBCCompliantTimezoneShift=true&useLegacyDatetimeCode=false&serverTimezone=UTC&autoReconnect=true&useSSL=false";
		
	}
	

	public String getHostname() {
		return hostname;
	}


	public void setHostname(String hostname) {
		this.hostname = hostname;
	}


	public String getDatabase() {
		return database;
	}


	public void setDatabase(String database) {
		this.database = database;
	}


	public String getPort() {
		return port;
	}


	public void setPort(String port) {
		this.port = port;
	}

	public String getUserLogin() {
		return userLogin;
	}

	public void setUserLogin(String userLogin) {
		this.userLogin = userLogin;
	}

	public String getUserPasswd() {
		return userPasswd;
	}

	public void setUserPasswd(String userPasswd) {
		this.userPasswd = userPasswd;
	}

	public UsuariClient autenticar(String email, String passwd) throws SQLException {
		
		Connection conn = DriverManager.getConnection("jdbc:mysql://"+this.hostname+"/"+this.database+this.temps,this.userLogin,this.userPasswd);
		//ArrayList<Producte> productes=new ArrayList<Producte>();
		String sql="Select * from persones as p,adreces as a where p.AdreçaID=a.ID and p.email=? && p.password=? && p.Discriminator='Client' ";
		
		PreparedStatement prs=conn.prepareStatement(sql);
		prs.setString(1, email);
		prs.setString(2, passwd);
		
		ResultSet rs=prs.executeQuery();
		
		UsuariClient user=null;
		while(rs.next()) {
			//productes.add(new Producte(rs.getInt("id"),rs.getString("nom"),rs.getInt("disponibilitat"),rs.getString("descripcio"),rs.getInt("preu"),rs.getString("propietari"),rs.getString("data")));
			user= new UsuariClient(rs.getString("NIF"),rs.getString("nom"),rs.getInt("edat"),rs.getString("email"),rs.getString("password"),rs.getInt("telefon"),rs.getString("cognoms"),new Date(rs.getTimestamp("dataNaixement").getTime()),rs.getString("Localitat"));
		
			//user=rs.getString("NIF");
			
	
		}
		
		return user;
	}

	public List<Espectacle> obtenirEspectacles() throws SQLException {
		// TODO Auto-generated method stub
		Connection conn = DriverManager.getConnection("jdbc:mysql://"+this.hostname+"/"+this.database+this.temps,this.userLogin,this.userPasswd);
		
		String sql="Select * from espectacles ";
		PreparedStatement prs=conn.prepareStatement(sql);
		
		ResultSet rs=prs.executeQuery();
		
		List<Espectacle> espectacles=new ArrayList<Espectacle>();
		
		while(rs.next()){
			espectacles.add(new Espectacle(rs.getInt("EspectacleID"),rs.getString("titol"),rs.getString("sinopsi"),rs.getString("durada"),rs.getString("cartell"),rs.getString("nifDirector"),rs.getString("nifAutor")));
		}
		
		return espectacles;
	}

	public List<Funcio> obtenirFuncions(int id) throws SQLException {
		// TODO Auto-generated method stub
Connection conn = DriverManager.getConnection("jdbc:mysql://"+this.hostname+"/"+this.database+this.temps,this.userLogin,this.userPasswd);
		
		String sql="Select * from funcions as f,teatres as t where espectacleId=?  and f.teatreID=t.ID;";
		PreparedStatement prs=conn.prepareStatement(sql);
		prs.setInt(1,id);
		ResultSet rs=prs.executeQuery();
		
		List<Funcio> funcions=new ArrayList<Funcio>();
		
		while(rs.next()){
			funcions.add(new Funcio(rs.getInt("ID"),rs.getInt("espectacleID"),rs.getInt("teatreID"),new Date(rs.getTimestamp("data").getTime()),rs.getString("horaInici"),rs.getString("horaFi"), new Teatre(rs.getInt(7),rs.getString("Nom"),rs.getInt("Files"),rs.getInt("Columnes"),rs.getInt("AdreçaID"))));
		}
		
		return funcions;
	}

	public Boolean insert(UsuariClient client) throws SQLException {
		// TODO Auto-generated method stub
		
		
		Connection conn = DriverManager.getConnection("jdbc:mysql://"+this.hostname+"/"+this.database+this.temps,this.userLogin,this.userPasswd);
		
		String sqll="select * from mpiscatalunyas where Nom=?";
		PreparedStatement primer=conn.prepareStatement(sqll);
		primer.setString(1, client.getlocalitat());
		ResultSet resultat=primer.executeQuery();
		
		while(resultat.next()) {
			String sqlA="insert into adreces(Comarca,Localitat,Codipostal) values(?,?,?)";
			PreparedStatement SEGON=conn.prepareStatement(sqlA);
			SEGON.setString(1, resultat.getString("Nomcomarca"));
			SEGON.setString(2, resultat.getString("Nom"));
			SEGON.setInt(3, resultat.getInt("Codi"));
			SEGON.executeUpdate();
		}
		
		
		String sql="INSERT INTO `gestioteatres`.`persones`\r\n" + 
				"(`NIF`,\r\n" + 
				"`nom`,\r\n" + 
				"`edat`,\r\n" + 
				"`AdreçaID`,\r\n" + 
				"`email`,\r\n" + 
				"`password`,\r\n" + 
				"`telefon`,\r\n" + 
				"`dataNaixement`,\r\n" + 
				"`Cognoms`,\r\n" + 
				"`Discriminator`) values(?,?,?,(select MAX(ID) from adreces),?,?,?,?,?,?)";
		PreparedStatement prs=conn.prepareStatement(sql);
		prs.setString(1,client.getNIF());
		prs.setString(2,client.getNom());
		prs.setInt(3,client.getEdat());
		//prs.setInt(4,1);
		prs.setString(4, client.getEmail());
		prs.setString(5, client.getPassword());
		prs.setInt(6, client.getTelefon());
		prs.setTimestamp(7,  new java.sql.Timestamp(client.getData().getTime()));
		prs.setString(8, client.getCognoms());
		prs.setString(9, "Client");
		int rs=prs.executeUpdate();
		
		if(rs==0) {
			
			return false;
		}else {
			return true;
		}
		
		
		
		
		
	}

	public Funcio obtenirFuncio(int id) throws SQLException {
		// TODO Auto-generated method stub
Connection conn = DriverManager.getConnection("jdbc:mysql://"+this.hostname+"/"+this.database+this.temps,this.userLogin,this.userPasswd);
		
		String sql="select * from funcions as f, teatres as t where f.ID=? and f.teatreID=t.ID;";
		PreparedStatement prs=conn.prepareStatement(sql);
		prs.setInt(1,id);
		ResultSet rs=prs.executeQuery();
		
		Funcio funcions=null;
		
		while(rs.next()){
			funcions=new Funcio(rs.getInt("ID"),rs.getInt("espectacleID"),rs.getInt("teatreID"),new Date(rs.getTimestamp("data").getTime()),rs.getString("horaInici"),rs.getString("horaFi"), new Teatre(rs.getInt(7),rs.getString("Nom"),rs.getInt("Files"),rs.getInt("Columnes"),rs.getInt("AdreçaID")));
		}
		
		return funcions;
		
	}

	public void insertCompra(String funcioID, int fila, int columna, String clientID) {
		// TODO Auto-generated method stub
		try {
Connection conn = DriverManager.getConnection("jdbc:mysql://"+this.hostname+"/"+this.database+this.temps,this.userLogin,this.userPasswd);
		
		String sql="insert into compres(funcioID,clientID,fila,columna) values(?,?,?,?)";
		PreparedStatement prs=conn.prepareStatement(sql);
		int idtmp=Integer.valueOf(funcioID);
		prs.setInt(1,idtmp);
		prs.setString(2, clientID);
		prs.setInt(3, fila);
		prs.setInt(4, columna);
		prs.executeUpdate();
		
		}catch(Exception e) {
			e.getMessage();
		}
		
		
	}

	public void obtenirOcupades(Funcio funcions) throws SQLException {
		// TODO Auto-generated method stub
		
Connection conn = DriverManager.getConnection("jdbc:mysql://"+this.hostname+"/"+this.database+this.temps,this.userLogin,this.userPasswd);
		
		String sql="select fila,columna from funcions as f,compres as c where f.ID=c.funcioID and f.ID=?;";
		PreparedStatement prs=conn.prepareStatement(sql);
		prs.setInt(1, funcions.getID());
		ResultSet rs= prs.executeQuery();
		ArrayList<Integer> fc=new ArrayList<Integer>();
		while(rs.next()) {
			int a = Integer.parseInt(rs.getInt("fila") + "" + rs.getInt("columna"));
			fc.add(a);
		}
		
		funcions.setButaquesOcupades(fc);
	}

	public ArrayList<Compra> obtenirCompres(String clientID) throws SQLException {
		// TODO Auto-generated method stub
		
		Connection conn = DriverManager.getConnection("jdbc:mysql://"+this.hostname+"/"+this.database+this.temps,this.userLogin,this.userPasswd);
		//String sql="select * from compres as c, funcions as f where f.ID=c.funcioID and clientID=?";
		String sql="select * from compres as c, funcions as f, teatres as t,espectacles as e where f.ID=c.funcioID and f.teatreID=t.ID and f.espectacleID=e.EspectacleID and c.clientID=?;";
		PreparedStatement prs=conn.prepareStatement(sql);
		prs.setString(1, clientID);
		
		ResultSet rs=prs.executeQuery();
		ArrayList<Compra> llista= new ArrayList<Compra>();
		while(rs.next()) {
			
			//llista.add(new Compra(rs.getInt(1), rs.getInt("funcioID"), rs.getString("clientID"), rs.getInt("fila"),rs.getInt("columna"), rs.getString("Nom"), rs.getString("titol"),new Date(rs.getTimestamp("data").getTime())));
			
			llista.add(new Compra(rs.getInt(1), rs.getInt("funcioID"), rs.getString("clientID"), rs.getInt("fila"),rs.getInt("columna"), new Funcio(rs.getInt("f.ID"),new Date(rs.getTimestamp("data").getTime()), new Teatre(rs.getString("Nom")), new Espectacle(rs.getString("titol")))));
			
			//llista.add(new Funcio(rs.getInt("ID"),rs.getInt("espectacleID"),rs.getInt("teatreID"),new Date(rs.getTimestamp("data").getTime()),rs.getString("horaInici")));
			
		}
		return llista;
	}

	public ArrayList<String> obtenirLocalitats() throws SQLException {
		// TODO Auto-generated method stub
		Connection conn = DriverManager.getConnection("jdbc:mysql://"+this.hostname+"/"+this.database+this.temps,this.userLogin,this.userPasswd);
		String sql="select nom from mpiscatalunyas";
		PreparedStatement prs=conn.prepareStatement(sql);
		ResultSet rs= prs.executeQuery();
		ArrayList<String> localitats= new ArrayList<String>();
		while(rs.next()) {
			localitats.add(rs.getString("Nom"));
		}
		
		return localitats;
		
		
	}

	public boolean existeixEmail(String email) throws SQLException {
		// TODO Auto-generated method stub
		
		// TODO Auto-generated method stub
		Connection conn = DriverManager.getConnection("jdbc:mysql://"+this.hostname+"/"+this.database+this.temps,this.userLogin,this.userPasswd);
		String sql="select email from persones where email=?";
		PreparedStatement prs=conn.prepareStatement(sql);
		prs.setString(1, email);
		ResultSet rs= prs.executeQuery();
		/*ArrayList<String> localitats= new ArrayList<String>();
		while(rs.next()) {
			localitats.add(rs.getString("Nom"));
		}*/
		
		return rs.wasNull();
		
		
	}

	public ArrayList<Teatre> filtrarTeatres(String search) throws SQLException {
		// TODO Auto-generated method stub
		Connection conn = DriverManager.getConnection("jdbc:mysql://"+this.hostname+"/"+this.database+this.temps,this.userLogin,this.userPasswd);
		String sql="select * from teatres as t,adreces as a where t.AdreçaID=a.ID and nom=?";
		
		PreparedStatement prs=conn.prepareStatement(sql);
		prs.setString(1, search);
		ArrayList<Teatre> teatres= new ArrayList<Teatre>();
		ResultSet rs=prs.executeQuery();
		
		while(rs.next()){
			teatres.add(new Teatre(rs.getInt(1), rs.getString("Nom"), rs.getInt("Files"), rs.getInt("Columnes"), rs.getInt("AdreçaID"), new Adreca(rs.getInt("a.ID"), rs.getString("Comarca"), rs.getString("Localitat"), rs.getInt("Codipostal"))));
		}
		
		return teatres;
	}

	public ArrayList<Espectacle> filtrarEspectacles(String search) throws SQLException {
		// TODO Auto-generated method stub
		Connection conn = DriverManager.getConnection("jdbc:mysql://"+this.hostname+"/"+this.database+this.temps,this.userLogin,this.userPasswd);
		String sql="select * from espectacles where titol=?";
		
		PreparedStatement prs=conn.prepareStatement(sql);
		prs.setString(1, search);
		ArrayList<Espectacle> espectacles= new ArrayList<Espectacle>();
		ResultSet rs=prs.executeQuery();
		
		while(rs.next()){
			//teatres.add(new Teatre(rs.getInt(1), rs.getString("Nom"), rs.getInt("Files"), rs.getInt("Columnes"), rs.getInt("AdreçaID")));
			espectacles.add(new Espectacle(rs.getInt("EspectacleID"), rs.getString("titol"), rs.getString("sinopsi"), rs.getString("durada"), rs.getString("cartell"), rs.getString("nifDirector"), rs.getString("nifAutor")));
		}
		
		return espectacles;
	}
	
	}