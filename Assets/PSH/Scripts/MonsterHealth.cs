using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public int maxHealth = 20; // �ִ� ü��
    public int currentHealth; // ���� ü��

    void Start()
    {
        currentHealth = maxHealth; // ���� �� ���� ü���� �ִ� ü������ �ʱ�ȭ
    }

    // ������ ü�� ���� �޼���
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // ü���� 0 ���Ϸ� �������� ��� ó��
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // ���Ͱ� ������� �� ȣ��� �޼���
    private void Die()
    {
        // ��� ó�� �ڵ�
        Debug.Log("Monster died!");
        Destroy(gameObject); // ���� ���� ������Ʈ�� ����
    }
}