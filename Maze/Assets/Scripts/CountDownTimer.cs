using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTimer : MonoBehaviour
{
    public bool counting;
    float currentTime;
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        counting = true;
        currentTime = 0f;
        player = (PlayerController)GameObject.Find("Player").GetComponent(typeof(PlayerController));
    }

    // Update is called once per frame
    void Update()
    {
        if (counting)
        {
            currentTime += 1 * Time.deltaTime;
            Debug.Log(currentTime);
            if(currentTime > 5f)
            {
                player.loseColdHP(10);
                currentTime = 0f;
            }

        }
        
    }
    public void resetTimer()
    {
        currentTime = 0f;
        counting = true;
    }
    public void stopTimer()
    {
        counting = false;
    }
    /*public void resumeTimer()
    {
        counting = true;
    }*/
}
