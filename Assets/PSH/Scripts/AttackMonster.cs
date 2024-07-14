using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMonster : MonoBehaviour
{
    public float damagePerClick = 2f; // Ŭ���� ������ �⺻ ������
    public float swordDamageBonus = 5f; // Į�� Ȱ��ȭ�Ǿ��� �� �߰��� ������ ������

    private GameObject affectedObject; // Ư�� ���͸� ������ ����

    private bool isSwordActive = false; // Į�� Ȱ��ȭ�Ǿ����� ����

    private Rigidbody rb;
    private float moveForce = 5f;
    private float jumpForce = 5f;

    void Start()
    {
        // ������ affectedObject�� Scene���� "Enemy" �±׸� ���� ù ��° ������Ʈ�� ����
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            affectedObject = enemies[0];
        }
        else
        {
            Debug.LogError("No enemy found with 'Enemy' tag.");
        }

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Į�� Ȱ��ȭ�Ǿ����� ���� üũ
        isSwordActive = InventoryNumber.Instance.IsSwordActive();

        // ���콺 ���� ��ư Ŭ�� �� ����
        if (Input.GetMouseButtonDown(0))
        {
            // Ŭ���� ��ġ�� ������Ʈ ��������
            GameObject clickedObject = GetClickedObject();

            // affectedObject�� �����Ǿ� �ְ� Ŭ���� ������Ʈ�� affectedObject�� ���� ����
            if (clickedObject != null && clickedObject == affectedObject)
            {
                // ������ ����
                float totalDamage = damagePerClick;
                if (isSwordActive)
                {
                    totalDamage += swordDamageBonus;
                }

                // �� �κп��� totalDamage�� int�� ��ȯ�Ͽ� ����մϴ�.
                int damageInt = Mathf.RoundToInt(totalDamage);

                // Enemy �±׸� ���� ������Ʈ���� MonsterHealth ������Ʈ�� �����ͼ� ������ ����
                MonsterHealth monsterHealth = affectedObject.GetComponent<MonsterHealth>();
                if (monsterHealth != null)
                {
                    monsterHealth.TakeDamage(damageInt);

                    // ������Ʈ�� �ڷ� �о�� (������ ���� ����Ͽ�)
                    rb.AddForce(-transform.forward * moveForce, ForceMode.Impulse);

                    // ���� ȿ�� �ֱ�
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                }
                else
                {
                    Debug.LogError("Affected object does not have MonsterHealth component.");
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