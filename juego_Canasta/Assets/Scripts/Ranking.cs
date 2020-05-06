using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Ranking {

	public List<Jugador> jugadores;
}

[System.Serializable]
public class Jugador{
	public string nombre;
	public int puntos;
    public string timeString;
    public string winDate;
}
