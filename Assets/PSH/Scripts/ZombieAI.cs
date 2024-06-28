using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAi : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform�� �Ҵ���� ����
    public float moveSpeed = 3f; // ������ �̵� �ӵ�
    public float detectionRange = 10f; // ���Ͱ� �÷��̾ �ν��� ����
    public float attackRange = 2f; // ���� ����
    public float attackDelay = 2f; // ���� ������

    private bool isAttacking = false; // ���� ������ ����

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // �÷��̾ �±׷� ã�� �Ҵ�
        InvokeRepeating("CheckPlayerDistance", 0f, 0.5f); // ���� �ֱ�� �÷��̾�� �Ÿ��� üũ
    }

    void Update()
    {
        // �÷��̾ �ν� ���� �ȿ� ������ ����
        if (Vector3.Distance(transform.position, player.position) <= detectionRange && !isAttacking)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

            // ���Ͱ� �÷��̾ �ٶ󺸵��� ȸ��
            transform.LookAt(player);
        }
    }

    void CheckPlayerDistance()
    {
        // �÷��̾�� ���� ���� �ȿ� ������ ����
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                Invoke("AttackPlayer", attackDelay);
            }
        }
        else
        {
            isAttacking = false;
        }
    }

    void AttackPlayer()
    {
        // ���� ���� �߰�
        Debug.Log("����!");
        // ���⿡ ���� �ִϸ��̼� ���� �߰��� �� ����
        Invoke("ResetAttack", attackDelay);
    }

    void ResetAttack()
    {
        isAttacking = false;
    }
}
