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
    private Rigidbody2D rb; // Rigidbody2D 컴포넌트에 대한 참조

    [SerializeField] private Slider interactionSlider; // 상호작용 슬라이더 참조
    private Coroutine interactionCoroutine; // 상호작용 코루틴 참조
    
    // private bool isInteracting; // 상호작용 중 여부를 추적

    private List<MobInteraction> interactableMobs = new List<MobInteraction>();  // 상호작용 몬스터 리스트, 임시
    public UnityEvent onInteraction;// 상호작용 완료 이벤트

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        interactionSlider.gameObject.SetActive(false); // 슬라이더 숨기기
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
        // 상호작용 가능한 몬스터가 있을 때만 상호작용 시작
        if (interactableMobs.Count > 0)
        {
            if (context.started)
            {
                interactionSlider.gameObject.SetActive(true);
                interactionCoroutine = StartCoroutine(FillSliderOverTime(1.0f)); // 코루틴 시작, 충전시간 1초
            }
            else if (context.canceled)
            {
                interactionSlider.gameObject.SetActive(false);
                StopCoroutine(interactionCoroutine); // 코루틴 중지
                interactionSlider.value = 0; // 슬라이더 값 초기화
            }
        }
    }

    public void CanInteract(MobInteraction mob, bool canInteract)
    {
        if (canInteract)
        {
            // TODO: 추후 몬스터 종류 추가
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
        Debug.Log("충전 완료, 아이템 획득 이미지, 인벤토리 획득 구현 예정");
        
        onInteraction.Invoke(); // 상호작용 완료 이벤트 호출

        interactionSlider.gameObject.SetActive(false);  // 충전 완료시 슬라이더 숨김
        interactionSlider.value = 0; // 슬라이더 값 초기화

        // TODO: 아이템 획득 이미지 표시 로직
        // TODO: 인벤토리 아이템 추가 로직
    }
}