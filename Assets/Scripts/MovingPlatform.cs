using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Array to hold all target points (can use just two for simple back-and-forth)
    public Transform[] waypoints;
    public float moveSpeed = 3f;
    private int currentWaypointIndex = 0;
    private Vector3 nextPosition;

    void Start()
    {
        // Set the initial target position to the first waypoint
        if (waypoints.Length > 0)
        {
            nextPosition = waypoints[currentWaypointIndex].position;
        }
    }

    void Update()
    {
        // Move the platform towards the next position
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);

        // Check if the platform has reached the target position
        if (Vector3.Distance(transform.position, nextPosition) < 0.1f)
        {
            // Move to the next waypoint in the array, looping if necessary
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            nextPosition = waypoints[currentWaypointIndex].position;
        }
    }

    // Optional: Draw lines in the editor to visualize the path
    void OnDrawGizmos()
    {
        if (waypoints != null && waypoints.Length > 0)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < waypoints.Length; i++)
            {
                if (waypoints[i] != null)
                {
                    Gizmos.DrawSphere(waypoints[i].position, 0.2f);
                    if (i + 1 < waypoints.Length && waypoints[i + 1] != null)
                    {
                        Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                    }
                }
            }
        }
    }
    // Add these methods to the MovingPlatform script
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Make the player a child of the platform
            other.transform.parent = this.transform;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Detach the player when they leave the platform
            other.transform.parent = null;
        }
    }

}
