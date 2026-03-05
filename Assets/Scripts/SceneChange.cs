using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public String Scene;
 private void OnTriggerEnter2D(Collider2D other)
 {

         // Load the next scene
         SceneManager.LoadScene(Scene);

 }
}
