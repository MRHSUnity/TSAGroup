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
    public Boolean starting;
    public Boolean transitioning;
    public Boolean looping = false;

    void Start()
    {
        source.resource = Astart;
        starting = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        // once Astart ends, set starting to true
        // play Atransition and set transitioning to true
        // once transitioning ends set transitioning to false
        // play Aloop and set to loop 
        // :D

        if (!source.isPlaying && transitioning == false && looping == false)
        {
            starting = true;
        }
        if (starting)
        {
            source.resource = Atransition;
            transitioning = true;
            starting = false;
        }
        if (!source.isPlaying && transitioning == true)
        {
            source.resource = Aloop;
            transitioning = false;
            looping = true;
            source.loop = true;
        }

    }
}
