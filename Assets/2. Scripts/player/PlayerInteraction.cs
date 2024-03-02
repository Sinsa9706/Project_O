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

    // private bool isInteracting; // ��ȣ�ۿ� �� ���θ� ����
    private List<MobInteraction> interactableMobs = new List<MobInteraction>();  // ��ȣ�ۿ� ���� ����Ʈ, �ӽ�
    public UnityEvent onInteraction;// ��ȣ�ۿ� �Ϸ� �̺�Ʈ

    void Start()
    {
        interactionSlider.gameObject.SetActive(false); // �����̴� �����
    }




    // SendMessage�� ���� �Ű����� ���� �޼��� �߰�
    public void OnInteraction()
    {
        // ��ȣ�ۿ� ������ ���Ͱ� ���� ���� ��ȣ�ۿ� ����
        if (interactableMobs.Count > 0)
        {
            interactionSlider.gameObject.SetActive(true);
            interactionCoroutine = StartCoroutine(FillSliderOverTime(1.0f)); // �ڷ�ƾ ����, �����ð� 1��
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