using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawnManager : MonoBehaviour
{
    public GameObject[] monsterPrefabs; // ������ ���� ������ �迭
    public BoxCollider2D spawnArea; // ���� ������ �����ϴ� �ڽ� �ݶ��̴�
    public int spawnCount = 5; // ������ ���� ��

    private void Start()
    {
        SpawnMonsters();
    }

    void SpawnMonsters()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            // ���� ���� ������ ������ ��ġ ����
            Vector2 spawnPos = new Vector2(
                Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y)
            );

            // ������ ���� ������ ����
            GameObject monsterPrefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];

            // ���� �ν��Ͻ�ȭ
            Instantiate(monsterPrefab, spawnPos, Quaternion.identity);
        }
    }
}
