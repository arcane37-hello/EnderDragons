using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitherHealth : MonoBehaviour
{
    public int maxHealth = 100; // ������ �ִ� ü��
    public int currentHealth;  // ���� ü��

    public GameObject summonPrefab; // ��ȯ�� ������Ʈ�� ������
    public Transform summonPoint; // ��ȯ�� ��ġ

    public float summonInterval = 5f; // ������Ʈ�� ��ȯ�� �ֱ� (��)

    void Start()
    {
        currentHealth = maxHealth; // ������ ü���� �ִ� ü������ �ʱ�ȭ
        StartCoroutine(AttackPattern()); // ���� ���� �ڷ�ƾ ����
    }

    void Update()
    {
        // ������ ü���� 0 ���Ϸ� �������� ���� ó��
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // ���ظ� �޾� ���� ü�� ����
        if (currentHealth <= 0)
        {
            Die(); // ü���� 0 ���Ϸ� �������� ���� ó��
        }
    }

    void Die()
    {
        Debug.Log("WitherBoss has been defeated!");
        // ������ �׾��� ���� �ൿ�� �߰��մϴ�.
        // ��: ������ ��� �ִϸ��̼�, ������ ���, ����ġ ȹ�� ��
        Destroy(gameObject); // ���� ������Ʈ ����
    }

    IEnumerator AttackPattern()
    {
        while (true)
        {
            yield return null; // ���� ����, ���� ���� ����
        }
    }

    IEnumerator SummonPattern()
    {
        while (true)
        {
            yield return new WaitForSeconds(summonInterval); // ��ȯ �ֱ⸸ŭ ���

            if (summonPrefab != null && summonPoint != null)
            {
                // ��ȯ�� ������Ʈ�� �ν��Ͻ��� �����մϴ�.
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