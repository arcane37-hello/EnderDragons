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
//        //// 버튼 1번을 누르면 1_inventory_button을 실행하고 싶다.
//        //if (Input.GetKeyDown(KeyCode.Alpha1))
//        //{
//        //    SwitchItem(Image1);
//        //}

//        //// 버튼 2번을 누르면 2_inventory_button을 실행하고 싶다.
//        //if (Input.GetKeyDown(KeyCode.Alpha2))
//        //{
//        //    SwitchItem(Image2);
//        //}


//        //// 버튼 3번을 누르면 3_inventory_button을 실행하고 싶다.
        
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
//    //    // 현재 무기가 있는 경우 비활성화
//    //    if (current != null)
//    //    {
//    //        current.SetActive(false);
//    //    }

//    //    // 새로운 무기 활성화
//    //    if (current != null)
//    //    {
//    //        newItem.SetActive(true);
//    //        current = newItem;
//    //    }
//    //    else
//    //    {
//    //        Debug.LogError("무기 프리팹이 설정되어 있지 않습니다!");
//    //    }
//    //}

//}
