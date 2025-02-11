using System;
using UnityEngine;

public enum GameState
{
    Loading,
    InGame,
}

[CreateAssetMenu(fileName = "GameStateManagerSO", menuName = "ScriptableObjects/GameStateManager")]
public class GameStateManager : ScriptableObject
{
    [HideInInspector] public GameState CurrentState;
}
