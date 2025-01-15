using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;


public class BoarderManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject BoarderPrefab;
    [SerializeField] private GameObject GameBoarder;
    [SerializeField] private float BoarderWidth = 1f;
    [SerializeField] private float WidthRatio = 9f;
    [SerializeField] private float HeightRatio = 16f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        float TargetRatio = WidthRatio / HeightRatio;
        float ScreenRatio = (float) Screen.width / Screen.height;
        float ChangeRatio = TargetRatio/ScreenRatio;
        Vector3 TRCorner = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));
        Vector3 TopPosition = new(0, TRCorner.y * math.min(1,1/ChangeRatio) + BoarderWidth / 2, 0);
        Vector3 RightPosition = new(TRCorner.x * math.min(1, ChangeRatio) + BoarderWidth / 2, 0, 0);
        MakeBoarder("TopBoarder",TopPosition, BoarderWidth, TRCorner.x *2);
        MakeBoarder("RightBoarder", RightPosition, TRCorner.y * 2, BoarderWidth);
        MakeBoarder("LeftBoarder", -RightPosition, TRCorner.y * 2, BoarderWidth);
    }
    void MakeBoarder(string name, Vector3 position, float length, float width)
    {
        GameObject newBoarder = Instantiate(BoarderPrefab, position, Quaternion.identity, GameBoarder.transform);
        newBoarder.name = name;
        newBoarder.transform.localScale = new Vector3(width, length, 1);
    }
}
