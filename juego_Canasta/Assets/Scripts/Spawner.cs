using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public List<GameObject> spwanableItemsPrefab;//lista de prefabs a instanciar

    private Vector3 spawnPos;//posicion donde se instancian los objetos

    public Vector2 spawnTimeRange;//tiempo cuando se instanciara un nuevo objeto    x = min  y = max
    private float spawnTime;//numero elejido al azar entre spwanTimeRange

    public int maxSpawnObjectsInSceneLimit;//Contador de objetos en la escena para no crear mas
    private GameObject spawnedObjectsContainer;//Contenedor de los objetos creados

    void Start()
    {
        spawnTime = Mathf.Round(Random.Range(spawnTimeRange.x, spawnTimeRange.y));// inicia el primer timer
        spawnedObjectsContainer = new GameObject("spawnedObjectsContainer");// Crea el cotenedor de objetos
    }

    // Update is called once per frame
    void Update()
    {
        //if gamerunning
        if (GameManager.Instance.GetCurrentGameState() == GameManager.GameState.playing)
        {
            spawnTime -= Time.deltaTime;//resta tiemp al spwantime

            if (spawnTime <= 0.0f && spawnedObjectsContainer.transform.childCount < maxSpawnObjectsInSceneLimit)//cuando spwantime es menor o igual a 0 y si la cantidad de objetos en la escena no supera el limite
            {
                //genero una nueva posicion donde se instancia el proximo objeto entre un valor aleatorio en el largo de la pantalla y la constante del alto
                spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Screen.height, 0));
                //le sumo 1 al y para que aparescan fuera de la pantalla
                spawnPos = new Vector3(spawnPos.x, spawnPos.y + 1, 0);

                //Instance un objeto al azar en la lista spwanableItemsPrefab. Elije de forma aleatoria entre 0 y la cantidad de objetos que hay en la lista
                GameObject spawnedObject = Instantiate(spwanableItemsPrefab[Random.Range(0, spwanableItemsPrefab.Count)], spawnPos, Quaternion.identity, spawnedObjectsContainer.transform);

                //destruye el objeto instanciado despues de x tiempo
                Destroy(spawnedObject, 5.0f);

                // pide un nuevo valor para spwantime y lo redondea
                spawnTime = Mathf.Round(Random.Range(spawnTimeRange.x, spawnTimeRange.y));
            }
        }

    }
}
