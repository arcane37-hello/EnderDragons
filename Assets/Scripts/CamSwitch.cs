
// ī�޶� ������
//using UnityEngine;

//public class CameraSwitcher : MonoBehaviour
//{
//    public Camera firstPersonCamera;
//    public Camera thirdPersonCamera;
//    public Vector3 thirdPersonOffset = new Vector3(0, 2, -4); // 3��Ī ī�޶� ������
//    private bool isThirdPerson = false;
//    private PlayerMove playerMove;

//    void Start()
//    {
//        playerMove = FindObjectOfType<PlayerMove>();
//        thirdPersonCamera.enabled = false; // ���� �� 3��Ī ī�޶� ��Ȱ��ȭ

//        // 3��Ī ī�޶��� �ʱ� ��ġ ����
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


// �Ȱ��� ���
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

////ī�޶� ���� ��ȯ�� �þ� �� ����. �ٸ� �����
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


// �����۵� ī�޶� ��ȯ + �̵� ���� (���� ī�޶� ��ȯ�� �þ� ���)
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




//// F5�� ī�޶� ��ȯ ���� ( ��ȯ�� �̵� �Ұ�)

//using UnityEngine;

//public class CamSwitch : MonoBehaviour
//{
//    public Camera MainCamera;
//    public Camera SecondCamera;
//    private bool isSecondCamera = false;

//    void Start()
//    {
//        // ó������ 1��Ī ī�޶� Ȱ��ȭ�ϰ� 3��Ī ī�޶�� ��Ȱ��ȭ
//        MainCamera.enabled = true;
//        SecondCamera.enabled = false;
//    }

//    void Update()
//    {
//        // 'F5' Ű �Է��� ����
//        if (Input.GetKeyDown(KeyCode.F5))
//        {
//            SwitchCamera();
//        }
//    }

//    void SwitchCamera()
//    {
//        // ���� ������ �ݴ�� ����
//        isSecondCamera = !isSecondCamera;

//        // ī�޶� Ȱ��ȭ/��Ȱ��ȭ
//        MainCamera.enabled = !isSecondCamera;
//        SecondCamera.enabled = isSecondCamera;
//    }
//}
