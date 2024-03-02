using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobInteraction : MonoBehaviour
{
    public bool CanInteractWithPlayer { get; private set; } // 플레이어와의 충돌 상태(상호 작용 가능한지) 추적

    public GameObject uiText; // 상호작용 UI Text 오브젝트

    private void Start()
    {
        uiText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            uiText.SetActive(true); // UI Text 활성화
            CanInteractWithPlayer = true; // 상호작용 가능 상태

            // 충돌한 플레이어의 스크립트, CanInteract 메서드에 충돌 상태 전달.
            collision.GetComponent<PlayerInteraction>().CanInteract(this, true);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            uiText.SetActive(false); // UI Text를 비활성화
            CanInteractWithPlayer = false; // 상호 작용 불가능

            // 플레이어에게 충돌이 끝났다고 전달
            collision.GetComponent<PlayerInteraction>().CanInteract(this, false);
        }
    }

}
