using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLayer : MonoBehaviour
{
    public LayerMask groundLayer; // Ground ���̾ ������ �� �ִ� ���̾� ����ũ

    private void Start()
    {
        RemoveOverlappingGroundObjects();
    }

    private void RemoveOverlappingGroundObjects()
    {
        // ��� Water ���̾��� Collider�� �����ɴϴ�.
        Collider[] waterColliders = GameObject.FindObjectsOfType<Collider>();

        foreach (var waterCollider in waterColliders)
        {
            if (waterCollider.gameObject.layer == LayerMask.NameToLayer("Water"))
            {
                Bounds waterBounds = waterCollider.bounds;

                // Water Collider�� Bounds�� Ground ���̾��� ������Ʈ�� Bounds�� ���� ���θ� �˻��մϴ�.
                Collider[] groundColliders = Physics.OverlapBox(
                    waterBounds.center,
                    waterBounds.extents,
                    Quaternion.identity,
                    groundLayer
                );

                foreach (var groundCollider in groundColliders)
                {
                    if (groundCollider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                    {
                        Bounds groundBounds = groundCollider.bounds;

                        // �� Bounds�� ������ ��ġ���� Ȯ���մϴ�.
                        if (AreBoundsCompletelyOverlapping(waterBounds, groundBounds))
                        {
                            Debug.Log($"Removing {groundCollider.gameObject.name} at position {groundCollider.transform.position}");
                            Destroy(groundCollider.gameObject);
                        }
                    }
                }
            }
        }
    }

    private bool AreBoundsCompletelyOverlapping(Bounds a, Bounds b)
    {
        // �� Bounds�� ���� ������ ��ġ���� Ȯ���մϴ�.
        return a.min.x <= b.min.x &&
               a.max.x >= b.max.x &&
               a.min.y <= b.min.y &&
               a.max.y >= b.max.y &&
               a.min.z <= b.min.z &&
               a.max.z >= b.max.z;
    }
}