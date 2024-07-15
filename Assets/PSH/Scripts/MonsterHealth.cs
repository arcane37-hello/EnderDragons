using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public int maxHealth = 20; // 최대 체력
    public int currentHealth; // 현재 체력

    void Start()
    {
        currentHealth = maxHealth; // 시작 시 현재 체력을 최대 체력으로 초기화
    }

    // 몬스터의 체력 감소 메서드
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // 체력이 0 이하로 떨어지면 사망 처리
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 몬스터가 사망했을 때 호출될 메서드
    private void Die()
    {
        // 사망 처리 코드
        Debug.Log("Monster died!");
        Destroy(gameObject); // 몬스터 게임 오브젝트를 삭제
    }
}