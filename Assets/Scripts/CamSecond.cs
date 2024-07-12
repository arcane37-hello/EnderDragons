//카메라가 고정됨
//using UnityEngine;

//public class ThirdPersonCamera : MonoBehaviour
//{
//    public Transform player; // 플레이어의 Transform
//    public Vector3 offset = new Vector3(0, 2, -4); // 카메라 오프셋
//    public float smoothSpeed = 0.125f; // 카메라 움직임의 부드러움 정도

//    void LateUpdate()
//    {
//        Vector3 desiredPosition = player.position + offset;
//        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
//        transform.position = smoothedPosition;

//        transform.LookAt(player.position + Vector3.up * 1.5f); // 카메라가 플레이어를 바라보도록 설정
//    }
//}




////카메라 움직임 부자연스러움
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
//        // 마우스 입력을 받아서 yaw와 pitch 계산
//        yaw += Input.GetAxis("Mouse X") * yawSpeed * Time.deltaTime;
//        pitch -= Input.GetAxis("Mouse Y") * pitchSpeed * Time.deltaTime;
//        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

//        // 카메라 위치 계산
//        Vector3 targetPosition = player.position + Quaternion.Euler(pitch, yaw, 0) * offset;
//        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * rotationSpeed);

//        // 카메라가 플레이어를 바라보도록 설정
//        transform.LookAt(player.position + Vector3.up * height);
//    }
//}


// 2번째 카메라에서 플레이어 이동과 시야 잘 확인 됨 (상하 이동시 플레이어를 벗어남)
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
