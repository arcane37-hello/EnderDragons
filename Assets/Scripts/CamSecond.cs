//ī�޶� ������
//using UnityEngine;

//public class ThirdPersonCamera : MonoBehaviour
//{
//    public Transform player; // �÷��̾��� Transform
//    public Vector3 offset = new Vector3(0, 2, -4); // ī�޶� ������
//    public float smoothSpeed = 0.125f; // ī�޶� �������� �ε巯�� ����

//    void LateUpdate()
//    {
//        Vector3 desiredPosition = player.position + offset;
//        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
//        transform.position = smoothedPosition;

//        transform.LookAt(player.position + Vector3.up * 1.5f); // ī�޶� �÷��̾ �ٶ󺸵��� ����
//    }
//}




////ī�޶� ������ ���ڿ�������
//using UnityEngine;

//public class CamSecond : MonoBehaviour
//{
//    public Transform player;
//    public Vector3 offset;
//    public float distance = 10.0f;
//    public float height = 10.0f;
//    public float rotationSpeed = 5.0f;
//    public float pitchMin = -30f;
//    public float pitchMax = 60f;
//    public float yawSpeed = 100.0f;
//    public float pitchSpeed = 80.0f;

//    private float yaw = 0.0f;
//    private float pitch = 0.0f;

//    void Start()
//    {
//        offset = new Vector3(0, height, -distance);
//    }

//    void LateUpdate()
//    {
//        // ���콺 �Է��� �޾Ƽ� yaw�� pitch ���
//        yaw += Input.GetAxis("Mouse X") * yawSpeed * Time.deltaTime;
//        pitch -= Input.GetAxis("Mouse Y") * pitchSpeed * Time.deltaTime;
//        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

//        // ī�޶� ��ġ ���
//        Vector3 targetPosition = player.position + Quaternion.Euler(pitch, yaw, 0) * offset;
//        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * rotationSpeed);

//        // ī�޶� �÷��̾ �ٶ󺸵��� ����
//        transform.LookAt(player.position + Vector3.up * height);
//    }
//}


// 2��° ī�޶󿡼� �÷��̾� �̵��� �þ� �� Ȯ�� �� (���� �̵��� �÷��̾ ���)
using UnityEngine;

public class CamSecond : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float rotationSpeed = 5.0f;

    void Start()
    {
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * rotationSpeed);

        transform.LookAt(player);
    }
}
