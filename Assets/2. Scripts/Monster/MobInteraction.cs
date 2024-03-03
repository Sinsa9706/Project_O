using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobInteraction : MonoBehaviour
{
    public string mobType; // ������ ������ �ĺ��ϴ� ���ڿ�
    public bool CanInteractWithPlayer { get; private set; } // �÷��̾���� �浹 ����(��ȣ �ۿ� ��������) ����
    public ColliderMark colliderMark; // �� ���͸��� �Ҵ�� ColliderMark ����
    private PlayerInteraction playerInteraction;
    private MobMovement mobMovement; // MobMovement ��ũ��Ʈ ����

    public GameObject uiText; // ��ȣ�ۿ� UI Text ������Ʈ

    public Animator animator; // Dead �ִϸ��̼� ó����

    private void Start()
    {
        uiText.SetActive(false);
        playerInteraction = FindObjectOfType<PlayerInteraction>();
        mobMovement = GetComponent<MobMovement>(); // MobMovement ������Ʈ
        if (playerInteraction != null)
        {
            playerInteraction.onInteraction.AddListener(HandleInteractionComplete);
        }
        else
        {
            Debug.LogError("PlayerInteraction ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }
    private void OnDestroy()
    {

        playerInteraction.onInteraction.RemoveListener(HandleInteractionComplete);
    }

    public void HandleInteractionComplete() // ������ ��ȣ�ۿ��� �Ϸ� �� �� �ൿ&�۵� �ڵ鷯
                                            

        // ������ ��ȣ�ۿ��� �ĺ��ϰ�, ���ڿ� ���� ������ ���� �ٸ� �޼��带 �۵�
    {
        // ���� ���Ͱ� ��ȣ�ۿ� ������ �������� Ȯ��
        if (CanInteractWithPlayer && colliderMark != null)
        {
            if (mobType.Contains("Corgi") || mobType.Contains("Mushroom") || mobType.Contains("Kirby")) // Ư�� ������ ���ڿ� 
            {
                colliderMark.ActiveLoveMark(); // ���� ������ ColliderMark�� �ִ� ActiveLoveMark �޼��� ȣ��
            }
            else if (mobType.Contains("Turtle") || mobType.Contains("Man-Eating"))
            {

                colliderMark.ActiveExclamationMark(); // ��� ��ũ Ȱ��ȭ
                StartCoroutine(HandleDeath()); ;  // ���� ó�� �޼��� ȣ��
            }
            else if (mobType.Contains("Flower") || mobType.Contains("Grape") || mobType.Contains("Herb") || mobType.Contains("Tree"))
            {
                // �Ĺ� ȹ�� ��ƼŬ
                // �Ĺ� ȹ�� ����
            }
            else
            {
                //������ ���� ó��
                colliderMark.ActiveExclamationMark(); // ��� ��ũ Ȱ��ȭ
                mobMovement.StartSurprisedMovement(); // ��� ���� �̵� Ȱ��ȭ
            }
        }
    }

    private IEnumerator HandleDeath() // 
    {
        animator.SetBool("IsDead", true); // ���� �ִϸ��̼� Ȱ��ȭ
        float fadeDuration = 1f; // n�� ���� fade out
        float fadeSpeed = 1f / fadeDuration;
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        for (float t = 0; t < 1f; t += Time.deltaTime * fadeSpeed)
        {
            Color spriteColor = spriteRenderer.color;
            spriteColor.a = 1 - t;
            spriteRenderer.color = spriteColor;
            yield return null;
        }

        Destroy(gameObject); // ������Ʈ �ı�
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            uiText.SetActive(true); // UI Text Ȱ��ȭ
            CanInteractWithPlayer = true; // ��ȣ�ۿ� ���� ����

            // �浹�� �÷��̾��� ��ũ��Ʈ, CanInteract �޼��忡 �浹 ���� ����.
            collision.GetComponent<PlayerInteraction>().CanInteract(this, true);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            uiText.SetActive(false); // UI Text�� ��Ȱ��ȭ
            CanInteractWithPlayer = false; // ��ȣ �ۿ� �Ұ���

            // �÷��̾�� �浹�� �����ٰ� ����
            collision.GetComponent<PlayerInteraction>().CanInteract(this, false);
        }
    }

}
