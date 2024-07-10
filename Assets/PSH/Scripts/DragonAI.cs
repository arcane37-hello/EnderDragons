using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAI : MonoBehaviour
{
    public float normalMoveSpeed = 3f; // ���� �̵� �ӵ�
    public float chargeMoveSpeed = 10f; // ���� �� �̵� �ӵ�
    public float rotationSpeed = 3f;
    public float attackCooldown = 15f; // ���� ��ٿ�
    public float flyHeight = 10f; // ���ƴٴ� ����
    public float orbitSpeed = 30f; // �ð� �������� ���� �ӵ�
    public float returnSpeed = 5f; // ������ �� ����� �ӵ� (���� ����)
    public float returnSmoothness = 0.5f; // ������ ���� �ε巯�� ����
    public float chargeDuration = 2f; // ���� ���� �ð�
    public float cooldownDuration = 10f; // ���� �� ��Ÿ��

    private Transform player;
    private Rigidbody rb;
    private float attackTimer;
    private float cooldownTimer; // ��Ÿ�� Ÿ�̸�
    private bool isCharging; // ���� ������ ����
    private float chargeStartTime; // ���� ���� �ð� ���
    private Vector3 startPosition; // ���� ��ġ ����
    private Vector3 targetPosition; // ��ǥ ��ġ ����
    private Vector3 chargeStartPosition; // ���� ���� ��ġ ����

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        attackTimer = attackCooldown;
        cooldownTimer = 0f;
        isCharging = false;
        startPosition = transform.position; // ���� ��ġ ����
    }

    void Update()
    {
        // �÷��̾� �������� ȸ��
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // �ð� �������� ����
        transform.RotateAround(player.position, Vector3.up, orbitSpeed * Time.deltaTime);

        // ���ƴٴϱ� (y�� ���� ����)
        targetPosition = startPosition + Vector3.up * flyHeight; // ��ǥ ��ġ�� ���ƴٴϴ� ���̷� ����
        rb.MovePosition(Vector3.MoveTowards(transform.position, targetPosition, normalMoveSpeed * Time.deltaTime));

        // ��Ȯ�� y ��ǥ�� 10�� ������ �� ���� ����
        if (transform.position.y == 10f && !isCharging && cooldownTimer <= 0f)
        {
            StartCharge();
        }

        // ���� ���� ���� �÷��̾� ������ ����
        if (isCharging)
        {
            Vector3 chargeDirection = (player.position - transform.position).normalized;
            rb.velocity = chargeDirection * chargeMoveSpeed; // ������ ����

            // ���� �ð� �� ���� ����
            if (Time.time - chargeStartTime >= chargeDuration)
            {
                StopCharge();
                cooldownTimer = cooldownDuration; // ���� �� ��Ÿ�� ����
            }
        }

        // ��Ÿ�� ����
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        // ���� ���� �� y ��ǥ�� 9 �̻��̸� ������ 10���� ���߱�
        if (!isCharging && transform.position.y >= 9f)
        {
            rb.MovePosition(new Vector3(transform.position.x, 10f, transform.position.z));
        }

        // ���� ��ٿ� ����
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0f)
        {
            attackTimer = attackCooldown;
        }
    }

    void Charge()
    {
        // ���� ����
        Debug.Log("���� �巡���� �÷��̾ ���� �����մϴ�!");
        isCharging = true;
        chargeStartTime = Time.time; // ���� ���� �ð� ���
        chargeStartPosition = transform.position; // ���� ���� ��ġ ����
    }

    void StartCharge()
    {
        Charge();
    }

    void StopCharge()
    {
        // ���� ���� �� �ڿ������� ���� ��ġ�� ����
        Debug.Log("���� �巡���� ������ ���߰� �ڿ������� ���� ��ġ�� �����մϴ�.");
        isCharging = false;
        rb.velocity = Vector3.zero; // �ӵ� �ʱ�ȭ

        // ���� ��ġ�� �ڿ������� �����ϱ�
        StartCoroutine(MoveToStartPosition(startPosition));
    }

    IEnumerator MoveToStartPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, returnSmoothness * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition; // ��Ȯ�� ��ġ�� ���߱�
    }
}