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
