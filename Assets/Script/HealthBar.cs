using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject HeartPrefab;

    private GameObject Player;
    private int HeartLeft;
    private List<GameObject> Hearts = new();
    private void Start()
    {
        Player = GameObject.Find("Player");
        PlayerProfile playerProfile = Player.GetComponent<Player_Controller>().playerProfile;
        HeartSystem HeartSys = Player.GetComponent<HeartSystem>();
        HeartLeft = playerProfile.Heart;
        HeartSys.OnHeartLoss.AddListener(UpdateHeart);

        for (int i = 0; i < HeartLeft; i++) 
        {
            GameObject NewHeart = Instantiate(HeartPrefab);
            NewHeart.transform.SetParent(transform);    
            NewHeart.transform.localPosition = Vector3.zero - new Vector3(0, -i * NewHeart.transform.localScale.y, 0);
            NewHeart.GetComponent<Renderer>().sortingOrder = -i;
            Hearts.Add(NewHeart);
        }
        }   

    public void UpdateHeart()
    {
        for (int i = 0; i < HeartLeft- Player.GetComponent<Player_Controller>().playerProfile.Heart; i++)
        {
            Destroy(Hearts[0]);
            Hearts.Remove(Hearts[0]);
            HeartLeft--;
        }
    }
}
 