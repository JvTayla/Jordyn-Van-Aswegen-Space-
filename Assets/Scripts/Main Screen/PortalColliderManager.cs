using System.Collections;
using UnityEngine;


public class PortalColliderManager : MonoBehaviour
{
    public Collider2D orangePortalCollider;
    public Collider2D pinkPortalCollider;

    private bool isCooldown = false; // Flag to indicate cooldown state
    private float cooldownDuration = 2f; // Cooldown duration in seconds

    // Enable or disable both portal colliders simultaneously
    public void SetPortalCollidersEnabled(bool isEnabled)
    {
        orangePortalCollider.enabled = isEnabled;
        pinkPortalCollider.enabled = isEnabled;

        if (!isEnabled)
        {
            StartCoroutine(EnableCollidersAfterCooldown());
        }
    }

    // Start the cooldown for both portal colliders
    public void StartPortalCooldown()
    {
        if (!isCooldown)
        {
            StartCoroutine(TeleportCooldown());
        }
    }

    private IEnumerator TeleportCooldown()
    {
        isCooldown = true; // Set cooldown flag
        yield return new WaitForSeconds(cooldownDuration);
        isCooldown = false; // Reset cooldown flag
    }

    private IEnumerator EnableCollidersAfterCooldown()
    {
        yield return new WaitForSeconds(cooldownDuration);
        orangePortalCollider.enabled = true;
        pinkPortalCollider.enabled = true;
    }
}