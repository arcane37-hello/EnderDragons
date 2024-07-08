using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryNumber : MonoBehaviour
{
    public GameObject pickaxePrefab;
    public GameObject swordPrefab;

    private GameObject currentWeapon; // ���� Ȱ��ȭ�� ����

    void Start()
    {
        currentWeapon = null;
    }

    void Update()
    {
        // ���� 1�� ������ ��� Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(pickaxePrefab);
        }

        // ���� 2�� ������ �� Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(swordPrefab);
        }

        // ���� 1�� 2 �̿��� �ٸ� ���� Ű�� ������ ���� ���� ��Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Alpha3) ||
            Input.GetKeyDown(KeyCode.Alpha4) ||
            Input.GetKeyDown(KeyCode.Alpha5) ||
            Input.GetKeyDown(KeyCode.Alpha6) ||
            Input.GetKeyDown(KeyCode.Alpha7) ||
            Input.GetKeyDown(KeyCode.Alpha8) ||
            Input.GetKeyDown(KeyCode.Alpha9) ||
            Input.GetKeyDown(KeyCode.Alpha0))
        {
            DisableCurrentWeapon();
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
}