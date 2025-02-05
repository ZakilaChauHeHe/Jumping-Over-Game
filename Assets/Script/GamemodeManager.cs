using UnityEngine;

public enum Gamemode
{
    Time,
    Stage,
    Score
}

public class GamemodeManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DataManager DataManager;
    public void SetGamemode(int gamemodeIndex)
    {
        DataManager.Gamemode = (Gamemode)gamemodeIndex;
    }
}
