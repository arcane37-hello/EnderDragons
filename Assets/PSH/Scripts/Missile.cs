using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float lifetime = 5f; // �̻����� ���� �ð�

    private void Start()
    {
        // ���� �ð��� ������ �̻����� �ڵ����� �ı��˴ϴ�.
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� `Player` �±׸� ������ ������ �̻����� �ı��մϴ�.
        if (collision.gameObject.CompareTag("Player"))
        {
            // ���⿡ PlayerHealth ������Ʈ�� ������ ó�� ������ �������� �ʽ��ϴ�.
            Destroy(gameObject); // �浹 �� �̻����� �ı��մϴ�
        }
        // �浹�� ������Ʈ�� `Ground` �±׸� ������ ������ �̻����� �ı��մϴ�.
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject); // �浹 �� �̻����� �ı��մϴ�
        }
    }
}