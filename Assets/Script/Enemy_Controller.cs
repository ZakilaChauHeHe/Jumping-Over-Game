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
    public int Health = 1;
    public float speed = 5f;
    [Header("Special Effect")]
    public UnityEvent<GameObject> OnWallCollide;
    public UnityEvent OnDamaged;
    

    private Color DefaultColor = Color.red;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 direction = rb.linearVelocity.normalized;
        float magnitude = math.log10(rb.linearVelocity.magnitude);
        Gizmos.DrawLine(transform.position, transform.position + direction * magnitude);
    }

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
            OnWallCollide.Invoke(collision.collider.gameObject);
        }
    }

    private void ApplyTag()
    {
        DefaultColor = spriteRenderer.color;
        gameObject.tag = "Tagged";
        spriteRenderer.color = Color.yellow;
    }
    private void RemoveTag()
    {
        gameObject.tag = "Untagged";
        spriteRenderer.color = DefaultColor;
    }
    public void ApplyForce(Vector2 direction)
    {
        rb.AddForce(direction * speed);
    }

    public void Damage()
    {
        Health--;
        if (Health <= 0)
        {
            FireDestroy();
            return;
        }
        RemoveTag();
        OnDamaged.Invoke();
    }

    private void FireDestroy()
    {
        Camera.main.GetComponent<ShakeEffect>().FireEffect(1f);
        GameObject particle = Instantiate(deathParticlePrefab);
        ParticleSystem.MainModule main = particle.GetComponent<ParticleSystem>().main;
        main.startColor = spriteRenderer.color;
        particle.transform.position = transform.position;
        Destroy(gameObject);
    }
}   
