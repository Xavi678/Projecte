package cat.almata.daw.models;

public class Teatre {
	private int ID;
	private String nom;
	private int files;
	private int columnes;
	private int Adre�aID;
	
	
	
	
	
	public Teatre(int iD, String nom, int files, int columnes, int adre�aID) {
		super();
		ID = iD;
		this.nom = nom;
		this.files = files;
		this.columnes = columnes;
		Adre�aID = adre�aID;
	}
	public int getID() {
		return ID;
	}
	public void setID(int iD) {
		ID = iD;
	}
	public String getNom() {
		return nom;
	}
	public void setNom(String nom) {
		this.nom = nom;
	}
	public int getFiles() {
		return files;
	}
	public void setFiles(int files) {
		this.files = files;
	}
	public int getColumnes() {
		return columnes;
	}
	public void setColumnes(int columnes) {
		this.columnes = columnes;
	}
	public int getAdre�aID() {
		return Adre�aID;
	}
	public void setAdre�aID(int adre�aID) {
		Adre�aID = adre�aID;
	}
	
	
	
}
