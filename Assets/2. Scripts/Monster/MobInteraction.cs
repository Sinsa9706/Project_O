using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobInteraction : MonoBehaviour
{
    public string mobType; // λͺ¬μ€??? ν ?λ³
    public bool CanInteractWithPlayer { get; private set; } // ?λ ?΄μ΄????νΈ?μ© κ°???¬λ?λ₯?μΆμ ?©λ??
    public ColliderMark colliderMark; // ??λͺ¬μ€?°μ ? λΉ??ColliderMark μ»΄ν¬?νΈ??μ°Έμ‘°
    private PlayerInteraction playerInteraction;
    private MobMovement mobMovement; // MobMovement?€ν¬λ¦½νΈ μ°Έμ‘°

    public GameObject uiText; // ?νΈ?μ© ???μ?λ UI ?μ€???€λΈ?νΈ

    public Animator animator; // Dead ? λλ©μ΄??μ²λ¦¬λ₯??ν Animator μ»΄ν¬?νΈ μ°Έμ‘°

    private void Start()
    {
        uiText.SetActive(false);
        playerInteraction = FindObjectOfType<PlayerInteraction>();
        mobMovement = GetComponent<MobMovement>(); // MobMovement μ»΄ν¬?νΈλ₯?? λΉ
        if (playerInteraction != null)
        {
            playerInteraction.onInteraction.AddListener(HandleInteractionComplete);
        }
        else
        {
            Debug.LogError("PlayerInteractiond μ»΄ν¬?νΈ ?μ.");
        }
    }
    private void OnDestroy()
    {

        playerInteraction.onInteraction.RemoveListener(HandleInteractionComplete);
    }

    public void HandleInteractionComplete()  // ?νΈ?μ©???λ£?μ?????ΈμΆ


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
                //?λ¬Ό κ΄??λ‘μ§
            }
            else
            {
                
                colliderMark.ActiveExclamationMark(); // ?λ¨Έμ§ λͺ¬μ€?°λ ???λ§ν¬λ₯??μ±?νκ³?????νλ‘??΄λ???μ
                mobMovement.StartSurprisedMovement(); 
            }
        }
    }

    private IEnumerator HandleDeath() //  μ£½μ μ²λ¦¬λ₯??ν μ½λ£¨??
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
            uiText.SetActive(true);// ?λ ?΄μ΄κ° κ·Όμ ?λ©΄ UI ?μ€?Έλ? ?μ±??
            CanInteractWithPlayer = true; // ?νΈ?μ© κ°???ν

            // ?λ ?΄μ΄?κ² ?νΈ?μ© κ°?₯ν?€κ³  ?λ¦Ό
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
