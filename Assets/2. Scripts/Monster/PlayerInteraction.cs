using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    
    private Vector2 moveDirection;

    [SerializeField]
    private float moveForce = 10f;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb; // Rigidbody2D ������Ʈ�� ���� ����

    [SerializeField] private Slider interactionSlider; // ��ȣ�ۿ� �����̴� ����
    private Coroutine interactionCoroutine; // ��ȣ�ۿ� �ڷ�ƾ ����
    
    // private bool isInteracting; // ��ȣ�ۿ� �� ���θ� ����

    private List<MobInteraction> interactableMobs = new List<MobInteraction>();  // ��ȣ�ۿ� ���� ����Ʈ, �ӽ�
    public UnityEvent onInteraction;// ��ȣ�ۿ� �Ϸ� �̺�Ʈ

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        interactionSlider.gameObject.SetActive(false); // �����̴� �����
    }

    void FixedUpdate()
    {
        MoveCharacter(moveDirection);
    }

    private void MoveCharacter(Vector2 direction)
    {
        rb.AddForce(direction * moveForce);

        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        // ��ȣ�ۿ� ������ ���Ͱ� ���� ���� ��ȣ�ۿ� ����
        if (interactableMobs.Count > 0)
        {
            if (context.started)
            {
                interactionSlider.gameObject.SetActive(true);
                interactionCoroutine = StartCoroutine(FillSliderOverTime(1.0f)); // �ڷ�ƾ ����, �����ð� 1��
            }
            else if (context.canceled)
            {
                interactionSlider.gameObject.SetActive(false);
                StopCoroutine(interactionCoroutine); // �ڷ�ƾ ����
                interactionSlider.value = 0; // �����̴� �� �ʱ�ȭ
            }
        }
    }

    public void CanInteract(MobInteraction mob, bool canInteract)
    {
        if (canInteract)
        {
            // TODO: ���� ���� ���� �߰�
            if (!interactableMobs.Contains(mob))
            {
                interactableMobs.Add(mob);
            }
        }
        else
        {
            interactableMobs.Remove(mob);
        }
    }

    private IEnumerator FillSliderOverTime(float duration)
    {
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            interactionSlider.value = time / duration;
            yield return null;
        }
        Debug.Log("���� �Ϸ�, ������ ȹ�� �̹���, �κ��丮 ȹ�� ���� ����");
        
        onInteraction.Invoke(); // ��ȣ�ۿ� �Ϸ� �̺�Ʈ ȣ��

        interactionSlider.gameObject.SetActive(false);  // ���� �Ϸ�� �����̴� ����
        interactionSlider.value = 0; // �����̴� �� �ʱ�ȭ

        // TODO: ������ ȹ�� �̹��� ǥ�� ����
        // TODO: �κ��丮 ������ �߰� ����
    }
}