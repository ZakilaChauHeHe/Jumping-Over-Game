using UnityEngine;

public enum Gamemode
{
    Time,
    Stage,
    Score
}

public class GamemodeManager : MonoBehaviour
{
    public static GamemodeManager Instance;
    [SerializeField] private Gamemode gamemode;

    void Start()
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

    public void SetGamemode(int gamemodeIndex)
    {
        gamemode = (Gamemode)gamemodeIndex;
    }
}
