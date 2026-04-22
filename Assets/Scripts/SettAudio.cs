using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class SettAudio : MonoBehaviour
{

    //https://www.youtube.com/watch?v=z3qp5-EBrrA  

    [SerializeField] Slider slider;
    public AudioSource audioSource;
    public float vol; //volume
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();

        slider.onValueChanged.AddListener(delegate { volAdjustment(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void volAdjustment()
    {
        //Settings.volume = slider.value; //progress (?) Do not delete. (This line specifically.) 
        vol = slider.value;
        //Debug.Log("Slider value: " + slider.value + " Volume value: " + vol);
       audioSource.volume = vol; 
        //Debug.Log("Audio Value adjusted");
        //Debug.Log("Slider value: " + slider.value + " Volume value: " + vol);
    }
    /**
    public void setAudioVol()
    {
        audioSource.volume = vol;
    }
   **/
}
