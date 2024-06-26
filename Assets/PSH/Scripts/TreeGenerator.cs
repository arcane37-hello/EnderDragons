using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
    public GameObject treeBlockPrefab; // 나무 블록 프리팹
    public GameObject leafBlockPrefab; // 나뭇잎 블록 프리팹
    public int numberOfTrees = 10; // 생성할 나무 개수
    public float groundWidth = 10f; // 땅의 가로 길이
    public float groundLength = 10f; // 땅의 세로 길이
    public float minHeight = 5f; // 나무의 최소 높이
    public float maxHeight = 7f; // 나무의 최대 높이

    void Start()
    {
        GenerateTrees();
    }

    void GenerateTrees()
    {
        for (int i = 0; i < numberOfTrees; i++)
        {
            // 무작위 위치 계산
            float randomX = Random.Range(-groundWidth / 2f, groundWidth / 2f);
            float randomZ = Random.Range(-groundLength / 2f, groundLength / 2f);
            Vector3 position = new Vector3(randomX, 0f, randomZ);

            // 무작위 높이 계산
            float randomHeight = Random.Range(minHeight, maxHeight);
            Vector3 trunkPosition = position + new Vector3(0f, randomHeight / 2f, 0f);

            // 나무 기둥 생성
            GameObject trunk = new GameObject("Tree");

            // 나무 블록들로 구성된 기둥 생성
            for (float y = 0.5f; y <= randomHeight; y += 1f)
            {
                GameObject treeBlock = Instantiate(treeBlockPrefab, trunkPosition + new Vector3(0f, y - 0.5f, 0f), Quaternion.identity);
                treeBlock.transform.SetParent(trunk.transform);
            }

            // 나뭇잎 생성
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

            // 생성된 나무를 땅 생성기(GroundGenerator)의 자식으로 설정 (선택 사항)
            trunk.transform.SetParent(transform);
        }
    }
}