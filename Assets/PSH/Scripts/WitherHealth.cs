using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitherHealth : MonoBehaviour
{
    public int maxHealth = 100; // 보스의 최대 체력
    public int currentHealth;  // 현재 체력

    public GameObject summonPrefab; // 소환할 오브젝트의 프리팹
    public Transform summonPoint; // 소환할 위치

    public float summonInterval = 5f; // 오브젝트를 소환할 주기 (초)

    void Start()
    {
        currentHealth = maxHealth; // 보스의 체력을 최대 체력으로 초기화
        StartCoroutine(AttackPattern()); // 공격 패턴 코루틴 시작
    }

    void Update()
    {
        // 보스의 체력이 0 이하로 떨어지면 죽음 처리
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // 피해를 받아 현재 체력 감소
        if (currentHealth <= 0)
        {
            Die(); // 체력이 0 이하로 떨어지면 죽음 처리
        }
    }

    void Die()
    {
        Debug.Log("WitherBoss has been defeated!");
        // 보스가 죽었을 때의 행동을 추가합니다.
        // 예: 보스의 사망 애니메이션, 아이템 드롭, 경험치 획득 등
        Destroy(gameObject); // 보스 오브젝트 삭제
    }

    IEnumerator AttackPattern()
    {
        while (true)
        {
            yield return null; // 무한 루프, 공격 패턴 실행
        }
    }

    IEnumerator SummonPattern()
    {
        while (true)
        {
            yield return new WaitForSeconds(summonInterval); // 소환 주기만큼 대기

            if (summonPrefab != null && summonPoint != null)
            {
                // 소환할 오브젝트의 인스턴스를 생성합니다.
                Instantiate(summonPrefab, summonPoint.position, summonPoint.rotation);
                Debug.Log($"Summoned a new object at ({summonPoint.position.x}, {summonPoint.position.y}, {summonPoint.position.z})");
            }
            else
            {
                Debug.LogError("Summon prefab or summon point is not assigned.");
            }
        }
    }
}