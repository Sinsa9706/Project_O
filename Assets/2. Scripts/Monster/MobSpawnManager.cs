using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawnManager : MonoBehaviour
{
    public GameObject[] monsterPrefabs; // ������ ���� ������ ����
    public BoxCollider2D spawnArea; // �ݶ��̴� ������ ���� ���� ����
    public int spawnCount = 5; // ������ ���� ����
    private List<GameObject> spawnedMonsters = new List<GameObject>();

    private void Start()
    {
        spawnArea = GetComponent<BoxCollider2D>(); 
        SpawnMonsters();
    }

    public void SpawnMonsters()
    {
        ClearMonsters(); // ���� ���� ����

        for (int i = 0; i < spawnCount; i++)
        {
            Vector2 spawnPos = new Vector2(
                Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y)
            );

            GameObject monsterPrefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];
            GameObject spawnedMonster = Instantiate(monsterPrefab, spawnPos, Quaternion.identity);
            spawnedMonsters.Add(spawnedMonster);
        }
    }

    public void ClearMonsters()
    {
        foreach (var monster in spawnedMonsters)
        {
            Destroy(monster);
        }
        spawnedMonsters.Clear();
    }
}
