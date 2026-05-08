using UnityEngine;

public class BossMovement2D : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    [Header("Normal Movement")]
    public float moveSpeed = 2f;
    public float roamRadius = 5f;

    [Header("Swoop Settings")]
    public float swoopDuration = 3f;
    public float swoopCurveHeight = 3f;
    public float timeBetweenSwoopChecks = 1f;

    [Header("Return Up Settings")]
    public float returnUpDuration = 1.5f;
    public float returnUpHeight = 4f;

    [Header("Confiner Settings")]
    public Vector2 minBounds;
    public Vector2 maxBounds;

    private Vector2 targetPos;
    private bool swooping = false;
    private bool returningUp = false;

    private Vector2 startPoint;
    private Vector2 controlPoint;
    private Vector2 endPoint;

    private Vector2 returnStartPoint;
    private Vector2 returnEndPoint;
    private float swoopTimer = 0f;
    private float returnTimer = 0f;

    void Start()
    {
        PickNewPos();
        InvokeRepeating(nameof(TryStartSwoop), 3f, timeBetweenSwoopChecks);
    }

    void Update()
    {
        if (swooping)
        {
            HandleSwoop();
        }
        else if (returningUp)
        {
            HandleReturnUp();
        }
        else
        {
            HandleMovement();
        }

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
        pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);
        transform.position = pos;
    }

    void HandleMovement()
    {
        transform.position = Vector2.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPos) < 0.5f)
        {
            PickNewPos();
        }
    }

    void PickNewPos()
    {
        targetPos = Random.insideUnitCircle * roamRadius + (Vector2)transform.position;

        targetPos.x = Mathf.Clamp(targetPos.x, minBounds.x, maxBounds.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minBounds.y, maxBounds.y);
    }

    void TryStartSwoop()
    {
        if (swooping || returningUp) return;
        if (player == null) return;

        OrbitProjectile2D[] projectiles = FindObjectsByType<OrbitProjectile2D>(FindObjectsSortMode.None);

        if (projectiles.Length > 0) return;

        StartSwoop();
    }

    void StartSwoop()
    {
        swooping = true;
        swoopTimer = 0f;

        startPoint = transform.position;
        endPoint = player.position;

        Vector2 mid = (startPoint + endPoint) / 2f;
        Vector2 direction = (endPoint - startPoint).normalized;
        Vector2 perpendicular = new Vector2(-direction.y, direction.x);

        controlPoint = mid + perpendicular * swoopCurveHeight;

        controlPoint.x = Mathf.Clamp(controlPoint.x, minBounds.x, maxBounds.x);
        controlPoint.y = Mathf.Clamp(controlPoint.y, minBounds.y, maxBounds.y);
    }

    void HandleSwoop()
    {
        swoopTimer += Time.deltaTime;

        float t = swoopTimer / swoopDuration;
        t = Mathf.Clamp01(t);
        t = Mathf.SmoothStep(0f, 1f, t);

        Vector2 pos =
            Mathf.Pow(1 - t, 2) * startPoint +
            2 * (1 - t) * t * controlPoint +
            Mathf.Pow(t, 2) * endPoint;

        transform.position = pos;

        if (t >= 1f)
        {
            swooping = false;
            StartReturnUp();
        }
    }

    void StartReturnUp()
    {
        returningUp = true;
        returnTimer = 0f;

        returnStartPoint = transform.position;
        returnEndPoint = new Vector2(
            transform.position.x,
            Mathf.Clamp(transform.position.y + returnUpHeight, minBounds.y, maxBounds.y)
        );
    }

    void HandleReturnUp()
    {
        returnTimer += Time.deltaTime;

        float t = returnTimer / returnUpDuration;
        t = Mathf.Clamp01(t);
        t = Mathf.SmoothStep(0f, 1f, t);

        transform.position = Vector2.Lerp(returnStartPoint, returnEndPoint, t);

        if (t >= 1f)
        {
            returningUp = false;
            PickNewPos();
        }
    }
}