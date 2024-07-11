using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject smallBlockPrefab; // 작은 블록 프리팹
    public Transform dropPoint; // 블록을 떨어뜨릴 위치

    public List<GameObject> excludeObjects; // 스크립트가 작동하지 않을 GameObject들을 설정할 리스트 변수
    private GameObject currentPrefab; // 현재 눌린 프리팹
    private Camera mainCamera; // 카메라 참조 변수

    void Start()
    {
        // 작은 블록 프리팹을 Resources 폴더에서 로드
        smallBlockPrefab = Resources.Load<GameObject>("Smallblock");

        // Scene에서 MainCamera를 찾아서 할당
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("MainCamera가 없습니다!");
        }
    }

    void Update()
    {
        // Q 키를 누르면 블록을 드롭
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropBlock();
        }

        // 블록을 부수는 조건 (예: 마우스 왼쪽 버튼 클릭)
        if (Input.GetMouseButtonDown(0))
        {
            DestroyBlock();
        }
    }

    void DropBlock()
    {
        if (smallBlockPrefab != null && dropPoint != null)
        {
            // dropPoint 위치에 작은 블록 프리팹 인스턴스화
            Instantiate(smallBlockPrefab, dropPoint.position, dropPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Small block prefab or drop point is not set.");
        }
    }

    void DestroyBlock()
    {
        // 마우스 왼쪽 버튼을 누르면
        if (Input.GetMouseButtonDown(0))
        {
            if (mainCamera == null)
            {
                Debug.LogError("MainCamera가 할당되지 않았습니다!");
                return;
            }

            // 마우스 위치에서 가장 가까운 프리팹 찾기
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // Raycast로 맞은 위치에 있는 GameObject 가져오기
                currentPrefab = hit.collider.gameObject;

                // excludeObjects에 포함된 GameObject라면 삭제하지 않고 함수 종료
                if (excludeObjects.Contains(currentPrefab))
                {
                    return;
                }
            }

            // 3초 후에 삭제하기
            Invoke("DestroyPrefab", 3f);
        }

        // 마우스 버튼을 뗐을 때 취소
        if (Input.GetMouseButtonUp(0))
        {
            // 취소하고 Invoke된 함수 중단
            CancelInvoke("DestroyPrefab");
            currentPrefab = null;
        }
        // 현재 블록을 부수는 로직(구현 필요)
        // 예: 블록을 클릭하여 파괴할 때
        // ...

        // 블록 파괴 후 작은 블록 드롭
        DropBlock();
    }
    // 프리팹을 삭제하는 함수
    void DestroyPrefab()
    {
        if (currentPrefab != null)
        {
            Destroy(currentPrefab);
            currentPrefab = null;
        }
    }
}

// 기존 파괴

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Destroy : MonoBehaviour
//{
//    public List<GameObject> excludeObjects; // 스크립트가 작동하지 않을 GameObject들을 설정할 리스트 변수

//    private GameObject currentPrefab; // 현재 눌린 프리팹
//    private Camera mainCamera; // 카메라 참조 변수

//    void Start()
//    {
//        // Scene에서 MainCamera를 찾아서 할당
//        mainCamera = Camera.main;
//        if (mainCamera == null)
//        {
//            Debug.LogError("MainCamera가 없습니다!");
//        }
//    }

//    void Update()
//    {
//        // 마우스 왼쪽 버튼을 누르면
//        if (Input.GetMouseButtonDown(0))
//        {
//            if (mainCamera == null)
//            {
//                Debug.LogError("MainCamera가 할당되지 않았습니다!");
//                return;
//            }

//            // 마우스 위치에서 가장 가까운 프리팹 찾기
//            RaycastHit hit;
//            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

//            if (Physics.Raycast(ray, out hit))
//            {
//                // Raycast로 맞은 위치에 있는 GameObject 가져오기
//                currentPrefab = hit.collider.gameObject;

//                // excludeObjects에 포함된 GameObject라면 삭제하지 않고 함수 종료
//                if (excludeObjects.Contains(currentPrefab))
//                {
//                    return;
//                }
//            }

//            // 3초 후에 삭제하기
//            Invoke("DestroyPrefab", 3f);
//        }

//        // 마우스 버튼을 뗐을 때 취소
//        if (Input.GetMouseButtonUp(0))
//        {
//            // 취소하고 Invoke된 함수 중단
//            CancelInvoke("DestroyPrefab");
//            currentPrefab = null;
//        }
//    }

//    // 프리팹을 삭제하는 함수
//    void DestroyPrefab()
//    {
//        if (currentPrefab != null)
//        {
//            Destroy(currentPrefab);
//            currentPrefab = null;
//        }
//    }
//}