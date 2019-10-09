using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClick : MonoBehaviour
{

    public void RestartClick()
    {
        Debug.Log("Button Clicked. restart.");
        GameManager.instance.RestartGame();
    }
}