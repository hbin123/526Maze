using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = GetComponent<Button>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        btn.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick() {
        playerController.triggerAttack();
    }
}
