using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MobMovement : MonoBehaviour
{
    private float speed = 3.0f;
    private float RunSpeed = 5.0f; 
    private Vector3 startPos; // 몬스터가 처음 생성된 위치
    private Vector3 randomPos; // 몬스터가 랜덤하게 이동할 위치
    public float mobRadius = 10f; // 몬스터가 이동할 수 있는 최대 범위

    private Animator animator; // 몹에 연결된 애니메이터  컴포넌트
    private bool isWalking = false; // 몬스터가 현재 걷고 있는지 여부, 기본은 정지 상태(false)
    public bool isDead = false; // 몹이 죽었는지 여부를 추적하는 변수
    private MobInteraction mobInteraction; // MobInteraction 컴포넌트 참조




    void Start()
    {
        animator = GetComponentInChildren<Animator>(); // Animator 컴포넌트를 자식 오브젝트에서 찾음
        mobInteraction = GetComponent<MobInteraction>();
        if (mobInteraction == null)
        {
            Debug.LogError("MobInteraction 컴포넌트 없음");
            return;
        }
       
        startPos = transform.position; // 초기 위치 설정
        StartCoroutine(Roaming());
    }

    IEnumerator Roaming() // 몹 배회
    {
        while (true)
        {
            if (isDead) // 죽음 상태인 경우 루프 중지
                yield break;

            if (mobInteraction.mobType.Contains("Flower") || mobInteraction.mobType.Contains("Grape") ||
                mobInteraction.mobType.Contains("Herb") || mobInteraction.mobType.Contains("Tree") 
                || mobInteraction.mobType.Contains("Man-Eating"))// 식물 몬스터는 이동 로직 수행하지 않음
            {
                yield break;
            }

            if (!isWalking) // 멈춤 상태
            {
                // 이동할 새로운 목적지 랜덤 설정
                randomPos = startPos + (Vector3)(UnityEngine.Random.insideUnitCircle * mobRadius);
                isWalking = true; // 걷는 상태로 설정
                animator.SetBool("IsWalking", true); // 애니메이터에 걷는 상태임을 알림
            }

            if (isWalking) // 걷는 상태
            {
                // 몬스터를 목적지 쪽으로 이동
                MoveTowards(randomPos);
            }
            if (Vector3.Distance(transform.position, randomPos) <= 0.1f) // 목적지에 도달했다면
            {
                isWalking = false; // 멈춤 상태로 설정
                animator.SetBool("IsWalking", false); // 애니메이터에 멈춤 상태임을 알림

                if (gameObject.name.StartsWith("Mob_Corgi") || gameObject.name.StartsWith("Mob_Mushroom") || gameObject.name.StartsWith("Mob_Kirby")) 
                    // 오브젝트 이름이 위로 시작한다면
                {
                    animator.SetBool("IsSearching", true); // 탐색 애니메이션 활성화
                    yield return new WaitForSeconds(2f); // 탐색하는 동안 대기
                    animator.SetBool("IsSearching", false); // 탐색 애니메이션 비활성화
                }

                // 랜덤한 시간 동안 대기
                yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 5f));
            }
            else
            {
                yield return null;
            }
        }
    }

    void MoveTowards(Vector3 target) // 일반적인
    {
        if (isDead) return; // 죽은 상태면 이동 로직을 수행하지 않음

        // 1. 목적지 방향으로 몬스터를 이동
        var direction = (target - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime; //   몬스터의 위치를 업데이트


        // 2. 이동 방향에 따른 스프라이트 방향 전환 : 하위 렌더러의 x축을 flip 
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (direction.x > 0)  // position x가 양수, 오른쪽 이동 시
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0) // position x가 음수, 왼쪽 이동 시
        {
            spriteRenderer.flipX = true;
        }

    }

    public void StartSurprisedMovement() // 놀란 상태의 이동
    {
        if (!isWalking && !isDead) // 걷고 있지 않음 + 안 죽었을 때
        {
            // startPos를 중심으로 roamRadius 내에서 랜덤한 목적지 설정
            Vector3 randomPos = startPos + new Vector3(
            UnityEngine.Random.Range(-mobRadius, mobRadius), 
            UnityEngine.Random.Range(-mobRadius, mobRadius), 
            0f
        );

            isWalking = true;
            animator.SetBool("IsSurprised", true); // 놀란 상태 애니메이션 활성화

            // 목적지로 이동 시작, 이동이 완료되면 콜백 함수를 호출
            StartCoroutine(MoveToPosition(randomPos, () =>
            {
                // 이동이 끝났을 때 콜백
                animator.SetBool("IsSurprised", false);
                isWalking = false;
            }));
        }
    }

    IEnumerator MoveToPosition(Vector3 target, Action onComplete)
    {
        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            // 방향 계산 및 이동
            Vector3 direction = (target - transform.position).normalized;
            transform.position += direction * RunSpeed * Time.deltaTime;

            // 스프라이트의 방향을 이동 방향에 맞추어 뒤집음
            SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.flipX = direction.x < 0; // 왼쪽 갈 때 플립

            yield return null;
        }
        onComplete?.Invoke();
    }
}

