package cat.almata.daw.models;

import java.text.SimpleDateFormat;
import java.util.Date;

import javax.xml.bind.annotation.XmlElement;



public class Compra {
	private int ID;
	private int funcioID;
	private String clientID;
	private int fila;
	private int columna;
	private UsuariClient client;
	private Funcio funcio;
	private SimpleDateFormat sdf=null;
	
	public Compra() {
		sdf = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
	}
	
	
	public Compra(int iD, int funcioID, String clientID, int fila, int columna, String nomTeatre,
			String titolEspectacle,Date data) {
		this();
		ID = iD;
		this.funcioID = funcioID;
		this.clientID = clientID;
		this.fila = fila;
		this.columna = columna;
		
	}
	
	
	
	public Compra(int iD, int funcioID, String clientID, int fila, int columna, Funcio funcio) {
		super();
		ID = iD;
		this.funcioID = funcioID;
		this.clientID = clientID;
		this.fila = fila;
		this.columna = columna;
		
		this.funcio = funcio;
	}


	public Compra(int iD, int funcioID, String clientID, int fila, int columna) {
		this();
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


	public UsuariClient getClient() {
		return client;
	}


	public void setClient(UsuariClient client) {
		this.client = client;
	}


	public Funcio getFuncio() {
		return funcio;
	}


	public void setFuncio(Funcio funcio) {
		this.funcio = funcio;
	}
	
	
	

}
