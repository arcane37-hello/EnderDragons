using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGenerator : MonoBehaviour
{
    public GameObject rockPrefab; // 기본 돌 프리팹
    public GameObject ironPrefab; // 철 프리팹
    public GameObject diamondPrefab; // 다이아몬드 프리팹

    // 기본 돌과 철, 다이아 생성 확률
    public float rockProbability = 0.7f;    // 기본 돌 70%
    public float ironProbability = 0.2f;    // 철 20%
    public float diamondProbability = 0.1f; // 다이아몬드 10%

    void Start()
    {
        GenerateRocks();
    }

    void GenerateRocks()
    {
        // 독립적으로 큐브들을 생성할 시작 위치 설정
        Vector3 startPosition = new Vector3(-50, -1, -50); // 예시로 설정한 값, 실제로 사용할 맵 크기에 맞게 조정하세요.

        for (int x = 0; x < 100; x++) // 예시로 x와 z의 범위를 100으로 설정했습니다.
        {
            for (int z = 0; z < 100; z++)
            {
                // Perlin Noise를 이용해 돌이 생성될 확률을 결정
                float rockNoise = Mathf.PerlinNoise((float)x / 20f, (float)z / 20f); // Perlin Noise 값 계산

                if (rockNoise < rockProbability)
                {
                    // 기본 돌 생성될 위치 계산
                    Vector3 rockPosition = startPosition + new Vector3(x, 0, z);
                    GameObject rock = Instantiate(rockPrefab, rockPosition, Quaternion.identity);
                    rock.transform.SetParent(transform); // RockGenerator의 자식으로 설정
                }
                else if (rockNoise < rockProbability + ironProbability)
                {
                    // 철 생성될 위치 계산
                    Vector3 ironPosition = startPosition + new Vector3(x, 0, z);
                    GameObject iron = Instantiate(ironPrefab, ironPosition, Quaternion.identity);
                    iron.transform.SetParent(transform); // RockGenerator의 자식으로 설정
                }
                else if (rockNoise < rockProbability + ironProbability + diamondProbability)
                {
                    // 다이아몬드 생성될 위치 계산
                    Vector3 diamondPosition = startPosition + new Vector3(x, 0, z);
                    GameObject diamond = Instantiate(diamondPrefab, diamondPosition, Quaternion.identity);
                    diamond.transform.SetParent(transform); // RockGenerator의 자식으로 설정
                }
            }
        }
    }
}