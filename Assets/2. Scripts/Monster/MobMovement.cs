using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : MonoBehaviour
{
    private float speed = 3.0f;
    private Vector3 pos; // ���� ������
    private Animator animator; // ���� ����� �ִϸ�����  ������Ʈ
    private bool isWalking = false; // ���Ͱ� ���� �Ȱ� �ִ��� ����, �⺻�� ���� ����(false)

    void Start()
    {
        animator = GetComponentInChildren<Animator>(); // Animator ������Ʈ�� �ڽ� ������Ʈ���� ã��
        StartCoroutine(Roaming());
    }

    IEnumerator Roaming() // �� ��ȸ
    {
        while (true)
        {
            if (!isWalking) // ���� ����
            {
                // �̵��� ���ο� ������ ���� ����
                pos = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), transform.position.z);
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

                if (gameObject.name.StartsWith("Mob_Corgi")) // ������Ʈ �̸��� "Mob_Corgi"��� ���ڿ��� �ִٸ�
                {
                    animator.SetBool("IsSearching", true); // Ž�� �ִϸ��̼� Ȱ��ȭ
                    yield return new WaitForSeconds(2f); // Ž���ϴ� ���� ���
                    animator.SetBool("IsSearching", false); // Ž�� �ִϸ��̼� ��Ȱ��ȭ
                }

                // ������ �ð� ���� ���
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
}
