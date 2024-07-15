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
        // 에디터 모드에서 종료 (에디터에서는 실행 종료가 안되므로 이 코드로 에디터 실행을 중지)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 빌드된 애플리케이션에서 종료
        Application.Quit();
#endif
    }
}
