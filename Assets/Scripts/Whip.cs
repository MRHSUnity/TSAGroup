using UnityEngine;

public class Whip : MonoBehaviour
{
    public GameObject attackArea;
    private Animator anim;
    public EnemyHealth enemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!anim.GetBool("isWalkingLeft") && !anim.GetBool("attackWhip"))
        {
            attackArea.transform.localPosition = new Vector3(.255f, attackArea.transform.localPosition.y, attackArea.transform.localPosition.z);
        }
        if (anim.GetBool("isWalkingLeft") && !anim.GetBool("attackWhip"))
        {
            attackArea.transform.localPosition = new Vector3(-.275f, attackArea.transform.localPosition.y, attackArea.transform.localPosition.z);
        }
    }
    public void attacky()
    {
        
       
        attackArea.SetActive(true);

    }
    
    public void endAttacky()
    {
        attackArea.SetActive(false);
        anim.SetBool("attackWhip", false);
        
    }
    
}
