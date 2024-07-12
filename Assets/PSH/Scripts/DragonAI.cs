using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAI : MonoBehaviour
{
    public float moveSpeed = 5f; // �巡���� ���� �ӵ�
    public float rotationSpeed = 3f; // �巡���� �÷��̾ ������ ���� ȸ�� �ӵ�
    public float flyHeight = 10f; // �巡���� ������ ���� ����

    private Transform player; // �÷��̾��� Transform

    void Start()
    {
        // "Player" �±׸� ���� ������Ʈ�� Transform�� ã���ϴ�.
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // �÷��̾ ���� ��� �ƹ� �۾��� ���� �ʽ��ϴ�.
        if (player == null)
            return;

        // �÷��̾ �����մϴ�.
        FollowPlayer();
    }

    void FollowPlayer()
    {
        // �÷��̾��� ��ġ�� �������� �巡���� ������ ȸ����ŵ�ϴ�.
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // �巡���� �÷��̾� ������ �̵��մϴ�.
        Vector3 targetPosition = new Vector3(player.position.x, flyHeight, player.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}