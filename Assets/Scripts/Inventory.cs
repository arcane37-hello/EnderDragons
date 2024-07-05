using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class Inventory : MonoBehaviour
{
    public GameObject GameOverUI;
    public GameObject Inventory1;
    public bool isInventoryOpen = false;

    void Start()
    {
        //�κ��丮�� ��Ȱ��ȭ�Ѵ�.
        Inventory1.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isInventoryOpen)
            {
                Inventory1.SetActive(true);
                isInventoryOpen = false;
            }
            else 
            {
                Inventory1.SetActive(false);
                isInventoryOpen=true;
            }



        }


        //if (Input.GetKeyDown(KeyCode.E))
        //    InventoryUI();

    }
//    public void InventoryUI
//    {
//         if (Input.GetKeyDown(KeyCode.E))
//        {
//            if(isInventoryOpen)
//            Inventory.SetActive(true);
//}

//    }


    // ���� ���� �� �����Լ�
    public void ShowGameOverUI()
    {
        // ���� ���� UI ������Ʈ�� Ȱ��ȭ�Ѵ�.
        GameOverUI.SetActive(true);

        // ������Ʈ �ð��� 0������� �����Ѵ�. (�ð��� ����)
        Time.timeScale = 0f;
    }
    // ����ϱ� �ÿ� �����Լ�
    public void ContinueGame()
    {
        GameOverUI.SetActive(false);
        Time.timeScale = 1.0f;
    }
    // ������ ó������ �ٽ� �����ϴ� �Լ�
    public void RestartGame()
    {
        // ������Ʈ �ð��� �ٽ� 1������ �����Ѵ�.
        Time.timeScale = 1.0f;

        // ���� �� �ٽ� ���
        SceneManager.LoadScene("PlayerScene");
    }
    // ���ø����̼��� �����ϴ� �Լ�
    public void QuitGame()
    {
#if UNITY_EDITOR
        // 1. �������� ���
        EditorApplication.ExitPlaymode();
#elif UNITY_STANDALONE
        // 2. ���ø����̼� ���
        Application.Quit();
#endif
    }
}