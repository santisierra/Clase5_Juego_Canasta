using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

/*
CLASE PARA MANEJAR UN SISTEMA DE RANKING.
AGREGAR EL SCRIPT A UN GAMEOBJECT EN LA PRIMER ESCENA, SE VA A MANTENER EN TODAS LAS ESCENAS
*/

public class ManejadorRanking : MonoBehaviour
{

    private int puntos;
    private string nombre;
    private string timeString;
    private string winDate;

    private int RankingPosition = 0;

    private string rutaArchivoRanking;

    //Para hacer el singleton, info: https://es.wikipedia.org/wiki/Singleton
    //Singleton inicio
    public static ManejadorRanking manejador;

    void Awake()
    {
        if (manejador == null)
        {
            DontDestroyOnLoad(gameObject);
            manejador = this;
        }
        else
        {
            if (manejador != this)
            {
                Destroy(gameObject);
            }
        }
        //Esto no es parte del singleton
        this.rutaArchivoRanking = Application.persistentDataPath + "/ranking.dat";
        Debug.Log("rutaArchivoRanking " + rutaArchivoRanking);
    }
    //Singleton fin

    //Llamar a este método cuando el jugador gana
    public void RecibirPuntos(int puntos)
    {
        this.puntos = puntos;
        Debug.Log("RecibirPuntos " + this.puntos);
    }

    //Llamar a este método cuando el jugador ingresa su nombre
    public void RecibirNombre(string nombre)
    {
        this.nombre = nombre;
        Debug.Log("RecibirNombre " + this.nombre);
    }
    public void SetRankingTimeString(string atime)
    {
        this.timeString = atime;
    }
    public void SetWinGameDate(string atime)
    {
        this.winDate = atime;
    }

    //Llamar a esta función luego de haber seteado el nombre y los puntos
    public void GuardarRanking()
    {
        //Debug.Log ("El ganador es: "+this.nombre + " con " +this.puntos+ " puntos");

        //Creo el objeto jugador, con los datos del jugador actual
        Jugador jugador = new Jugador();
        jugador.nombre = this.nombre;
        jugador.puntos = this.puntos;
        jugador.timeString = this.timeString;
        jugador.winDate = this.winDate;
        //Defino la variable ranking, le voy a asignar un valor en el if o en el else
        Ranking ranking;

        //Formateador para poder guardar un obejeto en un archivo
        BinaryFormatter bf = new BinaryFormatter();

        //Verifico si existe el archivo con los datos del ranking
        if (File.Exists(this.rutaArchivoRanking))
        {
            //Si existe el archivo...
            //Leo el archivo
            FileStream file = File.Open(this.rutaArchivoRanking, FileMode.Open);
            //El contenido del archivo lo cargo en la variable ranking (la clase ranking tiene una lista de jugadores)
            ranking = (Ranking)bf.Deserialize(file);
            //Cierro el archivo
            file.Close();
        }
        else
        {
            //Si el archivo no existe...
            //Inicializo la variable ranking
            ranking = new Ranking();
            //Inicializo la lista de jugadores
            ranking.jugadores = new List<Jugador>();
        }
        //Agrego el jugador a la lista del ranking (ya sea del archivo o del ranking inicial)
       // ranking.jugadores.Add(jugador);



        if (ranking.jugadores.Count == 0)
        {
            Debug.Log("entro la primera vez");
            ranking.jugadores.Add(jugador);
        }
        else
        {
            for (int i = 0; i < ranking.jugadores.Count; i++)
            {
                
                //si el nombre del jugador no existe se agrega a la lista
                if (CompareStrings(jugador.nombre,ranking.jugadores[i].nombre))
                {
                    ranking.jugadores[i] = jugador;;
                }
                else
                {
                    //agrega al nuevo jugador si no esta en la lista
                    ranking.jugadores.Add(jugador);
                    //si se agrega al jugador se hace un break por que sino vuelve a entra al loop y agregar otro jugador
                    break;
                }
                
            } 
        }
        //Guardo el ranking en el archivo
        FileStream fileToWrite = File.Create(this.rutaArchivoRanking);
        bf.Serialize(fileToWrite, ranking);
        fileToWrite.Close();
    }

    //compara el nombre de usuario
    //stackoverflow.com/questions/54058724/comparing-two-ui-text-unity
    public bool CompareStrings(string string1, string string2)
    {
        //Los nombres tienen diferente largo
        if (string1.Length != string2.Length)
            return false;

        //Los nombres tienen mismo largo
        for (int i = 0; i < string1.Length; i++)
        {
            char c1 = string1[i];
            char c2 = string2[i];
            if (c1 != c2)
            {
                Debug.Log(string1 + "---" + string2 + "  son diferentes");
                return false;
            }
                
        }

        //Es el mismo nombre
        Debug.Log(string1+"---"+string2+ "  es el mismo nombre");
        return true;
    }

    
    //Llamar a este método para mostrar el ranking
    public void MostrarRanking()
    {
        //Me aseguro de que existe el archivo del ranking
        if (File.Exists(this.rutaArchivoRanking))
        {

            //Leo el archivo y cargo los datos en el objeto ranking (notar que es identico a GuardarRanking() si existe el archivo)
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(this.rutaArchivoRanking, FileMode.Open);
            Ranking ranking = (Ranking)bf.Deserialize(file);
            file.Close();

            //Para poder ordenar la lista de jugadores
            //De forma ascendente
          //  List<Jugador> rankingOrdenado = ranking.jugadores.OrderBy(o => o.puntos).ToList();
            //De forma descendente
            List<Jugador> rankingOrdenado = ranking.jugadores.OrderBy(o=>o.puntos*-1).ToList();

            //Obtengo el componente donde quiero mostrar el ranking (un text dentro un canvas)
            //Asegurarse que el componente texto tengo Horizontal Overflow y Vertical Overflow en Overflow
            Text contenedorRanking = GameObject.Find("Ranking").GetComponent<Text>();
            RankingPosition = 0;

            foreach (Jugador p in rankingOrdenado)
            {
                //Al componente texto le agrego el jugador, los puntos y salto de línea (\n)
                RankingPosition++;
                contenedorRanking.text += RankingPosition + "--" + p.nombre + "--" + p.puntos + "--"+ p.winDate+"\n";
            }


            GameObject contenedorRanking1 = GameObject.Find("Ranking").gameObject;

        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       
        if (scene.name == "Puntajes")
        {
            Debug.Log("escena cargada " + scene.name);
            //Cuando se carga la escena del listado del ranking, llamo al método MostrarRanking
            this.MostrarRanking();
        }
    }
}