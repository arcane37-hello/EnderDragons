using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class Inventory : MonoBehaviour
{
    public GameObject Inventory1;
    public bool isInventoryOpen = false;
    public GameObject Inventorybackground; // ��ο� ���
    public bool isblackbackground = false;



    void Start()
    {
        //�κ��丮�� ��Ȱ��ȭ�Ѵ�.
        //Inventory1.SetActive(false);
        if (Inventory1 != null)
        {
            Inventory1.SetActive(false); // �ʱ� ���¿��� �κ��丮 ��Ȱ��ȭ
        }

        if (Inventorybackground != null)
        {
            Inventorybackground.SetActive(false);// �ʱ� ���� ��� ��Ȱ��ȭ
        }
    }



    void Update()
    {
       if (Input.GetKeyDown(KeyCode.E))
        {
            //    isInventoryOpen = !isInventoryOpen;

            //    if (isInventoryOpen)
            //    {
            //        if (Inventorybackground != null)
            //        {
            //            Inventorybackground.SetActive(true);
            //        }
            Cursor.lockState = CursorLockMode.None; // ���콺 Ŀ�� Ȱ��ȭ
            Cursor.visible = true;
            //    }
            //else
            //{
            //    ResumeGame(); // �Ͻ� ���� ����
            //}

            if (isInventoryOpen)
            {
                Inventory1.SetActive(true);
                isInventoryOpen = false;

            }
            else
            {
                Inventory1.SetActive(false);
                isInventoryOpen = true;
            }
            //if (isblackbackground)
            //{
            //    Inventorybackground.SetActive(true);
            //    isblackbackground = false;
            //}
            //else
            //{
            //    Inventorybackground.SetActive(true);
            //    isblackbackground= true;
            //}

        }
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    InventoryUI();
        //}
    }
    //public void ResumeGame()
    //{
    //    // ���� �簳
    //    isInventoryOpen = false;
    //    Time.timeScale = 1f;
    //    if (Inventorybackground != null)
    //    {
    //        Inventorybackground.SetActive(false);
    //    }
       
    
   // }
    //public void InventoryUI()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        if (isInventoryOpen)
    //        {
    //            Inventory1.SetActive(true);
    //        }
    //    }
    //}
}

   

    
            
    

    




//// ���� �κ��丮

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;
//using UnityEditor;

//public class Inventory : MonoBehaviour
//{
//    public GameObject GameOverUI;
//    public GameObject Inventory1;
//    public bool isInventoryOpen = false;
//    public GameObject blackbackground; // ��ο� ���
//    public bool isblackbackground = false;

//    void Start()
//    {
//        //�κ��丮�� ��Ȱ��ȭ�Ѵ�.
//        Inventory1.SetActive(false);
//        //if (Inventory1 != null)
//        //{
//        //    Inventory1.SetActive(false); // �ʱ� ���¿��� �κ��丮 ��Ȱ��ȭ
//        //}
//        //if (blackbackground != null)
//        //{
//        //    blackbackground.SetActive(false); // �ʱ� ���� ��� ��Ȱ��ȭ
//        //}
//        blackbackground.SetActive(false); // �ʱ� ���� ��� ��Ȱ��ȭ
//    }


//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.E))
//        {
//            if (isInventoryOpen)
//            {
//                Inventory1.SetActive(true);
//                isInventoryOpen = false;
//            }
//            else
//            {
//                Inventory1.SetActive(false);
//                isInventoryOpen = true;
//            }
//            if (isblackbackground)
//            {
//                blackbackground.SetActive(true); // ��� Ȱ��ȭ
//                isblackbackground = false;
//            }
//            else
//            {
//                if (blackbackground == null)
//                {
//                    blackbackground.SetActive(false); // ��� ��Ȱ��ȭ
//                    isblackbackground = true;
//                }
//            }


//        }
//        if (Input.GetKeyDown(KeyCode.Escape))
//        {
//            isblackbackground = !isblackbackground;

//            if (isblackbackground)
//            {

//                if (blackbackground != null)
//                {
//                    blackbackground.SetActive(true); // ��� Ȱ��ȭ
//                }

//                Cursor.lockState = CursorLockMode.None; // ���콺 Ŀ�� Ȱ��ȭ
//                Cursor.visible = true;
//                //Debug.Log("Game Paused");
//            }
//            else
//            {
//                ResumeGame();
//            }
//        }

//        //if (Input.GetKeyDown(KeyCode.E))
//        //    InventoryUI();

//    }
//    public void ResumeGame()
//    {
//        isblackbackground = false;
//        Time.timeScale = 1f; // ���� �ð��� �簳
//        if (blackbackground != null)
//        {
//            blackbackground.SetActive(false); // ��� ��Ȱ��ȭ
//        }

//        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� ��Ȱ��ȭ
//        Cursor.visible = true;
//        //Debug.Log("Game Resumed");
//    }
//    //    public void InventoryUI
//    //    {
//    //         if (Input.GetKeyDown(KeyCode.E))
//    //        {
//    //            if(isInventoryOpen)
//    //            Inventory.SetActive(true);
//    //}

//    //    }


//    // ���� ���� �� �����Լ�
//    //    public void ShowGameOverUI()
//    //    {
//    //        // ���� ���� UI ������Ʈ�� Ȱ��ȭ�Ѵ�.
//    //        GameOverUI.SetActive(true);

//    //        // ������Ʈ �ð��� 0������� �����Ѵ�. (�ð��� ����)
//    //        Time.timeScale = 0f;
//    //    }
//    //    // ����ϱ� �ÿ� �����Լ�
//    //    public void ContinueGame()
//    //    {
//    //        GameOverUI.SetActive(false);
//    //        Time.timeScale = 1.0f;
//    //    }
//    //    // ������ ó������ �ٽ� �����ϴ� �Լ�
//    //    public void RestartGame()
//    //    {
//    //        // ������Ʈ �ð��� �ٽ� 1������ �����Ѵ�.
//    //        Time.timeScale = 1.0f;

//    //        // ���� �� �ٽ� ���
//    //        SceneManager.LoadScene("PlayerScene");
//    //    }
//    //    // ���ø����̼��� �����ϴ� �Լ�
//    //    public void QuitGame()
//    //    {
//    //#if UNITY_EDITOR
//    //        // 1. �������� ���
//    //        EditorApplication.ExitPlaymode();
//    //#elif UNITY_STANDALONE
//    //        // 2. ���ø����̼� ���
//    //        Application.Quit();
//    //#endif
//    //    }
//}

