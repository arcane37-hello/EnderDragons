using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public AudioClip collisionSound; // 충돌 사운드
    public float destroyAfterNoCollisionDelay = 20f; // 충돌이 없을 때 20초 후 삭제하는 시간

    private bool hasCollided = false; // 충돌 여부를 체크하는 플래그

    private void Start()
    {
        // 20초 후에 화살을 삭제하는 코루틴을 시작합니다.
        StartCoroutine(DestroyAfterNoCollisionDelay(destroyAfterNoCollisionDelay));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasCollided)
        {
            HandleCollision(collision);
        }
    }

    private void HandleCollision(Collision collision)
    {
        // 충돌이 발생했음을 기록합니다.
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

        // 5초 후에 화살 오브젝트를 삭제합니다.
        StartCoroutine(DestroyAfterDelay(5f));
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