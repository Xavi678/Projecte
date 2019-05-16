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
	private String nomTeatre;
	private String titolEspectacle;
	private Date dataFuncio;
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
		this.nomTeatre = nomTeatre;
		this.titolEspectacle = titolEspectacle;
	this.dataFuncio=data;
	}
	public String getNomTeatre() {
		return nomTeatre;
	}
	public void setNomTeatre(String nomTeatre) {
		this.nomTeatre = nomTeatre;
	}
	public String getTitolEspectacle() {
		return titolEspectacle;
	}
	public void setTitolEspectacle(String titolEspectacle) {
		this.titolEspectacle = titolEspectacle;
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
	@XmlElement(name="data")
	public String getData() {
		return sdf.format(this.dataFuncio);
	}
	public void setData(String data) {
		try{
			this.dataFuncio = sdf.parse(data);
		}catch(Exception e) {
			e.printStackTrace();
		}
	}
	
	

}
