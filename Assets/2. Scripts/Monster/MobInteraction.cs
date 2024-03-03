using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobInteraction : MonoBehaviour
{
    public string mobType; // 몬스터 유형 식별
    public bool CanInteractWithPlayer { get; private set; } // 플레이어와의 상호작용 가능 여부를 추적합니다.
    public ColliderMark colliderMark; // 이 몬스터에 할당된 ColliderMark 컴포넌트의 참조
    private PlayerInteraction playerInteraction;
    private MobMovement mobMovement; // MobMovement스크립트 참조

    public GameObject uiText; // 상호작용 시 표시되는 UI 텍스트 오브젝트

    public Animator animator; // Dead 애니메이션 처리를 위한 Animator 컴포넌트 참조

    private void Start()
    {
        uiText.SetActive(false);
        playerInteraction = FindObjectOfType<PlayerInteraction>();
        mobMovement = GetComponent<MobMovement>(); // MobMovement 컴포넌트를 할당
        if (playerInteraction != null)
        {
            playerInteraction.onInteraction.AddListener(HandleInteractionComplete);
        }
        else
        {
            Debug.LogError("PlayerInteractiond 컴포넌트 없음.");
        }
    }
    private void OnDestroy()
    {

        playerInteraction.onInteraction.RemoveListener(HandleInteractionComplete);
    }

    public void HandleInteractionComplete()  // 상호작용이 완료되었을 때 호출


    {
        if (CanInteractWithPlayer && colliderMark != null)
        {
            if (mobType.Contains("Corgi") || mobType.Contains("Mushroom") || mobType.Contains("Kirby")) 
            {
                colliderMark.ActiveLoveMark(); 
            }
            else if (mobType.Contains("Turtle") || mobType.Contains("Man-Eating"))
            {

                colliderMark.ActiveExclamationMark();
                StartCoroutine(HandleDeath()); ; 
            }
            else if (mobType.Contains("Flower") || mobType.Contains("Grape") || mobType.Contains("Herb") || mobType.Contains("Tree"))
            {
                //식물 관련 로직
            }
            else
            {
                
                colliderMark.ActiveExclamationMark(); // 나머지 몬스터는 놀람 마크를 활성화하고 놀람 상태로 이동을 시작
                mobMovement.StartSurprisedMovement(); 
            }
        }
    }

    private IEnumerator HandleDeath() //  죽음 처리를 위한 코루틴
    {
        animator.SetBool("IsDead", true); 
        float fadeDuration = 1f; 
        float fadeSpeed = 1f / fadeDuration;
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        for (float t = 0; t < 1f; t += Time.deltaTime * fadeSpeed)
        {
            Color spriteColor = spriteRenderer.color;
            spriteColor.a = 1 - t;
            spriteRenderer.color = spriteColor;
            yield return null;
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            uiText.SetActive(true);// 플레이어가 근접하면 UI 텍스트를 활성화
            CanInteractWithPlayer = true; // 상호작용 가능 상태

            // 플레이어에게 상호작용 가능하다고 알림
            collision.GetComponent<PlayerInteraction>().CanInteract(this, true);

        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.CompareTag("Player"))
        {
            uiText.SetActive(false);
            CanInteractWithPlayer = false; 

            collision.GetComponent<PlayerInteraction>().CanInteract(this, false);
        }
    }

}
