using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    public GameObject cubePrefab;
    public int groundWidth = 100; // x 방향 큐브 개수
    public int groundHeight = 3; // y 방향 큐브 개수
    public int groundLength = 100; // z 방향 큐브 개수

    void Start()
    {
        GenerateGround();
    }

    void GenerateGround()
    {
        // 큐브들을 배치할 시작 위치 설정
        Vector3 startPosition = new Vector3(-groundWidth / 2, 0, -groundLength / 2); // y 방향 높이는 0으로 고정

        // 땅을 구성할 큐브들을 생성하는 코드
        for (int x = 0; x < groundWidth; x++)
        {
            for (int z = 0; z < groundLength; z++)
            {
                // Perlin Noise를 이용해 높이 결정
                float heightNoise = Mathf.PerlinNoise((float)x / 10f, (float)z / 10f); // Perlin Noise 값 계산
                int height = Mathf.RoundToInt(heightNoise * (groundHeight - 1)) + 1; // 높이 계산 (1 ~ groundHeight)

                for (int y = 0; y < height; y++)
                {
                    // 각 큐브의 위치 계산
                    Vector3 position = startPosition + new Vector3(x, y, z);

                    // 큐브 인스턴스 생성
                    GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);
                    cube.transform.SetParent(transform); // 땅 생성기(GroundGenerator)의 자식으로 설정
                }
            }
        }
    }
}
