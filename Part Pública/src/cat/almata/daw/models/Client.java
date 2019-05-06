package cat.almata.daw.models;

import java.util.Date;

public class Client {

	public Client(String nIF, String nom, int edat, String email, String password) {
		super();
		NIF = nIF;
		this.nom = nom;
		this.edat = edat;
		this.email = email;
		this.password = password;
	}
	private String NIF;
	private String nom;
	private int edat;
	private String email;
	private String password;
	private Date dataNaixement;
	private int telefon;
	
	public Client(String nIF, String nom, int edat, String email, String password, Date dataNaixement, int telefon) {
		super();
		NIF = nIF;
		this.nom = nom;
		this.edat = edat;
		this.email = email;
		this.password = password;
		this.dataNaixement = dataNaixement;
		this.telefon = telefon;
	}
	public Date getDataNaixement() {
		return dataNaixement;
	}
	public void setDataNaixement(Date dataNaixement) {
		this.dataNaixement = dataNaixement;
	}
	public int getTelefon() {
		return telefon;
	}
	public void setTelefon(int telefon) {
		this.telefon = telefon;
	}
	
	
	public String getNIF() {
		return NIF;
	}
	public void setNIF(String nIF) {
		NIF = nIF;
	}
	public String getNom() {
		return nom;
	}
	public void setNom(String nom) {
		this.nom = nom;
	}
	public int getEdat() {
		return edat;
	}
	public void setEdat(int edat) {
		this.edat = edat;
	}
	public String getEmail() {
		return email;
	}
	public void setEmail(String email) {
		this.email = email;
	}
	public String getPassword() {
		return password;
	}
	public void setPassword(String password) {
		this.password = password;
	}
	
	
	
	
}
