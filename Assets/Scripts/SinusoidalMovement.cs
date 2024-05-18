using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoidalMovement : MonoBehaviour
{
    // Amplitude of the sine wave (i.e., the maximum distance from the starting point)
    public float amplitude = 1.0f;

    // Frequency of the sine wave (i.e., how quickly it oscillates)
    public float frequency = 1.0f;

    // Starting position of the object
    private Vector3 startPosition;

    void Start()
    {
        // Record the starting position of the object
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new position based on a sine wave
        Vector3 newPosition = startPosition;
        newPosition.y += Mathf.Sin(Time.time * frequency) * amplitude;

        // Apply the new position to the object
        transform.position = newPosition;
    }
}
