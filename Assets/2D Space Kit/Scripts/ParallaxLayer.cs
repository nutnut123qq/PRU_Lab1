using UnityEngine;
using spaceExplorer.Player;

public class ParallaxLayer : MonoBehaviour
{
    public float movement_resistance = 1f; // Controls how much the background moves
    public float damping = 5f; // Higher values make it smoother

    private Vector3 targetPosition;

    private void Update()
    {
        if (Player.Instance == null || !Application.isPlaying)
        {
            return;
        }

        targetPosition = Player.Instance.transform.position * movement_resistance;
        targetPosition.z = transform.position.z;

        // Smoothly interpolate to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * damping);
    }
}

