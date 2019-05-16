package cat.almata.daw.models;

public class Compra {
	private int ID;
	private int funcioID;
	private String clientID;
	private int fila;
	private int columna;
	
	public Compra() {
		
	}
	public Compra(int iD, int funcioID, String clientID, int fila, int columna) {
		super();
		ID = iD;
		this.funcioID = funcioID;
		this.clientID = clientID;
		this.fila = fila;
		this.columna = columna;
	}
	public int getID() {
		return ID;
	}
	public void setID(int iD) {
		ID = iD;
	}
	public int getFuncioID() {
		return funcioID;
	}
	public void setFuncioID(int funcioID) {
		this.funcioID = funcioID;
	}
	public String getClientID() {
		return clientID;
	}
	public void setClientID(String clientID) {
		this.clientID = clientID;
	}
	public int getFila() {
		return fila;
	}
	public void setFila(int fila) {
		this.fila = fila;
	}
	public int getColumna() {
		return columna;
	}
	public void setColumna(int columna) {
		this.columna = columna;
	}
	
	

}
