using System;
using System.Collections;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.ParticleSystem;
    
public class Player_Controller : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DataManager dataManager;
    [SerializeField] public PlayerVisual playerVisual;
    [SerializeField] private HeartSystem heartSystem;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask GroundMask;
    [SerializeField] private LayerMask EnemyMask;
    [SerializeField] private float GroundCheckCastDistance = .7f;
    [Header("Attributes")]
    [SerializeField] private float JumpDampRatio = .75f;
    [SerializeField] private float Invincible_T = 1f;
    [Header("Event")]
    public UnityEvent OnJumped;
    public UnityEvent OnEnemyKilled;

    [HideInInspector] public PlayerProfile playerProfile;
    [HideInInspector] public bool Grounded = false;
    [HideInInspector] public bool Invincible { get; private set; } = false;
    private float horizontal;
    private int JumpLeft;

    private void Start()
    {
        playerProfile = dataManager.PlayerProfile;

    }
    void Update()
    {
        //Update Player Status
        UpdateGrounded();
        animator.SetBool("Grounded", Grounded);
        //Enemy Kill
        if (Grounded) CleanEnemy();
        //Player Movement
        float direction = math.clamp((int)rb.linearVelocityX, -1f, 1f);
        animator.SetFloat("MoveDirection", (direction != 0)? direction : 1);
        rb.linearVelocityX = horizontal * playerProfile.Speed;
    }   

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * GroundCheckCastDistance);
    }

    private void OnCollisionEnter2D(Collision2D collision) //player hitted
    {
        if (Invincible) return;
        if((1 << collision.gameObject.layer) == EnemyMask.value) // Mask is 2^(layer index) => 1<<(items layer) will check if the layer is the mask
        {
            Destroy(collision.gameObject);
            heartSystem.GotDamaged();
        }
    }

    private void UpdateGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, GroundCheckCastDistance, GroundMask);
        Grounded = (hit.collider != null);
    }


    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }
        
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!Grounded && JumpLeft == 0) return;
            if (Grounded) JumpLeft = playerProfile.AirJump_Charge;
            else JumpLeft--;
            rb.linearVelocityY = 0;
            rb.AddForceY(playerProfile.Jump_Power * math.pow(JumpDampRatio, playerProfile.AirJump_Charge - JumpLeft), ForceMode2D.Impulse);
            OnJumped.Invoke();
        }
    }

    public void ProcInvicible()
    {
        StartCoroutine(loadInvicible());
        playerVisual.PlayerDamagedEffect();
    }

    public IEnumerator loadInvicible()
    {
        Invincible = true;
        yield return new WaitForSeconds(Invincible_T);
        Invincible = false;
    }
    private void CleanEnemy()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Tagged");
        foreach (GameObject taggedObject in taggedObjects)
        {
            OnEnemyKilled.Invoke();
            taggedObject.GetComponent<Enemy_Controller>()?.FireDestroy();
        }
    }
}