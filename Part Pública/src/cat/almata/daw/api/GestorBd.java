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
		String sql="Select * from persones where email=? && password=? && Discriminator='Client' ";
		
		PreparedStatement prs=conn.prepareStatement(sql);
		prs.setString(1, email);
		prs.setString(2, passwd);
		
		ResultSet rs=prs.executeQuery();
		
		UsuariClient user=null;
		while(rs.next()) {
			//productes.add(new Producte(rs.getInt("id"),rs.getString("nom"),rs.getInt("disponibilitat"),rs.getString("descripcio"),rs.getInt("preu"),rs.getString("propietari"),rs.getString("data")));
			user= new UsuariClient(rs.getString("NIF"),rs.getString("nom"),rs.getInt("edat"),rs.getString("email"),rs.getString("password"),rs.getInt("telefon"),rs.getString("cognoms"));
			
			
	
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
			funcions.add(new Funcio(rs.getInt("ID"),rs.getInt("espectacleID"),rs.getInt("teatreID"),rs.getDate("data"),rs.getString("horaInici"),rs.getString("horaFi"), new Teatre(rs.getInt(7),rs.getString("Nom"),rs.getInt("Files"),rs.getInt("Columnes"),rs.getInt("AdreçaID"))));
		}
		
		return funcions;
	}

	public Boolean insert(UsuariClient client) throws SQLException {
		// TODO Auto-generated method stub
		Connection conn = DriverManager.getConnection("jdbc:mysql://"+this.hostname+"/"+this.database+this.temps,this.userLogin,this.userPasswd);
		
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
				"`Discriminator`) values(?,?,?,?,?,?,?,'2012-12-12',?,?)";
		PreparedStatement prs=conn.prepareStatement(sql);
		prs.setString(1,client.getNIF());
		prs.setString(2,client.getNom());
		prs.setInt(3,client.getEdat());
		prs.setInt(4,1);
		prs.setString(5, client.getEmail());
		prs.setString(6, client.getPassword());
		prs.setInt(7, client.getTelefon());
		//prs.setDate(8,  "2012-12-12");
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
			funcions=new Funcio(rs.getInt("ID"),rs.getInt("espectacleID"),rs.getInt("teatreID"),rs.getDate("data"),rs.getString("horaInici"),rs.getString("horaFi"), new Teatre(rs.getInt(7),rs.getString("Nom"),rs.getInt("Files"),rs.getInt("Columnes"),rs.getInt("AdreçaID")));
		}
		
		return funcions;
		
	}
	
	}