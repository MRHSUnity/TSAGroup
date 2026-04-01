using UnityEngine;

public class BossMovement2D : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    [Header("Normal Movement")]
    public float moveSpeed = 2f;
    public float roamRadius = 5f;

    [Header("Swoop Settings")]
    public float swoopDuration = 1.5f;
    public float swoopCurveHeight = 3f;
    public float timeBetweenSwoops = 6f;

    [Header("Confiner Settings")]
    public Vector2 minBounds; // bottom-left corner of arena
    public Vector2 maxBounds; // top-right corner of arena

    private Vector2 targetPos;
    private bool swooping = false;

    // Bezier points
    private Vector2 startPoint;
    private Vector2 controlPoint;
    private Vector2 endPoint;

    private float swoopTimer = 0f;

    void Start()
    {
        PickNewPos();
        InvokeRepeating(nameof(StartSwoop), 3f, timeBetweenSwoops);
    }

    void Update()
    {
        if (swooping)
        {
            HandleSwoop();
        }
        else
        {
            HandleMovement();
        }

        // Clamp position inside arena
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
        pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);
        transform.position = pos;
    }

    void HandleMovement()
    {
        transform.position = Vector2.Lerp(
            transform.position,
            targetPos,
            moveSpeed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, targetPos) < 0.5f)
        {
            PickNewPos();
        }
    }

    void PickNewPos()
    {
        targetPos = Random.insideUnitCircle * roamRadius + (Vector2)transform.position;

        // Make sure target is inside arena
        targetPos.x = Mathf.Clamp(targetPos.x, minBounds.x, maxBounds.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minBounds.y, maxBounds.y);
    }

    void StartSwoop()
    {
        if (player == null) return;

        swooping = true;
        swoopTimer = 0f;

        startPoint = transform.position;

        // LOCK player position at start
        endPoint = player.position;

        // Create curved arc
        Vector2 mid = (startPoint + endPoint) / 2f;
        Vector2 direction = (endPoint - startPoint).normalized;
        Vector2 perpendicular = new Vector2(-direction.y, direction.x);

        controlPoint = mid + perpendicular * swoopCurveHeight;

        // Clamp controlPoint to arena so swoop stays inside
        controlPoint.x = Mathf.Clamp(controlPoint.x, minBounds.x, maxBounds.x);
        controlPoint.y = Mathf.Clamp(controlPoint.y, minBounds.y, maxBounds.y);
    }

    void HandleSwoop()
    {
        swoopTimer += Time.deltaTime;
        float t = swoopTimer / swoopDuration;
        t = t * t; // ease-in

        if (t >= 1f)
        {
            swooping = false;
            PickNewPos();
            return;
        }

        Vector2 pos =
            Mathf.Pow(1 - t, 2) * startPoint +
            2 * (1 - t) * t * controlPoint +
            Mathf.Pow(t, 2) * endPoint;

        transform.position = pos;
    }
}