using UnityEngine;

public class AiScript : MonoBehaviour
{
    public float MaxMovementSpeed;
    private Rigidbody2D rb;
    private Vector2 startingPosition;

    public Rigidbody2D Puck;

    public Transform PlayerBoundaryHolder;
    private Boundary playerBoundary;

    public Transform PuckBoundaryHolder;
    private Boundary puckBoundary;

    private Vector2 targetPosition;

    private float offsetXFromTarget;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPosition = rb.position;

        playerBoundary = new Boundary(PlayerBoundaryHolder.GetChild(0).position.y,
                                      PlayerBoundaryHolder.GetChild(1).position.y,
                                      PlayerBoundaryHolder.GetChild(2).position.x,
                                      PlayerBoundaryHolder.GetChild(3).position.x);

        puckBoundary = new Boundary(PuckBoundaryHolder.GetChild(0).position.y,
                                    PuckBoundaryHolder.GetChild(1).position.y,
                                    PuckBoundaryHolder.GetChild(2).position.x,
                                    PuckBoundaryHolder.GetChild(3).position.x);
    }

    private void FixedUpdate()
    {
        if (!PuckScript.WasGoal)
        {
            float movementSpeed;

            if (Puck.position.y > playerBoundary.Down && Puck.position.y < playerBoundary.Up &&
                Puck.position.x > playerBoundary.Left && Puck.position.x < playerBoundary.Right)
            {
                // Prioritize hitting the puck within AI boundary
                targetPosition = Puck.position;
                movementSpeed = MaxMovementSpeed;
            }
            else if (Puck.position.y < puckBoundary.Down)
            {
                // Move back to starting position y-axis correlated to puck position
                float targetY = Mathf.Clamp(Puck.position.y, startingPosition.y - 1.5f, startingPosition.y + 1.5f);
                targetPosition = new Vector2(Mathf.Clamp(Puck.position.x + offsetXFromTarget, playerBoundary.Left,
                                                          playerBoundary.Right),
                                             targetY);
                movementSpeed = MaxMovementSpeed * UnityEngine.Random.Range(0.1f, 0.3f);
            }
            else
            {
                // Move back towards the starting position when puck is in opponent's boundary
                targetPosition = startingPosition;
                movementSpeed = MaxMovementSpeed;
            }

            rb.MovePosition(Vector2.MoveTowards(rb.position, targetPosition,
                                                movementSpeed * Time.fixedDeltaTime));
        }
    }

    public void ResetPosition()
    {
        if (rb != null)
        {
            ResetPosition(startingPosition);
        }
        else
        {
            Debug.LogWarning("Rigidbody2D is not initialized in AiScript.");
            // You can choose to handle this case differently based on your requirements
        }
    }

    public void ResetPosition(Vector2 position)
    {
        if (rb != null)
        {
            rb.position = position;
        }
        else
        {
            Debug.LogWarning("Rigidbody2D is not initialized in AiScript.");
            // You can choose to handle this case differently based on your requirements
        }
    }
}
