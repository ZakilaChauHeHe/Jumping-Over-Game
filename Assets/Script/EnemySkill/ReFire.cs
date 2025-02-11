using System.Collections;
using UnityEngine;

public class ReFire : MonoBehaviour,EnemySkill
{
    [Header("References")]
    [SerializeField] private Enemy_Controller EnemyController;
    [SerializeField] private Rigidbody2D rb;

    private GameObject Player;

    private void Start()
    {
        Player = GameObject.Find("Player");
    }

    public void Activate()
    {
        Vector3 direction;
        direction = (Player.transform.position - gameObject.transform.position).normalized;
        rb.linearVelocity = new Vector2(0, 0);
        rb.linearVelocity = direction;
        EnemyController.ApplyForce(direction);
    }

}
