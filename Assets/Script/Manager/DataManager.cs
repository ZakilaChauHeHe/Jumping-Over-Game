using UnityEngine;

[System.Serializable]
public class PlayerProfile
{
    public int Heart = 3;
    public float Speed = 7.5f;
    public float Jump_Power = 25f;
    public int AirJump_Charge = 1;
}
[CreateAssetMenu(fileName = "DataManagerSO",menuName = "ScriptableObjects/DataManager")]
public class DataManager : ScriptableObject
{
    [Header("Datas")]
    public Gamemode Gamemode;
    [HideInInspector] public PlayerProfile PlayerProfile;
    [SerializeField] private PlayerProfile SourceProfile;

    private void OnEnable()
    {
        ResetPlayerProfile();
    }

    public void ResetPlayerProfile()
    {
        PlayerProfile.Heart = SourceProfile.Heart;
        PlayerProfile.Speed = SourceProfile.Speed;
        PlayerProfile.Jump_Power = SourceProfile.Jump_Power;
        PlayerProfile.AirJump_Charge = SourceProfile.AirJump_Charge;
    }
}
