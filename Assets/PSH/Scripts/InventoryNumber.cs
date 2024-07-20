using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryNumber : MonoBehaviour
{
    public GameObject pickaxePrefab;
    public GameObject swordPrefab;
    public GameObject bowPrefab;
    public GameObject arrowPrefab;
    public GameObject breadPrefab;
    public GameObject grassPrefab;
    public GameObject rockPrefab;

    public float maxArrowForce = 50f;
    public float minArrowForce = 10f;
    public float maxAimingTime = 3f;
    public Transform arrowSpawnPoint; // 화살 발사 위치

    private GameObject currentWeapon;
    private bool isBowActive = false;
    private bool isAiming = false;
    private float aimTime = 0f;

    private static InventoryNumber instance;

    public static InventoryNumber Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryNumber>();
            }
            return instance;
        }
    }

    void Start()
    {
        currentWeapon = null;
        isBowActive = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(pickaxePrefab);
            isBowActive = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(swordPrefab);
            isBowActive = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeapon(grassPrefab);
            isBowActive = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchWeapon(rockPrefab);
            isBowActive = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchWeapon(bowPrefab);
            isBowActive = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SwitchWeapon(breadPrefab);
            isBowActive = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6) ||
            Input.GetKeyDown(KeyCode.Alpha7) ||
            Input.GetKeyDown(KeyCode.Alpha8) ||
            Input.GetKeyDown(KeyCode.Alpha0))
        {
            DisableCurrentWeapon();
            isBowActive = false;
        }

        if (Input.GetMouseButton(1) && isBowActive)
        {
            isAiming = true;
            aimTime += Time.deltaTime;

            if (aimTime > maxAimingTime)
            {
                aimTime = maxAimingTime;
            }

            
        }
        else if (Input.GetMouseButtonUp(1) && isAiming)
        {
            ShootArrow();
            isAiming = false;
            aimTime = 0f;

            
        }
        else if (Input.GetMouseButtonUp(1) && !isAiming)
        {
            aimTime = 0f;
          
        }
    }

    void SwitchWeapon(GameObject newWeaponPrefab)
    {
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false);
        }

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
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false);
            currentWeapon = null;
        }
    }

    void ShootArrow()
    {
        if (arrowPrefab != null && arrowSpawnPoint != null)
        {
            float arrowForce = Mathf.Lerp(minArrowForce, maxArrowForce, aimTime / maxAimingTime);

            GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
            Rigidbody rb = arrow.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(arrowSpawnPoint.forward * arrowForce, ForceMode.Impulse);
            }
            else
            {
                Debug.LogError("Arrow prefab is missing a Rigidbody component.");
            }
        }
        else
        {
            Debug.LogError("Arrow prefab or arrowSpawnPoint is not assigned.");
        }
    }

    public bool IsSwordActive()
    {
        // 예시로 swordPrefab이 현재 활성화된 무기인지 여부를 반환하는 메서드
        return currentWeapon == swordPrefab;
    }
}