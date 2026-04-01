using System.Collections.Generic;
using UnityEngine;

public class OrbitProjectiles2D : MonoBehaviour
{
    [Header("Debug Info")]
    public int projectilesLeft => projectiles.Count;
    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public int count = 6;
    public float radius = 2f;
    public float rotationSpeed = 100f;
    [Header("Launch Settings")]
    public Transform player;
    public float launchInterval = 1.5f;

    [Header("Refill Settings")]
    public float refillDelay = 2f;

    // List of active projectiles
    private List<OrbitProjectile2D> projectiles = new List<OrbitProjectile2D>();
    private float angleOffset = 0f;
    private bool refilling = false;

    // PUBLIC read-only variable to show in Inspector
    

    void Start()
    {
        SpawnRing();
        InvokeRepeating(nameof(LaunchProjectile), 2f, launchInterval);
    }

    void Update()
    {
        // Remove any destroyed projectiles from the list
        projectiles.RemoveAll(p => p == null);

        if (projectiles.Count == 0) return;

        angleOffset += rotationSpeed * Mathf.Deg2Rad * Time.deltaTime;

        for (int i = 0; i < projectiles.Count; i++)
        {
            float angle = (i * Mathf.PI * 2 / projectiles.Count) + angleOffset;
            Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            projectiles[i].transform.position = (Vector2)transform.position + offset;
        }
    }

    void LaunchProjectile()
    {
        if (projectiles.Count == 0)
        {
            if (!refilling)
            {
                refilling = true;
                Invoke(nameof(SpawnRing), refillDelay);
            }
            return;
        }

        int index = Random.Range(0, projectiles.Count);
        OrbitProjectile2D proj = projectiles[index];

        proj.Launch(player);
        projectiles.RemoveAt(index);
    }

    void SpawnRing()
    {
        projectiles.Clear();

        for (int i = 0; i < count; i++)
        {
            GameObject proj = Instantiate(projectilePrefab, transform);
            projectiles.Add(proj.GetComponent<OrbitProjectile2D>());
        }

        refilling = false;
    }
}