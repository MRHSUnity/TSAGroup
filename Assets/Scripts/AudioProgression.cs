using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioProgression : MonoBehaviour
{
    //audio start, audio transition, audio loop
    public AudioResource Astart;
    public AudioResource Atransition;
    public AudioResource Aloop;
    public AudioSource source;
    public Boolean starting = true;
    public Boolean transitioning = false;
    public Boolean looping = false;

    void Start()
    {
        source.resource = Astart;
        starting = false;
        transitioning = false;
        looping = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        // once Astart ends, set starting to true
        // play Atransition and set transitioning to true
        // once transitioning ends set transitioning to false
        // play Aloop and set to loop 
        // :D

        if (source.isPlaying == false && transitioning == false && looping == false)
        {
            starting = true;
            Debug.Log("starting transition audio");
        }
        if (starting == true)
        {
            source.resource = Atransition;
            transitioning = true;
            starting = false;
            Debug.Log("starting audio recieved");
            source.Play();
            Debug.Log("audio playing");
        }
        if (!source.isPlaying && transitioning == true)
        {
            source.resource = Aloop;
            transitioning = false;
            looping = true;
            source.loop = true;
            source.Play();
        }

    }
}
