using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioProgression : MonoBehaviour
{
    //audio start, audio transition, audio loop
    public AudioClip Astart;
    public AudioClip Atransition;
    public AudioClip Aloop;
    public AudioSource source;
    

    // Given a start, transition, and looping audio clips, will play each in order before ending on the final clip and repeating it indefinetaly.


    void Start()
    {
        source = GetComponent<AudioSource>();

        StartCoroutine(Playmoosic());
        /* source.clip = Astart;
         source.loop = false;
         starting = false;
         transitioning = false;
         looping = true;*/
    }

    IEnumerator Playmoosic()
    {
        source.clip = Astart;
        source.loop = false; // Ensure it does not loop
        source.Play();

        // 2. Wait until the introductory music finishes playing
        // yield return new WaitWhile(() => audioSource.isPlaying); // An alternative approach
        yield return new WaitForSeconds(Astart.length);

        source.clip = Atransition;
        source.loop = false;
        source.Play();

        yield return new WaitForSeconds(Atransition.length);
        // 3. Play the looping music and set it to loop indefinitely
        source.clip = Aloop;
        source.loop = true; // Set the loop flag to true
        source.Play(); // Start playing the second clip
    }

    // Update is called once per frame
    /*void Update() 
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
            source.clip = Atransition;
            transitioning = true;
            starting = false;
            Debug.Log("starting audio recieved");
            source.Play();
            Debug.Log("audio playing");
        }
        if (!source.isPlaying && transitioning == true)
        {
            source.clip = Aloop;
            transitioning = false;
            looping = true;
            source.loop = true;
            source.Play();
        }

    }*/
}
