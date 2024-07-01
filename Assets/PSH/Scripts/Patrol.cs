using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float moveSpeed = 5f; // �̵� �ӵ�
    public float minMoveDuration = 1f; // �ּ� �̵� �ð�
    public float maxMoveDuration = 3f; // �ִ� �̵� �ð�
    public float stopDuration = 1f; // ���ߴ� �ð�
    public float minMoveDistance = 3f; // �ּ� �̵� �Ÿ�

    private float moveTimer; // �̵� Ÿ�̸�
    private float stopTimer; // ���� Ÿ�̸�
    private Vector3 moveDirection; // �̵� ����
    private Vector3 initialPosition; // �ʱ� ��ġ ����

    void Start()
    {
        initialPosition = transform.position;
        SetNewMovement(); // �ʱ� �̵� ����
    }

    void Update()
    {
        if (stopTimer > 0f)
        {
            // ���� ���¿��� Ÿ�̸� ����
            stopTimer -= Time.deltaTime;
            if (stopTimer <= 0f)
            {
                // ������ ������ �ٽ� �̵� ����
                SetNewMovement();
            }
        }
        else
        {
            // �̵� ���¿��� Ÿ�̸� ����
            moveTimer -= Time.deltaTime;
            if (moveTimer <= 0f)
            {
                // �̵� �ð��� ������ ���� ����
                stopTimer = stopDuration; // ���� �ð� ����
                moveDirection = Vector3.zero; // ���ߵ��� �̵� ������ �ʱ�ȭ
            }
            else
            {
                // �̵�
                transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

                // �ּ� �̵� �Ÿ� üũ
                float currentDistance = Vector3.Distance(initialPosition, transform.position);
                if (currentDistance >= minMoveDistance)
                {
                    // ���� �Ÿ� �̵� �� ����
                    stopTimer = stopDuration;
                    moveDirection = Vector3.zero;
                }
            }
        }
    }

    void SetNewMovement()
    {
        // ������ ���� ����
        float randomAngle = Random.Range(0f, 360f);
        moveDirection = Quaternion.Euler(0f, randomAngle, 0f) * Vector3.forward;

        // ������ �̵� �ð� ����
        moveTimer = Random.Range(minMoveDuration, maxMoveDuration);
    }
}