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
}
