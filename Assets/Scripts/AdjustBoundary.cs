using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustBoundary : MonoBehaviour
{
    public GameObject PlayerRed; // Reference to the PlayerRed
    public GameObject Barrier; // Reference to the outside barrier
    public float padding = 0.1f; // Padding between the Player and barrier

    private Transform leftPoint;
    private Transform rightPoint;
    private Transform upPoint;
    private Transform downPoint;

    private void Start()
    {
        CalculateMovementLimits();
        AdjustBoundaryPosition();
    }

    private void CalculateMovementLimits()
    {
        if (PlayerRed == null)
        {
            Debug.LogError("Circle object is not assigned!");
            return;
        }

        CircleCollider2D circleCollider = PlayerRed.GetComponent<CircleCollider2D>();
        if (circleCollider == null)
        {
            Debug.LogError("Circle collider not found on the circle object!");
            return;
        }

        float circleRadius = circleCollider.radius;
        Vector3 circlePosition = PlayerRed.transform.position;

        leftPoint = new GameObject("LeftPoint").transform;
        leftPoint.position = new Vector3(circlePosition.x - circleRadius - padding, circlePosition.y, circlePosition.z);

        rightPoint = new GameObject("RightPoint").transform;
        rightPoint.position = new Vector3(circlePosition.x + circleRadius + padding, circlePosition.y, circlePosition.z);

        upPoint = new GameObject("UpPoint").transform;
        upPoint.position = new Vector3(circlePosition.x, circlePosition.y + circleRadius + padding, circlePosition.z);

        downPoint = new GameObject("DownPoint").transform;
        downPoint.position = new Vector3(circlePosition.x, circlePosition.y - circleRadius - padding, circlePosition.z);
    }

    private void AdjustBoundaryPosition()
    {
        if ( Barrier == null)
        {
            Debug.LogError("Boundary object is not assigned!");
            return;
        }

        Vector3 boundaryPosition = Barrier.transform.position;

        // Calculate the new position of the boundary to ensure the circle doesn't touch it
        float boundaryOffsetX = Mathf.Clamp(PlayerRed.transform.position.x, leftPoint.position.x, rightPoint.position.x) - boundaryPosition.x;
        float boundaryOffsetY = Mathf.Clamp(PlayerRed.transform.position.y, downPoint.position.y, upPoint.position.y) - boundaryPosition.y;

        Barrier.transform.position += new Vector3(boundaryOffsetX, boundaryOffsetY, 0f);
    }
}
