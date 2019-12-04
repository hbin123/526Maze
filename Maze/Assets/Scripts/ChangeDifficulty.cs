using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeDifficulty : MonoBehaviour
{
    public int difficulty;
    public Button btn;

    public void buttonEvent(){
        GameObject.Find("Player").GetComponent<CountDownTimer>().ChangeDifficulty(difficulty);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
