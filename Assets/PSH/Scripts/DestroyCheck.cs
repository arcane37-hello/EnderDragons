using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCheck : MonoBehaviour
{
    // 오브젝트 1과 오브젝트 2를 드래그 앤 드롭으로 할당합니다.
    public GameObject object1;
    public GameObject object2;

    void Start()
    {
        // 오브젝트 2를 비활성화합니다. 
        // (시작 시에 오브젝트 2가 비활성화된 상태라면 이 코드는 없어도 됩니다)
        if (object2 != null)
        {
            object2.SetActive(false);
        }
    }

    void Update()
    {
        // 오브젝트 1이 파괴되었는지 확인합니다.
        if (object1 == null)
        {
            // 오브젝트 2를 활성화합니다.
            if (object2 != null)
            {
                object2.SetActive(true);
            }
        }
    }
}