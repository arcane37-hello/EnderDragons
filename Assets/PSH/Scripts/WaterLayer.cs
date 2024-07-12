using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLayer : MonoBehaviour
{
    public LayerMask groundLayer; // Ground 레이어를 선택할 수 있는 레이어 마스크

    private void Start()
    {
        RemoveOverlappingGroundObjects();
    }

    private void RemoveOverlappingGroundObjects()
    {
        // 모든 Water 레이어의 Collider를 가져옵니다.
        Collider[] waterColliders = GameObject.FindObjectsOfType<Collider>();

        foreach (var waterCollider in waterColliders)
        {
            if (waterCollider.gameObject.layer == LayerMask.NameToLayer("Water"))
            {
                Bounds waterBounds = waterCollider.bounds;

                // Water Collider의 Bounds와 Ground 레이어의 오브젝트의 Bounds의 교차 여부를 검사합니다.
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

                        // 두 Bounds가 완전히 겹치는지 확인합니다.
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
        // 두 Bounds가 서로 완전히 겹치는지 확인합니다.
        return a.min.x <= b.min.x &&
               a.max.x >= b.max.x &&
               a.min.y <= b.min.y &&
               a.max.y >= b.max.y &&
               a.min.z <= b.min.z &&
               a.max.z >= b.max.z;
    }
}