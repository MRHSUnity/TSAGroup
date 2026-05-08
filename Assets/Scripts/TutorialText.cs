using UnityEngine;

public class TutorialText : MonoBehaviour
{
    public GameObject textToHide;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered the trigger has the "Player" tag
        if (other.CompareTag("Player"))
        {
            if (textToHide != null)
            {
                // Disables the object so it disappears from view
                textToHide.SetActive(false);
            }
        }
    }
}

// I hate to tell you but this code is very much straight from google ai <3 
// gods ai is gross
