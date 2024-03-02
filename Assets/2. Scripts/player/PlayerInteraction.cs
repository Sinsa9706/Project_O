using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{

    [SerializeField] private Slider interactionSlider; // 상호작용 슬라이더 참조
    private Coroutine interactionCoroutine; // 상호작용 코루틴 참조

    // private bool isInteracting; // 상호작용 중 여부를 추적
    private List<MobInteraction> interactableMobs = new List<MobInteraction>();  // 상호작용 몬스터 리스트, 임시
    public UnityEvent onInteraction;// 상호작용 완료 이벤트

    void Start()
    {
        interactionSlider.gameObject.SetActive(false); // 슬라이더 숨기기
    }




    // SendMessage를 위한 매개변수 없는 메서드 추가
    public void OnInteraction()
    {
        // 상호작용 가능한 몬스터가 있을 때만 상호작용 시작
        if (interactableMobs.Count > 0)
        {
            interactionSlider.gameObject.SetActive(true);
            interactionCoroutine = StartCoroutine(FillSliderOverTime(1.0f)); // 코루틴 시작, 충전시간 1초
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
                interactionSlider.value = 0; // 슬라이더 리셋
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
        Debug.Log("충전 완료, 아이템 획득 이미지, 인벤토리 획득 구현 예정");

        onInteraction.Invoke(); // 상호작용 완료 이벤트 호출

        interactionSlider.gameObject.SetActive(false);  // 충전 완료시 슬라이더 숨김
        interactionSlider.value = 0; // 슬라이더 값 초기화

        // TODO: 아이템 획득 이미지 표시 로직
        // TODO: 인벤토리 아이템 추가 로직
    }










    //public void CanInteract(MobInteraction mob, bool canInteract)  // Unity Event액션 용
    //{
    //    if (canInteract)
    //    {
    //        // TODO: 추후 몬스터 종류 추가
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

    //public void OnInteraction(InputAction.CallbackContext context) // Unity 이벤트액션 용
    //{
    //    // 상호작용 가능한 몬스터가 있을 때만 상호작용 시작
    //    if (interactableMobs.Count > 0)
    //    {
    //        if (context.started)
    //        {
    //            interactionSlider.gameObject.SetActive(true);
    //            interactionCoroutine = StartCoroutine(FillSliderOverTime(1.0f)); // 코루틴 시작, 충전시간 1초
    //        }
    //        else if (context.canceled)
    //        {
    //            interactionSlider.gameObject.SetActive(false);
    //            StopCoroutine(interactionCoroutine); // 코루틴 중지
    //            interactionSlider.value = 0; // 슬라이더 값 초기화
    //        }
    //    }
    //}
}