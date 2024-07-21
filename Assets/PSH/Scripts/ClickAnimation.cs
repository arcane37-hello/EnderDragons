using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAnimation : MonoBehaviour
{
    public Animator animator;  // Animator ������Ʈ

    void Start()
    {
        // Animator ������Ʈ ��������
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void Update()
    {
        // ���콺 ���� ��ư�� Ŭ���Ǿ��� ��
        if (Input.GetMouseButtonDown(0))
        {
            // �ִϸ��̼� �Ķ���͸� Ʈ�����Ͽ� �ִϸ��̼� ���
            if (animator != null)
            {
                // "TriggerName"�̶�� �̸��� Ʈ���� �Ķ���� Ʈ����
                animator.SetTrigger("Cycle");
            }
            else
            {
                Debug.LogWarning("Animator component is not assigned.");
            }
        }
    }
}