using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config",fileName = "NewWaveConfig")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private Transform pathPrefab;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float timeBetwenEnemySpwns = 1f;
    [SerializeField] private float spawnTimeVariance = 0;
    [SerializeField] private float minSpwnTime = 0.2f;

    public Transform GetStartingWayPoint() => pathPrefab.GetChild(0);

    public List<Transform> GetWayPoints()
    {
        List<Transform> result = new();
        foreach(Transform child in pathPrefab)
        {
            result.Add(child);
        }
        return result;
    }

    public float GetMoveSpeed() => moveSpeed;

    public int GetEnemyCount() => enemyPrefabs.Count;

    public GameObject GetEnemyPrefab(int index) => enemyPrefabs[index];

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetwenEnemySpwns - spawnTimeVariance
            ,timeBetwenEnemySpwns + spawnTimeVariance);
        return Mathf.Clamp(spawnTime,minSpwnTime,float.MaxValue);
    }
}