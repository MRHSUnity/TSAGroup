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
        slider.value = vol;
        audioSource.volume = slider.value;
    }
    /**
    public void setAudioVol()
    {
        audioSource.volume = vol;
    }
   **/
}
