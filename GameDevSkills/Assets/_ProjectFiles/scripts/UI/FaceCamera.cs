using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Transform cameraTransform;

    void Start()
    {
        // Get the main camera's transform
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // Make the plane look at the camera
        transform.LookAt(cameraTransform);

    }
}