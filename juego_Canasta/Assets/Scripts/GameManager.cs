using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public GameObject player;


    public float gameSessionTime = 30;
    public enum GameState
    {
        mainmenu,
        playing,
        pause,
        finish,
        ranking
    }
    public GameState currentGameState = GameState.mainmenu;


    [Header("----Canvas----")]
    public GameObject mainMenuCanvas;


    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        if (player == null)
        {
            //   player = FindObjectOfType<Player>().gameObject;
        }
        if (mainMenuCanvas != null) // solo llama al mainmenucanvas si esta asignado en el inspector
        {
            //Debug.Log(mainMenuCanvas.transform.position.x);
        }


        if (SceneManager.GetActiveScene().name == "Menu")
        {
            SetGameState(GameState.mainmenu);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (currentGameState == GameState.playing)
        {
            gameSessionTime -= Time.deltaTime;
            if (gameSessionTime <= 0)
            {
                SetGameState(GameState.finish);
            }
        }
    }

    public void SetGameState(GameState aState)
    {
        if (aState == GameState.playing)
        {

        }
        else if (aState == GameState.finish)
        {
            //stop game show menu ranking
        }
        currentGameState = aState;
    }

    public GameState GetCurrentGameState()
    {
        return currentGameState;
    }
}
