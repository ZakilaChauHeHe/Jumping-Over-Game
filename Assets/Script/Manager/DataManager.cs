using UnityEngine;

[System.Serializable]
public class PlayerProfile
{
    public int Heart = 3;
    public float Speed = 7.5f;
    public float Jump_Power = 25f;
    public int AirJump_Charge = 1;
}
[CreateAssetMenu(fileName = "DataManagerSO", menuName = "ScriptableObjects/DataManager")]
public class DataManager : ScriptableObject
{
    [Header("Datas")]
    public Gamemode Gamemode;
    public int Score { get; private set; }
    [SerializeField] private PlayerProfile SourceProfile;


    [HideInInspector] public PlayerProfile PlayerProfile;

    private void OnEnable()
    {
        FullReset();
    }

    public void FullReset()
    {
        ResetPlayerProfile();
        ResetGameData();
    }

    public void AddScore(int addScore)
    {
        Score += addScore;
    }

    public void ResetPlayerProfile()
    {
        PlayerProfile.Heart = SourceProfile.Heart;
        PlayerProfile.Speed = SourceProfile.Speed;
        PlayerProfile.Jump_Power = SourceProfile.Jump_Power;
        PlayerProfile.AirJump_Charge = SourceProfile.AirJump_Charge;
    }

    private void ResetGameData()
    {
        Score = 0;
    }
}
