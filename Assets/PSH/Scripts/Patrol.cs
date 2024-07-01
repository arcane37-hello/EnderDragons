using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    public float minMoveDuration = 1f; // 최소 이동 시간
    public float maxMoveDuration = 3f; // 최대 이동 시간
    public float stopDuration = 1f; // 멈추는 시간
    public float minMoveDistance = 3f; // 최소 이동 거리

    private float moveTimer; // 이동 타이머
    private float stopTimer; // 멈춤 타이머
    private Vector3 moveDirection; // 이동 방향
    private Vector3 initialPosition; // 초기 위치 저장

    void Start()
    {
        initialPosition = transform.position;
        SetNewMovement(); // 초기 이동 설정
    }

    void Update()
    {
        if (stopTimer > 0f)
        {
            // 멈춤 상태에서 타이머 감소
            stopTimer -= Time.deltaTime;
            if (stopTimer <= 0f)
            {
                // 멈춤이 끝나면 다시 이동 설정
                SetNewMovement();
            }
        }
        else
        {
            // 이동 상태에서 타이머 감소
            moveTimer -= Time.deltaTime;
            if (moveTimer <= 0f)
            {
                // 이동 시간이 끝나면 멈춤 설정
                stopTimer = stopDuration; // 멈춤 시간 설정
                moveDirection = Vector3.zero; // 멈추도록 이동 방향을 초기화
            }
            else
            {
                // 이동
                transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

                // 최소 이동 거리 체크
                float currentDistance = Vector3.Distance(initialPosition, transform.position);
                if (currentDistance >= minMoveDistance)
                {
                    // 일정 거리 이동 후 멈춤
                    stopTimer = stopDuration;
                    moveDirection = Vector3.zero;
                }
            }
        }
    }

    void SetNewMovement()
    {
        // 무작위 방향 설정
        float randomAngle = Random.Range(0f, 360f);
        moveDirection = Quaternion.Euler(0f, randomAngle, 0f) * Vector3.forward;

        // 무작위 이동 시간 설정
        moveTimer = Random.Range(minMoveDuration, maxMoveDuration);
    }
}