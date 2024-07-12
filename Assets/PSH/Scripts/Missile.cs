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
        // �浹�� ������Ʈ�� `Ground` �±׸� ������ ������ �̻��ϰ� �Բ� �ش� ������Ʈ�� �ı��մϴ�.
        else if (collision.gameObject.CompareTag("Ground"))
        {
            // Ground �±׸� ���� ������Ʈ�� �ı��մϴ�.
            Destroy(collision.gameObject); // �浹 �� Ground ������Ʈ�� �ı��մϴ�
            Destroy(gameObject); // �̻��ϵ� �ı��մϴ�
        }
        else
        {
            // �ٸ� �±��� ������Ʈ�� �浹 �� �̻��ϸ� �ı��մϴ�
            Destroy(gameObject);
        }
    }
}