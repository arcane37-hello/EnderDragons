using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float lifetime = 5f; // 미사일의 생존 시간

    private void Start()
    {
        // 일정 시간이 지나면 미사일이 자동으로 파괴됩니다.
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 `Player` 태그를 가지고 있으면 미사일을 파괴합니다.
        if (collision.gameObject.CompareTag("Player"))
        {
            // 여기에 PlayerHealth 컴포넌트와 데미지 처리 로직은 포함하지 않습니다.
            Destroy(gameObject); // 충돌 시 미사일을 파괴합니다
        }
        // 충돌한 오브젝트가 `Ground` 태그를 가지고 있으면 미사일과 함께 해당 오브젝트를 파괴합니다.
        else if (collision.gameObject.CompareTag("Ground"))
        {
            // Ground 태그를 가진 오브젝트도 파괴합니다.
            Destroy(collision.gameObject); // 충돌 시 Ground 오브젝트를 파괴합니다
            Destroy(gameObject); // 미사일도 파괴합니다
        }
        else
        {
            // 다른 태그의 오브젝트와 충돌 시 미사일만 파괴합니다
            Destroy(gameObject);
        }
    }
}