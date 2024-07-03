using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTurret : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform�� �Ҵ��� ����
    public GameObject arrowPrefab; // �߻��� ȭ�� ������
    public float shootInterval = 2f; // �߻� ����
    public float detectionRange = 10f; // �÷��̾� �ν� ����
    public float arrowSpeed = 10f; // ȭ�� �߻� �ӵ�

    private float shootTimer; // �߻� Ÿ�̸�

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // �÷��̾ �±׷� ã�� �Ҵ�
        shootTimer = shootInterval; // �ʱ� �߻� Ÿ�̸� ����
    }

    void Update()
    {
        // �÷��̾���� �Ÿ� ���
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // �÷��̾ �ν� ���� ���� �ְ� �߻� Ÿ�̸Ӱ� 0 ���ϸ� �߻�
        if (distanceToPlayer <= detectionRange && shootTimer <= 0f)
        {
            ShootArrow(); // ȭ�� �߻� �Լ� ȣ��
            shootTimer = shootInterval; // �߻� ���� �ٽ� ����
        }

        // �߻� Ÿ�̸� ����
        shootTimer -= Time.deltaTime;
    }

    void ShootArrow()
    {
        // ȭ�� �������� �÷��̾� ������ �߻�
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        Vector3 direction = (player.position - transform.position).normalized;
        arrow.GetComponent<Rigidbody>().velocity = direction * arrowSpeed; // ȭ�� �߻� �ӵ� ����
    }
}