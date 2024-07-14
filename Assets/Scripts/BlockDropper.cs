using UnityEngine;

public class BlockDropper : MonoBehaviour
{
    public GameObject smallBlockPrefab; // 작은 블록 프리팹
    public Transform dropPoint; // 블록을 떨어뜨릴 위치

    void Start()
    {
        // 작은 블록 프리팹을 Resources 폴더에서 로드
        smallBlockPrefab = Resources.Load<GameObject>("Smallblock");
    }

    void Update()
    {
        // Q 키를 누르면 블록을 드롭
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropBlock();
        }

        // 블록을 부수는 조건 (예: 마우스 오른쪽 버튼 클릭)
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
        // 현재 블록을 부수는 로직 (구현 필요)
        // 예: 블록을 클릭하여 파괴할 때
        // ...

        // 블록 파괴 후 작은 블록 드롭
        DropBlock();
    }
}
