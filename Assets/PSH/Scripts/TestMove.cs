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
        isGrounded = true; // ó������ ���� �ִ� ���·� ����
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(x, 0, z);
        transform.position += dir * speed * Time.deltaTime;

        // ���� �Է� ó��
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        // Rigidbody ������Ʈ�� ����Ͽ� ���� ����
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce);
            isGrounded = false; // ���� �Ŀ��� ������ ��� ���·� ����
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // ���� ��Ҵ��� Ȯ���Ͽ� isGrounded ���� ����
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}