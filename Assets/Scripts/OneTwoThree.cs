//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class OneTwoThree : MonoBehaviour
//{
//    //public GameObject Image1;
//    //public GameObject Image2;

//    //private GameObject current;

//    public GameObject inventoryImage;
//    public GameObject inventoryImage2;

//    void Start()
//    {
//        //current = null;

//        if (inventoryImage != null)
//        {
//            inventoryImage.SetActive(false);
//        }
//        if (inventoryImage2 != null)
//        {
//            inventoryImage2.SetActive(false);
//        }
//    }

//    void Update()
//    {
//        //// ��ư 1���� ������ 1_inventory_button�� �����ϰ� �ʹ�.
//        //if (Input.GetKeyDown(KeyCode.Alpha1))
//        //{
//        //    SwitchItem(Image1);
//        //}

//        //// ��ư 2���� ������ 2_inventory_button�� �����ϰ� �ʹ�.
//        //if (Input.GetKeyDown(KeyCode.Alpha2))
//        //{
//        //    SwitchItem(Image2);
//        //}


//        //// ��ư 3���� ������ 3_inventory_button�� �����ϰ� �ʹ�.
        
//        if (Input.GetKeyDown(KeyCode.Alpha1))
//        {
//            if (inventoryImage != null)
//            {
//                inventoryImage.SetActive(!inventoryImage.activeSelf);
//            }
//        }
//        if (Input.GetKeyDown(KeyCode.Alpha2))
//        {
//            if (inventoryImage != null)
//            {
//                inventoryImage.SetActive(!inventoryImage2.activeSelf);
//            }
//        }

//    }

//    //void SwitchItem(GameObject newItem)
//    //{
//    //    // ���� ���Ⱑ �ִ� ��� ��Ȱ��ȭ
//    //    if (current != null)
//    //    {
//    //        current.SetActive(false);
//    //    }

//    //    // ���ο� ���� Ȱ��ȭ
//    //    if (current != null)
//    //    {
//    //        newItem.SetActive(true);
//    //        current = newItem;
//    //    }
//    //    else
//    //    {
//    //        Debug.LogError("���� �������� �����Ǿ� ���� �ʽ��ϴ�!");
//    //    }
//    //}

//}
