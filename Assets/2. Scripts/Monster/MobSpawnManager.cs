using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawnManager : MonoBehaviour
{
    public GameObject[] monsterPrefabs; // 스폰할 몬스터 프리팹 종류 배열 ( 인스펙터에서 설정 )
    public BoxCollider2D spawnArea; // 콜라이더 범위가 몬스터 스폰 범위 ( 인스펙터에서 콜라이더 범위로 설정 )
    public int spawnCount = 5; // 스폰할 몬스터 숫자 ( 인스펙터에서 설정 )
    
    private List<GameObject> spawnedMonsters = new List<GameObject>();
    // 몬스터 추적 리스트 : 각각의 스폰용 오브젝트에서 스폰된 몬스터 객체를 추적해서 (아침에) 파괴하기 위한 리스트

    private void Start()
    {
        spawnArea = GetComponent<BoxCollider2D>(); // 스크립트가 부착된 게임 오브젝트에서 BoxCollider2D 컴포넌트 슥삭
        SpawnMonsters(); // 시작 할 때 스폰
    }

    public void SpawnMonsters()
    {
        ClearMonsters(); // 기존 몬스터 삭제

        for (int i = 0; i < spawnCount; i++)
        {
            // 스폰 범위 내에서 랜덤한 스폰 위치에서 스폰
            Vector2 spawnPos = new Vector2(
                Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y)
            );

            // 몬스터 프리팹 배열에서 랜덤하게 하나를 선택하여 인스턴스화
            GameObject monsterPrefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];
            GameObject spawnedMonster = Instantiate(monsterPrefab, spawnPos, Quaternion.identity);
            spawnedMonsters.Add(spawnedMonster); // 생성된 몬스터를 추적용 리스트에 추가
        }
    }

    public void ClearMonsters()
    {
        // 리스트에 있는 모든 몬스터를 순회해서 파-괴
        foreach (var monster in spawnedMonsters)
        {
            Destroy(monster);
        }
        spawnedMonsters.Clear(); // 몬스터 추적 리스트 클리어
    }
}
