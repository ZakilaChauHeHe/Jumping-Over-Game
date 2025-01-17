using System.Collections;
using UnityEngine;

public class ShakeEffect : MonoBehaviour
{
    [SerializeField] private float duration = .3f;
    [SerializeField] private AnimationCurve curve;
    
    private bool shaking = false;
    public void FireEffect(float _strength)
    {
        if(!shaking) StartCoroutine(Shake(_strength));
    }
    private IEnumerator Shake(float _strength)
    {
        shaking = true;
        Vector3 startPos = transform.position;
        float elpasedTime = 0f;
        while (elpasedTime < duration)
        {
            elpasedTime += Time.deltaTime;
            float strength = curve.Evaluate(elpasedTime/duration) * _strength;
            transform.position = startPos + Random.insideUnitSphere * strength;
            yield return null;
        }
        transform.position = startPos;
        shaking = false;
    }
}
