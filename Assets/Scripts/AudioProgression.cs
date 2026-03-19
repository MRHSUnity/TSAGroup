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
    public Boolean transitioning;

    void Start()
    {
        source.resource = Astart;
    }

    // Update is called once per frame
    void Update()
    {
        
        // once Astart ends, set starting to true
        // play Atransition and set transitioning to true
        // once transitioning ends set transitioning to false
        // play Aloop and set to loop 
        // :D

    }
}
