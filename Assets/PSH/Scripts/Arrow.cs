using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public AudioClip collisionSound; // �浹 ����
    public float destroyAfterNoCollisionDelay = 20f; // �浹�� ���� �� 20�� �� �����ϴ� �ð�
    public int monsterDamageAmount = 10; // ���Ϳ��� ������ ������
    public int witherBossDamageAmount = 10; // ���� �������� ������ ������

    private bool hasCollided = false; // �浹 ���θ� üũ�ϴ� �÷���

    private void Start()
    {
        // 20�� �Ŀ� ȭ���� �����ϴ� �ڷ�ƾ�� �����մϴ�.
        StartCoroutine(DestroyAfterNoCollisionDelay(destroyAfterNoCollisionDelay));
    }

    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� �±׸� üũ�Ͽ� Player, Missile, Arrow �±��� ���� �浹 ó���� ���� �ʽ��ϴ�.
        if (other.CompareTag("Player") || other.CompareTag("Missile") || other.CompareTag("Arrow"))
        {
            return;
        }

        // �浹�� �߻������� ����մϴ�.
        if (!hasCollided)
        {
            hasCollided = true;

            // ȭ���� ������ ���� ����ϴ�.
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero; // �ӵ��� 0���� ����ϴ�.
                rb.angularVelocity = Vector3.zero; // ���ӵ��� 0���� ����ϴ�.
                rb.isKinematic = true; // ������ ��ȣ�ۿ��� ��Ȱ��ȭ�մϴ�.
            }

            // �浹 �� ���带 ����մϴ�.
            if (collisionSound != null)
            {
                AudioSource.PlayClipAtPoint(collisionSound, transform.position);
            }

            // ���Ϳ� ���� ������ �浹 ó��
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
                Destroy(gameObject); // WitherBoss�� �浹 �� ȭ���� �����մϴ�.
                return; // �浹 ó�� �� �� �̻� �������� ����
            }

            // 5�� �Ŀ� ȭ�� ������Ʈ�� �����մϴ�.
            StartCoroutine(DestroyAfterDelay(5f));
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        // �־��� �ð���ŭ ��ٸ��ϴ�.
        yield return new WaitForSeconds(delay);

        // ȭ�� ������Ʈ�� �����մϴ�.
        Destroy(gameObject);
    }

    private IEnumerator DestroyAfterNoCollisionDelay(float delay)
    {
        // �־��� �ð���ŭ ��ٸ��ϴ�.
        yield return new WaitForSeconds(delay);

        // ȭ�� ������Ʈ�� �����մϴ�.
        if (!hasCollided)
        {
            Destroy(gameObject);
        }
    }
}