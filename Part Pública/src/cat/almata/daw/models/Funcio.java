package cat.almata.daw.models;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;

public class Funcio {
	
	private int ID;
	private int EspectacleID;
	private int teatreID;
	private Date data;
	private String HoraInici;
	private String HoraFi;
	private Teatre teatre;
	private ArrayList<Integer> butaquesOcupades;
	private SimpleDateFormat sdf=null;
	
	public ArrayList<Integer> getButaquesOcupades() {
		return butaquesOcupades;
	}

	public void setButaquesOcupades(ArrayList<Integer> butaquesOcupades) {
		this.butaquesOcupades = butaquesOcupades;
	}

	public Funcio() {
		sdf = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
	}
	
	public Funcio(int iD, int espectacleID, int teatreID, Date data, String horaInici, String horaFi, Teatre teatre) {
		this();
		ID = iD;
		EspectacleID = espectacleID;
		this.teatreID = teatreID;
		this.data = data;
		HoraInici = horaInici;
		HoraFi = horaFi;
		this.teatre=teatre;
	}
	
	
	
	public Teatre getTeatre() {
		return teatre;
	}



	public void setTeatre(Teatre teatre) {
		this.teatre = teatre;
	}



	public int getID() {
		return ID;
	}
	public void setID(int iD) {
		ID = iD;
	}
	public int getEspectacleID() {
		return EspectacleID;
	}
	public void setEspectacleID(int espectacleID) {
		EspectacleID = espectacleID;
	}
	public int getTeatreID() {
		return teatreID;
	}
	public void setTeatreID(int teatreID) {
		this.teatreID = teatreID;
	}
	public String getData() {
		return sdf.format(this.data);
	}
	public void setDataNaixement(String data) {
		try{
			this.data = sdf.parse(data);
		}catch(Exception e) {
			e.printStackTrace();
		}
	}
	public String getHoraInici() {
		return HoraInici;
	}
	public void setHoraInici(String horaInici) {
		HoraInici = horaInici;
	}
	public String getHoraFi() {
		return HoraFi;
	}
	public void setHoraFi(String horaFi) {
		HoraFi = horaFi;
	}
	
	
	

}
