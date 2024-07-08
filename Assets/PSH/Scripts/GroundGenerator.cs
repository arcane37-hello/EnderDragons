using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    public GameObject cubePrefab; // ���� ������ ť�� ������
    public GameObject waterCubePrefab; // ���� ��Ÿ���� ť�� ������
    public int groundWidth = 50; // x ���� ť�� ����
    public int groundHeight = 3; // y ���� ť�� ����
    public int groundLength = 50; // z ���� ť�� ����

    public int lakeSize = 5; // ȣ���� ũ�� (5x5)

    void Start()
    {
        GenerateGround();
    }

    void GenerateGround()
    {
        Debug.Log("Generating ground...");

        // ť����� ��ġ�� ���� ��ġ ����
        Vector3 startPosition = new Vector3(-groundWidth / 2, 0, -groundLength / 2); // y ���� ���̴� 0���� ����

        // ȣ���� ������ ������ x, z ��ġ ��� (-25���� 25����)
        int lakeStartX = Random.Range(-25, 26 - lakeSize); // -25���� 25 ���̿��� ����
        int lakeStartZ = Random.Range(-25, 26 - lakeSize); // -25���� 25 ���̿��� ����

        Debug.Log($"Lake start position: ({lakeStartX}, {lakeStartZ})");

        // �� ť�긦 ���� ����
        GenerateWater(lakeStartX, lakeStartZ);

        // �� ť�긦 ����
        GenerateTerrain(startPosition, lakeStartX, lakeStartZ);

        Debug.Log("Ground generation completed.");
    }

    // �� ť�� ���� �޼���
    void GenerateWater(int lakeStartX, int lakeStartZ)
    {
        Debug.Log("Generating water...");

        // ȣ�� ������ �� ť�� ����
        for (int x = lakeStartX; x < lakeStartX + lakeSize; x++)
        {
            for (int z = lakeStartZ; z < lakeStartZ + lakeSize; z++)
            {
                // �� ť���� ��ġ ���� (y�� �� ��������)
                Vector3 waterPosition = new Vector3(x, 1, z);

                // �� �������� ���� ������ �� �ֵ���
                if (!IsPositionOnGround(x, z))
                {
                    // �� ť�� �ν��Ͻ� ����
                    GameObject waterCube = Instantiate(waterCubePrefab, waterPosition, Quaternion.identity);
                    waterCube.transform.SetParent(transform); // �� ������(GroundGenerator)�� �ڽ����� ����
                    Debug.Log($"Water cube instantiated at ({waterPosition.x}, {waterPosition.y}, {waterPosition.z})");
                }
            }
        }

        Debug.Log("Water generation completed.");
    }

    // �� ť�� ���� �޼���
    void GenerateTerrain(Vector3 startPosition, int lakeStartX, int lakeStartZ)
    {
        Debug.Log("Generating terrain...");

        // �� ť�긦 ����
        for (int x = 0; x < groundWidth; x++)
        {
            for (int z = 0; z < groundLength; z++)
            {
                // ���� �ִ� ��ġ���� �� ť�� �������� ����
                if (!IsPositionInLake(x, z, lakeStartX, lakeStartZ))
                {
                    // Perlin Noise�� �̿��� ���� ����
                    float heightNoise = Mathf.PerlinNoise((float)x / 10f, (float)z / 10f); // Perlin Noise �� ���
                    int height = Mathf.RoundToInt(heightNoise * (groundHeight - 1)) + 1; // ���� ��� (1 ~ groundHeight)

                    for (int y = 0; y < height; y++)
                    {
                        // �� ť���� ��ġ ���
                        Vector3 groundPosition = startPosition + new Vector3(x, y, z);

                        // ť�� �ν��Ͻ� ����
                        GameObject cube = Instantiate(cubePrefab, groundPosition, Quaternion.identity);
                        cube.transform.SetParent(transform); // �� ������(GroundGenerator)�� �ڽ����� ����
                        cube.tag = "Ground"; // �±� �Ҵ�
                        Debug.Log($"Ground cube instantiated at ({groundPosition.x}, {groundPosition.y}, {groundPosition.z})");
                    }
                }
            }
        }

        Debug.Log("Terrain generation completed.");
    }

    // �ش� ��ǥ�� ���� �ִ��� Ȯ���ϴ� �޼���
    bool IsPositionOnGround(int x, int z)
    {
        // ���� ���� ���
        int startX = groundWidth / 2;
        int endX = startX + groundWidth - 1;
        int startZ = groundLength / 2;
        int endZ = startZ + groundLength - 1;

        // ��ǥ�� �� ���� ���� �ִ��� Ȯ��
        if (x >= startX && x <= endX && z >= startZ && z <= endZ)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // �ش� ��ǥ�� ȣ���� ���ϴ��� Ȯ���ϴ� �޼���
    bool IsPositionInLake(int x, int z, int lakeStartX, int lakeStartZ)
    {
        // ȣ���� ���� ���
        int lakeEndX = lakeStartX + lakeSize - 1;
        int lakeEndZ = lakeStartZ + lakeSize - 1;

        // ��ǥ�� ȣ�� ���� ���� �ִ��� Ȯ��
        if (x >= lakeStartX && x <= lakeEndX && z >= lakeStartZ && z <= lakeEndZ)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}