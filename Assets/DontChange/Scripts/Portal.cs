using UnityEngine;

public class Portal : MonoBehaviour
{
    [Header("Portal Settings")]
    [Tooltip("The destination GameObject to teleport the player to.")]
    public Transform destination;

    [Tooltip("Enable if you want to prevent multiple triggers in quick succession.")]
    public bool enableCooldown = true;

    [Tooltip("Cooldown duration in seconds.")]
    public float cooldownDuration = 1.0f;

    private bool isOnCooldown = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player") && !isOnCooldown)
        {
            Teleport(other.transform);

            if (enableCooldown)
            {
                StartCoroutine(Cooldown());
            }
        }
    }

    private void Teleport(Transform player)
    {
        if (destination != null)
        {
            // Teleport the player to the destination
            player.position = destination.position;

            // Optional: Log for debugging purposes
            Debug.Log($"Player teleported to {destination.position}");
        }
        else
        {
            Debug.LogWarning("Destination is not set for this portal!");
        }
    }

    private System.Collections.IEnumerator Cooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownDuration);
        isOnCooldown = false;
    }
}
