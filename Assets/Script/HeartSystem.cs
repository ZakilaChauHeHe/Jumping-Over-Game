using UnityEngine;
using UnityEngine.Events;

public class HeartSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Player_Controller PlayerController;
    [Header("Event")]
    public UnityEvent OnHeartLoss;
    public UnityEvent OnDie;

    [HideInInspector] public int HeartLeft {  get; private set; }
    private void Start()
    {
        HeartLeft = PlayerController.Heart;
    }

    public void GotDamaged()
    {
        PlayerController.Heart--;
        if (PlayerController.Heart <= 0)
        {
            OnDie.Invoke();
            return;
        }
        HeartLeft = PlayerController.Heart;
        OnHeartLoss.Invoke();
    }
}
