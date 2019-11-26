using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMemoryGame : MonoBehaviour
{

    public void OnMouseDown()
    {
        bool ret = checkStatus();

        if(ret == true)
        {
            Debug.Log("Button Clicked. start Memory Game.");
            string goToScene = "MemoryGame";
            GameManager.instance.BreakGame();
            SceneManager.LoadScene(goToScene);
        }
    }

    public bool checkStatus()
    {
        int count = 0;
        for (int i=0; i < 10; i++)
        {   if (GameManager.instance.bonfireStates[i] == true) count++;
        }

        if (count == 10)
            return true;

        return false;
    }
}
