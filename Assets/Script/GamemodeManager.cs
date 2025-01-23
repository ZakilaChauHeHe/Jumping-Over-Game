using UnityEngine;

public enum Gamemode
{
    Time,
    Stage,
    Score
}

public class GamemodeManager : MonoBehaviour
{
    public void SetGamemode(int gamemodeIndex)
    {
        DataManager.Instance.Gamemode = (Gamemode)gamemodeIndex;
    }
}
