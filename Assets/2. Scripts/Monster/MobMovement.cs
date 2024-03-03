using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : MonoBehaviour
{
    private float speed = 3.0f;
    private Vector3 pos; // 몬스터 목적지
    private Animator animator; // 몹에 연결된 애니메이터  컴포넌트
    private bool isWalking = false; // 몬스터가 현재 걷고 있는지 여부, 기본은 정지 상태(false)

    void Start()
    {
        animator = GetComponentInChildren<Animator>(); // Animator 컴포넌트를 자식 오브젝트에서 찾음
        StartCoroutine(Roaming());
    }

    IEnumerator Roaming() // 몹 배회
    {
        while (true)
        {
            if (!isWalking) // 멈춤 상태
            {
                // 이동할 새로운 목적지 랜덤 설정
                pos = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), transform.position.z);
                isWalking = true; // 걷는 상태로 설정
                animator.SetBool("IsWalking", true); // 애니메이터에 걷는 상태임을 알림
            }

            if (isWalking) // 걷는 상태
            {
                // 몬스터를 목적지 쪽으로 이동
                MoveTowards(pos);
            }
            if (Vector3.Distance(transform.position, pos) <= 0.1f) // 목적지에 도달했다면
            {
                isWalking = false; // 멈춤 상태로 설정
                animator.SetBool("IsWalking", false); // 애니메이터에 멈춤 상태임을 알림

                if (gameObject.name.StartsWith("Mob_Corgi")) // 오브젝트 이름에 "Mob_Corgi"라는 문자열이 있다면
                {
                    animator.SetBool("IsSearching", true); // 탐색 애니메이션 활성화
                    yield return new WaitForSeconds(2f); // 탐색하는 동안 대기
                    animator.SetBool("IsSearching", false); // 탐색 애니메이션 비활성화
                }

                // 랜덤한 시간 동안 대기
                yield return new WaitForSeconds(Random.Range(3f, 5f));
            }
            else
            {
                yield return null;
            }
        }
    }

    void MoveTowards(Vector3 target)
    {
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
}
