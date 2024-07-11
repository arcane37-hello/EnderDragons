using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryNumber : MonoBehaviour
{
    public GameObject pickaxePrefab;
    public GameObject swordPrefab;
    public GameObject bowPrefab;
    public GameObject arrowPrefab; // ȭ�� ������

    private GameObject currentWeapon; // ���� Ȱ��ȭ�� ����
    private bool isBowActive = false; // Ȱ�� Ȱ��ȭ�Ǿ����� ����

    void Start()
    {
        currentWeapon = null;
        isBowActive = false; // �ʱ⿡�� Ȱ�� ��Ȱ��ȭ ����
    }

    void Update()
    {
        // ���� 1�� ������ ��� Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(pickaxePrefab);
            isBowActive = false; // Ȱ ��Ȱ��ȭ ����
        }

        // ���� 2�� ������ �� Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(swordPrefab);
            isBowActive = false; // Ȱ ��Ȱ��ȭ ����
        }

        // ���� 5�� ������ Ȱ Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchWeapon(bowPrefab);
            isBowActive = true; // Ȱ Ȱ��ȭ ����
        }

        // ���� 1�� 2 �̿��� �ٸ� ���� Ű�� ������ ���� ���� ��Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Alpha3) ||
            Input.GetKeyDown(KeyCode.Alpha4) ||
            Input.GetKeyDown(KeyCode.Alpha6) ||
            Input.GetKeyDown(KeyCode.Alpha7) ||
            Input.GetKeyDown(KeyCode.Alpha8) ||
            Input.GetKeyDown(KeyCode.Alpha9) ||
            Input.GetKeyDown(KeyCode.Alpha0))
        {
            DisableCurrentWeapon();
            isBowActive = false; // Ȱ ��Ȱ��ȭ ����
        }

        // ������ ���콺 ��ư Ŭ�� �� Ȱ�� Ȱ��ȭ�Ǿ� ���� ���� ȭ�� �߻�
        if (Input.GetMouseButtonDown(1) && isBowActive)
        {
            ShootArrow();
        }
    }

    void SwitchWeapon(GameObject newWeaponPrefab)
    {
        // ���� ���Ⱑ �ִ� ��� ��Ȱ��ȭ
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false);
        }

        // ���ο� ���� Ȱ��ȭ
        if (newWeaponPrefab != null)
        {
            newWeaponPrefab.SetActive(true);
            currentWeapon = newWeaponPrefab;
        }
        else
        {
            Debug.LogError("���� �������� �����Ǿ� ���� �ʽ��ϴ�!");
        }
    }

    void DisableCurrentWeapon()
    {
        // ���� ���� ��Ȱ��ȭ
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false);
            currentWeapon = null;
        }
    }

    void ShootArrow()
    {
        if (arrowPrefab != null)
        {
            // ȭ�� �߻�
            GameObject arrow = Instantiate(arrowPrefab, transform.position + transform.forward, Quaternion.identity);
            Rigidbody rb = arrow.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(transform.forward * 50f, ForceMode.Impulse); // ȭ�� �߻� ��
            }
            else
            {
                Debug.LogError("Arrow prefab is missing a Rigidbody component.");
            }
        }
        else
        {
            Debug.LogError("Arrow prefab is not assigned.");
        }
    }
}