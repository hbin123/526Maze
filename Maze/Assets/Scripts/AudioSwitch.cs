using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSwitch : MonoBehaviour
{
    //存放音频的数组
    public AudioClip[] audios;
    // Use this for initialization
    void Start()
    {
        //默认播放第二个音频
        this.GetComponent<AudioSource>().clip = audios[1];
        this.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<AudioSource>().clip = audios[0];
        this.GetComponent<AudioSource>().Play();
    }
}