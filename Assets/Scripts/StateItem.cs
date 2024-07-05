//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateItem : MonoBehaviour
{
    public GameObject[] StateInventoryImage;
//    public Texture2D[] StateInven;
//    public RawImage[] StateInventory;
//    public int InventoryCount = 10;
//    public bool useArray = false;

//        //반복문
//        //배열 번호만 액티브
//        //

    void Start()
    {
//        if (useArray)
//        {
//            StateInventory = new GameObject[InventoryCount];

//            for (int i = 0; i < InventoryCount; i++)
//            {

//            }
//        }
        foreach (var image in StateInventoryImage)
        {
            if (image != null)
            {
                image.SetActive(false);
            }
        }

    }

    void Update()
    {
//        for (int i = 0; i < StateInventory.Length; i++)
//        {
//            if (StateInventory[i] != null)
//            {
//                if (!StateInventory[i].activeInHierachy)
//                {
//                    StateInventory[i].SetActive(true);
//                }
//            }
//        }
        for (int i = 1; i <= 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + (i - 1)))
            {
                ToggleStateInventoryImage(i - 1);
            }
        }
    }
    void ToggleStateInventoryImage(int index)
    {
        if (index >= 0 && index < StateInventoryImage.Length && StateInventoryImage[index] != null)
        {
            bool isActive = StateInventoryImage[index].activeSelf;
            StateInventoryImage[index].SetActive(isActive);
        }
    }
}
