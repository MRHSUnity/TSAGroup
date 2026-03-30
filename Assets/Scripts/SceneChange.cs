using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public String Scene;
    public LayerMask player;
 private void OnTriggerEnter2D(Collider2D other)
 {

         // Load the next scene
         if(player==null)
        {
            SceneManager.LoadScene(Scene);

        }
        if (((1 << other.gameObject.layer) & player) != 0)
        {
                SceneManager.LoadScene(Scene);


        }
    }
}
