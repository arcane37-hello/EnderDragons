using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryNumber : MonoBehaviour
{
    public GameObject pickaxePrefab;
    public GameObject swordPrefab;

    private GameObject currentWeapon; // 현재 활성화된 무기

    void Start()
    {
        currentWeapon = null;
    }

    void Update()
    {
        // 숫자 1을 누르면 곡괭이 활성화
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(pickaxePrefab);
        }

        // 숫자 2를 누르면 검 활성화
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(swordPrefab);
        }

        // 숫자 1과 2 이외의 다른 숫자 키를 누르면 현재 무기 비활성화
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
        // 현재 무기가 있는 경우 비활성화
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false);
        }

        // 새로운 무기 활성화
        if (newWeaponPrefab != null)
        {
            newWeaponPrefab.SetActive(true);
            currentWeapon = newWeaponPrefab;
        }
        else
        {
            Debug.LogError("무기 프리팹이 설정되어 있지 않습니다!");
        }
    }

    void DisableCurrentWeapon()
    {
        // 현재 무기 비활성화
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false);
            currentWeapon = null;
        }
    }
}