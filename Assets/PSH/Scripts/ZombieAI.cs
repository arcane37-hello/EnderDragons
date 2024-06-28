using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAi : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform을 할당받을 변수
    public float moveSpeed = 3f; // 몬스터의 이동 속도
    public float detectionRange = 10f; // 몬스터가 플레이어를 인식할 범위
    public float attackRange = 2f; // 공격 범위
    public float attackDelay = 2f; // 공격 딜레이

    private bool isAttacking = false; // 공격 중인지 여부

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // 플레이어를 태그로 찾아 할당
        InvokeRepeating("CheckPlayerDistance", 0f, 0.5f); // 일정 주기로 플레이어와 거리를 체크
    }

    void Update()
    {
        // 플레이어가 인식 범위 안에 있으면 추적
        if (Vector3.Distance(transform.position, player.position) <= detectionRange && !isAttacking)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

            // 몬스터가 플레이어를 바라보도록 회전
            transform.LookAt(player);
        }
    }

    void CheckPlayerDistance()
    {
        // 플레이어와 공격 범위 안에 있으면 공격
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                Invoke("AttackPlayer", attackDelay);
            }
        }
        else
        {
            isAttacking = false;
        }
    }

    void AttackPlayer()
    {
        // 공격 로직 추가
        Debug.Log("공격!");
        // 여기에 공격 애니메이션 등을 추가할 수 있음
        Invoke("ResetAttack", attackDelay);
    }

    void ResetAttack()
    {
        isAttacking = false;
    }
}
