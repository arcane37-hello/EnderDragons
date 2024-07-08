using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallBlock : MonoBehaviour
{
    public GameObject dirtPrefab; // �� ������
    public GameObject stonePrefab; // �� ������

    private enum BuildState
    {
        None,
        PlacingDirt,
        PlacingStone
    }

    private BuildState currentBuildState = BuildState.None;

    void Update()
    {
        // ���� Ű �Է� ����
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentBuildState = BuildState.PlacingDirt;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentBuildState = BuildState.PlacingStone;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1)) // ���콺 ������ ��ư Ŭ��
        {
            // ���� ���¿� ���� ������ ��ġ
            switch (currentBuildState)
            {
                case BuildState.PlacingDirt:
                    PlacePrefab(dirtPrefab);
                    break;
                case BuildState.PlacingStone:
                    PlacePrefab(stonePrefab);
                    break;
                default:
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) ||
                 Input.GetKeyDown(KeyCode.Alpha2) ||
                 Input.GetKeyDown(KeyCode.Alpha5) ||
                 Input.GetKeyDown(KeyCode.Alpha6) ||
                 Input.GetKeyDown(KeyCode.Alpha7) ||
                 Input.GetKeyDown(KeyCode.Alpha8) ||
                 Input.GetKeyDown(KeyCode.Alpha9) ||
                 Input.GetKeyDown(KeyCode.Alpha0))
        {
            // 3���� 4�� �̿��� �ٸ� ���� Ű�� ������ ���� �ʱ�ȭ
            currentBuildState = BuildState.None;
        }
    }

    void PlacePrefab(GameObject prefab)
    {
        // ���콺 ��ġ���� Raycast�� ����Ͽ� �ٴڿ� �������� ��ġ
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // ǥ���� ���� ��������
            Vector3 surfaceNormal = hit.normal;
            Vector3 position = hit.point + surfaceNormal * 0.5f; // �������� �߽��� ǥ�鿡 ��ġ�ϵ��� ����

            // ���� ��ǥ�� ��ȯ�Ͽ� ������ ��ġ
            Vector3Int intPosition = new Vector3Int(
                Mathf.RoundToInt(position.x),
                Mathf.RoundToInt(position.y),
                Mathf.RoundToInt(position.z)
            );

            Instantiate(prefab, intPosition, Quaternion.FromToRotation(Vector3.up, surfaceNormal));
        }
    }
}