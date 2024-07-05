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
        // 버튼 1번을 누르면 1_inventory_button을 실행하고 싶다.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
           ToggleInventoryImage();

        }

        // 버튼 2번을 누르면 2_inventory_button을 실행하고 싶다.


        // 버튼 3번을 누르면 3_inventory_button을 실행하고 싶다.
        

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
