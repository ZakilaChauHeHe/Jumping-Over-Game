using UnityEngine;
using UnityEngine.Events;

public class HeartSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Player_Controller PlayerController;
    [Header("Event")]
    public UnityEvent OnHeartLoss;
    public UnityEvent OnDie;

    public void GotDamaged()
    {
        PlayerController.playerProfile.Heart--;
        if (PlayerController.playerProfile.Heart<= 0)
        {
            OnDie.Invoke();
            return;
        }
        OnHeartLoss.Invoke();
    }
}
