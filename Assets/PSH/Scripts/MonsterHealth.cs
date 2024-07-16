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
        // 1초 동안 Z 방향으로 90도 회전
        StartCoroutine(RotateAndDestroy());
    }

    // Z 방향으로 90도 회전하고 일정 시간 후에 오브젝트를 삭제하는 코루틴
    private IEnumerator RotateAndDestroy()
    {
        float duration = 1f; // 회전 시간
        float elapsedTime = 0f; // 경과 시간
        Quaternion startRotation = transform.rotation; // 시작 회전
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, 90); // 끝 회전

        // 회전을 1초 동안 부드럽게 수행
        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 회전을 마친 후 정확한 목표 회전 상태로 설정
        transform.rotation = endRotation;

        // 0.5초 후에 몬스터 게임 오브젝트를 삭제
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}