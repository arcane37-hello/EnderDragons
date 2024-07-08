using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public List<GameObject> excludeObjects; // ��ũ��Ʈ�� �۵����� ���� GameObject���� ������ ����Ʈ ����

    private GameObject currentPrefab; // ���� ���� ������
    private Camera mainCamera; // ī�޶� ���� ����

    void Start()
    {
        // Scene���� MainCamera�� ã�Ƽ� �Ҵ�
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("MainCamera�� �����ϴ�!");
        }
    }

    void Update()
    {
        // ���콺 ���� ��ư�� ������
        if (Input.GetMouseButtonDown(0))
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
            }

            // 3�� �Ŀ� �����ϱ�
            Invoke("DestroyPrefab", 3f);
        }

        // ���콺 ��ư�� ���� �� ���
        if (Input.GetMouseButtonUp(0))
        {
            // ����ϰ� Invoke�� �Լ� �ߴ�
            CancelInvoke("DestroyPrefab");
            currentPrefab = null;
        }
    }

    // �������� �����ϴ� �Լ�
    void DestroyPrefab()
    {
        if (currentPrefab != null)
        {
            Destroy(currentPrefab);
            currentPrefab = null;
        }
    }
}