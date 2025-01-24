using UnityEngine;

public enum Gamemode
{
    Time,
    Stage,
    Score
}

public class GamemodeManager : MonoBehaviour
{
    private DataManager dataManager;
    public void SetGamemode(int gamemodeIndex)
    {
        dataManager.Gamemode = (Gamemode)gamemodeIndex;
    }
}
