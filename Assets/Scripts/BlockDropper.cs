using UnityEngine;

public class BlockDropper : MonoBehaviour
{
    public GameObject smallBlockPrefab; // ���� ��� ������
    public Transform dropPoint; // ����� ����߸� ��ġ

    void Start()
    {
        // ���� ��� �������� Resources �������� �ε�
        smallBlockPrefab = Resources.Load<GameObject>("Smallblock");
    }

    void Update()
    {
        // Q Ű�� ������ ����� ���
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropBlock();
        }

        // ����� �μ��� ���� (��: ���콺 ������ ��ư Ŭ��)
        if (Input.GetMouseButtonDown(0))
        {
            DestroyBlock();
        }
    }

    void DropBlock()
    {
        if (smallBlockPrefab != null && dropPoint != null)
        {
            // dropPoint ��ġ�� ���� ��� ������ �ν��Ͻ�ȭ
            Instantiate(smallBlockPrefab, dropPoint.position, dropPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Small block prefab or drop point is not set.");
        }
    }

    void DestroyBlock()
    {
        // ���� ����� �μ��� ���� (���� �ʿ�)
        // ��: ����� Ŭ���Ͽ� �ı��� ��
        // ...

        // ��� �ı� �� ���� ��� ���
        DropBlock();
    }
}
