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

    private bool isInPhaseTwo = false; // ������ �� ��° �ܰ� ����

    void Start()
    {
        currentHealth = maxHealth; // ������ ü���� �ִ� ü������ �ʱ�ȭ
        StartCoroutine(AttackPattern()); // ���� ���� �ڷ�ƾ ����
    }

    void Update()
    {
        // ������ ü���� 50% ���Ϸ� �������� �� ��° �ܰ�� ��ȯ
        if (currentHealth <= maxHealth / 2 && !isInPhaseTwo)
        {
            EnterPhaseTwo();
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

    void EnterPhaseTwo()
    {
        isInPhaseTwo = true;
        Debug.Log("WitherBoss has entered Phase Two!");

        // �� ��° �ܰ迡���� �ൿ ������ �߰��� �� �ֽ��ϴ�.
        // 5�ʸ��� ��ȯ ���� ����
        StartCoroutine(SummonPattern());
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
        while (!isInPhaseTwo)
        {
            yield return null; // ���� ����, Phase Two���� ��ȯ�� ���� ���
        }

        // Phase Two������ 5�ʸ��� ��ȯ ������ Ȱ��ȭ��
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