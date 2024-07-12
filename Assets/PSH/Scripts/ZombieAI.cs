using UnityEngine;

public class ZombieAi : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float attackDelay = 2f;
    public float jumpForce = 5f;
    public int damage = 1; // ���� �� ������ ������

    private bool isAttacking = false;
    private Rigidbody rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("CheckPlayerDistance", 0f, 0.5f);
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= detectionRange && !isAttacking)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
            transform.LookAt(player);
        }
    }

    void CheckPlayerDistance()
    {
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
        Debug.Log("����!");

        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            HungerAndHealthSystem playerHealth = player.GetComponent<HungerAndHealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }

        Invoke("ResetAttack", attackDelay);
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Vector3 normal = collision.contacts[0].normal;

            if (Mathf.Abs(normal.y) < 0.1f)
            {
                Jump();
            }
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ZombieAi : MonoBehaviour
//{
//    public Transform player; // �÷��̾��� Transform�� �Ҵ���� ����
//    public float moveSpeed = 3f; // ������ �̵� �ӵ�
//    public float detectionRange = 10f; // ���Ͱ� �÷��̾ �ν��� ����
//    public float attackRange = 2f; // ���� ����
//    public float attackDelay = 2f; // ���� ������
//    public float jumpForce = 5f; // ���� ��

//    private bool isAttacking = false; // ���� ������ ����
//    private Rigidbody rb;

//    void Start()
//    {
//        player = GameObject.FindGameObjectWithTag("Player").transform; // �÷��̾ �±׷� ã�� �Ҵ�
//        rb = GetComponent<Rigidbody>(); // Rigidbody ������Ʈ ��������
//        InvokeRepeating("CheckPlayerDistance", 0f, 0.5f); // ���� �ֱ�� �÷��̾�� �Ÿ��� üũ
//    }

//    void Update()
//    {
//        // �÷��̾ �ν� ���� �ȿ� ������ ����
//        if (Vector3.Distance(transform.position, player.position) <= detectionRange && !isAttacking)
//        {
//            Vector3 direction = (player.position - transform.position).normalized;
//            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

//            // ���Ͱ� �÷��̾ �ٶ󺸵��� ȸ��
//            transform.LookAt(player);
//        }
//    }

//    void CheckPlayerDistance()
//    {
//        // �÷��̾�� ���� ���� �ȿ� ������ ����
//        if (Vector3.Distance(transform.position, player.position) <= attackRange)
//        {
//            if (!isAttacking)
//            {
//                isAttacking = true;
//                Invoke("AttackPlayer", attackDelay);
//            }
//        }
//        else
//        {
//            isAttacking = false;
//        }
//    }

//    void AttackPlayer()
//    {
//        // ���� ���� �߰�
//        Debug.Log("����!");
//        // ���⿡ ���� �ִϸ��̼� ���� �߰��� �� ����
//        Invoke("ResetAttack", attackDelay);
//    }

//    void ResetAttack()
//    {
//        isAttacking = false;
//    }

//    void OnCollisionEnter(Collision collision)
//    {
//        // �浹�� ������Ʈ�� �±װ� "Ground"�� ��
//        if (collision.gameObject.CompareTag("Ground"))
//        {
//            // ���� ���͸� ������
//            Vector3 normal = collision.contacts[0].normal;

//            // ���� ���� ������ y���� 0�� �����ٸ� (�����)
//            if (Mathf.Abs(normal.y) < 0.1f)
//            {
//                // ����
//                Jump();
//            }
//        }
//    }

//    void Jump()
//    {
//        // ���� ���� �߰�
//        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
//    }
//}