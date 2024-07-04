using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTurret : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform을 할당할 변수
    public GameObject arrowPrefab; // 발사할 화살 프리팹
    public float shootInterval = 2f; // 발사 간격
    public float detectionRange = 10f; // 플레이어 인식 범위
    public float arrowSpeed = 10f; // 화살 발사 속도

    private float shootTimer; // 발사 타이머

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // 플레이어를 태그로 찾아 할당
        shootTimer = shootInterval; // 초기 발사 타이머 설정
    }

    void Update()
    {
        // 플레이어와의 거리 계산
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 플레이어가 인식 범위 내에 있고 발사 타이머가 0 이하면 발사
        if (distanceToPlayer <= detectionRange && shootTimer <= 0f)
        {
            ShootArrow(); // 화살 발사 함수 호출
            shootTimer = shootInterval; // 발사 간격 다시 설정
        }

        // 발사 타이머 감소
        shootTimer -= Time.deltaTime;
    }

    void ShootArrow()
    {
        // 화살 프리팹을 플레이어 쪽으로 발사
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        Vector3 direction = (player.position - transform.position).normalized;
        arrow.GetComponent<Rigidbody>().velocity = direction * arrowSpeed; // 화살 발사 속도 설정
    }
}