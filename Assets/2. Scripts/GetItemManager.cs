using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetItemManager : MonoBehaviour
{
    // ���Ϳ� ��ȣ �ۿ�� �÷��̾��� �Ӹ� ���� �������� �������� �� ����.
    // �÷��̾� ������Ʈ �ȿ� Canvas(���� �����̽�) ����� �̹��� Slider ����.

    public Slider gaugeSlider; // UI ������ �����̴�
    private GameObject currentMonster; // ���� ��ȣ�ۿ� ���� ����
    private bool isCharging = false; // ������ ���� �� ����
    private float chargeTime = 0f; // ���� ���� �ð�
    private float maxChargeTime = 2f; // �ִ� ���� �ð�

    public GameObject itemPrefab; // ������ ������, �ӽ� �׽�Ʈ��
    public SpriteRenderer itemSprite; // ������ ��������Ʈ ������, �ӽ� �׽�Ʈ��

    void Update()
    {
           if (isCharging && currentMonster != null)
        {
            if (Input.GetKey(KeyCode.Space)) // �����̽� Ű�� �׽�Ʈ
            {
                Debug.Log("Ű ��������");
                chargeTime += Time.deltaTime;
                gaugeSlider.value = chargeTime / maxChargeTime; // �����̴��� value ������Ʈ

                if (chargeTime >= maxChargeTime)
                {
                    ShowItemSprite();
                    ResetCharge(); // ���� ����
                }
            }
            else
            {
                // Ű���� ���� ���� ���� ����
                ResetCharge();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision) // �÷��̾�� ���� �浹 ����
    {
        if (collision.CompareTag("Monster"))
        {
            Debug.Log("�浹 ����");
            StartCharging(collision.gameObject);
        }
    }

    
    void OnTriggerExit2D(Collider2D collision) // �÷��̾�� ���� ���� �浹 ����
    {
        if (collision.CompareTag("Monster"))
        {
            Debug.Log("�浹 ����");
            ResetCharge();
        }
    }

    // ������ ����
    public void StartCharging(GameObject monster)
    {
        currentMonster = monster;
        isCharging = true;
        chargeTime = 0;
        gaugeSlider.value = 0; // �����̴��� value �ʱ�ȭ
    }

    // ������ ����
    private void ResetCharge()
    {
        isCharging = false;
        chargeTime = 0;
        gaugeSlider.value = 0; // �����̴��� value �ʱ�ȭ
        currentMonster = null;
    }

    private void ShowItemSprite() // �÷��̾� �Ӹ� �� ������ ��������Ʈ ǥ��
    {
        // ������ ȹ�� ǥ�ÿ� �ӽ� �ڵ�
        GameObject itemObject = Instantiate(itemPrefab, transform.position + Vector3.up * 2, Quaternion.identity);
        SpriteRenderer spriteRenderer = itemObject.GetComponent<SpriteRenderer>();


    }
}

