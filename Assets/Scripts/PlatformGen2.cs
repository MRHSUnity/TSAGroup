using UnityEngine;

public class PlatformGen2 : MonoBehaviour
{
    public GameObject platformPrefab;
    public Transform refPoint;
    private GameObject lastPlatform;
    public int lastPlatformPos = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastPlatform = Instantiate(platformPrefab, refPoint.position, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        // Assuming you move platforms to the left over time
        if (lastPlatform != null && lastPlatform.transform.position.x <= -lastPlatformPos )
        {
            // Spawn the new platform further to the right
            Vector3 spawnPos = new Vector3(refPoint.position.x, refPoint.position.y, 0);
            lastPlatform = Instantiate(platformPrefab, spawnPos, Quaternion.identity);
        }

    }
}
