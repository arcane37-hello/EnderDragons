using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallBlock : MonoBehaviour
{
    public GameObject dirtPrefab; // 흙 프리팹
    public GameObject stonePrefab; // 돌 프리팹

    private enum BuildState
    {
        None,
        PlacingDirt,
        PlacingStone
    }

    private BuildState currentBuildState = BuildState.None;

    void Update()
    {
        // 숫자 키 입력 감지
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentBuildState = BuildState.PlacingDirt;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentBuildState = BuildState.PlacingStone;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1)) // 마우스 오른쪽 버튼 클릭
        {
            // 현재 상태에 따라 프리팹 설치
            switch (currentBuildState)
            {
                case BuildState.PlacingDirt:
                    PlacePrefab(dirtPrefab);
                    break;
                case BuildState.PlacingStone:
                    PlacePrefab(stonePrefab);
                    break;
                default:
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) ||
                 Input.GetKeyDown(KeyCode.Alpha2) ||
                 Input.GetKeyDown(KeyCode.Alpha5) ||
                 Input.GetKeyDown(KeyCode.Alpha6) ||
                 Input.GetKeyDown(KeyCode.Alpha7) ||
                 Input.GetKeyDown(KeyCode.Alpha8) ||
                 Input.GetKeyDown(KeyCode.Alpha9) ||
                 Input.GetKeyDown(KeyCode.Alpha0))
        {
            // 3번과 4번 이외의 다른 숫자 키를 누르면 상태 초기화
            currentBuildState = BuildState.None;
        }
    }

    void PlacePrefab(GameObject prefab)
    {
        // 마우스 위치에서 Raycast를 사용하여 바닥에 프리팹을 설치
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // 표면의 정보 가져오기
            Vector3 surfaceNormal = hit.normal;
            Vector3 position = hit.point + surfaceNormal * 0.5f; // 프리팹의 중심이 표면에 위치하도록 보정

            // 정수 좌표로 변환하여 프리팹 설치
            Vector3Int intPosition = new Vector3Int(
                Mathf.RoundToInt(position.x),
                Mathf.RoundToInt(position.y),
                Mathf.RoundToInt(position.z)
            );

            Instantiate(prefab, intPosition, Quaternion.FromToRotation(Vector3.up, surfaceNormal));
        }
    }
}