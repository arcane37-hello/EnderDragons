using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
    public GameObject treeBlockPrefab; // ���� ��� ������
    public GameObject leafBlockPrefab; // ������ ��� ������
    public int numberOfTrees = 10; // ������ ���� ����
    public float groundWidth = 10f; // ���� ���� ����
    public float groundLength = 10f; // ���� ���� ����
    public float minHeight = 5f; // ������ �ּ� ����
    public float maxHeight = 7f; // ������ �ִ� ����

    void Start()
    {
        GenerateTrees();
    }

    void GenerateTrees()
    {
        for (int i = 0; i < numberOfTrees; i++)
        {
            // ������ ��ġ ���
            float randomX = Random.Range(-groundWidth / 2f, groundWidth / 2f);
            float randomZ = Random.Range(-groundLength / 2f, groundLength / 2f);
            Vector3 position = new Vector3(randomX, 0f, randomZ);

            // ������ ���� ���
            float randomHeight = Random.Range(minHeight, maxHeight);
            Vector3 trunkPosition = position + new Vector3(0f, randomHeight / 2f, 0f);

            // ���� ��� ����
            GameObject trunk = new GameObject("Tree");

            // ���� ��ϵ�� ������ ��� ����
            for (float y = 0.5f; y <= randomHeight; y += 1f)
            {
                GameObject treeBlock = Instantiate(treeBlockPrefab, trunkPosition + new Vector3(0f, y - 0.5f, 0f), Quaternion.identity);
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