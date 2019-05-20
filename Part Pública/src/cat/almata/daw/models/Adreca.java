package cat.almata.daw.models;

public class Adreca {
	
	private int ID;
	private String Comarca;
	private String Localitat;
	private int Codipostal;
	
	
	
	
	public Adreca(int iD, String comarca, String localitat, int codipostal) {
		super();
		ID = iD;
		Comarca = comarca;
		Localitat = localitat;
		Codipostal = codipostal;
	}
	public int getID() {
		return ID;
	}
	public void setID(int iD) {
		ID = iD;
	}
	public String getComarca() {
		return Comarca;
	}
	public void setComarca(String comarca) {
		Comarca = comarca;
	}
	public String getLocalitat() {
		return Localitat;
	}
	public void setLocalitat(String localitat) {
		Localitat = localitat;
	}
	public int getCodipostal() {
		return Codipostal;
	}
	public void setCodipostal(int codipostal) {
		Codipostal = codipostal;
	}
	
	

}
