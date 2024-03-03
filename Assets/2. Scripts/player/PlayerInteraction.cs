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

    private List<MobInteraction> interactableMobs = new List<MobInteraction>();  // 상호작용 몬스터 리스트, 임시

    // 상호작용 완료 이벤트를 non-static으로 변경
    public UnityEvent onInteraction = new UnityEvent();

    private bool isInteracting = false;  // 상호작용 중인지 여부
    private bool isPressingInteractKey = false; // 상호작용 키를 누르고 있는지 여부


    void Start()
    {
        interactionSlider.gameObject.SetActive(false); // 슬라이더 숨기기
    }

    void Update()
    {
        // Input 확인 (Input System)
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


    // SendMessage를 위한 매개변수 없는 메서드 추가
    public void OnInteraction()
    {
        // 상호작용 가능한 몬스터가 있을 때만 상호작용 시작
        if (interactableMobs.Count > 0)
        {
            interactionSlider.gameObject.SetActive(true);
            interactionCoroutine = StartCoroutine(FillSliderOverTime(1.0f, interactableMobs[0])); // 코루틴 시작, 충전시간 1초
        }
    }
    public void StartInteraction(MobInteraction interactedMob)
    {
        // 상호작용 가능한 몬스터가 있을 때만 상호작용 시작
        if (!isInteracting && interactableMobs.Contains(interactedMob))
        {
            isInteracting = true; // 상호작용 시작
            interactionSlider.gameObject.SetActive(true);
            // 현재 상호작용하는 MobInteraction 인스턴스를 코루틴에 전달
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
                interactionSlider.value = 0; // 슬라이더 리셋
                interactionSlider.gameObject.SetActive(false);
            }
            interactableMobs.Remove(mob);
        }
    }


    private IEnumerator FillSliderOverTime(float duration, MobInteraction interactedMob)
    {
        float time = 0;
        while (time < duration && isInteracting) // isInteracting도 체크하여 키를 떼면 중단
        {
            if (isPressingInteractKey) // 키를 누르고 있는지 확인
            {
                time += Time.deltaTime;
                interactionSlider.value = time / duration;
            }
            else
            {
                // 키를 뗄 경우, 슬라이더 중지 및 초기화
                StopInteraction();
                yield break; // 코루틴 종료
            }
            yield return null;
        }
                    
        if (isInteracting) // 상호작용이 여전히 진행 중인 경우 완료 처리
        {
            // 상호작용 완료 시, 특정 몬스터에 대해서만 이벤트를 발생
            if (interactedMob != null)
            {
                interactedMob.HandleInteractionComplete();
            }
            Debug.Log("충전 완료, 아이템 획득 이미지, 인벤토리 획득 구현 예정");
            //onInteraction.Invoke(); // 상호작용 완료 이벤트 호출


            // 상호작용 완료 후 Z 키 입력 초기화
            isPressingInteractKey = false;
            isInteracting = false;
            interactionSlider.gameObject.SetActive(false);  // 충전 완료시 슬라이더 숨김
            interactionSlider.value = 0;  // 슬라이더 값 초기화
        }
        // TODO: 아이템 획득 이미지 표시 로직
        // TODO: 인벤토리 아이템 추가 로직
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
        isInteracting = false; // 상호작용 종료
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