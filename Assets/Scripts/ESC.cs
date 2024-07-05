using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel; // �Ͻ� ���� �г�
    public GameObject blackbackground; // ��ο� ���
    public Button resumeButton; // ����ϱ� ��ư

    private bool isPaused = false;

    void Start()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false); // �ʱ� ���¿��� �г� ��Ȱ��ȭ
        }
        if (blackbackground != null)
        {
            blackbackground.SetActive(false); // �ʱ� ���� ��� ��Ȱ��ȭ
        }
        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(ResumeGame); // ��ư Ŭ�� �̺�Ʈ�� ������Ʈ �߰�
        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                Time.timeScale = 0f; // ���� �ð��� ����
                if (blackbackground != null)
                {
                    blackbackground.SetActive(true); // ��� Ȱ��ȭ
                }
                if (pausePanel != null)
                {
                    pausePanel.SetActive(true); // �Ͻ� ���� �г� Ȱ��ȭ
                }
                Cursor.lockState = CursorLockMode.None; // ���콺 Ŀ�� Ȱ��ȭ
                Cursor.visible = true;
                //Debug.Log("Game Paused");
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // ���� �ð��� �簳
        if (blackbackground != null)
        {
            blackbackground.SetActive(false); // ��� ��Ȱ��ȭ
        }
        if (pausePanel != null)
        {
            pausePanel.SetActive(false); // �Ͻ� ���� �г� ��Ȱ��ȭ
        }
        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� ��Ȱ��ȭ
        Cursor.visible = false;
        //Debug.Log("Game Resumed");
    }

}
