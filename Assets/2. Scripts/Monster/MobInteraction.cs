using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobInteraction : MonoBehaviour
{
    public string mobType; // 몬스???�형 ?�별
    public bool CanInteractWithPlayer { get; private set; } // ?�레?�어?�???�호?�용 가???��?�?추적?�니??
    public ColliderMark colliderMark; // ??몬스?�에 ?�당??ColliderMark 컴포?�트??참조
    private PlayerInteraction playerInteraction;
    private MobMovement mobMovement; // MobMovement?�크립트 참조

    public GameObject uiText; // ?�호?�용 ???�시?�는 UI ?�스???�브?�트

    public Animator animator; // Dead ?�니메이??처리�??�한 Animator 컴포?�트 참조

    private void Start()
    {
        uiText.SetActive(false);
        playerInteraction = FindObjectOfType<PlayerInteraction>();
        mobMovement = GetComponent<MobMovement>(); // MobMovement 컴포?�트�??�당
        if (playerInteraction != null)
        {
            playerInteraction.onInteraction.AddListener(HandleInteractionComplete);
        }
        else
        {
            Debug.LogError("PlayerInteractiond 컴포?�트 ?�음.");
        }
    }
    private void OnDestroy()
    {

        playerInteraction.onInteraction.RemoveListener(HandleInteractionComplete);
    }

    public void HandleInteractionComplete()  // ?�호?�용???�료?�었?????�출


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
                //?�물 관??로직
            }
            else
            {
                
                colliderMark.ActiveExclamationMark(); // ?�머지 몬스?�는 ?�??마크�??�성?�하�??�???�태�??�동???�작
                mobMovement.StartSurprisedMovement(); 
            }
        }
    }

    private IEnumerator HandleDeath() //  죽음 처리�??�한 코루??
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
            uiText.SetActive(true);// ?�레?�어가 근접?�면 UI ?�스?��? ?�성??
            CanInteractWithPlayer = true; // ?�호?�용 가???�태

            // ?�레?�어?�게 ?�호?�용 가?�하?�고 ?�림
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
