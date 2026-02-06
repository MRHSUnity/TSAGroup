using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogueBox;
    private bool playerInRange = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")){
            playerInRange = false;
            dialogueBox.Stop();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            dialogueBox.StartDialogue();
        }

    }
}
