using UnityEngine.SceneManagement;
using UnityEngine;

public class Bosspatal : MonoBehaviour
{
    // 충돌을 감지할 트리거 설정
    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트의 태그가 "Player"인지 확인
        if (other.CompareTag("Player"))
        {
            // 씬 전환: "2"번 씬으로 이동
            SceneManager.LoadScene(2);  // 2는 씬의 인덱스입니다. 
        }
    }

    // 트리거를 활성화하려면 해당 콜라이더의 "Is Trigger"를 체크해야 합니다.
}