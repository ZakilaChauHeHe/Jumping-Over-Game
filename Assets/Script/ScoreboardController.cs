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
        int gameScore = dataManager.Score;

        switch (dataManager.Gamemode)
        {
            case Gamemode.Time:
                displayText = "Time: " + gameScore;
                break;
            case Gamemode.Stage:
                displayText = "Stage " + gameScore;
                break;
            case Gamemode.Score:
                displayText = "Score: " + gameScore;
                break;
        }

        GetComponent<TextMeshProUGUI>().text = displayText;
    }
}
