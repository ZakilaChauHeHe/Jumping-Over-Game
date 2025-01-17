using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(Player_Controller))]
public class PlayerVisual : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Player_Controller _controller;
    [SerializeField] private Rigidbody2D rb;
    [Header("VFX")]
    [SerializeField] private float spinFreq = 1f;
    [SerializeField] private GameObject DamagedParticlePrefab;
    [SerializeField] private Color DamagedParticleColor = Color.red;
    [SerializeField] private Color invincible_Color = Color.white;

    private Player_Controller Controller;
    private void Start()
    {
        Controller = gameObject.GetComponentInParent<Player_Controller>();
    }

    private void Update()
    {
        if (Controller.onGround) ResetOrintation();
    }

    private void ResetOrintation()
    {
        transform.rotation = Quaternion.identity;
    }

    public void SpinPlayerModel()
    {
        float spinDirection = -math.clamp((int)rb.linearVelocityX, -1f, 1f);
        spinDirection = (spinDirection == 0) ? 1 : spinDirection;
        rb.angularVelocity = spinDirection * spinFreq * 360;
    }

    public void PlayerDamagedEffect()
    {
        GameObject bleed = Instantiate(DamagedParticlePrefab,transform.position,quaternion.identity);
        ParticleSystem.MainModule main = bleed.GetComponent<ParticleSystem>().main;
        main.startColor = DamagedParticleColor;

        Camera.main.GetComponent<ShakeEffect>()?.FireEffect(1);
        StartCoroutine(ProcInvincible());
    }

    private IEnumerator ProcInvincible()
    {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        Color ori_Color = renderer.color;
        renderer.color = invincible_Color;
        yield return new WaitUntil(() => !_controller.Invincible);
       renderer.color = ori_Color;
    }
}
        