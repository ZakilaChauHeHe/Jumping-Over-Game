using UnityEngine;

public class ShieldExplode : MonoBehaviour, EnemySkill
{
    [Header("References")]
    [SerializeField] private GameObject TransformInto;

    private void Transform()
    {   
        GameObject targetObject = Instantiate(TransformInto, transform.position,transform.rotation,transform.parent);
        Rigidbody2D targetRb = targetObject.GetComponent<Rigidbody2D>();
        targetRb.linearVelocity = gameObject.GetComponent<Rigidbody2D>().linearVelocity;
        Destroy(gameObject);
    }
    public void Activate()
    {
        Transform();
    }
}
