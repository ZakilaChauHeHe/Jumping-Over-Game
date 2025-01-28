using UnityEngine;

public class StageManager : MonoBehaviour
{
    [Header("Datas")]
    [SerializeField] private int Stage = 0;


    private void Start()
    {


    }

    private void NextStage()
    {
        Stage++;
    }
}
