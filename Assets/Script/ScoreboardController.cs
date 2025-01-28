using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreboardController : MonoBehaviour
{
    [SerializeField] private DataManager dataManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateDisplay();
    }


    public void UpdateDisplay()
    {
        string displayText = "Display Error!";

        switch (dataManager.Gamemode)
        {
            case Gamemode.Time:
                displayText = "Time: " + Game_Manager.Instance.Score;
                break;
            case Gamemode.Stage:
                displayText = "Stage " + Game_Manager.Instance.Score;
                break;
            case Gamemode.Score:
                displayText = "Score: " + Game_Manager.Instance.Score;
                break;
        }

        GetComponent<TextMeshProUGUI>().text = displayText;
    }
}
