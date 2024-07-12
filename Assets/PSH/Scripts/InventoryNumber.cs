using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryNumber : MonoBehaviour
{
    public GameObject pickaxePrefab;
    public GameObject swordPrefab;
    public GameObject bowPrefab;
    public GameObject arrowPrefab; // 화살 프리팹

    private GameObject currentWeapon; // 현재 활성화된 무기
    private bool isBowActive = false; // 활이 활성화되었는지 여부
    private bool isAiming = false; // 활을 조준 중인지 여부
    private float aimTime = 0f; // 조준 시간

    public float maxArrowForce = 50f; // 최대 화살 발사 힘
    public float minArrowForce = 10f; // 최소 화살 발사 힘
    public float maxAimingTime = 3f;  // 최대 조준 시간

    void Start()
    {
        currentWeapon = null;
        isBowActive = false; // 초기에는 활이 비활성화 상태
    }

    void Update()
    {
        // 숫자 1을 누르면 곡괭이 활성화
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(pickaxePrefab);
            isBowActive = false; // 활 비활성화 상태
        }

        // 숫자 2를 누르면 검 활성화
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(swordPrefab);
            isBowActive = false; // 활 비활성화 상태
        }

        // 숫자 5를 누르면 활 활성화
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchWeapon(bowPrefab);
            isBowActive = true; // 활 활성화 상태
        }

        // 숫자 1과 2 이외의 다른 숫자 키를 누르면 현재 무기 비활성화
        if (Input.GetKeyDown(KeyCode.Alpha3) ||
            Input.GetKeyDown(KeyCode.Alpha4) ||
            Input.GetKeyDown(KeyCode.Alpha6) ||
            Input.GetKeyDown(KeyCode.Alpha7) ||
            Input.GetKeyDown(KeyCode.Alpha8) ||
            Input.GetKeyDown(KeyCode.Alpha9) ||
            Input.GetKeyDown(KeyCode.Alpha0))
        {
            DisableCurrentWeapon();
            isBowActive = false; // 활 비활성화 상태
        }

        // 오른쪽 마우스 버튼 클릭 시 활이 활성화되어 있을 때만 화살 발사
        if (Input.GetMouseButton(1) && isBowActive)
        {
            isAiming = true;
            aimTime += Time.deltaTime;

            // 최대 조준 시간 초과 시 aimTime을 maxAimingTime으로 제한
            if (aimTime > maxAimingTime)
            {
                aimTime = maxAimingTime;
            }
        }
        else if (Input.GetMouseButtonUp(1) && isAiming)
        {
            ShootArrow();
            isAiming = false;
            aimTime = 0f; // 조준 시간 초기화
        }

        // 조준 중이 아니고 마우스 버튼이 아닌 상태에서 조준 시간 초기화
        if (Input.GetMouseButtonUp(1) && !isAiming)
        {
            aimTime = 0f; // 조준 시간 초기화
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

    void ShootArrow()
    {
        if (arrowPrefab != null)
        {
            // 조준 시간에 따라 발사 힘을 설정합니다.
            float arrowForce = Mathf.Lerp(minArrowForce, maxArrowForce, aimTime / maxAimingTime);

            // 화살 발사
            GameObject arrow = Instantiate(arrowPrefab, transform.position + transform.forward, transform.rotation);
            Rigidbody rb = arrow.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(transform.forward * arrowForce, ForceMode.Impulse); // 조준 시간에 따라 힘 조절
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