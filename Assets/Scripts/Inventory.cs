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
    public GameObject blackbackground; // 어두운 배경
    public bool isblackbackground = false;

    void Start()
    {
        //인벤토리를 비활성화한다.
        Inventory1.SetActive(false);
        //if (Inventory1 != null)
        //{
        //    Inventory1.SetActive(false); // 초기 상태에서 인벤토리 비활성화
        //}
        //if (blackbackground != null)
        //{
        //    blackbackground.SetActive(false); // 초기 상태 배경 비활성화
        //}
        blackbackground.SetActive(false); // 초기 상태 배경 비활성화
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
             if (isblackbackground)
                {
                    blackbackground.SetActive(true); // 배경 활성화
                    isblackbackground = false;
                }
             else
            {
                if (blackbackground == null)
                {
                    blackbackground.SetActive(false); // 배경 비활성화
                    isblackbackground = true;
                }
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


    // 게임 오버 시 실행함수
    public void ShowGameOverUI()
    {
        // 게임 오버 UI 오브젝트를 활성화한다.
        GameOverUI.SetActive(true);

        // 업데이트 시간을 0배속으로 변경한다. (시간을 멈춤)
        Time.timeScale = 0f;
    }
    // 계속하기 시에 실행함수
    public void ContinueGame()
    {
        GameOverUI.SetActive(false);
        Time.timeScale = 1.0f;
    }
    // 게임을 처음부터 다시 시작하는 함수
    public void RestartGame()
    {
        // 업데이트 시간을 다시 1배율로 변경한다.
        Time.timeScale = 1.0f;

        // 현재 씬 다시 재생
        SceneManager.LoadScene("PlayerScene");
    }
    // 어플리케이션을 종료하는 함수
    public void QuitGame()
    {
#if UNITY_EDITOR
        // 1. 에디터의 경우
        EditorApplication.ExitPlaymode();
#elif UNITY_STANDALONE
        // 2. 어플리케이션 경우
        Application.Quit();
#endif
    }
}
