package cat.almata.daw.models;

public class Teatre {
	private int ID;
	private String nom;
	private int files;
	private int columnes;
	private int AdrešaID;
	
	
	
	
	
	public Teatre(int iD, String nom, int files, int columnes, int adrešaID) {
		super();
		ID = iD;
		this.nom = nom;
		this.files = files;
		this.columnes = columnes;
		AdrešaID = adrešaID;
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
	public int getAdrešaID() {
		return AdrešaID;
	}
	public void setAdrešaID(int adrešaID) {
		AdrešaID = adrešaID;
	}
	
	
	
}
