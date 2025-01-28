using UnityEngine;


[CreateAssetMenu(fileName = "MutatorListSO", menuName = "ScriptableObjects/MutatorList")]
public class MutatorList : ScriptableObject
{
    public void IncreaseEnemySize(GameObject enemy)
    {
        Debug.Log("Increase Size");
        if(enemy == null || enemy.GetComponent<Enemy_Controller>() == null) return;
        enemy.transform.localScale *= 2;
    }

    public void IncreaseEnemySpeed(GameObject enemy)
    {
        Debug.Log("Increase Size");
        if (enemy == null || enemy.GetComponent<Enemy_Controller>() == null) return;
        enemy.GetComponent<Enemy_Controller>().speed *= 2;
    }
}
