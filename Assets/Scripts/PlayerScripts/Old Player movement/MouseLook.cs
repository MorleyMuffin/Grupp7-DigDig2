using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // Configurable parameters
    [SerializeField] float mouseSensitivity = 1500f;

    // Private variables
    float xRotation = 0f;

    // Cached references
    Transform playerBody;

    void Awake()
    {
        playerBody = transform.parent.transform;
    }

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void Update()
    {
        Look();
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}