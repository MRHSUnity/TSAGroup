using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float input;
    private GameObject attackArea;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
    public void enemyAttack()
    {
        if (input > 0)
        {
            attackArea.transform.localPosition = new Vector3(.255f, attackArea.transform.localPosition.y, attackArea.transform.localPosition.z);
        }
        if (input<0)
        {
            attackArea.transform.localPosition = new Vector3(-.275f, attackArea.transform.localPosition.y, attackArea.transform.localPosition.z);
        }
       
        attackArea.SetActive(true);
        
        
    }
    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
    }
}
