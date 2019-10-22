using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonFireTrigger : MonoBehaviour
{
    public bool isTriggered = false;
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = (PlayerController) GameObject.Find("Player").GetComponent(typeof(PlayerController));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (!isTriggered)
        {// if the bonfire is not triggered, trigger it first
            triggerBonFire();
        }
        //reset hp
        player.resetHP();

    }
    private void OnTriggerExit(Collider other)
    {
        /*Debug.Log("Exit");*/
        //Once exit, we can start a timer for player to count down and lose hp
    }
    public void triggerBonFire()
    {
        GameObject child = gameObject.transform.GetChild(0).gameObject;
        child.SetActive(true); 
        isTriggered = true;
    }
}
