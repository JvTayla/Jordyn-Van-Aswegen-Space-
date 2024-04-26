using System.Collections;
using UnityEngine;



public class Portals : MonoBehaviour
{
    public bool isOrange;
    public GameObject portalObject;
    public Vector3 startPosition; // Define the start position of the portal
    public Collider2D portalCollider;
    public SpriteRenderer spriteRenderer;
    public PortalColliderManager colliderManager; // Reference to the collider manager

    private Transform destination;
    private bool isActive = true;
    private float disappearTime = 15f; // Time in seconds before the portal disappears
    private float reappearTime = 15f; // Time in seconds before the portal reappears

    private void Start()
    {
        if (isOrange)
        {
            destination = GameObject.FindGameObjectWithTag("OrangePortal").transform;
        }
        else
        {
            destination = GameObject.FindGameObjectWithTag("PinkPortal").transform;
        }

        ResetPosition(false); // Reset the position to the start position
        StartCoroutine(PortalHandler());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive && other.CompareTag("Puck"))
        {
            Vector2 offset = destination.position - other.transform.position;
            other.transform.position += (Vector3)offset; // Teleport the puck

            colliderManager.SetPortalCollidersEnabled(false); // Disable both portal colliders
            colliderManager.StartPortalCooldown(); // Start the cooldown for both portal colliders
        }
    }

    private IEnumerator PortalHandler()
    {
        while (true)
        {
            yield return new WaitForSeconds(isActive ? disappearTime : reappearTime);
            isActive = !isActive;
            portalCollider.enabled = isActive;
            spriteRenderer.enabled = isActive;
            yield return null; // Yield once more to ensure proper execution
        }
    }

    public void ResetPosition(bool resetForGameRestart)
    {
        if (resetForGameRestart)
        {
            transform.position = startPosition; // Reset the position to the start position
        }
    }
}









