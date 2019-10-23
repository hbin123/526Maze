using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarControl : MonoBehaviour
{
    [Range(0, 1)]
    public float value;

    private Image bar;
    private const float MAXHP = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
