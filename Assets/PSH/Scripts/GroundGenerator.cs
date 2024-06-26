using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    public GameObject cubePrefab;

    void Start()
    {
        GenerateGround();
    }

    void GenerateGround()
    {
        int groundWidth = 10; // x 방향 큐브 개수
        int groundLength = 10; // z 방향 큐브 개수

        // 큐브들을 배치할 시작 위치 설정
        Vector3 startPosition = new Vector3(-groundWidth / 2, 0, -groundLength / 2);

        // 땅을 구성할 큐브들을 생성하는 코드
        for (int x = 0; x < groundWidth; x++)
        {
            for (int z = 0; z < groundLength; z++)
            {
                // 각 큐브의 위치 계산
                Vector3 position = startPosition + new Vector3(x, 0, z);

                // 큐브 인스턴스 생성
                GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);
                cube.transform.SetParent(transform); // 땅 생성기(GroundGenerator)의 자식으로 설정
            }
        }
    }
}
