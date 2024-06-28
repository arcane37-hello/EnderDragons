using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGenerator : MonoBehaviour
{
    public GameObject rockPrefab; // �⺻ �� ������
    public GameObject ironPrefab; // ö ������
    public GameObject diamondPrefab; // ���̾Ƹ�� ������

    // �⺻ ���� ö, ���̾� ���� Ȯ��
    public float rockProbability = 0.7f;    // �⺻ �� 70%
    public float ironProbability = 0.2f;    // ö 20%
    public float diamondProbability = 0.1f; // ���̾Ƹ�� 10%

    void Start()
    {
        GenerateRocks();
    }

    void GenerateRocks()
    {
        // ���������� ť����� ������ ���� ��ġ ����
        Vector3 startPosition = new Vector3(-50, -1, -50); // ���÷� ������ ��, ������ ����� �� ũ�⿡ �°� �����ϼ���.

        for (int x = 0; x < 100; x++) // ���÷� x�� z�� ������ 100���� �����߽��ϴ�.
        {
            for (int z = 0; z < 100; z++)
            {
                // Perlin Noise�� �̿��� ���� ������ Ȯ���� ����
                float rockNoise = Mathf.PerlinNoise((float)x / 20f, (float)z / 20f); // Perlin Noise �� ���

                if (rockNoise < rockProbability)
                {
                    // �⺻ �� ������ ��ġ ���
                    Vector3 rockPosition = startPosition + new Vector3(x, 0, z);
                    GameObject rock = Instantiate(rockPrefab, rockPosition, Quaternion.identity);
                    rock.transform.SetParent(transform); // RockGenerator�� �ڽ����� ����
                }
                else if (rockNoise < rockProbability + ironProbability)
                {
                    // ö ������ ��ġ ���
                    Vector3 ironPosition = startPosition + new Vector3(x, 0, z);
                    GameObject iron = Instantiate(ironPrefab, ironPosition, Quaternion.identity);
                    iron.transform.SetParent(transform); // RockGenerator�� �ڽ����� ����
                }
                else if (rockNoise < rockProbability + ironProbability + diamondProbability)
                {
                    // ���̾Ƹ�� ������ ��ġ ���
                    Vector3 diamondPosition = startPosition + new Vector3(x, 0, z);
                    GameObject diamond = Instantiate(diamondPrefab, diamondPosition, Quaternion.identity);
                    diamond.transform.SetParent(transform); // RockGenerator�� �ڽ����� ����
                }
            }
        }
    }
}