using UnityEngine;

public class DialogueSensor : MonoBehaviour
{
    public LayerMask player;
    public GameObject dialogueBox;
    public Dialogue dialogue;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(((1<<collision.gameObject.layer) & player)!=0)
        {
            dialogueBox.SetActive(true);
            dialogue.StartDialogue();

        }
    }
}
