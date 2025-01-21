using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    [Header("Datas")]
    [SerializeField] private int Stage = 0;


    private void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void NextStage()
    {
        Stage++;
    }
}
