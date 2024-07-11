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
