using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    [Header("Datas")]
    public Gamemode Gamemode;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
