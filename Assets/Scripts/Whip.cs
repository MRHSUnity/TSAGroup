using UnityEngine;

public class Whip : MonoBehaviour
{
    public GameObject attackArea;
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
 
    }
    public void attacky()
    {
        if (!anim.GetBool("isWalkingLeft"))
        {
            attackArea.transform.localPosition = new Vector3(.255f, attackArea.transform.localPosition.y, attackArea.transform.localPosition.z);
        }
        if (anim.GetBool("isWalkingLeft"))
        {
            attackArea.transform.localPosition = new Vector3(-.275f, attackArea.transform.localPosition.y, attackArea.transform.localPosition.z);
        }
       
        attackArea.SetActive(true);
        

    }
    
    public void endAttacky()
    {
        attackArea.SetActive(false);
        anim.SetBool("attackWhip", false);
        
    }
    
}
