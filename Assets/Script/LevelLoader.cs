using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;

    public Animator transition;
    public float transitionTime = 1f;

    [SerializeField] private bool SkipEndAnimation = false;

    private void Start()
    {
        instance = this;

        transition.SetBool("SkipEndAnim", SkipEndAnimation);
    }

    public void LoadGameScene()
    {
        StartCoroutine(LoadScene(1));
    }
    
    public void LoadRewardBuffScene()
    {
        StartCoroutine (LoadScene(2));
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneIndex);
    }
}
