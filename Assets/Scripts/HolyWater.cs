using Unity.VisualScripting;
using UnityEngine;

public class HolyWater : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform target;

    public float shootRate;
    [SerializeField] private float shootTimer;
    
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
    private void Update()
    {
        shootTimer += Time.deltaTime;
        
    }
    public void attacky()
    {
        if (shootTimer <=0)
        {
            shootTimer = shootRate;
            Instantiate(projectilePrefab, target.position, Quaternion.identity);   

        }
        // audioSource.Play();
        // needs audio for holy water   
    }
 
    
}

