using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame_Button : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Load_Scene(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
    }
}
