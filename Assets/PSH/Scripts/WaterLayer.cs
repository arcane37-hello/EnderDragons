using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLayer : MonoBehaviour
{
    public LayerMask waterLayer; // Water 레이어를 선택할 수 있는 레이어 마스크
    public LayerMask nonWaterLayer; // Water가 아닌 레이어를 선택할 수 있는 레이어 마스크

    void Update()
    {
        // Water 레이어에 있는 모든 오브젝트를 찾기 위한 루프
        var waterObjects = GameObject.FindObjectsOfType<Collider>();

        foreach (var waterObject in waterObjects)
        {
            // 현재 오브젝트가 Water 레이어에 있는지 확인
            if (waterObject.gameObject.layer == LayerMask.NameToLayer("Water"))
            {
                // Water 레이어의 오브젝트와 겹치는 다른 오브젝트를 찾기 위한 Collider 배열
                Collider[] colliders = Physics.OverlapBox(
                    waterObject.transform.position,
                    waterObject.bounds.extents,
                    waterObject.transform.rotation,
                    nonWaterLayer
                );

                foreach (var collider in colliders)
                {
                    // Water가 아닌 오브젝트를 제거
                    if (collider.gameObject.layer != LayerMask.NameToLayer("Water"))
                    {
                        Destroy(collider.gameObject);
                    }
                }
            }
        }
    }
}