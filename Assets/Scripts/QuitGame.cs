using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    public void ExitGame()
    {
        // ������ ��忡�� ���� (�����Ϳ����� ���� ���ᰡ �ȵǹǷ� �� �ڵ�� ������ ������ ����)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ����� ���ø����̼ǿ��� ����
        Application.Quit();
#endif
    }
}
