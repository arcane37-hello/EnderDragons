using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    public float speed = 10;
    public float jumpForce = 500;
    private bool isGrounded;

    void Start()
    {
        isGrounded = true; // 처음에는 땅에 있는 상태로 시작
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(x, 0, z);
        transform.position += dir * speed * Time.deltaTime;

        // 점프 입력 처리
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        // Rigidbody 컴포넌트를 사용하여 점프 구현
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce);
            isGrounded = false; // 점프 후에는 땅에서 벗어난 상태로 설정
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // 땅에 닿았는지 확인하여 isGrounded 값을 설정
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}