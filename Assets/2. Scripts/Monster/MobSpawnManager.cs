using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawnManager : MonoBehaviour
{
    public GameObject[] monsterPrefabs; // 스폰할 몬스터 프리팹 배열
    public BoxCollider2D spawnArea; // 스폰 지역을 정의하는 박스 콜라이더
    public int spawnCount = 5; // 스폰할 몬스터 수

    private void Start()
    {
        SpawnMonsters();
    }

    void SpawnMonsters()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            // 스폰 지역 내에서 랜덤한 위치 생성
            Vector2 spawnPos = new Vector2(
                Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y)
            );

            // 랜덤한 몬스터 프리팹 선택
            GameObject monsterPrefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];

            // 몬스터 인스턴스화
            Instantiate(monsterPrefab, spawnPos, Quaternion.identity);
        }
    }
}
