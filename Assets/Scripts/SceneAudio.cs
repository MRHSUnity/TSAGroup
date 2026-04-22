using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class SceneAudio : MonoBehaviour
{
    public GameObject settAudio; // Asks for variable assignment in inspector. Doubt it will work as the script its looking for asks for an audio slider (which doesnt exist in this scene)
     public AudioSource theAudio;

    void Start()
    {
        SettAudio script = settAudio.GetComponent<SettAudio>();
        theAudio.volume = script.vol;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
