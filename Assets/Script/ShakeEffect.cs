using System.Collections;
using UnityEngine;

public class ShakeEffect : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    [SerializeField] private AnimationCurve curve;
    
    private bool shaking = false;
    public void FireEffect()
    {
        if(!shaking) StartCoroutine(Shake());
    }
    private IEnumerator Shake()
    {
        shaking = true;
        Vector3 startPos = transform.position;
        float elpasedTime = 0f;
        while (elpasedTime < duration)
        {
            elpasedTime += Time.deltaTime;
            float strength = curve.Evaluate(elpasedTime/duration);
            transform.position = startPos + Random.insideUnitSphere * strength;
            yield return null;
        }
        transform.position = startPos;
        shaking = false;
    }
}
