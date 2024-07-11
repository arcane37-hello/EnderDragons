using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLayer : MonoBehaviour
{
    public LayerMask waterLayer; // Water ���̾ ������ �� �ִ� ���̾� ����ũ
    public LayerMask nonWaterLayer; // Water�� �ƴ� ���̾ ������ �� �ִ� ���̾� ����ũ

    void Update()
    {
        // Water ���̾ �ִ� ��� ������Ʈ�� ã�� ���� ����
        var waterObjects = GameObject.FindObjectsOfType<Collider>();

        foreach (var waterObject in waterObjects)
        {
            // ���� ������Ʈ�� Water ���̾ �ִ��� Ȯ��
            if (waterObject.gameObject.layer == LayerMask.NameToLayer("Water"))
            {
                // Water ���̾��� ������Ʈ�� ��ġ�� �ٸ� ������Ʈ�� ã�� ���� Collider �迭
                Collider[] colliders = Physics.OverlapBox(
                    waterObject.transform.position,
                    waterObject.bounds.extents,
                    waterObject.transform.rotation,
                    nonWaterLayer
                );

                foreach (var collider in colliders)
                {
                    // Water�� �ƴ� ������Ʈ�� ����
                    if (collider.gameObject.layer != LayerMask.NameToLayer("Water"))
                    {
                        Destroy(collider.gameObject);
                    }
                }
            }
        }
    }
}