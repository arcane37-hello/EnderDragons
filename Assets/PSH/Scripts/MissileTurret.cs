using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTurret : MonoBehaviour
{
    public float attackCooldown = 5f; // 미사일 발사 쿨다운 시간
    public GameObject missilePrefab; // 미사일 프리팹
    public Transform missileSpawnPoint; // 미사일 발사 지점

    private Transform player; // 플레이어의 Transform
    private float attackTimer; // 공격 쿨다운 타이머

    void Start()
    {
        // "Player" 태그를 가진 오브젝트의 Transform을 찾습니다.
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // 공격 쿨다운 타이머를 초기화합니다.
        attackTimer = attackCooldown;
    }

    void Update()
    {
        // 플레이어가 없는 경우 아무 작업도 하지 않습니다.
        if (player == null)
            return;

        // 공격 쿨다운 타이머를 감소시킵니다.
        attackTimer -= Time.deltaTime;

        // 공격 쿨다운이 끝나면 미사일을 발사합니다.
        if (attackTimer <= 0f)
        {
            Attack();
            // 쿨다운 타이머를 재설정합니다.
            attackTimer = attackCooldown;
        }
    }

    void Attack()
    {
        // 미사일을 발사하는 함수입니다.
        Debug.Log("미사일 포탑이 미사일을 발사합니다!");

        // 미사일을 생성합니다.
        GameObject missile = Instantiate(missilePrefab, missileSpawnPoint.position, missileSpawnPoint.rotation);

        // 미사일의 Rigidbody를 가져옵니다.
        Rigidbody missileRb = missile.GetComponent<Rigidbody>();
        if (missileRb != null)
        {
            // 미사일이 플레이어를 향해 날아가도록 속도를 설정합니다.
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            missileRb.velocity = directionToPlayer * 10f; // 10f는 미사일의 발사 속도
        }
        else
        {
            Debug.LogWarning("미사일 프리팹에는 Rigidbody 컴포넌트가 없습니다.");
        }
    }
}