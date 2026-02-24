using UnityEngine;

public class Whip : MonoBehaviour
{
    public GameObject attackArea;
    private Animator anim;
    public EnemyHealth enemy;
    public AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame

    public void attacky()
    {
        audioSource.Play();
        attackArea.SetActive(true);
    }
    
    public void endAttacky()
    {
        attackArea.SetActive(false);
        anim.SetBool("attack", false);
        
    }
    
}
