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
        // 1�� ���� Z �������� 90�� ȸ��
        StartCoroutine(RotateAndDestroy());
    }

    // Z �������� 90�� ȸ���ϰ� ���� �ð� �Ŀ� ������Ʈ�� �����ϴ� �ڷ�ƾ
    private IEnumerator RotateAndDestroy()
    {
        float duration = 1f; // ȸ�� �ð�
        float elapsedTime = 0f; // ��� �ð�
        Quaternion startRotation = transform.rotation; // ���� ȸ��
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, 90); // �� ȸ��

        // ȸ���� 1�� ���� �ε巴�� ����
        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ȸ���� ��ģ �� ��Ȯ�� ��ǥ ȸ�� ���·� ����
        transform.rotation = endRotation;

        // 0.5�� �Ŀ� ���� ���� ������Ʈ�� ����
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}