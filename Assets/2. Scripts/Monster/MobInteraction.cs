using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobInteraction : MonoBehaviour
{
    public bool CanInteractWithPlayer { get; private set; } // �÷��̾���� �浹 ����(��ȣ �ۿ� ��������) ����

    public GameObject uiText; // ��ȣ�ۿ� UI Text ������Ʈ

    private void Start()
    {
        uiText.SetActive(false);
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
