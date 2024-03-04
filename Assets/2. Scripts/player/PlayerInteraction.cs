using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Item Image Effect")]
    public GameObject itemImage; // 화면 표시 이미지 오브젝트
    public Sprite[] itemImageArray; // 아이템 이미지 배열
    public float itemEffectDuration = 2f; // 이미지가 사라지는 시간
    public float itemEffectDistance = 5f; // 이미지 이동 거리
    private float itemEffectTimer = 0.0f; // 효과 경과 시간 초기화
    private bool isGetItem = false;

    [Header("Item Text Effect")]
    public TextMeshProUGUI itemText; // 화면에 표시될 텍스트 오브젝트
    public float textMoveDuration = 2.5f;
    public float textMoveDistance = 2f;
    private float textMoveTimer = 0.0f;
    private bool isGetText = false;


    [SerializeField] private Slider interactionSlider; // 상호작용 슬라이더 참조
    private Coroutine interactionCoroutine; // 상호작용 코루틴 참조

    private List<MobInteraction> interactableMobs = new List<MobInteraction>();  // 상호작용 몬스터 리스트, 임시

    public UnityEvent onInteraction = new UnityEvent();

    private bool isInteracting = false;  // 상호작용 중인지 여부
    private bool isPressingInteractKey = false; // 상호작용 키를 누르고 있는지 여부



    void Start()
    {
        interactionSlider.gameObject.SetActive(false); // 슬라이더 숨기기
        
        itemImage.SetActive(false);
        itemText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isGetItem)
        {
            UpdateItemEffect();
        }
        if (isGetText)

        {
            UpdateTextEffect();
        }

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

    private void UpdateTextEffect()
    {
        textMoveTimer += Time.deltaTime;

        if (textMoveTimer <= textMoveDuration) // 텍스트 이동 타이머가 지속되는 동안
        {
            float yOffset = textMoveDistance * (textMoveTimer / textMoveDuration); // 텍스트 이동 거리 계산
            itemText.rectTransform.anchoredPosition = new Vector2(0f, yOffset); // 텍스트 위치 조정
            float alpha = 1.0f - (textMoveTimer / textMoveDuration); // 텍스트 투명도 계산
            itemText.color = new Color(1f, 1f, 1f, alpha); // 텍스트 투명도 적용
        }
        else // 텍스트 이동 타이머가 종료된 경우
        {
            
            itemText.gameObject.SetActive(false); 
            textMoveTimer = 0.0f; 
            isGetText = false; 
        }
    }

    // 아이템 획득 시 아이템 이미지 이펙트 업데이트
    private void UpdateItemEffect()
    {
        itemEffectTimer += Time.deltaTime;

        if (itemEffectTimer <= itemEffectDuration) // 아이템 이펙트 타이머가 지속되는 동안
        {
            float yOffset = itemEffectDuration * (itemEffectTimer / itemEffectDuration); // 아이템 이동 거리 계산
            itemImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, yOffset); // 아이템 이미지 위치 조정
            float alpha = 1.0f - (itemEffectTimer / itemEffectDuration); // 아이템 투명도 계산
            itemImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, alpha); // 아이템 투명도 적용
        }
        else // 아이템 이펙트 타이머가 종료된 경우
        {
            // 효과 종료 후 초기화
            itemImage.SetActive(false); // 아이템 이미지 비활성화
            itemEffectTimer = 0.0f; // 타이머 초기화
            isGetItem = false; // 아이템 획득 플래그 초기화
        }
    }

    // 아이템 이미지 표시
    public void ShowGetItem(int itemType)
    {
        itemImage.GetComponent<Image>().sprite = itemImageArray[itemType]; // 아이템 이미지 설정
        itemImage.SetActive(true); 
        isGetItem = true; 
        itemEffectTimer = 0.0f; // 아이템 이펙트 타이머 초기화
    }

    // 아이템 획득 시 텍스트 표시
    public void ShowItemText(string itemName)
    {
        itemText.text = itemName; // 아이템 텍스트 설정
        itemText.gameObject.SetActive(true); 
        isGetText = true; 
        textMoveTimer = 0.0f; // 텍스트 이펙트 타이머 초기화
    }




    // SendMessage를 위한 매개변수 없는 메서드 추가
    public void OnInteraction()
    {
        MainSoundManager.instance.PlaySFX(0);
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

            isPressingInteractKey = false;
            isInteracting = false;
            interactionSlider.gameObject.SetActive(false);  // 충전 완료시 슬라이더 숨김
            interactionSlider.value = 0;  // 슬라이더 값 초기화



            switch (true)
            {
                case bool _ when interactedMob.mobType.Contains("Corgi"):
                    Inventory.Instance.AddItem(10020005);
                    ShowItemText("'코기의 분비물(?)' 을 얻었다!");
                    itemImage.GetComponent<Image>().sprite = itemImageArray[0];
                    MainSoundManager.instance.PlaySFX(3);
                    // 인벤토리에 코기 분비물 추가
                    break;

                case bool _ when interactedMob.mobType.StartsWith("Mushroom"):
                    ShowItemText("'마른 버섯' 을 얻었다!");
                    Inventory.Instance.AddItem(10010006);
                    itemImage.GetComponent<Image>().sprite = itemImageArray[1];
                    MainSoundManager.instance.PlaySFX(3);
                    // 인벤토리에 마른 버섯 추가
                    break;

                case bool _ when interactedMob.mobType.Contains("Kirby"):
                    ShowItemText("'커비의 별가루' 를 얻었다!");
                    itemImage.GetComponent<Image>().sprite = itemImageArray[2];
                    Inventory.Instance.AddItem(10020006);
                    MainSoundManager.instance.PlaySFX(3);
                    // 인벤토리에 커비의 별가루 추가
                    break;

                case bool _ when interactedMob.mobType.StartsWith("Turtle"):
                    ShowItemText("'유사-꼬부기의 등껍질' 을 얻었다!");
                    itemImage.GetComponent<Image>().sprite = itemImageArray[3];
                    Inventory.Instance.AddItem(10020002);
                    MainSoundManager.instance.PlaySFX(3);
                    // 인벤토리에 유사-꼬부기 등껍질 추가 
                    break;

                case bool _ when interactedMob.mobType.Contains("Salamander"):
                    ShowItemText("'날도롱뇽의 꼬리' 를 얻었다!");
                    itemImage.GetComponent<Image>().sprite = itemImageArray[4];
                    Inventory.Instance.AddItem(10020001);
                    MainSoundManager.instance.PlaySFX(3);
                    // 인벤토리에 날도롱뇽 날개 추가 로직
                    break;

                case bool _ when interactedMob.mobType.StartsWith("Grasshopper"):
                    ShowItemText("'여치의 더듬이' 를 얻었다!");
                    itemImage.GetComponent<Image>().sprite = itemImageArray[5];
                    Inventory.Instance.AddItem(10020003);
                    MainSoundManager.instance.PlaySFX(3);
                    // 인벤토리에 여치 더듬이 추가 로직
                    break;

                case bool _ when interactedMob.mobType.Contains("Lizard"):
                    ShowItemText("'불 도롱뇽의 꼬리' 를 얻었다!");
                    itemImage.GetComponent<Image>().sprite = itemImageArray[6];
                    Inventory.Instance.AddItem(10020004);
                    MainSoundManager.instance.PlaySFX(3);
                    // 인벤토리에 불도마뱀 꼬리 추가 로직
                    break;

                case bool _ when interactedMob.mobType.StartsWith("Man-Eating"):
                    ShowItemText("'식인꽃 꽃잎' 을 얻었다!");
                    itemImage.GetComponent<Image>().sprite = itemImageArray[7];
                    Inventory.Instance.AddItem(10010003);
                    MainSoundManager.instance.PlaySFX(3);
                    // 인벤토리에 식인꽃 꽃잎 추가 로직
                    break;

                case bool _ when interactedMob.mobType.Contains("Tree"):
                    ShowItemText("'사탕' 을 얻었다!");
                    itemImage.GetComponent<Image>().sprite = itemImageArray[8];
                    Inventory.Instance.AddItem(10010002);
                    MainSoundManager.instance.PlaySFX(3);
                    // 인벤토리에 사탕 추가 로직
                    break;

                case bool _ when interactedMob.mobType.StartsWith("Grape"):
                    Inventory.Instance.AddItem(10010005);

                    float randomValue = UnityEngine.Random.Range(0f, 1f); // 2가지의 랜덤 분기

                    if (randomValue < 0.5f)
                    {
                        ShowItemText("'오스틴 산 포도' 를 얻었다.");
                    itemImage.GetComponent<Image>().sprite = itemImageArray[9];
                    MainSoundManager.instance.PlaySFX(3);
                    }
                    else
                    {
                        // 두 번째 로직 : 이스터 에그
                        ShowItemText("사장님이 포도를 주셨다 아이고~ ");
                        itemImage.GetComponent<Image>().sprite = itemImageArray[12];
                        MainSoundManager.instance.PlaySFX(7);
                    }
                    break;

                case bool _ when interactedMob.mobType.Contains("Herb"):
                    ShowItemText("'야자 허브 잎' 을 얻었다!");
                    itemImage.GetComponent<Image>().sprite = itemImageArray[10];
                    Inventory.Instance.AddItem(10010004);
                    MainSoundManager.instance.PlaySFX(3);
                    // 인벤토리에 야자 허브 추가 로직
                    break;

                case bool _ when interactedMob.mobType.StartsWith("Flower"):
                    ShowItemText("'백일홍 꽃' 을 얻었다!");
                    itemImage.GetComponent<Image>().sprite = itemImageArray[11];
                    Inventory.Instance.AddItem(10010001);
                    MainSoundManager.instance.PlaySFX(3);
                    // 인벤토리에 백일홍 꽃잎 추가 로직
                    break;

                default:
                    Debug.Log("아이템을 획득 할 수 없습니다.");
                    break;
            }
            if (isGetItem == false) // 아이템 이미지 표시 시작
            {
                itemImage.SetActive(true);
                isGetItem = true;
                itemEffectTimer = 0.0f;
            }
        }
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



}