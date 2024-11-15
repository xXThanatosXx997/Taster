using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private Transform[] backgroundLayers; // Assign your background layers here in the inspector
    [SerializeField] private float[] parallaxScales;       // Define the parallax strength for each layer (1 = slowest, closer to camera)
    [SerializeField] private float smoothing = 1f;         // Smoothing factor (must be > 0)

    private Transform cam;         // Reference to the main camera's transform
    private Vector3 previousCamPos; // Camera's position in the previous frame

    private void Start()
    {
        // Initialize camera and previous camera position
        cam = Camera.main.transform;
        previousCamPos = cam.position;

        // Validate scales array size matches layers
        if (parallaxScales.Length != backgroundLayers.Length)
        {
            Debug.LogError("Parallax scales array must match the number of background layers!");
        }
    }

    private void Update()
    {
        // Loop through each background layer
        for (int i = 0; i < backgroundLayers.Length; i++)
        {
            if (backgroundLayers[i] == null) continue;

            // Calculate parallax effect for the current layer
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            // Target position for the current layer
            float targetPosX = backgroundLayers[i].position.x + parallax;

            // Smoothly interpolate to the target position
            Vector3 targetPosition = new Vector3(targetPosX, backgroundLayers[i].position.y, backgroundLayers[i].position.z);
            backgroundLayers[i].position = Vector3.Lerp(backgroundLayers[i].position, targetPosition, smoothing * Time.deltaTime);
        }

        // Update the previous camera position
        previousCamPos = cam.position;
    }
}
