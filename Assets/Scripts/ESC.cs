using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel; // 일시 정지 패널

    private bool isPaused = false;

    void Start()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false); // 초기 상태에서 패널 비활성화
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                Time.timeScale = 0f; // 게임 시간을 멈춤
                if (pausePanel != null)
                {
                    pausePanel.SetActive(true); // 일시 정지 패널 활성화
                }
                Cursor.lockState = CursorLockMode.None; // 마우스 커서 활성화
                Cursor.visible = true;
                //Debug.Log("Game Paused");
            }
            else
            {
                Time.timeScale = 1f; // 게임 시간을 재개
                if (pausePanel != null)
                {
                    pausePanel.SetActive(false); // 일시 정지 패널 비활성화
                }
                Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 비활성화
                Cursor.visible = false;
                //Debug.Log("Game Resumed");
            }
        }
    }

   
}
