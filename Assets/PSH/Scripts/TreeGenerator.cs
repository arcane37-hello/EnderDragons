using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
    public GameObject treeBlockPrefab; // 나무 블록 프리팹
    public GameObject leafBlockPrefab; // 나뭇잎 블록 프리팹
    public int numberOfTrees = 10; // 생성할 나무 개수
    public int groundWidth = 10; // 땅의 가로 길이
    public int groundLength = 10; // 땅의 세로 길이
    public int minHeight = 5; // 나무의 최소 높이
    public int maxHeight = 7; // 나무의 최대 높이

    void Start()
    {
        GenerateTrees();
    }

    void GenerateTrees()
    {
        for (int i = 0; i < numberOfTrees; i++)
        {
            // 무작위 위치 계산
            int randomX = Random.Range(-groundWidth / 2, groundWidth / 2);
            int randomZ = Random.Range(-groundLength / 2, groundLength / 2);
            Vector3 position = new Vector3(randomX, 0, randomZ);

            // 무작위 높이 계산
            int randomHeight = Random.Range(minHeight, maxHeight);
            Vector3 trunkPosition = position + new Vector3(0, randomHeight / 2, 0);

            // 나무 기둥 생성
            GameObject trunk = new GameObject("Tree");

            // 나무 블록들로 구성된 기둥 생성
            for (int y = 1; y <= randomHeight; y++)
            {
                GameObject treeBlock = Instantiate(treeBlockPrefab, trunkPosition + new Vector3(0, y - 1, 0), Quaternion.identity);
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