using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawnManager : MonoBehaviour
{
    public GameObject[] monsterPrefabs; // ������ ���� ������ ���� �迭 ( �ν����Ϳ��� ���� )
    public BoxCollider2D spawnArea; // �ݶ��̴� ������ ���� ���� ���� ( �ν����Ϳ��� �ݶ��̴� ������ ���� )
    public int spawnCount = 5; // ������ ���� ���� ( �ν����Ϳ��� ���� )
    
    private List<GameObject> spawnedMonsters = new List<GameObject>();
    // ���� ���� ����Ʈ : ������ ������ ������Ʈ���� ������ ���� ��ü�� �����ؼ� (��ħ��) �ı��ϱ� ���� ����Ʈ

    private void Start()
    {
        spawnArea = GetComponent<BoxCollider2D>(); // ��ũ��Ʈ�� ������ ���� ������Ʈ���� BoxCollider2D ������Ʈ ����
        SpawnMonsters(); // ���� �� �� ����
    }

    public void SpawnMonsters()
    {
        ClearMonsters(); // ���� ���� ����

        for (int i = 0; i < spawnCount; i++)
        {
            // ���� ���� ������ ������ ���� ��ġ���� ����
            Vector2 spawnPos = new Vector2(
                Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y)
            );

            // ���� ������ �迭���� �����ϰ� �ϳ��� �����Ͽ� �ν��Ͻ�ȭ
            GameObject monsterPrefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];
            GameObject spawnedMonster = Instantiate(monsterPrefab, spawnPos, Quaternion.identity);
            spawnedMonsters.Add(spawnedMonster); // ������ ���͸� ������ ����Ʈ�� �߰�
        }
    }

    public void ClearMonsters()
    {
        // ����Ʈ�� �ִ� ��� ���͸� ��ȸ�ؼ� ��-��
        foreach (var monster in spawnedMonsters)
        {
            Destroy(monster);
        }
        spawnedMonsters.Clear(); // ���� ���� ����Ʈ Ŭ����
    }
}
