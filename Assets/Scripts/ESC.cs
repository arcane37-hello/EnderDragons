using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel; // �Ͻ� ���� �г�

    private bool isPaused = false;

    void Start()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false); // �ʱ� ���¿��� �г� ��Ȱ��ȭ
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
                Time.timeScale = 1f; // ���� �ð��� �簳
                if (pausePanel != null)
                {
                    pausePanel.SetActive(false); // �Ͻ� ���� �г� ��Ȱ��ȭ
                }
                Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� ��Ȱ��ȭ
                Cursor.visible = false;
                //Debug.Log("Game Resumed");
            }
        }
    }

   
}
