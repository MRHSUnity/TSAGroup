using UnityEngine;

public class PlatformGen1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject gameObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = transform.position + new Vector3(-0.005f, 0, 0);
    }
}
