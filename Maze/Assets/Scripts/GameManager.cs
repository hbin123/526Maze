using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum GameState
{
    RUN,
    LOSE,
    WIN
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public static GameState MyState;

    public GameObject win;
    public GameObject lose;

    GameObject player = null;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        //else if (instance != this)
        //    Destroy(gameObject);
        //DontDestroyOnLoad(gameObject);

        InitGame();
    }

    void InitGame()
    {
        lose = GameObject.Find("lose");
        lose.SetActive(false);
        //MyState = GameState.RUN;
    }

    public void LoseGame()
    {
        PauseGame();
        if (lose != null)
        {
            lose.SetActive(true);
        }
        else
        {
            Debug.Log("lose is null");
        }
    }

    public void RestartGame()
    {
        InitGame();
        ResumeGame();
        SceneManager.LoadScene("SampleScene");
       
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
