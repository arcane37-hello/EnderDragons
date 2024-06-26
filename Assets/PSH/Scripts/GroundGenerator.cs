using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    public GameObject cubePrefab;
    public int groundWidth = 100; // x ���� ť�� ����
    public int groundLength = 100; // z ���� ť�� ����

    void Start()
    {
        GenerateGround();
    }

    void GenerateGround()
    {

        // ť����� ��ġ�� ���� ��ġ ����
        Vector3 startPosition = new Vector3(-groundWidth / 2, 0, -groundLength / 2);

        // ���� ������ ť����� �����ϴ� �ڵ�
        for (int x = 0; x < groundWidth; x++)
        {
            for (int z = 0; z < groundLength; z++)
            {
                // �� ť���� ��ġ ���
                Vector3 position = startPosition + new Vector3(x, 0, z);

                // ť�� �ν��Ͻ� ����
                GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);
                cube.transform.SetParent(transform); // �� ������(GroundGenerator)�� �ڽ����� ����
            }
        }
    }
}
