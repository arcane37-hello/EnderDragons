using UnityEngine;

public class RotatingBlock : MonoBehaviour
{
    public float rotationSpeed = 45f; // 회전 속도

    void Update()
    {
        // 프리팹을 제자리에서 회전
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
