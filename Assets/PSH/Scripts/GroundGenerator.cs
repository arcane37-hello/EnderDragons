using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    public GameObject cubePrefab; // 땅을 구성할 큐브 프리팹
    public GameObject waterCubePrefab; // 물을 나타내는 큐브 프리팹
    public int groundWidth = 50; // x 방향 큐브 개수
    public int groundHeight = 3; // y 방향 큐브 개수
    public int groundLength = 50; // z 방향 큐브 개수

    public int lakeSize = 5; // 호수의 크기 (5x5)

    void Start()
    {
        GenerateGround();
    }

    void GenerateGround()
    {
        Debug.Log("Generating ground...");

        // 큐브들을 배치할 시작 위치 설정
        Vector3 startPosition = new Vector3(-groundWidth / 2, 0, -groundLength / 2); // y 방향 높이는 0으로 고정

        // 호수를 생성할 랜덤한 x, z 위치 계산 (-25에서 25까지)
        int lakeStartX = Random.Range(-25, 26 - lakeSize); // -25부터 25 사이에서 랜덤
        int lakeStartZ = Random.Range(-25, 26 - lakeSize); // -25부터 25 사이에서 랜덤

        Debug.Log($"Lake start position: ({lakeStartX}, {lakeStartZ})");

        // 물 큐브를 먼저 생성
        GenerateWater(lakeStartX, lakeStartZ);

        // 땅 큐브를 생성
        GenerateTerrain(startPosition, lakeStartX, lakeStartZ);

        Debug.Log("Ground generation completed.");
    }

    // 물 큐브 생성 메서드
    void GenerateWater(int lakeStartX, int lakeStartZ)
    {
        Debug.Log("Generating water...");

        // 호수 영역에 물 큐브 생성
        for (int x = lakeStartX; x < lakeStartX + lakeSize; x++)
        {
            for (int z = lakeStartZ; z < lakeStartZ + lakeSize; z++)
            {
                // 물 큐브의 위치 설정 (y는 땅 위에서만)
                Vector3 waterPosition = new Vector3(x, 1, z);

                // 땅 위에서만 물이 생성될 수 있도록
                if (!IsPositionOnGround(x, z))
                {
                    // 물 큐브 인스턴스 생성
                    GameObject waterCube = Instantiate(waterCubePrefab, waterPosition, Quaternion.identity);
                    waterCube.transform.SetParent(transform); // 땅 생성기(GroundGenerator)의 자식으로 설정
                    Debug.Log($"Water cube instantiated at ({waterPosition.x}, {waterPosition.y}, {waterPosition.z})");
                }
            }
        }

        Debug.Log("Water generation completed.");
    }

    // 땅 큐브 생성 메서드
    void GenerateTerrain(Vector3 startPosition, int lakeStartX, int lakeStartZ)
    {
        Debug.Log("Generating terrain...");

        // 땅 큐브를 생성
        for (int x = 0; x < groundWidth; x++)
        {
            for (int z = 0; z < groundLength; z++)
            {
                // 물이 있는 위치에는 땅 큐브 생성하지 않음
                if (!IsPositionInLake(x, z, lakeStartX, lakeStartZ))
                {
                    // Perlin Noise를 이용해 높이 결정
                    float heightNoise = Mathf.PerlinNoise((float)x / 10f, (float)z / 10f); // Perlin Noise 값 계산
                    int height = Mathf.RoundToInt(heightNoise * (groundHeight - 1)) + 1; // 높이 계산 (1 ~ groundHeight)

                    for (int y = 0; y < height; y++)
                    {
                        // 각 큐브의 위치 계산
                        Vector3 groundPosition = startPosition + new Vector3(x, y, z);

                        // 큐브 인스턴스 생성
                        GameObject cube = Instantiate(cubePrefab, groundPosition, Quaternion.identity);
                        cube.transform.SetParent(transform); // 땅 생성기(GroundGenerator)의 자식으로 설정
                        cube.tag = "Ground"; // 태그 할당
                        Debug.Log($"Ground cube instantiated at ({groundPosition.x}, {groundPosition.y}, {groundPosition.z})");
                    }
                }
            }
        }

        Debug.Log("Terrain generation completed.");
    }

    // 해당 좌표가 땅에 있는지 확인하는 메서드
    bool IsPositionOnGround(int x, int z)
    {
        // 땅의 영역 계산
        int startX = groundWidth / 2;
        int endX = startX + groundWidth - 1;
        int startZ = groundLength / 2;
        int endZ = startZ + groundLength - 1;

        // 좌표가 땅 범위 내에 있는지 확인
        if (x >= startX && x <= endX && z >= startZ && z <= endZ)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // 해당 좌표가 호수에 속하는지 확인하는 메서드
    bool IsPositionInLake(int x, int z, int lakeStartX, int lakeStartZ)
    {
        // 호수의 범위 계산
        int lakeEndX = lakeStartX + lakeSize - 1;
        int lakeEndZ = lakeStartZ + lakeSize - 1;

        // 좌표가 호수 범위 내에 있는지 확인
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