using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetItemManager : MonoBehaviour
{
    // 몬스터와 상호 작용시 플레이어의 머리 위에 게이지가 차오르게 할 예정.
    // 플레이어 오브젝트 안에 Canvas(월드 스페이스) 만들고 이미지 Slider 만들어서.

    public Slider gaugeSlider; // UI 게이지 슬라이더
    private GameObject currentMonster; // 현재 상호작용 중인 몬스터
    private bool isCharging = false; // 게이지 충전 중 여부
    private float chargeTime = 0f; // 현재 충전 시간
    private float maxChargeTime = 2f; // 최대 충전 시간

    public GameObject itemPrefab; // 아이템 프리팹, 임시 테스트용
    public SpriteRenderer itemSprite; // 아이템 스프라이트 렌더러, 임시 테스트용

    void Update()
    {
           if (isCharging && currentMonster != null)
        {
            if (Input.GetKey(KeyCode.Space)) // 스페이스 키로 테스트
            {
                Debug.Log("키 누르는중");
                chargeTime += Time.deltaTime;
                gaugeSlider.value = chargeTime / maxChargeTime; // 슬라이더의 value 업데이트

                if (chargeTime >= maxChargeTime)
                {
                    ShowItemSprite();
                    ResetCharge(); // 충전 리셋
                }
            }
            else
            {
                // 키에서 손을 떼면 충전 리셋
                ResetCharge();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision) // 플레이어랑 몬스터 충돌 감지
    {
        if (collision.CompareTag("Monster"))
        {
            Debug.Log("충돌 시작");
            StartCharging(collision.gameObject);
        }
    }

    
    void OnTriggerExit2D(Collider2D collision) // 플레이어와 몬스터 간의 충돌 종료
    {
        if (collision.CompareTag("Monster"))
        {
            Debug.Log("충돌 종료");
            ResetCharge();
        }
    }

    // 게이지 시작
    public void StartCharging(GameObject monster)
    {
        currentMonster = monster;
        isCharging = true;
        chargeTime = 0;
        gaugeSlider.value = 0; // 슬라이더의 value 초기화
    }

    // 게이지 리셋
    private void ResetCharge()
    {
        isCharging = false;
        chargeTime = 0;
        gaugeSlider.value = 0; // 슬라이더의 value 초기화
        currentMonster = null;
    }

    private void ShowItemSprite() // 플레이어 머리 위 아이템 스프라이트 표시
    {
        // 아이템 획득 표시용 임시 코드
        GameObject itemObject = Instantiate(itemPrefab, transform.position + Vector3.up * 2, Quaternion.identity);
        SpriteRenderer spriteRenderer = itemObject.GetComponent<SpriteRenderer>();


    }
}

