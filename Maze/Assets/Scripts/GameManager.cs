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

    //GameObject player = null;

    public int hp;
    public int coldHp;

    public Vector3 position;
    public Quaternion rotation;

    // Use this for initialization
    void Awake()
    {

        // singleton: only one instance/GameManager, which is used for storing global variables

        if (instance == null)
        {
            Debug.Log("Awake: initial, hp: " + this.hp + ", coldHp: " + this.coldHp);
            // setup initial value
            instance = this;
            instance.hp = 100;
            instance.coldHp = 100;
            instance.position = new Vector3(-123.3f, 0f, 39.65f);
            instance.rotation = Quaternion.Euler(0f,0f,0f);
            DontDestroyOnLoad(instance);

        }
        else if (instance != this)
        {
            // when scnene change happened, by default, it would create a new instance
            // destroy the new instance, keep the old one
            Debug.Log("Awake: destroy object, hp: " + instance.hp + ", this.hp: " + this.hp);
            Destroy(this.gameObject);

        }

        InitGame();
    }

    void InitGame()
    {
        instance.lose = GameObject.Find("lose");
        instance.lose.SetActive(false);

        Debug.Log("InitGame(),hp: " + instance.hp + ",coldHp: " + instance.coldHp);
    }

    public void BreakGame()
    {
        HealthBarControl hpControl = (HealthBarControl)GameObject.FindGameObjectWithTag("HPBar").GetComponent(typeof(HealthBarControl));
        ManaBarControl coldControl = (ManaBarControl)GameObject.FindGameObjectWithTag("ManaBar").GetComponent(typeof(ManaBarControl));

        instance.hp = hpControl.getValue();
        instance.coldHp = coldControl.getValue();

        Debug.Log("BreakGame(),hp: " + hp + ",coldHp: " + coldHp);
        //Debug.Log("check instance: " + hpControl.getValue());

        PlayerController player = (PlayerController)GameObject.Find("Player").GetComponent(typeof(PlayerController));

        instance.position = player.transform.position;
        instance.rotation = player.transform.rotation;

        Debug.Log("cur position: " + player.transform.position);

    }

    public void LoseGame()
    {
        PauseGame();
        if (instance.lose != null)
        {
            instance.lose.SetActive(true);
        }
        else
        {
            Debug.Log("lose is null");
        }
    }

    public void RestartGame()
    {
        Debug.Log("RestartGame()");
        // reset with intial value
        instance.hp = 100;
        instance.coldHp = 100;
        instance.position = new Vector3(-123.3f, 0f, 39.65f);
        instance.rotation = Quaternion.Euler(0f, 0f, 0f);

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
        Debug.Log("PauseGame()");
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Debug.Log("ResumeGame()");
        Time.timeScale = 1;
    }
}
