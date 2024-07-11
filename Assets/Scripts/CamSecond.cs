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
