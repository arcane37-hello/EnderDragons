
// �ִ��ſ��� ���ڴ�� ����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;       // UI Ŭ���� ���� ���� �����̽�
using UnityEngine.SceneManagement;  // Scene�� �ٷ�� �����ϴ� Ŭ���� ���� ���� �����̽�

public class ESC : MonoBehaviour
{
    public GameObject pausePanel; // �Ͻ� ���� �г�
    public GameObject blackbackground; // ��ο� ���
    public Button resumeButton; // ����ϱ� ��ư
    public GameObject gameOverUI; // ���� ���� UI �г�

    private bool isPaused = false; // �Ͻ� ���� ���¸� �����ϴ� ����

    void Start()
    {
        // �Ͻ� ���� �гΰ� ��ο� ����� �ʱ� ���¿��� ��Ȱ��ȭ
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
        if (blackbackground != null)
        {
            blackbackground.SetActive(false);
        }
     

        // ���� ���� UI �ʱ� ��Ȱ��ȭ
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
    }

    void Update()
    {
        // ESC Ű�� ������ �Ͻ� ���� ���� ��ȯ
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                // ���� �Ͻ� ����
                Time.timeScale = 0f;
                if (blackbackground != null)
                {
                    blackbackground.SetActive(true);
                }
                if (pausePanel != null)
                {
                    pausePanel.SetActive(true);
                }
                Cursor.lockState = CursorLockMode.None; // ���콺 Ŀ�� Ȱ��ȭ
                Cursor.visible = true;
            }
            else
            {
                ResumeGame(); // �Ͻ� ���� ����
            }
        }
        // ����ϱ� ��ư Ŭ�� �̺�Ʈ�� ResumeGame �޼��� ����
        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(ResumeGame);
        }
    }

    public void ResumeGame()
    {
        // ���� �簳
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
        //Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� ��Ȱ��ȭ
        //Cursor.visible = false;
    }
    // ������ ó������ �ٽ� �����ϴ� �Լ�
    public void Title()
    {
        // ������Ʈ �ð��� �ٽ� 1������ �����Ѵ�.
        Time.timeScale = 1.0f;

        // ���� ���� �ٽ� �����Ѵ�.
        SceneManager.LoadScene(0);
    }

    // ���� ���� UI�� ǥ���ϰ� �ð��� ���ߴ� �޼���
    public void ShowGameOverUI()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        Time.timeScale = 0f;
    }

    // ���� ���� �� ����ϱ� ��ư�� ������ �� ������ �޼���
    public void ContinueGame()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
        Time.timeScale = 1f;
    }
}


//// ���� ESC���׳� shooting ������Ʈ �����غ���

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;       // UI Ŭ���� ���� ���� �����̽�
//using UnityEngine.SceneManagement;  // Scene�� �ٷ�� �����ϴ� Ŭ���� ���� ���� �����̽�
//using UnityEditor;          // Unity Editor ���� ����� �ٷ�� Ŭ���� ���� ���� �����̽�

//public class ESC : MonoBehaviour
//{
//    public GameObject pausePanel; // �Ͻ� ���� �г�
//    public GameObject blackbackground; // ��ο� ���
//    public Button resumeButton; // ����ϱ� ��ư

//    public GameObject gameOverUI;

//    private bool isPaused = false;

//    void Start()
//    {
//        if (pausePanel != null)
//        {
//            pausePanel.SetActive(false); // �ʱ� ���¿��� �г� ��Ȱ��ȭ
//        }
//        if (blackbackground != null)
//        {
//            blackbackground.SetActive(false); // �ʱ� ���� ��� ��Ȱ��ȭ
//        }
//        if (resumeButton != null)
//        {
//            resumeButton.onClick.AddListener(ResumeGame); // ��ư Ŭ�� �̺�Ʈ�� ������Ʈ �߰�
//        }

//    }

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Escape))
//        {
//            isPaused = !isPaused;

//            if (isPaused)
//            {
//                Time.timeScale = 0f; // ���� �ð��� ����
//                if (blackbackground != null)
//                {
//                    blackbackground.SetActive(true); // ��� Ȱ��ȭ
//                }
//                if (pausePanel != null)
//                {
//                    pausePanel.SetActive(true); // �Ͻ� ���� �г� Ȱ��ȭ
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
//    }

//    public void ResumeGame()
//    {
//        isPaused = false;
//        Time.timeScale = 1f; // ���� �ð��� �簳
//        if (blackbackground != null)
//        {
//            blackbackground.SetActive(false); // ��� ��Ȱ��ȭ
//        }
//        if (pausePanel != null)
//        {
//            pausePanel.SetActive(false); // �Ͻ� ���� �г� ��Ȱ��ȭ
//        }
//        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� ��Ȱ��ȭ
//        Cursor.visible = false;
//        //Debug.Log("Game Resumed");
//    }

//    // ���� ������ �Ǹ� ������ �Լ�
//    public void ShowGameOverUI()
//    {
//        // ���� ���� UI ������Ʈ�� Ȱ��ȭ�Ѵ�.
//        gameOverUI.SetActive(true);

//        // ������Ʈ �ð��� 0������� �����Ѵ�. (�ð��� �����)
//        Time.timeScale = 0;
//    }

//    // ����ϱ� ��ư�� ������ �� ������ �Լ�
//    public void ContinueGame()
//    {
//        gameOverUI.SetActive(false);
//        Time.timeScale = 1.0f;
//    }

//}



// ����Ƽ å ���� �Ϸ��µ� ���� ���� ���ڶ� �ȵ�
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;

//public class PauseManager : MonoBehaviour
//{
//    public GameObject pausePanel; // �Ͻ� ���� �г�
//    public GameObject blackbackground; // ��ο� ���
//    public Button resumeButton; // ����ϱ� ��ư


//    private bool isPaused = false;

//    // ���� ���� ���
//    public enum GameState
//    {
//        Ready,
//        Run,
//        Pause,
//        GameOver
//    }
//    // �ɼ� ȭ�� UI ������Ʈ ����
//    public GameObject gameOption;

//    void Start()
//    {
//        if (pausePanel != null)
//        {
//            pausePanel.SetActive(false); // �ʱ� ���¿��� �г� ��Ȱ��ȭ
//        }
//        if (blackbackground != null)
//        {
//            blackbackground.SetActive(false); // �ʱ� ���� ��� ��Ȱ��ȭ
//        }
//        if (resumeButton != null)
//        {
//            resumeButton.onClick.AddListener(ResumeGame); // ��ư Ŭ�� �̺�Ʈ�� ������Ʈ �߰�
//        }

//    }

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Escape))
//        {
//            isPaused = !isPaused;

//            if (isPaused)
//            {
//                Time.timeScale = 0f; // ���� �ð��� ����
//                if (blackbackground != null)
//                {
//                    blackbackground.SetActive(true); // ��� Ȱ��ȭ
//                }
//                if (pausePanel != null)
//                {
//                    pausePanel.SetActive(true); // �Ͻ� ���� �г� Ȱ��ȭ
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
//    }

//    public void ResumeGame()
//    {
//        isPaused = false;
//        Time.timeScale = 1f; // ���� �ð��� �簳
//        if (blackbackground != null)
//        {
//            blackbackground.SetActive(false); // ��� ��Ȱ��ȭ
//        }
//        if (pausePanel != null)
//        {
//            pausePanel.SetActive(false); // �Ͻ� ���� �г� ��Ȱ��ȭ
//        }
//        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� ��Ȱ��ȭ
//        Cursor.visible = false;
//        //Debug.Log("Game Resumed");
//    }

//    // �ɼ� ȭ�� Ű��
//    public void OpenOptionWindow()
//    {
//        // �ɼ� â Ȱ��ȭ
//        gameOption.SetActive(true );
//        //���� �ӵ� 0���
//        Time.timeScale = 0f;
//        // ���� ���¸� �Ͻ������� ����
//        gState = GameState.Pause;
//    }

//    // ����ϱ� �ɼ�
//    public void CloseOptionWindow()
//    {
//        // �ɼ� â ��Ȱ��ȭ
//        gameOption.SetActive(false );
//        // ���� �ӵ��� 1������� ��ȯ
//        Time.timeScale = 1f;
//        // ���� ���¸� ���� �� ���·� ����
//        GameState = GameState.Run;
//    }

//    // �ٽ��ϱ� �ɼ�
//    public void RestartGame()
//    {
//        // ���� �ӵ��� 1������� ��ȯ
//        Time.timeScale *= 1f;
//        // ���� �� ��ȣ�� �ٽ� �ε�
//        SceneManager.LoadScene(SceneManager.GetActiveScene(), buildIndex);
//    }

//    //���� ���� 
//    public void QuitGame()
//    {
//        //���ø����̼� ����
//        Application.Quit();
//    }


//}

// ���� ESC
//using UnityEngine;
//using UnityEngine.UI;

//public class PauseManager : MonoBehaviour
//{
//    public GameObject pausePanel; // �Ͻ� ���� �г�
//    public GameObject blackbackground; // ��ο� ���
//    public Button resumeButton; // ����ϱ� ��ư

//    private bool isPaused = false;

//    void Start()
//    {
//        if (pausePanel != null)
//        {
//            pausePanel.SetActive(false); // �ʱ� ���¿��� �г� ��Ȱ��ȭ
//        }
//        if (blackbackground != null)
//        {
//            blackbackground.SetActive(false); // �ʱ� ���� ��� ��Ȱ��ȭ
//        }
//        if (resumeButton != null)
//        {
//            resumeButton.onClick.AddListener(ResumeGame); // ��ư Ŭ�� �̺�Ʈ�� ������Ʈ �߰�
//        }

//    }

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Escape))
//        {
//            isPaused = !isPaused;

//            if (isPaused)
//            {
//                Time.timeScale = 0f; // ���� �ð��� ����
//                if (blackbackground != null)
//                {
//                    blackbackground.SetActive(true); // ��� Ȱ��ȭ
//                }
//                if (pausePanel != null)
//                {
//                    pausePanel.SetActive(true); // �Ͻ� ���� �г� Ȱ��ȭ
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
//    }

//    public void ResumeGame()
//    {
//        isPaused = false;
//        Time.timeScale = 1f; // ���� �ð��� �簳
//        if (blackbackground != null)
//        {
//            blackbackground.SetActive(false); // ��� ��Ȱ��ȭ
//        }
//        if (pausePanel != null)
//        {
//            pausePanel.SetActive(false); // �Ͻ� ���� �г� ��Ȱ��ȭ
//        }
//        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� ��Ȱ��ȭ
//        Cursor.visible = false;
//        //Debug.Log("Game Resumed");
//    }



//}
