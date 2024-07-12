
// 카메라가 고정됨
//using UnityEngine;

//public class CameraSwitcher : MonoBehaviour
//{
//    public Camera firstPersonCamera;
//    public Camera thirdPersonCamera;
//    public Vector3 thirdPersonOffset = new Vector3(0, 2, -4); // 3인칭 카메라 오프셋
//    private bool isThirdPerson = false;
//    private PlayerMove playerMove;

//    void Start()
//    {
//        playerMove = FindObjectOfType<PlayerMove>();
//        thirdPersonCamera.enabled = false; // 시작 시 3인칭 카메라를 비활성화

//        // 3인칭 카메라의 초기 위치 설정
//        if (thirdPersonCamera != null && playerMove != null)
//        {
//            thirdPersonCamera.transform.position = playerMove.transform.position + thirdPersonOffset;
//            thirdPersonCamera.transform.LookAt(playerMove.transform.position + Vector3.up * 1.5f);
//        }
//    }

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.F5))
//        {
//            isThirdPerson = !isThirdPerson;
//            SwitchCamera();
//        }
//    }

//    void SwitchCamera()
//    {
//        if (isThirdPerson)
//        {
//            firstPersonCamera.enabled = false;
//            thirdPersonCamera.enabled = true;
//        }
//        else
//        {
//            firstPersonCamera.enabled = true;
//            thirdPersonCamera.enabled = false;
//        }

//        playerMove.mainCamera = isThirdPerson ? thirdPersonCamera : firstPersonCamera;
//    }
//}


// 똑같이 어색
//using UnityEngine;

//public class CameSwitch : MonoBehaviour
//{
//    public Camera firstPersonCamera;
//    public Camera thirdPersonCamera;
//    public Transform thirdPersonCameraPosition;
//    private bool isThirdPerson = false;
//    private PlayerMove playerMove;

//    void Start()
//    {
//        playerMove = FindObjectOfType<PlayerMove>();
//    }

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.F5))
//        {
//            isThirdPerson = !isThirdPerson;
//            SwitchCamera();
//        }
//    }

//    void SwitchCamera()
//    {
//        if (isThirdPerson)
//        {
//            firstPersonCamera.enabled = false;
//            thirdPersonCamera.enabled = true;
//        }
//        else
//        {
//            firstPersonCamera.enabled = true;
//            thirdPersonCamera.enabled = false;
//        }

//        playerMove.mainCamera = isThirdPerson ? thirdPersonCamera : firstPersonCamera;
//    }
//}

////카메라 상하 전환시 시야 잘 잡힘. 다만 어색함
//using UnityEngine;

//public class CamSwitch : MonoBehaviour
//{
//    public Camera firstPersonCamera;
//    public Camera thirdPersonCamera;
//    public Transform thirdPersonCameraPosition;
//    private bool isThirdPerson = false;

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.F5))
//        {
//            isThirdPerson = !isThirdPerson;
//            SwitchCamera();
//        }
//    }

//    void SwitchCamera()
//    {
//        if (isThirdPerson)
//        {
//            firstPersonCamera.enabled = false;
//            thirdPersonCamera.enabled = true;
//            thirdPersonCamera.transform.position = thirdPersonCameraPosition.position;
//            thirdPersonCamera.transform.rotation = thirdPersonCameraPosition.rotation;
//        }
//        else
//        {
//            firstPersonCamera.enabled = true;
//            thirdPersonCamera.enabled = false;
//        }
//    }
//}


// 정상작동 카메라 전환 + 이동 가능 (상하 카메라 전환시 시야 벗어남)
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    public Camera MainCamera;
    public Camera SecondCamera;
    public Transform SecondCameraPosition;

    void Start()
    {
        SecondCamera.transform.position = SecondCameraPosition.position;
        SecondCamera.transform.rotation = SecondCameraPosition.rotation;
        SwitchToFirstPerson();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            if (MainCamera.gameObject.activeSelf)
            {
                SwitchToThirdPerson();
            }
            else
            {
                SwitchToFirstPerson();
            }
        }
    }

    void SwitchToFirstPerson()
    {
        MainCamera.gameObject.SetActive(true);
        SecondCamera.gameObject.SetActive(false);
    }

    void SwitchToThirdPerson()
    {
        MainCamera.gameObject.SetActive(false);
        SecondCamera.gameObject.SetActive(true);
    }
}




//// F5로 카메라 전환 가능 ( 전환시 이동 불가)

//using UnityEngine;

//public class CamSwitch : MonoBehaviour
//{
//    public Camera MainCamera;
//    public Camera SecondCamera;
//    private bool isSecondCamera = false;

//    void Start()
//    {
//        // 처음에는 1인칭 카메라를 활성화하고 3인칭 카메라는 비활성화
//        MainCamera.enabled = true;
//        SecondCamera.enabled = false;
//    }

//    void Update()
//    {
//        // 'F5' 키 입력을 감지
//        if (Input.GetKeyDown(KeyCode.F5))
//        {
//            SwitchCamera();
//        }
//    }

//    void SwitchCamera()
//    {
//        // 현재 시점의 반대로 변경
//        isSecondCamera = !isSecondCamera;

//        // 카메라 활성화/비활성화
//        MainCamera.enabled = !isSecondCamera;
//        SecondCamera.enabled = isSecondCamera;
//    }
//}
