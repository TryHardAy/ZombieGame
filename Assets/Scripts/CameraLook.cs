using System.Threading;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public float angle_down = 20f;
    public float angle_up = -45f;
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float MouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, angle_up, angle_down);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * MouseX);
    }
}