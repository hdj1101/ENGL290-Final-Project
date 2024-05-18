using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeGripped : MonoBehaviour
{
    public Transform hand;

    Rigidbody2D rb;

    int tCount;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("We're in");
        // if (other.name.Contains("Cage"))
        // {
                transform.SetParent(hand);
                rb.isKinematic = true;
        // }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        tCount--;

        if (transform.parent == hand)
        {
            transform.parent = null;
            rb.isKinematic = false;
        }
    }
}
