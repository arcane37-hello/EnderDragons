using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAI : MonoBehaviour
{
    public float moveSpeed = 5f; // 드래곤의 비행 속도
    public float rotationSpeed = 3f; // 드래곤이 플레이어를 추적할 때의 회전 속도
    public float flyHeight = 10f; // 드래곤이 비행할 때의 높이

    private Transform player; // 플레이어의 Transform

    void Start()
    {
        // "Player" 태그를 가진 오브젝트의 Transform을 찾습니다.
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // 플레이어가 없는 경우 아무 작업도 하지 않습니다.
        if (player == null)
            return;

        // 플레이어를 추적합니다.
        FollowPlayer();
    }

    void FollowPlayer()
    {
        // 플레이어의 위치를 기준으로 드래곤의 방향을 회전시킵니다.
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // 드래곤이 플레이어 쪽으로 이동합니다.
        Vector3 targetPosition = new Vector3(player.position.x, flyHeight, player.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}