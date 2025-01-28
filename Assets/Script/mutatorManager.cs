using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using UnityEngine;

public enum Mutation
{
    NULL,
    IncreaseEnemySpeed,
    IncreaseEnemySize,
}

public class MutatorManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private MutatorList mutatorList;
    [HideInInspector] public Mutation CurrentMutator { get; private set; } = Mutation.NULL;

    public void NewMutator()
    {
        Mutation newMutation = (Mutation)UnityEngine.Random.Range(0, Enum.GetValues(typeof(Mutation)).Length - 1);
        Debug.Log(newMutation);
        SwitchMutator(newMutation);
    }

    private void SwitchMutator(Mutation NewMutator)
    {   
        DisableMutator(CurrentMutator);
        EnableMutator(NewMutator);
    }

    private void DisableMutator(Mutation TargetMutator)
    {
        switch (TargetMutator)
        {
            case Mutation.NULL:
                break;
            case Mutation.IncreaseEnemySpeed:
                spawnManager.PreSpawnEffects.Remove(mutatorList.IncreaseEnemySpeed);
                break;
            case Mutation.IncreaseEnemySize:
                spawnManager.PreSpawnEffects.Remove(mutatorList.IncreaseEnemySize);
                break;
        }
        CurrentMutator = Mutation.NULL;
    }

    private void EnableMutator(Mutation TargetMutator)
    {
        switch (TargetMutator)
        {
            case Mutation.NULL:
                break;
            case Mutation.IncreaseEnemySpeed:
                spawnManager.PreSpawnEffects.Add(mutatorList.IncreaseEnemySpeed);
                break;
            case Mutation.IncreaseEnemySize:
                spawnManager.PreSpawnEffects.Add(mutatorList.IncreaseEnemySize);
                break;
        }
        CurrentMutator = TargetMutator;
    }
} 
