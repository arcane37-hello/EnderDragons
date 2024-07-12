using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTurret : MonoBehaviour
{
    public float attackCooldown = 5f; // �̻��� �߻� ��ٿ� �ð�
    public GameObject missilePrefab; // �̻��� ������
    public Transform missileSpawnPoint; // �̻��� �߻� ����

    private Transform player; // �÷��̾��� Transform
    private float attackTimer; // ���� ��ٿ� Ÿ�̸�

    void Start()
    {
        // "Player" �±׸� ���� ������Ʈ�� Transform�� ã���ϴ�.
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // ���� ��ٿ� Ÿ�̸Ӹ� �ʱ�ȭ�մϴ�.
        attackTimer = attackCooldown;
    }

    void Update()
    {
        // �÷��̾ ���� ��� �ƹ� �۾��� ���� �ʽ��ϴ�.
        if (player == null)
            return;

        // ���� ��ٿ� Ÿ�̸Ӹ� ���ҽ�ŵ�ϴ�.
        attackTimer -= Time.deltaTime;

        // ���� ��ٿ��� ������ �̻����� �߻��մϴ�.
        if (attackTimer <= 0f)
        {
            Attack();
            // ��ٿ� Ÿ�̸Ӹ� �缳���մϴ�.
            attackTimer = attackCooldown;
        }
    }

    void Attack()
    {
        // �̻����� �߻��ϴ� �Լ��Դϴ�.
        Debug.Log("�̻��� ��ž�� �̻����� �߻��մϴ�!");

        // �̻����� �����մϴ�.
        GameObject missile = Instantiate(missilePrefab, missileSpawnPoint.position, missileSpawnPoint.rotation);

        // �̻����� Rigidbody�� �����ɴϴ�.
        Rigidbody missileRb = missile.GetComponent<Rigidbody>();
        if (missileRb != null)
        {
            // �̻����� �÷��̾ ���� ���ư����� �ӵ��� �����մϴ�.
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            missileRb.velocity = directionToPlayer * 10f; // 10f�� �̻����� �߻� �ӵ�
        }
        else
        {
            Debug.LogWarning("�̻��� �����տ��� Rigidbody ������Ʈ�� �����ϴ�.");
        }
    }
}