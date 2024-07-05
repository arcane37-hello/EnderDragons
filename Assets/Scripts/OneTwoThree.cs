using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneTwoThree : MonoBehaviour
{
    public GameObject InventoryImage;

    void Start()
    {
        if (InventoryImage != null)
        {
            InventoryImage.SetActive(false);
        }

    }

    void Update()
    {
        // ��ư 1���� ������ 1_inventory_button�� �����ϰ� �ʹ�.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
           ToggleInventoryImage();

        }

        // ��ư 2���� ������ 2_inventory_button�� �����ϰ� �ʹ�.


        // ��ư 3���� ������ 3_inventory_button�� �����ϰ� �ʹ�.
        

    }
    void ToggleInventoryImage()
    {
        if (InventoryImage != null)
        {
            bool isActive = InventoryImage.activeSelf;
            InventoryImage.SetActive(!isActive);
            Debug.Log("Toggled Inventory Image : " + !isActive);
        }
    }
}
