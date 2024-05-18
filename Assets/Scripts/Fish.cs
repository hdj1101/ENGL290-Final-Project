using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float minSpeed = 2f;
    public float maxSpeed = 7f;

    private float speed;
    private bool movingRight;
    private Transform skyBoundary;
    private Vector2 skyBoundsMin;
    private Vector2 skyBoundsMax;

    void Start()
    {
        // Find the GameObject named "Sky" and get its Transform
        skyBoundary = GameObject.Find("Sky").transform;

        // Get the bounds of the sky object
        Renderer skyRenderer = skyBoundary.GetComponent<Renderer>();
        skyBoundsMin = skyRenderer.bounds.min;
        skyBoundsMax = skyRenderer.bounds.max;

        // Randomize initial direction and speed
        movingRight = Random.value > 0.5f;
        speed = Random.Range(minSpeed, maxSpeed);

        // Set the initial facing direction
        UpdateFishDirection();
    }

    void Update()
    {
        MoveFish();
    }

    void MoveFish()
    {
        Vector3 movement = new Vector3(speed * Time.deltaTime, 0, 0);
        transform.position += movingRight ? movement : -movement;

        // Check if the fish hit the sky boundaries
        if (transform.position.x >= skyBoundsMax.x)
        {
            movingRight = false;
            UpdateFishDirection();
        }
        else if (transform.position.x <= skyBoundsMin.x)
        {
            movingRight = true;
            UpdateFishDirection();
        }
    }

    void UpdateFishDirection()
    {
        // Flip the fish's sprite to face the correct direction
        Vector3 newScale = transform.localScale;
        newScale.x = movingRight ? Mathf.Abs(newScale.x) : -Mathf.Abs(newScale.x);
        transform.localScale = newScale;
    }
}