using System.Collections.Generic;
using UnityEngine;

public class MoveAlongwayPoints : MonoBehaviour
{
    // Public variables to be set in the Unity editor
    public List<Vector3> WayPoints; // List of waypoints to follow
    public float MoveSpeed; // Speed at which the object moves
    public Vector3 Current_Target; // The current target waypoint
    public Vector3 FirstPos; // Initial position of the object
    public Vector3 LastPos; // Last position of the object
    public bool IsMoving = true; // Flag to check if the object is moving
    public int currentWayPoint; // Index of the current waypoint
    public bool CanMove; // Flag to check if the object can move
    public bool canBeInterrupted; // Flag to check if the movement can be interrupted
    [SerializeField] public bool ReturnFlag; // Flag to determine if the object should return to the first position
    [SerializeField] public bool CanReturn; // Flag to determine if the object can return to the first position
    public bool StayAfterReturning; // Flag to determine if the object should stay after returning
    [SerializeField] public bool DestroyAtEnd; // Flag to determine if the object should be destroyed after reaching the last waypoint
    public TimeRewinderV2 Tr2; // Reference to a TimeRewinderV2 component

    public bool showGhosts = false; // Flag to control the visibility of ghost models

    // Method called at the start of the game
    void Start()
    {
        Initialize(); // Initialize the object
    }

    // Method called once per frame
    void Update()
    {
        if (!Application.isPlaying) return; // Do nothing if the application is not playing

        // If the TimeRewinder is not active, move along the waypoints
        if (Tr2 != null && !Tr2.Isrewinding)
        {
            MoveAlongWaypoints(); // Move the object along the waypoints
        }
    }

    // Method to initialize the object's parameters
    public void Initialize()
    {
        var player = GameObject.FindWithTag("Player_01"); // Reference to the player object
        if (player)
        {
            Tr2 = player.GetComponent<TimeRewinderV2>(); // Get the TimeRewinderV2 component from the player
        }

        if (WayPoints.Count >= 0)
        {
            gameObject.transform.position = WayPoints[0]; // Set the object's position to the first waypoint
        }
        // Set the initial and current target positions to the object's current position
        Current_Target = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        FirstPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        LastPos = FirstPos; // Set the last position to the initial position
        currentWayPoint = 0; // Start from the first waypoint
    }

    // Method to move the object along the waypoints
    public void MoveAlongWaypoints()
    {
        if (currentWayPoint < WayPoints.Count && CanMove) // If there are more waypoints and the object can move
        {
            Current_Target = WayPoints[currentWayPoint]; // Set the current target to the next waypoint
            float step = MoveSpeed * Time.deltaTime; // Calculate the step size based on the move speed and delta time
            transform.position = Vector3.MoveTowards(transform.position, Current_Target, step); // Move the object towards the current target

            if (transform.position == Current_Target) // If the object reaches the current target
            {
                if (StayAfterReturning && CanReturn && Current_Target == WayPoints[0]) // If the object should stay after returning
                {
                    CanMove = false; // Stop the object from moving
                }
                else
                {
                    LastPos = transform.position; // Update the last position
                    currentWayPoint++; // Move to the next waypoint
                }
            }
        }

        if (currentWayPoint >= WayPoints.Count) // If all waypoints are reached
        {
            if (DestroyAtEnd) // If the object should be destroyed at the end
            {
                Destroy(gameObject); // Destroy the object
            }
            else if (CanReturn && ReturnFlag) // If the object can return and should return
            {
                Current_Target = FirstPos; // Set the target to the initial position
                currentWayPoint = 0; // Reset the waypoint index
            }
        }
    }

    // Method to preview the movement in the editor
    public void PreviewMovementInEditor()
    {
        Initialize(); // Initialize the object
        CanMove = true; // Allow the object to move
    }

    // Method to add a new waypoint at the object's current position
    public void AddNewWayPoint()
    {
        WayPoints.Add(gameObject.transform.position); // Add the current position to the list of waypoints
    }

    // Method to visualize the waypoints in the editor
    void OnDrawGizmos()
    {
        if (WayPoints == null || WayPoints.Count < 2)
        {
            return;
        }

        Gizmos.color = Color.green; // Set the color for the lines

        for (int i = 0; i < WayPoints.Count - 1; i++)
        {
            Gizmos.DrawLine(WayPoints[i], WayPoints[i + 1]); // Draw lines between waypoints
            Gizmos.DrawSphere(WayPoints[i], 0.1f); // Draw spheres at waypoints
        }

        // Draw the last waypoint sphere
        Gizmos.DrawSphere(WayPoints[WayPoints.Count - 1], 0.1f);

        // Draw the ghost model at each waypoint if enabled
        if (showGhosts)
        {
            MeshFilter meshFilter = GetComponent<MeshFilter>();
            Renderer renderer = GetComponent<Renderer>();

            if (meshFilter != null && renderer != null)
            {
                Mesh ghostMesh = meshFilter.sharedMesh;
                Material ghostMaterial = renderer.sharedMaterial;

                if (ghostMesh != null && ghostMaterial != null)
                {
                    for (int i = 0; i < WayPoints.Count; i++)
                    {
                        Gizmos.color = ghostMaterial.color; // Set the color to the ghost material color
                        Gizmos.DrawMesh(ghostMesh, WayPoints[i], Quaternion.identity, transform.localScale); // Draw the ghost mesh
                    }
                }
            }
        }
    }
}