using UnityEngine;

public class ZombieAi : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float attackDelay = 2f;
    public float jumpForce = 5f;
    public int damage = 1; // 공격 시 입히는 데미지

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
        Debug.Log("공격!");

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
//    public Transform player; // 플레이어의 Transform을 할당받을 변수
//    public float moveSpeed = 3f; // 몬스터의 이동 속도
//    public float detectionRange = 10f; // 몬스터가 플레이어를 인식할 범위
//    public float attackRange = 2f; // 공격 범위
//    public float attackDelay = 2f; // 공격 딜레이
//    public float jumpForce = 5f; // 점프 힘

//    private bool isAttacking = false; // 공격 중인지 여부
//    private Rigidbody rb;

//    void Start()
//    {
//        player = GameObject.FindGameObjectWithTag("Player").transform; // 플레이어를 태그로 찾아 할당
//        rb = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 가져오기
//        InvokeRepeating("CheckPlayerDistance", 0f, 0.5f); // 일정 주기로 플레이어와 거리를 체크
//    }

//    void Update()
//    {
//        // 플레이어가 인식 범위 안에 있으면 추적
//        if (Vector3.Distance(transform.position, player.position) <= detectionRange && !isAttacking)
//        {
//            Vector3 direction = (player.position - transform.position).normalized;
//            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

//            // 몬스터가 플레이어를 바라보도록 회전
//            transform.LookAt(player);
//        }
//    }

//    void CheckPlayerDistance()
//    {
//        // 플레이어와 공격 범위 안에 있으면 공격
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
//        // 공격 로직 추가
//        Debug.Log("공격!");
//        // 여기에 공격 애니메이션 등을 추가할 수 있음
//        Invoke("ResetAttack", attackDelay);
//    }

//    void ResetAttack()
//    {
//        isAttacking = false;
//    }

//    void OnCollisionEnter(Collision collision)
//    {
//        // 충돌한 오브젝트의 태그가 "Ground"일 때
//        if (collision.gameObject.CompareTag("Ground"))
//        {
//            // 법선 벡터를 가져옴
//            Vector3 normal = collision.contacts[0].normal;

//            // 만약 법선 벡터의 y값이 0에 가깝다면 (수평면)
//            if (Mathf.Abs(normal.y) < 0.1f)
//            {
//                // 점프
//                Jump();
//            }
//        }
//    }

//    void Jump()
//    {
//        // 점프 로직 추가
//        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
//    }
//}