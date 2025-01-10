using Unity.Mathematics;
using UnityEngine;

public class BoarderManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject BoarderPrefab;
    [SerializeField] private float BoarderWidth = 1f;

    private Vector3 TopRightScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TopRightScreen = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));
        GameObject TopBoarder = Instantiate(BoarderPrefab,new Vector2(0,TopRightScreen.y+ BoarderWidth/2), Quaternion.identity);
        TopBoarder.transform.localScale = new Vector3(TopRightScreen.x*2, BoarderWidth, 1);
        GameObject LeftBoarder = Instantiate(BoarderPrefab, new Vector2(-(TopRightScreen.x+ BoarderWidth/2),0), Quaternion.identity);
        LeftBoarder.transform.localScale = new Vector3(BoarderWidth, TopRightScreen.y*2, 1);
        GameObject RightBoarder = Instantiate(BoarderPrefab, new Vector2(TopRightScreen.x + BoarderWidth / 2, 0), Quaternion.identity);
        RightBoarder.transform.localScale = new Vector3(BoarderWidth, TopRightScreen.y * 2, 1);
    }

}
