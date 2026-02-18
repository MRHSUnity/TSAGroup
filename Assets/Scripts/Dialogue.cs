using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using NUnit.Framework.Internal.Commands;
using System;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpd;
    private int index;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        index = 0;
        //StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }
    public void StartDialogue()
    {
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());
        Debug.Log(lines[index]);

    }
    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpd);
        }
    }
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine (TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public void Stop()
    {
        gameObject.SetActive(false);
    }
    
}
