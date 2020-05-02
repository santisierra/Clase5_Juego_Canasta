using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Botones : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame (){

        SceneManager.LoadScene(1);

    }

    public void ScoreTable (){

        SceneManager.LoadScene(2);
    }

    public void Quit(){

        Debug.Log("Se cierra el juego");
        Application.Quit();
    }
}
