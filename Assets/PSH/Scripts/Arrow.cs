using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public AudioClip collisionSound; // �浹 ����
    public float destroyAfterNoCollisionDelay = 20f; // �浹�� ���� �� 20�� �� �����ϴ� �ð�

    private bool hasCollided = false; // �浹 ���θ� üũ�ϴ� �÷���

    private void Start()
    {
        // 20�� �Ŀ� ȭ���� �����ϴ� �ڷ�ƾ�� �����մϴ�.
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
        // �浹�� �߻������� ����մϴ�.
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

        // 5�� �Ŀ� ȭ�� ������Ʈ�� �����մϴ�.
        StartCoroutine(DestroyAfterDelay(5f));
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