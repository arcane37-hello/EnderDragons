
// 있던거에서 내멋대로 변형
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;       // UI 클래스 관련 네임 스페이스
using UnityEngine.SceneManagement;  // Scene을 다루고 관리하는 클래스 관련 네임 스페이스

public class ESC : MonoBehaviour
{
    public GameObject pausePanel; // 일시 정지 패널
    public GameObject blackbackground; // 어두운 배경
    public Button resumeButton; // 계속하기 버튼
    public GameObject gameOverUI; // 게임 오버 UI 패널

    private bool isPaused = false; // 일시 정지 상태를 추적하는 변수

    void Start()
    {
        // 일시 정지 패널과 어두운 배경을 초기 상태에서 비활성화
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
        if (blackbackground != null)
        {
            blackbackground.SetActive(false);
        }
     

        // 게임 오버 UI 초기 비활성화
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
    }

    void Update()
    {
        // ESC 키를 누르면 일시 정지 상태 전환
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                // 게임 일시 정지
                Time.timeScale = 0f;
                if (blackbackground != null)
                {
                    blackbackground.SetActive(true);
                }
                if (pausePanel != null)
                {
                    pausePanel.SetActive(true);
                }
                Cursor.lockState = CursorLockMode.None; // 마우스 커서 활성화
                Cursor.visible = true;
            }
            else
            {
                ResumeGame(); // 일시 정지 해제
            }
        }
        // 계속하기 버튼 클릭 이벤트에 ResumeGame 메서드 연결
        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(ResumeGame);
        }
    }

    public void ResumeGame()
    {
        // 게임 재개
        isPaused = false;
        Time.timeScale = 1f;
        if (blackbackground != null)
        {
            blackbackground.SetActive(false);
        }
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
        //Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 비활성화
        //Cursor.visible = false;
    }
    // 게임을 처음부터 다시 시작하는 함수
    public void Title()
    {
        // 업데이트 시간을 다시 1배율로 변경한다.
        Time.timeScale = 1.0f;

        // 현재 씬을 다시 시작한다.
        SceneManager.LoadScene(0);
    }

    // 게임 오버 UI를 표시하고 시간을 멈추는 메서드
    public void ShowGameOverUI()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        Time.timeScale = 0f;
    }

    // 게임 오버 후 계속하기 버튼을 눌렀을 때 실행할 메서드
    public void ContinueGame()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
        Time.timeScale = 1f;
    }
}


//// 기존 ESC에그냥 shooting 프로젝트 따라해보기

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;       // UI 클래스 관련 네임 스페이스
//using UnityEngine.SceneManagement;  // Scene을 다루고 관리하는 클래스 관련 네임 스페이스
//using UnityEditor;          // Unity Editor 관련 기능을 다루는 클래스 관련 네임 스페이스

//public class ESC : MonoBehaviour
//{
//    public GameObject pausePanel; // 일시 정지 패널
//    public GameObject blackbackground; // 어두운 배경
//    public Button resumeButton; // 계속하기 버튼

//    public GameObject gameOverUI;

//    private bool isPaused = false;

//    void Start()
//    {
//        if (pausePanel != null)
//        {
//            pausePanel.SetActive(false); // 초기 상태에서 패널 비활성화
//        }
//        if (blackbackground != null)
//        {
//            blackbackground.SetActive(false); // 초기 상태 배경 비활성화
//        }
//        if (resumeButton != null)
//        {
//            resumeButton.onClick.AddListener(ResumeGame); // 버튼 클릭 이벤트에 업데이트 추가
//        }

//    }

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Escape))
//        {
//            isPaused = !isPaused;

//            if (isPaused)
//            {
//                Time.timeScale = 0f; // 게임 시간을 멈춤
//                if (blackbackground != null)
//                {
//                    blackbackground.SetActive(true); // 배경 활성화
//                }
//                if (pausePanel != null)
//                {
//                    pausePanel.SetActive(true); // 일시 정지 패널 활성화
//                }
//                Cursor.lockState = CursorLockMode.None; // 마우스 커서 활성화
//                Cursor.visible = true;
//                //Debug.Log("Game Paused");
//            }
//            else
//            {
//                ResumeGame();
//            }
//        }
//    }

//    public void ResumeGame()
//    {
//        isPaused = false;
//        Time.timeScale = 1f; // 게임 시간을 재개
//        if (blackbackground != null)
//        {
//            blackbackground.SetActive(false); // 배경 비활성화
//        }
//        if (pausePanel != null)
//        {
//            pausePanel.SetActive(false); // 일시 정지 패널 비활성화
//        }
//        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 비활성화
//        Cursor.visible = false;
//        //Debug.Log("Game Resumed");
//    }

//    // 게임 오버가 되면 실행할 함수
//    public void ShowGameOverUI()
//    {
//        // 게임 오버 UI 오브젝트를 활성화한다.
//        gameOverUI.SetActive(true);

//        // 업데이트 시간을 0배속으로 변경한다. (시간을 멈춘다)
//        Time.timeScale = 0;
//    }

//    // 계속하기 버튼을 눌렀을 때 실행할 함수
//    public void ContinueGame()
//    {
//        gameOverUI.SetActive(false);
//        Time.timeScale = 1.0f;
//    }

//}



// 유니티 책 보고 하려는데 변수 선언 모자라서 안됨
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;

//public class PauseManager : MonoBehaviour
//{
//    public GameObject pausePanel; // 일시 정지 패널
//    public GameObject blackbackground; // 어두운 배경
//    public Button resumeButton; // 계속하기 버튼


//    private bool isPaused = false;

//    // 게임 상태 상수
//    public enum GameState
//    {
//        Ready,
//        Run,
//        Pause,
//        GameOver
//    }
//    // 옵션 화면 UI 오브젝트 변수
//    public GameObject gameOption;

//    void Start()
//    {
//        if (pausePanel != null)
//        {
//            pausePanel.SetActive(false); // 초기 상태에서 패널 비활성화
//        }
//        if (blackbackground != null)
//        {
//            blackbackground.SetActive(false); // 초기 상태 배경 비활성화
//        }
//        if (resumeButton != null)
//        {
//            resumeButton.onClick.AddListener(ResumeGame); // 버튼 클릭 이벤트에 업데이트 추가
//        }

//    }

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Escape))
//        {
//            isPaused = !isPaused;

//            if (isPaused)
//            {
//                Time.timeScale = 0f; // 게임 시간을 멈춤
//                if (blackbackground != null)
//                {
//                    blackbackground.SetActive(true); // 배경 활성화
//                }
//                if (pausePanel != null)
//                {
//                    pausePanel.SetActive(true); // 일시 정지 패널 활성화
//                }
//                Cursor.lockState = CursorLockMode.None; // 마우스 커서 활성화
//                Cursor.visible = true;
//                //Debug.Log("Game Paused");
//            }
//            else
//            {
//                ResumeGame();
//            }
//        }
//    }

//    public void ResumeGame()
//    {
//        isPaused = false;
//        Time.timeScale = 1f; // 게임 시간을 재개
//        if (blackbackground != null)
//        {
//            blackbackground.SetActive(false); // 배경 비활성화
//        }
//        if (pausePanel != null)
//        {
//            pausePanel.SetActive(false); // 일시 정지 패널 비활성화
//        }
//        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 비활성화
//        Cursor.visible = false;
//        //Debug.Log("Game Resumed");
//    }

//    // 옵션 화면 키기
//    public void OpenOptionWindow()
//    {
//        // 옵션 창 활성화
//        gameOption.SetActive(true );
//        //게임 속도 0배속
//        Time.timeScale = 0f;
//        // 게임 상태를 일시정지로 변경
//        gState = GameState.Pause;
//    }

//    // 계속하기 옵션
//    public void CloseOptionWindow()
//    {
//        // 옵션 창 비활성화
//        gameOption.SetActive(false );
//        // 게임 속도를 1배속으로 전환
//        Time.timeScale = 1f;
//        // 게임 상태를 게임 중 상태로 변경
//        GameState = GameState.Run;
//    }

//    // 다시하기 옵션
//    public void RestartGame()
//    {
//        // 게임 속도를 1배속으로 전환
//        Time.timeScale *= 1f;
//        // 현재 씬 번호를 다시 로드
//        SceneManager.LoadScene(SceneManager.GetActiveScene(), buildIndex);
//    }

//    //게임 종료 
//    public void QuitGame()
//    {
//        //어플리케이션 종료
//        Application.Quit();
//    }


//}

// 기존 ESC
//using UnityEngine;
//using UnityEngine.UI;

//public class PauseManager : MonoBehaviour
//{
//    public GameObject pausePanel; // 일시 정지 패널
//    public GameObject blackbackground; // 어두운 배경
//    public Button resumeButton; // 계속하기 버튼

//    private bool isPaused = false;

//    void Start()
//    {
//        if (pausePanel != null)
//        {
//            pausePanel.SetActive(false); // 초기 상태에서 패널 비활성화
//        }
//        if (blackbackground != null)
//        {
//            blackbackground.SetActive(false); // 초기 상태 배경 비활성화
//        }
//        if (resumeButton != null)
//        {
//            resumeButton.onClick.AddListener(ResumeGame); // 버튼 클릭 이벤트에 업데이트 추가
//        }

//    }

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Escape))
//        {
//            isPaused = !isPaused;

//            if (isPaused)
//            {
//                Time.timeScale = 0f; // 게임 시간을 멈춤
//                if (blackbackground != null)
//                {
//                    blackbackground.SetActive(true); // 배경 활성화
//                }
//                if (pausePanel != null)
//                {
//                    pausePanel.SetActive(true); // 일시 정지 패널 활성화
//                }
//                Cursor.lockState = CursorLockMode.None; // 마우스 커서 활성화
//                Cursor.visible = true;
//                //Debug.Log("Game Paused");
//            }
//            else
//            {
//                ResumeGame();
//            }
//        }
//    }

//    public void ResumeGame()
//    {
//        isPaused = false;
//        Time.timeScale = 1f; // 게임 시간을 재개
//        if (blackbackground != null)
//        {
//            blackbackground.SetActive(false); // 배경 비활성화
//        }
//        if (pausePanel != null)
//        {
//            pausePanel.SetActive(false); // 일시 정지 패널 비활성화
//        }
//        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 비활성화
//        Cursor.visible = false;
//        //Debug.Log("Game Resumed");
//    }



//}
