using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject smallBlockPrefab; // ���� ��� ������
    public LayerMask groundLayer; // ����� ����߸� ��ġ

    public List<GameObject> excludeObjects; // ��ũ��Ʈ�� �۵����� ���� GameObject���� ������ ����Ʈ ����
    private GameObject currentPrefab; // ���� ���� ������
    private Camera mainCamera; // ī�޶� ���� ����

    private bool isPressing = false; // ���콺 ��ư�� ������ �������� ����
    private float pressTime = 0f; // ���콺 ��ư�� ���� �ð�
    private float requiredPressTime = 1f; // ����� �μ����� ���� �ð�

    void Start()
    {
        // ���� ��� �������� Resources �������� �ε�
        smallBlockPrefab = Resources.Load<GameObject>("Smallblock");

        // Scene���� MainCamera�� ã�Ƽ� �Ҵ�
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("MainCamera�� �����ϴ�!");
        }
    }

    void Update()
    {
        // Q Ű�� ������ ����� ���
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropBlock();
        }

        // ���콺 ��ư�� ������ ��
        if (Input.GetMouseButtonDown(0))
        {
            isPressing = true;
            pressTime = 0f;
        }

        // ���콺 ��ư�� ���� �ִ� ����
        if (isPressing)
        {
            pressTime += Time.deltaTime;

            if (pressTime >= requiredPressTime)
            {
                DestroyBlock();
                isPressing = false;
            }
        }

        // ���콺 ��ư�� ���� ��
        if (Input.GetMouseButtonUp(0))
        {
            isPressing = false;
        }
    }

    void DropBlock()
    {
        // ī�޶�κ��� ���콺 ��ġ�� ����ĳ��Ʈ�� ����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // ����ĳ��Ʈ�� ���鿡 �浹�ϸ�
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            // �浹 ������ ����� �ν��Ͻ�ȭ
            Instantiate(smallBlockPrefab, hit.point, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No valid ground detected at mouse position.");
        }
    }

    void DestroyBlock()
    {
        if (mainCamera == null)
        {
            Debug.LogError("MainCamera�� �Ҵ���� �ʾҽ��ϴ�!");
            return;
        }

        // ���콺 ��ġ���� ���� ����� ������ ã��
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            // Raycast�� ���� ��ġ�� �ִ� GameObject ��������
            currentPrefab = hit.collider.gameObject;

            // excludeObjects�� ���Ե� GameObject��� �������� �ʰ� �Լ� ����
            if (excludeObjects.Contains(currentPrefab))
            {
                return;
            }

            // ��� �ı�
            Destroy(currentPrefab);

            // ���� ��ġ�� ���� ��� ����
            Instantiate(smallBlockPrefab, hit.point, Quaternion.identity);
        }
    }
}


//using System.Collections.Generic;
//using UnityEngine;

//public class Destroy : MonoBehaviour
//{
//    public GameObject smallBlockPrefab; // ���� ��� ������
//    public LayerMask groundLayer; // ����� ����߸� ��ġ

//    public List<GameObject> excludeObjects; // ��ũ��Ʈ�� �۵����� ���� GameObject���� ������ ����Ʈ ����
//    private GameObject currentPrefab; // ���� ���� ������
//    private Camera mainCamera; // ī�޶� ���� ����

//    void Start()
//    {
//        // ���� ��� �������� Resources �������� �ε�
//        smallBlockPrefab = Resources.Load<GameObject>("Smallblock");

//        // Scene���� MainCamera�� ã�Ƽ� �Ҵ�
//        mainCamera = Camera.main;
//        if (mainCamera == null)
//        {
//            Debug.LogError("MainCamera�� �����ϴ�!");
//        }
//    }

//    void Update()
//    {
//        // Q Ű�� ������ ����� ���
//        if (Input.GetKeyDown(KeyCode.Q))
//        {
//            DropBlock();
//        }

//        // ����� �μ��� ���� (��: ���콺 ���� ��ư Ŭ��)
//        if (Input.GetMouseButtonDown(0))
//        {
//            DestroyBlock();
//        }
//    }

//    void DropBlock()
//    {
//        // ī�޶�κ��� ���콺 ��ġ�� ����ĳ��Ʈ�� ����
//        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        RaycastHit hit;

//        // ����ĳ��Ʈ�� ���鿡 �浹�ϸ�
//        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
//        {
//            // �浹 ������ ����� �ν��Ͻ�ȭ
//            Instantiate(smallBlockPrefab, hit.point, Quaternion.identity);
//        }
//        else
//        {
//            Debug.LogWarning("No valid ground detected at mouse position.");
//        }
//    }

//    void DestroyBlock()
//    {
//        if (mainCamera == null)
//        {
//            Debug.LogError("MainCamera�� �Ҵ���� �ʾҽ��ϴ�!");
//            return;
//        }

//        // ���콺 ��ġ���� ���� ����� ������ ã��
//        RaycastHit hit;
//        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

//        if (Physics.Raycast(ray, out hit))
//        {
//            // Raycast�� ���� ��ġ�� �ִ� GameObject ��������
//            currentPrefab = hit.collider.gameObject;

//            // excludeObjects�� ���Ե� GameObject��� �������� �ʰ� �Լ� ����
//            if (excludeObjects.Contains(currentPrefab))
//            {
//                return;
//            }

//            // ��� �ı�
//            Destroy(currentPrefab);

//            // ���� ��ġ�� ���� ��� ����
//            Instantiate(smallBlockPrefab, hit.point, Quaternion.identity);
//        }
//    }
//}



// ����� ĳ�� �ȳ���

//using System.Collections.Generic;
//using UnityEngine;

//public class Destroy : MonoBehaviour
//{
//    public GameObject smallBlockPrefab; // ���� ��� ������
//    public LayerMask groundLayer; // ����� ����߸� ��ġ

//    public List<GameObject> excludeObjects; // ��ũ��Ʈ�� �۵����� ���� GameObject���� ������ ����Ʈ ����
//    private GameObject currentPrefab; // ���� ���� ������
//    private Camera mainCamera; // ī�޶� ���� ����

//    void Start()
//    {
//        // ���� ��� �������� Resources �������� �ε�
//        smallBlockPrefab = Resources.Load<GameObject>("Smallblock");

//        // Scene���� MainCamera�� ã�Ƽ� �Ҵ�
//        mainCamera = Camera.main;
//        if (mainCamera == null)
//        {
//            Debug.LogError("MainCamera�� �����ϴ�!");
//        }
//    }

//    void Update()
//    {
//        // Q Ű�� ������ ����� ���
//        if (Input.GetKeyDown(KeyCode.Q))
//        {
//            DropBlock();
//        }

//        // ����� �μ��� ���� (��: ���콺 ���� ��ư Ŭ��)
//        if (Input.GetMouseButtonDown(0))
//        {
//            DestroyBlock();
//        }
//    }

//    void DropBlock()
//    {
//        // ī�޶�κ��� ���콺 ��ġ�� ����ĳ��Ʈ�� ����
//        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        RaycastHit hit;

//        // ����ĳ��Ʈ�� ���鿡 �浹�ϸ�
//        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
//        {
//            // �浹 ������ ����� �ν��Ͻ�ȭ
//            Instantiate(smallBlockPrefab, hit.point, Quaternion.identity);
//        }
//        else
//        {
//            Debug.LogWarning("No valid ground detected at mouse position.");
//        }
//    }

//    void DestroyBlock()
//    {
//        // ���콺 ���� ��ư�� ������
//        if (Input.GetMouseButtonDown(0))
//        {
//            if (mainCamera == null)
//            {
//                Debug.LogError("MainCamera�� �Ҵ���� �ʾҽ��ϴ�!");
//                return;
//            }

//            // ���콺 ��ġ���� ���� ����� ������ ã��
//            RaycastHit hit;
//            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

//            if (Physics.Raycast(ray, out hit))
//            {
//                // Raycast�� ���� ��ġ�� �ִ� GameObject ��������
//                currentPrefab = hit.collider.gameObject;

//                // excludeObjects�� ���Ե� GameObject��� �������� �ʰ� �Լ� ����
//                if (excludeObjects.Contains(currentPrefab))
//                {
//                    return;
//                }
//            }

//            // 3�� �Ŀ� �����ϱ�
//            Invoke("DestroyPrefab", 3f);
//        }

//        // ���콺 ��ư�� ���� �� ���
//        if (Input.GetMouseButtonUp(0))
//        {
//            // ����ϰ� Invoke�� �Լ� �ߴ�
//            CancelInvoke("DestroyPrefab");
//            currentPrefab = null;
//        }
//        // ���� ����� �μ��� ����(���� �ʿ�)
//        // ��: ����� Ŭ���Ͽ� �ı��� ��
//        // ...

//        // ��� �ı� �� ���� ��� ���
//        DropBlock();
//    }
//    // �������� �����ϴ� �Լ�
//    void DestroyPrefab()
//    {
//        if (currentPrefab != null)
//        {
//            Destroy(currentPrefab);
//            currentPrefab = null;
//        }
//    }
//}


// ��� ���� �� ������ ��� ��� ( �������� �÷��̾� �ڸ��� �������� ����)

//using System.Collections.Generic;
//using UnityEngine;

//public class Destroy : MonoBehaviour
//{
//    public GameObject smallBlockPrefab; // ���� ��� ������
//    public Transform dropPoint; // ����� ����߸� ��ġ

//    public List<GameObject> excludeObjects; // ��ũ��Ʈ�� �۵����� ���� GameObject���� ������ ����Ʈ ����
//    private GameObject currentPrefab; // ���� ���� ������
//    private Camera mainCamera; // ī�޶� ���� ����

//    void Start()
//    {
//        // ���� ��� �������� Resources �������� �ε�
//        smallBlockPrefab = Resources.Load<GameObject>("Smallblock");

//        // Scene���� MainCamera�� ã�Ƽ� �Ҵ�
//        mainCamera = Camera.main;
//        if (mainCamera == null)
//        {
//            Debug.LogError("MainCamera�� �����ϴ�!");
//        }
//    }

//    void Update()
//    {
//        // Q Ű�� ������ ����� ���
//        if (Input.GetKeyDown(KeyCode.Q))
//        {
//            DropBlock();
//        }

//        // ����� �μ��� ���� (��: ���콺 ���� ��ư Ŭ��)
//        if (Input.GetMouseButtonDown(0))
//        {
//            DestroyBlock();
//        }
//    }

//    void DropBlock()
//    {
//        if (smallBlockPrefab != null && dropPoint != null)
//        {
//            // dropPoint ��ġ�� ���� ��� ������ �ν��Ͻ�ȭ
//            Instantiate(smallBlockPrefab, dropPoint.position, dropPoint.rotation);
//        }
//        else
//        {
//            Debug.LogWarning("Small block prefab or drop point is not set.");
//        }
//    }

//    void DestroyBlock()
//    {
//        // ���콺 ���� ��ư�� ������
//        if (Input.GetMouseButtonDown(0))
//        {
//            if (mainCamera == null)
//            {
//                Debug.LogError("MainCamera�� �Ҵ���� �ʾҽ��ϴ�!");
//                return;
//            }

//            // ���콺 ��ġ���� ���� ����� ������ ã��
//            RaycastHit hit;
//            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

//            if (Physics.Raycast(ray, out hit))
//            {
//                // Raycast�� ���� ��ġ�� �ִ� GameObject ��������
//                currentPrefab = hit.collider.gameObject;

//                // excludeObjects�� ���Ե� GameObject��� �������� �ʰ� �Լ� ����
//                if (excludeObjects.Contains(currentPrefab))
//                {
//                    return;
//                }
//            }

//            // 3�� �Ŀ� �����ϱ�
//            Invoke("DestroyPrefab", 3f);
//        }

//        // ���콺 ��ư�� ���� �� ���
//        if (Input.GetMouseButtonUp(0))
//        {
//            // ����ϰ� Invoke�� �Լ� �ߴ�
//            CancelInvoke("DestroyPrefab");
//            currentPrefab = null;
//        }
//        // ���� ����� �μ��� ����(���� �ʿ�)
//        // ��: ����� Ŭ���Ͽ� �ı��� ��
//        // ...

//        // ��� �ı� �� ���� ��� ���
//        DropBlock();
//    }
//    // �������� �����ϴ� �Լ�
//    void DestroyPrefab()
//    {
//        if (currentPrefab != null)
//        {
//            Destroy(currentPrefab);
//            currentPrefab = null;
//        }
//    }
//}

// ���� �ı� ( ������ ��� X)

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Destroy : MonoBehaviour
//{
//    public List<GameObject> excludeObjects; // ��ũ��Ʈ�� �۵����� ���� GameObject���� ������ ����Ʈ ����

//    private GameObject currentPrefab; // ���� ���� ������
//    private Camera mainCamera; // ī�޶� ���� ����

//    void Start()
//    {
//        // Scene���� MainCamera�� ã�Ƽ� �Ҵ�
//        mainCamera = Camera.main;
//        if (mainCamera == null)
//        {
//            Debug.LogError("MainCamera�� �����ϴ�!");
//        }
//    }

//    void Update()
//    {
//        // ���콺 ���� ��ư�� ������
//        if (Input.GetMouseButtonDown(0))
//        {
//            if (mainCamera == null)
//            {
//                Debug.LogError("MainCamera�� �Ҵ���� �ʾҽ��ϴ�!");
//                return;
//            }

//            // ���콺 ��ġ���� ���� ����� ������ ã��
//            RaycastHit hit;
//            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

//            if (Physics.Raycast(ray, out hit))
//            {
//                // Raycast�� ���� ��ġ�� �ִ� GameObject ��������
//                currentPrefab = hit.collider.gameObject;

//                // excludeObjects�� ���Ե� GameObject��� �������� �ʰ� �Լ� ����
//                if (excludeObjects.Contains(currentPrefab))
//                {
//                    return;
//                }
//            }

//            // 3�� �Ŀ� �����ϱ�
//            Invoke("DestroyPrefab", 3f);
//        }

//        // ���콺 ��ư�� ���� �� ���
//        if (Input.GetMouseButtonUp(0))
//        {
//            // ����ϰ� Invoke�� �Լ� �ߴ�
//            CancelInvoke("DestroyPrefab");
//            currentPrefab = null;
//        }
//    }

//    // �������� �����ϴ� �Լ�
//    void DestroyPrefab()
//    {
//        if (currentPrefab != null)
//        {
//            Destroy(currentPrefab);
//            currentPrefab = null;
//        }
//    }
//}