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

            // 나무 블록들로 구성된 기둥 생성 (크기는 고정)
            for (int y = 1; y <= randomHeight; y++)
            {
                GameObject treeBlock = Instantiate(treeBlockPrefab, trunkPosition + new Vector3(0, y - 1, 0), Quaternion.identity);
                treeBlock.transform.SetParent(trunk.transform);
            }

            // 나뭇잎 생성
            GameObject leaves = new GameObject("Leaves");

            // 랜덤한 나뭇잎 패턴 생성
            int leafRange = Random.Range(1, 2); // 각 축의 범위를 랜덤하게 설정할 변수

            for (int x = -leafRange; x <= leafRange; x++)
            {
                for (int y = -leafRange; y <= leafRange; y++)
                {
                    for (int z = -leafRange; z <= leafRange; z++)
                    {
                        // 나뭇잎 위치와 회전을 랜덤하게 설정할 수 있음
                        Vector3 leafPosition = trunkPosition + new Vector3(x, randomHeight + y, z);
                        GameObject leafBlock = Instantiate(leafBlockPrefab, leafPosition, Quaternion.identity);
                        leafBlock.transform.SetParent(leaves.transform);
                    }
                }
            }

            // 생성된 나무를 땅 생성기(GroundGenerator)의 자식으로 설정 (선택 사항)
            trunk.transform.SetParent(transform);
        }
    }
}