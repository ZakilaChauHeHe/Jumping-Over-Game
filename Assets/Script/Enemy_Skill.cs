using System.Collections;
using UnityEngine;

public class Enemy_Skill : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Enemy_Controller EnemyController;
    [SerializeField] private Rigidbody2D rb;

    private GameObject Player;

    private void Start()
    {
        Player = GameObject.Find("Player");
    }
    public void ReFire(float delayT)
    {
        Vector3 direction;
        direction = (Player.transform.position - gameObject.transform.position).normalized;
        Debug.Log(direction);
        rb.linearVelocity = new Vector2(0, 0);
        rb.linearVelocity = direction;
        EnemyController.ApplyForce(direction);
    }
}
