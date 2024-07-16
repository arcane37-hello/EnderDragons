using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public AudioClip collisionSound; // 충돌 사운드
    public float destroyAfterNoCollisionDelay = 20f; // 충돌이 없을 때 20초 후 삭제하는 시간
    public int monsterDamageAmount = 10; // 몬스터에게 입히는 데미지
    public int witherBossDamageAmount = 10; // 위더 보스에게 입히는 데미지

    private bool hasCollided = false; // 충돌 여부를 체크하는 플래그

    private void Start()
    {
        // 20초 후에 화살을 삭제하는 코루틴을 시작합니다.
        StartCoroutine(DestroyAfterNoCollisionDelay(destroyAfterNoCollisionDelay));
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트의 태그를 체크하여 Player, Missile, Arrow 태그인 경우는 충돌 처리를 하지 않습니다.
        if (other.CompareTag("Player") || other.CompareTag("Missile") || other.CompareTag("Arrow"))
        {
            return;
        }

        // 충돌이 발생했음을 기록합니다.
        if (!hasCollided)
        {
            hasCollided = true;

            // 화살의 물리적 힘을 멈춥니다.
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero; // 속도를 0으로 만듭니다.
                rb.angularVelocity = Vector3.zero; // 각속도를 0으로 만듭니다.
                rb.isKinematic = true; // 물리적 상호작용을 비활성화합니다.
            }

            // 충돌 시 사운드를 재생합니다.
            if (collisionSound != null)
            {
                AudioSource.PlayClipAtPoint(collisionSound, transform.position);
            }

            // 몬스터와 위더 보스의 충돌 처리
            if (other.CompareTag("Enemy"))
            {
                MonsterHealth monsterHealth = other.GetComponent<MonsterHealth>();
                if (monsterHealth != null)
                {
                    monsterHealth.TakeDamage(monsterDamageAmount);
                }
            }
            else if (other.CompareTag("WitherBoss"))
            {
                WitherHealth witherHealth = other.GetComponent<WitherHealth>();
                if (witherHealth != null)
                {
                    witherHealth.TakeDamage(witherBossDamageAmount);
                }
                Destroy(gameObject); // WitherBoss와 충돌 시 화살을 삭제합니다.
                return; // 충돌 처리 후 더 이상 진행하지 않음
            }

            // 5초 후에 화살 오브젝트를 삭제합니다.
            StartCoroutine(DestroyAfterDelay(5f));
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        // 주어진 시간만큼 기다립니다.
        yield return new WaitForSeconds(delay);

        // 화살 오브젝트를 삭제합니다.
        Destroy(gameObject);
    }

    private IEnumerator DestroyAfterNoCollisionDelay(float delay)
    {
        // 주어진 시간만큼 기다립니다.
        yield return new WaitForSeconds(delay);

        // 화살 오브젝트를 삭제합니다.
        if (!hasCollided)
        {
            Destroy(gameObject);
        }
    }
}