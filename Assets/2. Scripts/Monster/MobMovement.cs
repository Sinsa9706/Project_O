using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MobMovement : MonoBehaviour
{
    private float speed = 3.0f;
    private Vector3 pos; // ���� ������
    private Animator animator; // ���� ����� �ִϸ�����  ������Ʈ
    private bool isWalking = false; // ���Ͱ� ���� �Ȱ� �ִ��� ����, �⺻�� ���� ����(false)
    public bool isDead = false; // ���� �׾����� ���θ� �����ϴ� ����
    private MobInteraction mobInteraction; // MobInteraction ������Ʈ ����


    void Start()
    {
        animator = GetComponentInChildren<Animator>(); // Animator ������Ʈ�� �ڽ� ������Ʈ���� ã��
        mobInteraction = GetComponent<MobInteraction>();
        if (mobInteraction == null)
        {
            Debug.LogError("MobInteraction ������Ʈ ����");
            return;
        }

        StartCoroutine(Roaming());
    }

    IEnumerator Roaming() // �� ��ȸ
    {
        while (true)
        {
            if (isDead) // ���� ������ ��� ���� ����
                yield break;

            if (mobInteraction.mobType.Contains("Flower") || mobInteraction.mobType.Contains("Grape") ||
                mobInteraction.mobType.Contains("Herb") || mobInteraction.mobType.Contains("Tree") 
                || mobInteraction.mobType.Contains("Man-Eating"))// �Ĺ� ���ʹ� �̵� ���� �������� ����
            {
                yield break;
            }

            if (!isWalking) // ���� ����
            {
                // �̵��� ���ο� ������ ���� ����
                pos = new Vector3(UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(-3f, 3f), transform.position.z);
                isWalking = true; // �ȴ� ���·� ����
                animator.SetBool("IsWalking", true); // �ִϸ����Ϳ� �ȴ� �������� �˸�
            }

            if (isWalking) // �ȴ� ����
            {
                // ���͸� ������ ������ �̵�
                MoveTowards(pos);
            }
            if (Vector3.Distance(transform.position, pos) <= 0.1f) // �������� �����ߴٸ�
            {
                isWalking = false; // ���� ���·� ����
                animator.SetBool("IsWalking", false); // �ִϸ����Ϳ� ���� �������� �˸�

                if (gameObject.name.StartsWith("Mob_Corgi") || gameObject.name.StartsWith("Mob_Mushroom") || gameObject.name.StartsWith("Mob_Kirby")) 
                    // ������Ʈ �̸��� ���� �����Ѵٸ�
                {
                    animator.SetBool("IsSearching", true); // Ž�� �ִϸ��̼� Ȱ��ȭ
                    yield return new WaitForSeconds(2f); // Ž���ϴ� ���� ���
                    animator.SetBool("IsSearching", false); // Ž�� �ִϸ��̼� ��Ȱ��ȭ
                }

                // ������ �ð� ���� ���
                yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 5f));
            }
            else
            {
                yield return null;
            }
        }
    }

    void MoveTowards(Vector3 target) // �Ϲ�����
    {
        if (isDead) return; // ���� ���¸� �̵� ������ �������� ����

        // 1. ������ �������� ���͸� �̵�
        var direction = (target - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime; //   ������ ��ġ�� ������Ʈ


        // 2. �̵� ���⿡ ���� ��������Ʈ ���� ��ȯ : ���� �������� x���� flip 
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (direction.x > 0)  // position x�� ���, ������ �̵� ��
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0) // position x�� ����, ���� �̵� ��
        {
            spriteRenderer.flipX = true;
        }

    }

    public void StartSurprisedMovement() // ��� ������ �̵�
    {
        if (!isWalking && !isDead) // �Ȱ� ���� ���� + �� �׾��� ��
        {
            // ��� ���¿����� ������ ������ ����
            pos = new Vector3(UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(-3f, 3f), transform.position.z);
            isWalking = true;
            animator.SetBool("IsSurprised", true); // ��� ���� �ִϸ��̼� Ȱ��ȭ

            // �������� �̵� ����, �̵��� �Ϸ�Ǹ� �ݹ� �Լ��� ȣ��
            StartCoroutine(MoveToPosition(pos, () =>
            {
                // �̵��� ������ �� �ݹ�
                animator.SetBool("IsSurprised", false);
                isWalking = false;
            }));
        }
    }

    IEnumerator MoveToPosition(Vector3 target, Action action)
    {
        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            // ���� ��� �� �̵�
            Vector3 direction = (target - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // ��������Ʈ�� ������ �̵� ���⿡ ���߾� ������ (�������� �⺻�����̸� �̹� ������)
            SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.flipX = direction.x < 0; // ���� �� �� �ø�

            yield return null;
        }
        action?.Invoke();
    }
}

