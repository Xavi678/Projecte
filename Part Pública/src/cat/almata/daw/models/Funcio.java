package cat.almata.daw.models;

import java.util.Date;

public class Funcio {
	
	private int ID;
	private int EspectacleID;
	private int teatreID;
	private Date data;
	private String HoraInici;
	private String HoraFi;
	
	
	
	public Funcio(int iD, int espectacleID, int teatreID, Date data, String horaInici, String horaFi) {
		super();
		ID = iD;
		EspectacleID = espectacleID;
		this.teatreID = teatreID;
		this.data = data;
		HoraInici = horaInici;
		HoraFi = horaFi;
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
	public Date getData() {
		return data;
	}
	public void setData(Date data) {
		this.data = data;
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
