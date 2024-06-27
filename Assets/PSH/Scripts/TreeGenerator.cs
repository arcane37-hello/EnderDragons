using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
    public GameObject treeBlockPrefab; // ���� ��� ������
    public GameObject leafBlockPrefab; // ������ ��� ������
    public int numberOfTrees = 10; // ������ ���� ����
    public int groundWidth = 10; // ���� ���� ����
    public int groundLength = 10; // ���� ���� ����
    public int minHeight = 5; // ������ �ּ� ����
    public int maxHeight = 7; // ������ �ִ� ����

    void Start()
    {
        GenerateTrees();
    }

    void GenerateTrees()
    {
        for (int i = 0; i < numberOfTrees; i++)
        {
            // ������ ��ġ ���
            int randomX = Random.Range(-groundWidth / 2, groundWidth / 2);
            int randomZ = Random.Range(-groundLength / 2, groundLength / 2);
            Vector3 position = new Vector3(randomX, 0, randomZ);

            // ������ ���� ���
            int randomHeight = Random.Range(minHeight, maxHeight);
            Vector3 trunkPosition = position + new Vector3(0, randomHeight / 2, 0);

            // ���� ��� ����
            GameObject trunk = new GameObject("Tree");

            // ���� ��ϵ�� ������ ��� ����
            for (int y = 1; y <= randomHeight; y++)
            {
                GameObject treeBlock = Instantiate(treeBlockPrefab, trunkPosition + new Vector3(0, y - 1, 0), Quaternion.identity);
                treeBlock.transform.SetParent(trunk.transform);
            }

            // ������ ����
            GameObject leaves = new GameObject("Leaves");
            for (int x = -1; x <= 1; x++)
            {
                for (int y = 0; y <= 1; y++)
                {
                    for (int z = -1; z <= 1; z++)
                    {
                        Instantiate(leafBlockPrefab, trunkPosition + new Vector3(x, randomHeight + y, z), Quaternion.identity, leaves.transform);
                    }
                }
            }

            // ������ ������ �� ������(GroundGenerator)�� �ڽ����� ���� (���� ����)
            trunk.transform.SetParent(transform);
        }
    }
}