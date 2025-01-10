using System;
using NUnit.Framework.Internal;
using Unity.Mathematics;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject deathParticlePrefab;
    [SerializeField] private LayerMask playerMask;
    [Header("Attribute")]
    public float speed = 5f;
    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, transform.up,999999999, playerMask);
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
