using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColliderMark : MonoBehaviour
{
    public Image questionMark; // ����ǥ �̹���
    public Animator questionMarkAnimator; // ����ǥ �̹����� ���� Animator ������Ʈ

    public Image loveMark; // ���� �̹���
    public Animator loveMarkAnimator; // ���� �ִϸ�����

    public PlayerInteraction playerInteraction; // �÷��̾� ��ȣ�ۿ� ��ũ��Ʈ ����

    void Start()
    {
        //if (playerInteraction == null)
        //{
        //    Debug.LogError("ColliderMark: PlayerInteraction ������Ʈ�� �Ҵ���� �ʾҽ��ϴ�.", this);
        //    return; // PlayerInteraction ������Ʈ�� ������, ���� �޽����� ����ϰ� �ʱ�ȭ�� �ߴ��մϴ�.
        //}

        if (questionMark != null)
        {
            questionMark.enabled = false; // ����ǥ �̹��� ��Ȱ��ȭ
        }
        if (loveMark != null)
        {
            loveMark.enabled = false; // ���� �̹��� ��Ȱ��ȭ
        }
        playerInteraction.onInteraction.AddListener(ActiveLoveMark); // �̺�Ʈ ������ �߰�

    }
    public void ActiveLoveMark() // �ν����� �̺�Ʈ �����ʿ� �����Ϸ��� public���� �ٲ������ (�Ϸ�)
    {
        if (loveMark != null)
        {
            loveMark.enabled = true;

            if(loveMarkAnimator != null)
            {
                loveMarkAnimator.Rebind(); // �ִϸ����� �ʱ�ȭ
                loveMarkAnimator.Play("Love", -1, 0f); // �ش� �ִϸ��̼� ���
            }
            StartCoroutine(DeactivateImage(loveMark, loveMarkAnimator, 2.0f)); // 2���� ��Ȱ��ȭ
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ����ǥ ��ũ ������Ʈ�� �ݶ��̴�, �÷��̾�� �浹�ߴ��� Ȯ��
        if (collision.CompareTag("Player") && questionMark != null)
        {
            questionMark.enabled = true; // ����ǥ �̹��� Ȱ��ȭ

            if (questionMarkAnimator != null)
            {
                // Animator ������Ʈ�� ����Ͽ� �ִϸ��̼��� ����
                questionMarkAnimator.Rebind(); // Animator ������Ʈ ���� �ʱ�ȭ(ó������)
                
                questionMarkAnimator.Play("Question", -1, 0f);
                // "Question" �̶�� �ִϸ��̼� / -1 : ���̾�(-1�� ��� ���̾�) / 0f : �ִϸ��̼�(�ð�)�� ó������.
                // ��� : ù �����Ӻ��� ����Ѵ�
            }
            StartCoroutine(DeactivateImage(questionMark, questionMarkAnimator, 1.0f));
            // ������ �ð� 1�� �Ŀ� �̹����� �ִϸ��̼��� ��Ȱ��ȭ
        }
    }
    private IEnumerator DeactivateImage(Image image, Animator animator, float delay) // �Ű����� - �̹��� / �ִϸ����� / ���� �ð�
    {
        yield return new WaitForSeconds(delay);

        if (animator != null)
        {
            animator.Rebind(); // �ִϸ����� ���� ����
        }

        image.enabled = false; // �̹����� ��Ȱ��ȭ
    }
}
