using UnityEngine;

public class BlockPlacer : MonoBehaviour
{
    public GameObject blockPrefab; // 블록 프리팹
    public LayerMask groundLayer; // 블록을 설치할 레이어
    public float placementDistance = 10f; // 블록을 설치할 최대 거리
    public Vector3 blockSize = Vector3.one; // 블록의 크기

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // 마우스 우클릭
        {
            PlaceBlock();
        }
    }

    void PlaceBlock()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * placementDistance, Color.red, 2f); // Ray를 빨간색으로 그리기

        if (Physics.Raycast(ray, out hit, placementDistance, groundLayer))
        {
            Debug.Log("Raycast hit something: " + hit.collider.name);

            // 클릭한 위치에 블록 배치
            Vector3 placementPosition = hit.point;
            // 블록을 그리드 위치에 스냅
            placementPosition = new Vector3(
                Mathf.Round(placementPosition.x),
                Mathf.Round(placementPosition.y),
                Mathf.Round(placementPosition.z)
            );

            Debug.Log("Placement position: " + placementPosition);

            // 기존 블록이 없는지 확인 (OverlapBox 사용)
            Collider[] colliders = Physics.OverlapBox(placementPosition, blockSize * 0.5f);
            if (colliders.Length == 0)
            {
                Debug.Log("No existing block found, placing new block");

                // 블록 Prefab 배치
                GameObject newBlock = Instantiate(blockPrefab, placementPosition, Quaternion.identity);
                Debug.Log("New block placed at: " + newBlock.transform.position);
            }
            else
            {
                Debug.Log("Block already exists at position");
            }
        }
        else
        {
            Debug.Log("Raycast did not hit anything");
        }
    }

    // Scene 뷰에 OverlapBox 시각화
    void OnDrawGizmos()
    {
        if (Camera.main != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction * placementDistance);
        }
    }
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//    public class BlockCreate : MonoBehaviour
//{ 
//    public GameObject blockPrefab; // 블록 프리팹
//    public LayerMask groundLayer; // 블록을 설치할 레이어
//    public float placementDistance = 10f; // 블록을 설치할 최대 거리

//    // Start is called before the first frame update
//    void Start()
//    {
//        // 게임 시작 시 블록을 생성하여 Prefab이 제대로 생성되는지 확인
//        Instantiate(blockPrefab, new Vector3(0, 1, 0), Quaternion.identity);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetMouseButtonDown(1)) // 마우스 우클릭
//        {
//            PlaceBlock();
//        }
//    }
//    void PlaceBlock()
//    {
//        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        RaycastHit hit;
//        // Ray가 발사되는 지점에 대한 디버그 메시지 출력
//        Debug.Log("Ray origin: " + ray.origin);
//        Debug.Log("Ray direction: " + ray.direction);
//        Debug.DrawRay(ray.origin, ray.direction * placementDistance, Color.red, 2f); // Ray를 빨간색으로 그리기

//        if (Physics.Raycast(ray, out hit, placementDistance, groundLayer))
//        {
//            Debug.Log("Raycast hit something");
//            // 클릭한 위치에 블록 배치
//            Vector3 placementPosition = hit.point;
//            // 블록을 그리드 위치에 스냅
//            placementPosition = new Vector3(
//                Mathf.Round(placementPosition.x),
//                Mathf.Round(placementPosition.y),
//                Mathf.Round(placementPosition.z)
//            );
//            Debug.Log("Placement position: " + placementPosition);
//            // 기존 블록이 없는지 확인
//            Collider[] colliders = Physics.OverlapBox(placementPosition, Vector3.one * 0.5f);
//            if (colliders.Length == 0)
//            {
//                Debug.Log("No existing block found, placing new block");
//                Instantiate(blockPrefab, placementPosition, Quaternion.identity);
//            }
//            else
//            {
//                Debug.Log("Block already exists at position");
//            }
//            //Instantiate(blockPrefab, placementPosition, Quaternion.identity);
//        }
//        else
//        {
//            Debug.Log("Raycast did not hit anything");
//        }
//    }
//}
