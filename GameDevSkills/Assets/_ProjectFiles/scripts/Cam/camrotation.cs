using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camrotation : MonoBehaviour
{
    [SerializeField] private Transform target; // The character's transform to follow
    [SerializeField] private float distance = 5.0f; // Camera distance from the character
    [SerializeField] private float height = 2.0f; // Camera height from the character
    [SerializeField] private float rotationSpeed = 5.0f; // Camera rotation speed

    private Vector3 offset; // The initial offset between camera and character

    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target (character) is not assigned to the MarioStyleCamera script.");
            enabled = false; // Disable the script if no target is assigned
            return;
        }

        // Calculate the initial offset between camera and character
        offset = transform.position - target.position;
    }

    private void Update()
    {
        if (target == null)
            return;

        // Handle camera rotation based on mouse input
        float rotationInputX = Input.GetAxis("Mouse X") * rotationSpeed;

        // Update the camera's rotation
        Quaternion currentRotation = transform.rotation;
        transform.RotateAround(target.position, Vector3.up, rotationInputX);
        Quaternion newRotation = transform.rotation;

        // Calculate the desired position of the camera
        Vector3 targetPosition = target.position - (newRotation * offset);
        targetPosition.y = target.position.y + height;

        // Smoothly move the camera towards the target position
        transform.rotation = currentRotation; // Restore the current rotation before movement
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * rotationSpeed);
    }
}
