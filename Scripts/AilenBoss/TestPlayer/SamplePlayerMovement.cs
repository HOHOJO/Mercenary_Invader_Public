using UnityEngine;

public class SamplePlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 700f;
    public float lookSpeed = 2f;

    private float verticalRotation = 0f;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * mouseX * rotationSpeed * Time.deltaTime);

        float mouseY = Input.GetAxis("Mouse Y");
        verticalRotation -= mouseY * lookSpeed;
        verticalRotation = Mathf.Clamp(verticalRotation, -80f, 80f);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0f, vertical) * speed * Time.deltaTime;

        movement = Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * movement;

        characterController.Move(movement);
    }
}
