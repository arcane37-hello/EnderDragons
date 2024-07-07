using UnityEngine;

public class BlockPlacer : MonoBehaviour
{
    public GameObject blockPrefab; // ��� ������
    public LayerMask groundLayer; // ����� ��ġ�� ���̾�
    public float placementDistance = 10f; // ����� ��ġ�� �ִ� �Ÿ�
    public Vector3 blockSize = Vector3.one; // ����� ũ��

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // ���콺 ��Ŭ��
        {
            PlaceBlock();
        }
    }

    void PlaceBlock()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * placementDistance, Color.red, 2f); // Ray�� ���������� �׸���

        if (Physics.Raycast(ray, out hit, placementDistance, groundLayer))
        {
            Debug.Log("Raycast hit something: " + hit.collider.name);

            // Ŭ���� ��ġ�� ��� ��ġ
            Vector3 placementPosition = hit.point;
            // ����� �׸��� ��ġ�� ����
            placementPosition = new Vector3(
                Mathf.Round(placementPosition.x),
                Mathf.Round(placementPosition.y),
                Mathf.Round(placementPosition.z)
            );

            Debug.Log("Placement position: " + placementPosition);

            // ���� ����� ������ Ȯ�� (OverlapBox ���)
            Collider[] colliders = Physics.OverlapBox(placementPosition, blockSize * 0.5f);
            if (colliders.Length == 0)
            {
                Debug.Log("No existing block found, placing new block");

                // ��� Prefab ��ġ
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

    // Scene �信 OverlapBox �ð�ȭ
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
//    public GameObject blockPrefab; // ��� ������
//    public LayerMask groundLayer; // ����� ��ġ�� ���̾�
//    public float placementDistance = 10f; // ����� ��ġ�� �ִ� �Ÿ�

//    // Start is called before the first frame update
//    void Start()
//    {
//        // ���� ���� �� ����� �����Ͽ� Prefab�� ����� �����Ǵ��� Ȯ��
//        Instantiate(blockPrefab, new Vector3(0, 1, 0), Quaternion.identity);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetMouseButtonDown(1)) // ���콺 ��Ŭ��
//        {
//            PlaceBlock();
//        }
//    }
//    void PlaceBlock()
//    {
//        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        RaycastHit hit;
//        // Ray�� �߻�Ǵ� ������ ���� ����� �޽��� ���
//        Debug.Log("Ray origin: " + ray.origin);
//        Debug.Log("Ray direction: " + ray.direction);
//        Debug.DrawRay(ray.origin, ray.direction * placementDistance, Color.red, 2f); // Ray�� ���������� �׸���

//        if (Physics.Raycast(ray, out hit, placementDistance, groundLayer))
//        {
//            Debug.Log("Raycast hit something");
//            // Ŭ���� ��ġ�� ��� ��ġ
//            Vector3 placementPosition = hit.point;
//            // ����� �׸��� ��ġ�� ����
//            placementPosition = new Vector3(
//                Mathf.Round(placementPosition.x),
//                Mathf.Round(placementPosition.y),
//                Mathf.Round(placementPosition.z)
//            );
//            Debug.Log("Placement position: " + placementPosition);
//            // ���� ����� ������ Ȯ��
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
