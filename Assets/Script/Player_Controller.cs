using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Game_Manager game_manager;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask GroundMask;
    [SerializeField] private LayerMask EnemyMask;
    [SerializeField] private float GroundCheckCastDistance = .7f;
    [Header("Attributes")]
    [SerializeField] private float speed = 6f;
    [SerializeField] private float Jump_Power = 1f;
    [SerializeField] private int AirJump_Charge = 1;
    [SerializeField] private float spinFreq = 1;


    [HideInInspector] public bool onGround = false;
    private float horizontal;
    private int JumpLeft;


    // Update is called once per frame
    void Update()
    {
        UpdateGrounded();
        if (onGround) ResetOrintation();
        rb.linearVelocity = new Vector2(horizontal * speed,rb.linearVelocityY);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * GroundCheckCastDistance);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((1 << collision.gameObject.layer) == EnemyMask.value) // Mask is 2^(layer index) => 1<<(items layer) will check if the layer is the mask
        {
            game_manager.player_Died?.Invoke(gameObject);
        }
    }

    private void ResetOrintation()
    {
        transform.rotation = Quaternion.identity;
    }

    private void UpdateGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, GroundCheckCastDistance, GroundMask);
        onGround = (hit.collider != null);
    }


    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }
        
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!onGround && JumpLeft == 0) return;
            if (onGround) JumpLeft = AirJump_Charge;
            else JumpLeft--;
            rb.linearVelocityY = 0;
            rb.AddForceY(Jump_Power, ForceMode2D.Impulse);
            float spinDirection = (rb.linearVelocityX == 0) ? rb.angularVelocity : -rb.linearVelocityX;
            rb.angularVelocity = spinDirection * spinFreq;
        }
    }
}   