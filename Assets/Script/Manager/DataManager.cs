using UnityEngine;



[System.Serializable]
public class PlayerProfile
{
    public int Heart = 3;
    public int AirJump_Charge = 1;
    public float speed = 6f;
    public float Jump_Power = 3f;
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    [Header("Datas")]
    public Gamemode Gamemode;
    public PlayerProfile PlayerProfile;

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
