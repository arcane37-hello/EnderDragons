//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateItem : MonoBehaviour
{
    public GameObject[] StateInventoryImage; //인벤토리 이미지 배열
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
// 초기 이미지 비활성화
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
// 숫자키 입력 처리
        for (int i = 1; i <= 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + (i - 1))) //1~9까지
            {
                ToggleStateInventoryImage(i - 1); //0키를 제외
            }
        }
    }
    void ToggleStateInventoryImage(int index)
    {
        // 모든 이미지 비활성화
         for (int i = 0; i<StateInventoryImage.Length; i++)
        {
            if (StateInventoryImage[i] != null)
            {
                StateInventoryImage[i].SetActive(false);
            }

         }
        //해당 사진 활성화
            if (index >= 0 && index < StateInventoryImage.Length && StateInventoryImage[index] != null)
            {
                //bool isActive = StateInventoryImage[index].activeSelf;
                //StateInventoryImage[index].SetActive(!isActive);
                StateInventoryImage[index].SetActive(true);

            }
     }
}
