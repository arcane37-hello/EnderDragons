using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAnimation : MonoBehaviour
{
    public Animator animator;  // Animator 컴포넌트

    void Start()
    {
        // Animator 컴포넌트 가져오기
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void Update()
    {
        // 마우스 왼쪽 버튼이 클릭되었을 때
        if (Input.GetMouseButtonDown(0))
        {
            // 애니메이션 파라미터를 트리거하여 애니메이션 재생
            if (animator != null)
            {
                // "TriggerName"이라는 이름의 트리거 파라미터 트리거
                animator.SetTrigger("Cycle");
            }
            else
            {
                Debug.LogWarning("Animator component is not assigned.");
            }
        }
    }
}