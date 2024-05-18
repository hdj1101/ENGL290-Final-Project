using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public Transform cage;
    public GameManager gameManager;
    Rigidbody2D rb;
    Collider2D cd;

    private bool scoreAdded = false;

    void Start()
    {
        cage = GameObject.Find("Cage2").transform;
        gameManager = FindObjectOfType<GameManager>();

        cd = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){}
    
    public string trashName;
    public int weight;
    public int monetaryValue;
    public string Name { get { return trashName; } }
    public int Weight { get { return weight; } }
    public float MonetaryValue { get { return monetaryValue; } }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("Cage"))
        {
            transform.SetParent(cage);

            if (!scoreAdded)
            {
            gameManager.AddScore(monetaryValue);
            scoreAdded = true; 
            }

            Renderer renderer = GetComponent<Renderer>();
            renderer.enabled = false;
            
            // cd.enabled = false;
            // rb.simulated = false;
            // rb.isKinematic = true;
            // rb.gravityScale = 0;
        }
    }
}
