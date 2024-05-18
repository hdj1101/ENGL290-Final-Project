using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float collectionRadius = 1f;
    private bool isActive = false;
    private float maxWeight = 5f;
    private float currentWeight = 0f;
    private List<Trash> collectedTrash = new List<Trash>();

    public Transform skyBoundary; // The background object named "Sky"
    public LayerMask obstacleLayer; // Layer mask to identify Rocks and Cage
    public Camera mainCamera; // The main camera
    public Transform polePivot; // The child object named "PolePivot"

    private Vector2 skyBoundsMin;
    private Vector2 skyBoundsMax;

    void Start()
    {
        // Get the bounds of the sky object
        Renderer skyRenderer = skyBoundary.GetComponent<Renderer>();
        skyBoundsMin = skyRenderer.bounds.min;
        skyBoundsMax = skyRenderer.bounds.max;
    }

    void Update()
    {
        MoveMagnet();
        HandleMagnetActivation();
    }

    void MoveMagnet()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        Vector3 newPosition = transform.position + new Vector3(moveX, moveY, 0);
        newPosition = ClampPositionToSkyBounds(newPosition);

        // Check for collisions with obstacles
        if (!IsCollidingWithObstacles(newPosition))
        {
            transform.position = newPosition;
            AdjustCameraAndPolePivot(newPosition.y, moveY);
        }
    }

    Vector3 ClampPositionToSkyBounds(Vector3 position)
    {
        return new Vector3(
            Mathf.Clamp(position.x, skyBoundsMin.x + 1f, skyBoundsMax.x -1f),
            Mathf.Clamp(position.y, skyBoundsMin.y, skyBoundsMax.y),
            position.z
        );
    }

    bool IsCollidingWithObstacles(Vector3 position)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(position, 0.1f, obstacleLayer);

        foreach (var hitCollider in hitColliders)
        {
            // Check if the collider is the sky collider
            if (hitCollider.CompareTag("Rocks") || hitCollider.CompareTag("Cage"))
            {
                return true;
            }
        }

        return false;
    }


    void AdjustCameraAndPolePivot(float magnetYPosition, float orient)
    {
        // Adjust camera position
        float cameraY = mainCamera.transform.position.y;
        if (!(cameraY >= 0 && orient > 0) && !(cameraY <= -20 && orient < 0) && !(transform.position.y >= 0) && !(transform.position.y <= -20))
        {
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, magnetYPosition, mainCamera.transform.position.z);
        }

        // Adjust PolePivot scale
        float viewportY = mainCamera.WorldToViewportPoint(transform.position).y;
        float poleScaleY = Mathf.Lerp(22, 1, viewportY);
        polePivot.localScale = new Vector3(polePivot.localScale.x, poleScaleY, polePivot.localScale.z);
    }

    void HandleMagnetActivation()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }

        if (isActive)
        {
            CollectTrash();
        }
        else
        {
            ReleaseTrash();
        }
    }

    void CollectTrash()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, collectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            Trash trash = hitCollider.GetComponent<Trash>();
            if (trash != null && (currentWeight + trash.Weight) <= maxWeight && trash.transform.position.y < transform.position.y)
            {
                AttachTrash(trash);
            }
        }
    }

    void ReleaseTrash()
    {
        foreach (var trash in collectedTrash)
        {
            trash.transform.SetParent(null); // Detach from the magnet
            // trash.GetComponent<Rigidbody2D>().isKinematic = false;
            trash.transform.GetComponent<Rigidbody2D>().gravityScale = 1.0f; // Reset gravity scale
            // You may need to reset other properties of the trash object here if needed
        }

        collectedTrash.Clear(); // Clear the list of collected trash
        currentWeight = 0f; // Reset current weight
    }

    void AttachTrash(Trash trash)
    {
        trash.transform.SetParent(transform);
        // trash.transform.localPosition = Vector3.zero; // Adjust as needed for positioning
        // trash.GetComponent<Rigidbody2D>().isKinematic = true;
        trash.transform.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        collectedTrash.Add(trash);
        currentWeight += trash.Weight;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, collectionRadius);
    }
}
