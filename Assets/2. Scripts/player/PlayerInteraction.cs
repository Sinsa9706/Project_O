using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{

    [SerializeField] private Slider interactionSlider; // ��ȣ�ۿ� �����̴� ����
    private Coroutine interactionCoroutine; // ��ȣ�ۿ� �ڷ�ƾ ����

    private List<MobInteraction> interactableMobs = new List<MobInteraction>();  // ��ȣ�ۿ� ���� ����Ʈ, �ӽ�

    // ��ȣ�ۿ� �Ϸ� �̺�Ʈ�� non-static���� ����
    public UnityEvent onInteraction = new UnityEvent();

    private bool isInteracting = false;  // ��ȣ�ۿ� ������ ����
    private bool isPressingInteractKey = false; // ��ȣ�ۿ� Ű�� ������ �ִ��� ����


    void Start()
    {
        interactionSlider.gameObject.SetActive(false); // �����̴� �����
    }

    void Update()
    {
        // Input Ȯ�� (Input System)
        isPressingInteractKey = Keyboard.current.zKey.isPressed;

        if (isPressingInteractKey && !isInteracting && interactableMobs.Count > 0)
        {
            StartInteraction(interactableMobs[0]);
        }
        else if (!isPressingInteractKey && isInteracting)
        {
            StopInteraction();
        }
    }


    // SendMessage�� ���� �Ű����� ���� �޼��� �߰�
    public void OnInteraction()
    {
        // ��ȣ�ۿ� ������ ���Ͱ� ���� ���� ��ȣ�ۿ� ����
        if (interactableMobs.Count > 0)
        {
            interactionSlider.gameObject.SetActive(true);
            interactionCoroutine = StartCoroutine(FillSliderOverTime(1.0f, interactableMobs[0])); // �ڷ�ƾ ����, �����ð� 1��
        }
    }
    public void StartInteraction(MobInteraction interactedMob)
    {
        // ��ȣ�ۿ� ������ ���Ͱ� ���� ���� ��ȣ�ۿ� ����
        if (!isInteracting && interactableMobs.Contains(interactedMob))
        {
            isInteracting = true; // ��ȣ�ۿ� ����
            interactionSlider.gameObject.SetActive(true);
            // ���� ��ȣ�ۿ��ϴ� MobInteraction �ν��Ͻ��� �ڷ�ƾ�� ����
            interactionCoroutine = StartCoroutine(FillSliderOverTime(1.0f, interactedMob));
        }
    }



    public void CanInteract(MobInteraction mob, bool canInteract)
    {
        if (canInteract)
        {
            if (!interactableMobs.Contains(mob))
            {
                interactableMobs.Add(mob);
            }
        }
        else
        {
            if (interactionCoroutine != null)
            {
                StopCoroutine(interactionCoroutine);
                interactionSlider.value = 0; // �����̴� ����
                interactionSlider.gameObject.SetActive(false);
            }
            interactableMobs.Remove(mob);
        }
    }


    private IEnumerator FillSliderOverTime(float duration, MobInteraction interactedMob)
    {
        float time = 0;
        while (time < duration && isInteracting) // isInteracting�� üũ�Ͽ� Ű�� ���� �ߴ�
        {
            if (isPressingInteractKey) // Ű�� ������ �ִ��� Ȯ��
            {
                time += Time.deltaTime;
                interactionSlider.value = time / duration;
            }
            else
            {
                // Ű�� �� ���, �����̴� ���� �� �ʱ�ȭ
                StopInteraction();
                yield break; // �ڷ�ƾ ����
            }
            yield return null;
        }
                    
        if (isInteracting) // ��ȣ�ۿ��� ������ ���� ���� ��� �Ϸ� ó��
        {
            // ��ȣ�ۿ� �Ϸ� ��, Ư�� ���Ϳ� ���ؼ��� �̺�Ʈ�� �߻�
            if (interactedMob != null)
            {
                interactedMob.HandleInteractionComplete();
            }
            Debug.Log("���� �Ϸ�, ������ ȹ�� �̹���, �κ��丮 ȹ�� ���� ����");
            //onInteraction.Invoke(); // ��ȣ�ۿ� �Ϸ� �̺�Ʈ ȣ��


            // ��ȣ�ۿ� �Ϸ� �� Z Ű �Է� �ʱ�ȭ
            isPressingInteractKey = false;
            isInteracting = false;
            interactionSlider.gameObject.SetActive(false);  // ���� �Ϸ�� �����̴� ����
            interactionSlider.value = 0;  // �����̴� �� �ʱ�ȭ
        }
        // TODO: ������ ȹ�� �̹��� ǥ�� ����
        // TODO: �κ��丮 ������ �߰� ����
    }

    private void StopInteraction()
    {
        if (interactionCoroutine != null)
        {
            StopCoroutine(interactionCoroutine);
            interactionCoroutine = null;
        }
        interactionSlider.gameObject.SetActive(false);
        interactionSlider.value = 0;
        isInteracting = false; // ��ȣ�ۿ� ����
    }










    //public void CanInteract(MobInteraction mob, bool canInteract)  // Unity Event�׼� ��
    //{
    //    if (canInteract)
    //    {
    //        // TODO: ���� ���� ���� �߰�
    //        if (!interactableMobs.Contains(mob))
    //        {
    //            interactableMobs.Add(mob);
    //        }
    //    }
    //    else
    //    {
    //        interactableMobs.Remove(mob);
    //    }
    //}

    //public void OnInteraction(InputAction.CallbackContext context) // Unity �̺�Ʈ�׼� ��
    //{
    //    // ��ȣ�ۿ� ������ ���Ͱ� ���� ���� ��ȣ�ۿ� ����
    //    if (interactableMobs.Count > 0)
    //    {
    //        if (context.started)
    //        {
    //            interactionSlider.gameObject.SetActive(true);
    //            interactionCoroutine = StartCoroutine(FillSliderOverTime(1.0f)); // �ڷ�ƾ ����, �����ð� 1��
    //        }
    //        else if (context.canceled)
    //        {
    //            interactionSlider.gameObject.SetActive(false);
    //            StopCoroutine(interactionCoroutine); // �ڷ�ƾ ����
    //            interactionSlider.value = 0; // �����̴� �� �ʱ�ȭ
    //        }
    //    }
    //}
}