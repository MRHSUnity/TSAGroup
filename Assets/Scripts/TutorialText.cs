using UnityEngine;
using System;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{

    public GameObject currentText;
    public BoxCollider2D trigger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        currentText.SetActive(false);
    }


}
