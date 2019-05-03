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
import java.util.UUID;




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

	public Client autenticar(String email, String passwd) throws SQLException {
		
		Connection conn = DriverManager.getConnection("jdbc:mysql://"+this.hostname+"/"+this.database+this.temps,this.userLogin,this.userPasswd);
		//ArrayList<Producte> productes=new ArrayList<Producte>();
		String sql="Select * from persones where email=? && password=? && Discriminator='Client' ";
		
		PreparedStatement prs=conn.prepareStatement(sql);
		prs.setString(1, email);
		prs.setString(2, passwd);
		
		ResultSet rs=prs.executeQuery();
		
		Client user=null;
		while(rs.next()) {
			//productes.add(new Producte(rs.getInt("id"),rs.getString("nom"),rs.getInt("disponibilitat"),rs.getString("descripcio"),rs.getInt("preu"),rs.getString("propietari"),rs.getString("data")));
			user= new Client(rs.getString("NIF"),rs.getString("nom"),rs.getInt("edat"),rs.getString("email"),rs.getString("password"),rs.getDate("dataNaixement"),rs.getInt("telefon"));
			
			
	
		}
		
		return user;
	}
	
	}