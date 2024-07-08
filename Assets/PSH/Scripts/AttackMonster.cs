using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMonster : MonoBehaviour
{
    public GameObject affectedObject; // Ư�� ������Ʈ���� ������ ��� ������Ʈ�� �����ϴ� public ����

    private int clickCount = 0;
    private bool isObjectActive = true;
    private Rigidbody rb;
    private float moveForce = 5f;
    private float jumpForce = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // ���콺 ���� ��ư Ŭ�� �� ����
        if (Input.GetMouseButtonDown(0))
        {
            // Ŭ���� ��ġ�� ������Ʈ ��������
            GameObject clickedObject = GetClickedObject();

            // affectedObject�� �����Ǿ� �ְ� Ŭ���� ������Ʈ�� affectedObject�� ���� ����
            if (clickedObject != null && clickedObject == affectedObject && isObjectActive)
            {
                clickCount++;

                // Ŭ�� Ƚ���� ���� �ٸ� ���� ����
                if (clickCount <= 10)
                {
                    // ������Ʈ�� �ڷ� �о�� (������ ���� ����Ͽ�)
                    rb.AddForce(-transform.forward * moveForce, ForceMode.Impulse);

                    // ���� ȿ�� �ֱ�
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                }
                else
                {
                    // ������Ʈ�� ��Ȱ��ȭ (������� ��)
                    gameObject.SetActive(false);
                }
            }
        }
    }

    GameObject GetClickedObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject;
        }

        return null;
    }
}