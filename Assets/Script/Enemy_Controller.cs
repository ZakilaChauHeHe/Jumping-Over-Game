using System;
using NUnit.Framework.Internal;
using Unity.Mathematics;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask playerMask;
    [Header("Attribute")]
    public float speed = 5f;
    // Update is called once per frame

    public EventHandler GameEnd;
    void Update()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, transform.up,10000, playerMask);
        if(hit2D.collider != null)
        {
            ApplyTag();
        }
    }

    private void ApplyTag()
    {    
        gameObject.tag = "Tagged";
        spriteRenderer.color = Color.yellow;
    }
    
    public void ApplyForce(Vector2 direction)
    {
        rb.AddForce(direction * speed);
    }
}   
