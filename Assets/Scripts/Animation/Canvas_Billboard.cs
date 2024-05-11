using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_Billboard : MonoBehaviour
{
    private Camera mainCamera;
    public float smoothness = 5.0f;

    private Quaternion originalRotation;

    private void Start()
    {
        // Get the main camera
        mainCamera = Camera.main;

        // Store the original rotation of the canvas
        originalRotation = transform.rotation;
    }

    private void Update()
    {
        // Update the billboard rotation each frame to face the camera smoothly
        UpdateBillboard();
    }

    private void UpdateBillboard()
    {
        // Check if the main camera is available
        if (mainCamera != null)
        {
            // Calculate the direction from the canvas to the camera
            Vector3 directionToCamera = mainCamera.transform.position - transform.position;

            // Preserve the original Y-axis rotation
            Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);
            targetRotation *= Quaternion.Euler(0, -originalRotation.eulerAngles.y, 0);

            // Ensure the canvas faces the camera by setting its rotation smoothly
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothness * Time.deltaTime);
        }
        else
        {
            // If the main camera is not available, log an error message
            Debug.LogError("Main camera not found. Make sure there is an active camera in the scene.");
        }
    }
}