using System;
using NUnit.Framework.Internal;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class Enemy_Controller : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject deathParticlePrefab;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask GroundMask;
    [Header("Attribute")]
    public float speed = 5f;
    [Header("Special Effect")]
    public UnityEvent OnWallCollide;
    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, transform.up,999999999, playerMask);
        if(hit2D.collider != null)
        {
            ApplyTag();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GroundMask.value == (1 << collision.collider.gameObject.layer))
        {
            OnWallCollide.Invoke();
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

    public void FireDestroy()
    {
        Camera.main.GetComponent<ShakeEffect>().FireEffect();
        GameObject particle = Instantiate(deathParticlePrefab);
        ParticleSystem.MainModule main = particle.GetComponent<ParticleSystem>().main;
        main.startColor = spriteRenderer.color;
        particle.transform.position = transform.position;
        Destroy(gameObject);
    }
}   
