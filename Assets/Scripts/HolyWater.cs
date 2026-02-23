using Unity.VisualScripting;
using UnityEngine;

public class HolyWater : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform target;

    [SerializeField]private float shootRate;
    private float shootTimer;
    
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
        // audioSource.Play();
        // needs audio for holy water   
        Instantiate(projectilePrefab, target.position, Quaternion.identity);   
    }
    
    public void endAttacky()
    {
        attackArea.SetActive(false);
        anim.SetBool("attack", false);
        
    }
    
}

