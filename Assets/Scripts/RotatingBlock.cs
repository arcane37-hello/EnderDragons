using UnityEngine;

public class RotatingBlock : MonoBehaviour
{
    public float rotationSpeed = 45f; // ȸ�� �ӵ�

    void Update()
    {
        // �������� ���ڸ����� ȸ��
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
