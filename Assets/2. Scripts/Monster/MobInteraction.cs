using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobInteraction : MonoBehaviour
{
    public string mobType; // 몬스터의 유형을 식별하는 문자열
    public bool CanInteractWithPlayer { get; private set; } // 플레이어와의 충돌 상태(상호 작용 가능한지) 추적
    public ColliderMark colliderMark; // 각 몬스터마다 할당된 ColliderMark 참조
    private PlayerInteraction playerInteraction;
    private MobMovement mobMovement; // MobMovement 스크립트 참조

    public GameObject uiText; // 상호작용 UI Text 오브젝트

    public Animator animator; // Dead 애니메이션 처리용

    private void Start()
    {
        uiText.SetActive(false);
        playerInteraction = FindObjectOfType<PlayerInteraction>();
        mobMovement = GetComponent<MobMovement>(); // MobMovement 컴포넌트
        if (playerInteraction != null)
        {
            playerInteraction.onInteraction.AddListener(HandleInteractionComplete);
        }
        else
        {
            Debug.LogError("PlayerInteraction 컴포넌트를 찾을 수 없습니다.");
        }
    }
    private void OnDestroy()
    {

        playerInteraction.onInteraction.RemoveListener(HandleInteractionComplete);
    }

    public void HandleInteractionComplete() // 몬스터의 상호작용이 완료 된 후 행동&작동 핸들러
                                            

        // 몬스터의 상호작용을 식별하고, 문자열 포함 유형에 따라 다른 메서드를 작동
    {
        // 현재 몬스터가 상호작용 가능한 상태인지 확인
        if (CanInteractWithPlayer && colliderMark != null)
        {
            if (mobType.Contains("Corgi") || mobType.Contains("Mushroom") || mobType.Contains("Kirby")) // 특정 몬스터의 문자열
            {
                colliderMark.ActiveLoveMark(); // 현재 몬스터의 ColliderMark에 있는 ActiveLoveMark 메서드 호출
            }
            else if (mobType.Contains("Turtle") || mobType.Contains("Man-Eating"))
            {

                colliderMark.ActiveExclamationMark(); // 놀람 마크 활성화
                StartCoroutine(HandleDeath()); ;  // 죽음 처리 메서드 호출
            }
            else if (mobType.Contains("Flower") || mobType.Contains("Grape") || mobType.Contains("Herb") || mobType.Contains("Tree"))
            {// 식물이면 작동

                // 식물 획득 파티클
                // 식물 획득 로직
            }
            else
            {
                //나머지 몬스터 처리
                colliderMark.ActiveExclamationMark(); // 놀람 마크 활성화
                mobMovement.StartSurprisedMovement(); // 놀람 상태 이동 활성화
            }
        }
    }

    private IEnumerator HandleDeath() // 
    {
        animator.SetBool("IsDead", true); // 죽음 애니메이션 활성화
        float fadeDuration = 1f; // n초 동안 fade out
        float fadeSpeed = 1f / fadeDuration;
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        for (float t = 0; t < 1f; t += Time.deltaTime * fadeSpeed)
        {
            Color spriteColor = spriteRenderer.color;
            spriteColor.a = 1 - t;
            spriteRenderer.color = spriteColor;
            yield return null;
        }

        Destroy(gameObject); // 오브젝트 파괴
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            uiText.SetActive(true); // UI Text 활성화
            CanInteractWithPlayer = true; // 상호작용 가능 상태

            // 충돌한 플레이어의 스크립트, CanInteract 메서드에 충돌 상태 전달.
            collision.GetComponent<PlayerInteraction>().CanInteract(this, true);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            uiText.SetActive(false); // UI Text를 비활성화
            CanInteractWithPlayer = false; // 상호 작용 불가능

            // 플레이어에게 충돌이 끝났다고 전달
            collision.GetComponent<PlayerInteraction>().CanInteract(this, false);
        }
    }

}
