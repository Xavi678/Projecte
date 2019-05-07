package cat.almata.daw.models;

import java.sql.Time;

public class Espectacle {

	private int EspectacleID;
	private String titol;
	private String sinopsi;
	private String durada;
	private String cartell;
	private String nifDirector;
	private String nifAutor;
	
	public Espectacle(int espectacleID, String titol, String sinopsi, String durada, String cartell, String nifDirector,
			String nifAutor) {
		super();
		EspectacleID = espectacleID;
		this.titol = titol;
		this.sinopsi = sinopsi;
		this.durada = durada;
		this.cartell = cartell;
		this.nifDirector = nifDirector;
		this.nifAutor = nifAutor;
	}

	

	public int getEspectacleID() {
		return EspectacleID;
	}

	public void setEspectacleID(int espectacleID) {
		EspectacleID = espectacleID;
	}

	public String getTitol() {
		return titol;
	}

	public void setTitol(String titol) {
		this.titol = titol;
	}

	public String getSinopsi() {
		return sinopsi;
	}

	public void setSinopsi(String sinopsi) {
		this.sinopsi = sinopsi;
	}

	public String getDurada() {
		return durada;
	}

	public void setDurada(String durada) {
		this.durada = durada;
	}

	public String getCartell() {
		return cartell;
	}

	public void setCartell(String cartell) {
		this.cartell = cartell;
	}

	public String getNifDirector() {
		return nifDirector;
	}

	public void setNifDirector(String nifDirector) {
		this.nifDirector = nifDirector;
	}

	public String getNifAutor() {
		return nifAutor;
	}

	public void setNifAutor(String nifAutor) {
		this.nifAutor = nifAutor;
	}
	
	
	
}
