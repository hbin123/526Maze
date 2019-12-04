using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTimer : MonoBehaviour
{
    public bool counting;
    float currentTime;
    PlayerController player;
    FrostEffect frostEffect;
    public int difficulty = 2;

    // Start is called before the first frame update
    void Start()
    {
        counting = true;
        currentTime = 0f;
        player = (PlayerController)GameObject.Find("Player").GetComponent(typeof(PlayerController));
        frostEffect = GameObject.Find("FirstPersonView").GetComponent<FrostEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (counting)
        {
            currentTime += 1 * Time.deltaTime;
            // Debug.Log(currentTime);
            //frostEffect.incrementFrostCount();
            if (currentTime > 5f)
            {
                player.loseColdHP(5 * difficulty);
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

    public void ChangeDifficulty(int difficulty) {
        Debug.Log("Difficulty changed");
        this.difficulty = difficulty;
    }
    /*public void resumeTimer()
    {
        counting = true;
    }*/
}
