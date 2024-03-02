using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColliderMark : MonoBehaviour
{
    public Image questionMark; // 물음표 이미지
    public Animator questionMarkAnimator; // 물음표 이미지에 대한 Animator 컴포넌트

    public Image loveMark; // 러브 이미지
    public Animator loveMarkAnimator; // 러브 애니메이터

    public PlayerInteraction playerInteraction; // 플레이어 상호작용 스크립트 참조

    void Start()
    {
        //if (playerInteraction == null)
        //{
        //    Debug.LogError("ColliderMark: PlayerInteraction 컴포넌트가 할당되지 않았습니다.", this);
        //    return; // PlayerInteraction 컴포넌트가 없으면, 오류 메시지를 출력하고 초기화를 중단합니다.
        //}

        if (questionMark != null)
        {
            questionMark.enabled = false; // 물음표 이미지 비활성화
        }
        if (loveMark != null)
        {
            loveMark.enabled = false; // 러브 이미지 비활성화
        }
        playerInteraction.onInteraction.AddListener(ActiveLoveMark); // 이벤트 리스너 추가

    }
    public void ActiveLoveMark() // 인스펙터 이벤트 리스너에 연결하려면 public으로 바꿔줘야함 (완료)
    {
        if (loveMark != null)
        {
            loveMark.enabled = true;

            if(loveMarkAnimator != null)
            {
                loveMarkAnimator.Rebind(); // 애니메이터 초기화
                loveMarkAnimator.Play("Love", -1, 0f); // 해당 애니메이션 재생
            }
            StartCoroutine(DeactivateImage(loveMark, loveMarkAnimator, 2.0f)); // 2초후 비활성화
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 물음표 마크 오브젝트의 콜라이더, 플레이어와 충돌했는지 확인
        if (collision.CompareTag("Player") && questionMark != null)
        {
            questionMark.enabled = true; // 물음표 이미지 활성화

            if (questionMarkAnimator != null)
            {
                // Animator 컴포넌트를 사용하여 애니메이션을 제어
                questionMarkAnimator.Rebind(); // Animator 컴포넌트 상태 초기화(처음으로)
                
                questionMarkAnimator.Play("Question", -1, 0f);
                // "Question" 이라는 애니메이션 / -1 : 레이어(-1은 모든 레이어) / 0f : 애니메이션(시간)의 처음부터.
                // 요약 : 첫 프레임부터 재생한다
            }
            StartCoroutine(DeactivateImage(questionMark, questionMarkAnimator, 1.0f));
            // 지정된 시간 1초 후에 이미지와 애니메이션을 비활성화
        }
    }
    private IEnumerator DeactivateImage(Image image, Animator animator, float delay) // 매개변수 - 이미지 / 애니메이터 / 지정 시간
    {
        yield return new WaitForSeconds(delay);

        if (animator != null)
        {
            animator.Rebind(); // 애니메이터 상태 리셋
        }

        image.enabled = false; // 이미지를 비활성화
    }
}
