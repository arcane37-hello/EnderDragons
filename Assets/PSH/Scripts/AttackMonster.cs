using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMonster : MonoBehaviour
{
    public float damagePerClick = 2f; // Ŭ���� ������ �⺻ ������
    public float swordDamageBonus = 5f; // Į�� Ȱ��ȭ�Ǿ��� �� �߰��� ������ ������

    private bool isSwordActive = false; // Į�� Ȱ��ȭ�Ǿ����� ����
    private Rigidbody rb;
    private float moveForce = 5f;
    private float jumpForce = 5f;

    public AudioClip[] sounds; //�Ҹ� �迭
    AudioSource audioSource2;

    void Start()
    {
        // affectedObject�� �� ��ũ��Ʈ�� �پ� �ִ� ������Ʈ �ڽ����� ����
        // 'affectedObject' ������ �� �̻� �ʿ� �����Ƿ� ����
        rb = GetComponent<Rigidbody>();
        // �����
        audioSource2 = transform.GetComponent<AudioSource>();
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

            // Ŭ���� ������Ʈ�� �� ��ũ��Ʈ�� �پ� �ִ� ������Ʈ�� ���� ��쿡�� ����
            if (clickedObject != null && clickedObject == gameObject)
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
                MonsterHealth monsterHealth = GetComponent<MonsterHealth>();
                if (monsterHealth != null)
                {
                    monsterHealth.TakeDamage(damageInt);

                    // ������Ʈ�� �ڷ� �о�� (������ ���� ����Ͽ�)
                    rb.AddForce(-transform.forward * moveForce, ForceMode.Impulse);

                    // ���� ȿ�� �ֱ�
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                }
                // ���Դ� �Ҹ��� �����Ѵ�.
                audioSource2.clip = sounds[0];
                audioSource2.volume = 3f;
                audioSource2.Play();
                //else
                //{
                //    Debug.LogError("This object does not have MonsterHealth component.");
                //}
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