using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAI : MonoBehaviour
{
    public float normalMoveSpeed = 3f; // 평상시 이동 속도
    public float chargeMoveSpeed = 10f; // 돌진 시 이동 속도
    public float rotationSpeed = 3f;
    public float attackCooldown = 15f; // 돌진 쿨다운
    public float flyHeight = 10f; // 날아다닐 높이
    public float orbitSpeed = 30f; // 시계 방향으로 공전 속도
    public float returnSpeed = 5f; // 복귀할 때 사용할 속도 (조정 가능)
    public float returnSmoothness = 0.5f; // 복귀할 때의 부드러움 정도
    public float chargeDuration = 2f; // 돌진 지속 시간
    public float cooldownDuration = 10f; // 돌진 후 쿨타임

    private Transform player;
    private Rigidbody rb;
    private float attackTimer;
    private float cooldownTimer; // 쿨타임 타이머
    private bool isCharging; // 돌진 중인지 여부
    private float chargeStartTime; // 돌진 시작 시간 기록
    private Vector3 startPosition; // 시작 위치 저장
    private Vector3 targetPosition; // 목표 위치 저장
    private Vector3 chargeStartPosition; // 돌진 시작 위치 저장

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        attackTimer = attackCooldown;
        cooldownTimer = 0f;
        isCharging = false;
        startPosition = transform.position; // 시작 위치 저장
    }

    void Update()
    {
        // 플레이어 방향으로 회전
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // 시계 방향으로 공전
        transform.RotateAround(player.position, Vector3.up, orbitSpeed * Time.deltaTime);

        // 날아다니기 (y축 높이 유지)
        targetPosition = startPosition + Vector3.up * flyHeight; // 목표 위치를 날아다니는 높이로 설정
        rb.MovePosition(Vector3.MoveTowards(transform.position, targetPosition, normalMoveSpeed * Time.deltaTime));

        // 정확히 y 좌표가 10에 도달할 때 돌진 시작
        if (transform.position.y == 10f && !isCharging && cooldownTimer <= 0f)
        {
            StartCharge();
        }

        // 돌진 중일 때만 플레이어 쪽으로 직진
        if (isCharging)
        {
            Vector3 chargeDirection = (player.position - transform.position).normalized;
            rb.velocity = chargeDirection * chargeMoveSpeed; // 빠르게 돌진

            // 일정 시간 후 돌진 종료
            if (Time.time - chargeStartTime >= chargeDuration)
            {
                StopCharge();
                cooldownTimer = cooldownDuration; // 돌진 후 쿨타임 시작
            }
        }

        // 쿨타임 관리
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        // 복귀 중일 때 y 좌표가 9 이상이면 강제로 10으로 맞추기
        if (!isCharging && transform.position.y >= 9f)
        {
            rb.MovePosition(new Vector3(transform.position.x, 10f, transform.position.z));
        }

        // 공격 쿨다운 관리
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0f)
        {
            attackTimer = attackCooldown;
        }
    }

    void Charge()
    {
        // 돌진 시작
        Debug.Log("엔더 드래곤이 플레이어를 향해 돌진합니다!");
        isCharging = true;
        chargeStartTime = Time.time; // 돌진 시작 시간 기록
        chargeStartPosition = transform.position; // 돌진 시작 위치 저장
    }

    void StartCharge()
    {
        Charge();
    }

    void StopCharge()
    {
        // 돌진 종료 후 자연스럽게 원래 위치로 복귀
        Debug.Log("엔더 드래곤이 돌진을 멈추고 자연스럽게 원래 위치로 복귀합니다.");
        isCharging = false;
        rb.velocity = Vector3.zero; // 속도 초기화

        // 시작 위치로 자연스럽게 복귀하기
        StartCoroutine(MoveToStartPosition(startPosition));
    }

    IEnumerator MoveToStartPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, returnSmoothness * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition; // 정확한 위치로 맞추기
    }
}