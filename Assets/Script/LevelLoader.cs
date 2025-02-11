using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;
    [Header("Refernces")]
    [SerializeField] private GameStateManager GSManager;
    [Header("Info")]
    public Animator transition;
    public float TransitionInTime = 1f;
    public float transitionOutTime = 1f;

    private void Start()
    {
        instance = this;
        StartCoroutine(WaitLoadinAnim());

    }

    public void LoadGameScene()
    {
        StartCoroutine(LoadScene(1));
    }
    
    public void LoadRewardBuffScene()
    {
        StartCoroutine (LoadScene(2));
    }

    IEnumerator WaitLoadinAnim()
    {
        GSManager.CurrentState = GameState.Loading;
        yield return new WaitForSeconds(TransitionInTime);
        GSManager.CurrentState = GameState.InGame;
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        transition.SetTrigger("Start");
        GSManager.CurrentState = GameState.Loading;
        yield return new WaitForSeconds(transitionOutTime);
        SceneManager.LoadScene(sceneIndex);
    }
}
