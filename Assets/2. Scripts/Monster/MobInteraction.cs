using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobInteraction : MonoBehaviour
{
    public string mobType; // ëª¬ìŠ¤??? í˜• ?ë³„
    public bool CanInteractWithPlayer { get; private set; } // ?Œë ˆ?´ì–´?€???í˜¸?‘ìš© ê°€???¬ë?ë¥?ì¶”ì ?©ë‹ˆ??
    public ColliderMark colliderMark; // ??ëª¬ìŠ¤?°ì— ? ë‹¹??ColliderMark ì»´í¬?ŒíŠ¸??ì°¸ì¡°
    private PlayerInteraction playerInteraction;
    private MobMovement mobMovement; // MobMovement?¤í¬ë¦½íŠ¸ ì°¸ì¡°

    public GameObject uiText; // ?í˜¸?‘ìš© ???œì‹œ?˜ëŠ” UI ?ìŠ¤???¤ë¸Œ?íŠ¸

    public Animator animator; // Dead ? ë‹ˆë©”ì´??ì²˜ë¦¬ë¥??„í•œ Animator ì»´í¬?ŒíŠ¸ ì°¸ì¡°

    private void Start()
    {
        uiText.SetActive(false);
        playerInteraction = FindObjectOfType<PlayerInteraction>();
        mobMovement = GetComponent<MobMovement>(); // MobMovement ì»´í¬?ŒíŠ¸ë¥?? ë‹¹
        if (playerInteraction != null)
        {
            playerInteraction.onInteraction.AddListener(HandleInteractionComplete);
        }
        else
        {
            Debug.LogError("PlayerInteractiond ì»´í¬?ŒíŠ¸ ?†ìŒ.");
        }
    }
    private void OnDestroy()
    {

        playerInteraction.onInteraction.RemoveListener(HandleInteractionComplete);
    }

    public void HandleInteractionComplete()  // ?í˜¸?‘ìš©???„ë£Œ?˜ì—ˆ?????¸ì¶œ


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
                //?ë¬¼ ê´€??ë¡œì§
            }
            else
            {
                
                colliderMark.ActiveExclamationMark(); // ?˜ë¨¸ì§€ ëª¬ìŠ¤?°ëŠ” ?€??ë§ˆí¬ë¥??œì„±?”í•˜ê³??€???íƒœë¡??´ë™???œì‘
                mobMovement.StartSurprisedMovement(); 
            }
        }
    }

    private IEnumerator HandleDeath() //  ì£½ìŒ ì²˜ë¦¬ë¥??„í•œ ì½”ë£¨??
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
            uiText.SetActive(true);// ?Œë ˆ?´ì–´ê°€ ê·¼ì ‘?˜ë©´ UI ?ìŠ¤?¸ë? ?œì„±??
            CanInteractWithPlayer = true; // ?í˜¸?‘ìš© ê°€???íƒœ

            // ?Œë ˆ?´ì–´?ê²Œ ?í˜¸?‘ìš© ê°€?¥í•˜?¤ê³  ?Œë¦¼
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
