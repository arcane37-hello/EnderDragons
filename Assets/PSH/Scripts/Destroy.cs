using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public List<GameObject> excludeObjects; // 스크립트가 작동하지 않을 GameObject들을 설정할 리스트 변수

    private GameObject currentPrefab; // 현재 눌린 프리팹
    private Camera mainCamera; // 카메라 참조 변수

    void Start()
    {
        // Scene에서 MainCamera를 찾아서 할당
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("MainCamera가 없습니다!");
        }
    }

    void Update()
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