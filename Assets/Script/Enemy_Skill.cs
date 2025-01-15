using UnityEngine;

public class Enemy_Skill : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject Player;
    [SerializeField] private Enemy_Controller EnemyController;
    [SerializeField] private Rigidbody2D rb;
    public void ReFire(float delayT)
    {
        Vector3 direction;
        direction = (Player.transform.position - gameObject.transform.position).normalized;
        rb.linearVelocity = new Vector3(0, 0, 0);
        EnemyController.ApplyForce(direction);
    }
}
