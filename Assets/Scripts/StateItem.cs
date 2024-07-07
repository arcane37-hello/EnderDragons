//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateItem : MonoBehaviour
{
    public GameObject[] StateInventoryImage; //�κ��丮 �̹��� �迭
//    public Texture2D[] StateInven;
//    public RawImage[] StateInventory;
//    public int InventoryCount = 10;
//    public bool useArray = false;

//        //�ݺ���
//        //�迭 ��ȣ�� ��Ƽ��
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
// �ʱ� �̹��� ��Ȱ��ȭ
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
// ����Ű �Է� ó��
        for (int i = 1; i <= 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + (i - 1))) //1~9����
            {
                ToggleStateInventoryImage(i - 1); //0Ű�� ����
            }
        }
    }
    void ToggleStateInventoryImage(int index)
    {
        // ��� �̹��� ��Ȱ��ȭ
         for (int i = 0; i<StateInventoryImage.Length; i++)
        {
            if (StateInventoryImage[i] != null)
            {
                StateInventoryImage[i].SetActive(false);
            }

         }
        //�ش� ���� Ȱ��ȭ
            if (index >= 0 && index < StateInventoryImage.Length && StateInventoryImage[index] != null)
            {
                //bool isActive = StateInventoryImage[index].activeSelf;
                //StateInventoryImage[index].SetActive(!isActive);
                StateInventoryImage[index].SetActive(true);

            }
     }
}
