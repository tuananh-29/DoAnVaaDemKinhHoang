using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimpleTestMove_TuanAnh : MonoBehaviour
{
    [Header("Di chuyển")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Nhìn quanh (chuột)")]
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private Transform cameraTransform; // kéo Main Camera (con của capsule) vào đây

    private CharacterController controller;
    private float verticalRotation = 0f;
    private float verticalVelocity = 0f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        // Nếu quên kéo camera vào Inspector, tự tìm camera con
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Xoay thân capsule theo trục ngang
        transform.Rotate(Vector3.up * mouseX);

        // Xoay camera lên xuống, giới hạn góc để không bị lộn ngược
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -80f, 80f);
        if (cameraTransform != null)
        {
            cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }
    }

    private void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = transform.right * h + transform.forward * v;

        // Trọng lực đơn giản để capsule không lơ lửng khi đi qua dốc/bậc thang
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }
        verticalVelocity += gravity * Time.deltaTime;
        move.y = verticalVelocity;

        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    // Bấm ESC để mở khóa chuột khi cần thoát test
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
