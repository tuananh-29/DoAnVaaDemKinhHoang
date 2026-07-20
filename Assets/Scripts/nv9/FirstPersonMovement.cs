using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class FirstPersonMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 4f;
    public float gravity = -9.81f;

    [Header("Mouse Look Settings")]
    public Transform cameraTransform; // Kéo Main Camera (đã gắn làm con nhân vật) vào đây
    public float mouseSensitivity = 2f;
    public float minPitch = -80f;
    public float maxPitch = 80f;

    private CharacterController controller;
    private Animator animator;
    private Vector3 velocity;
    private float pitch = 0f; // Góc nhìn lên/xuống của camera

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // Khóa và ẩn con trỏ chuột giữa màn hình
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Xoay thân nhân vật trái/phải theo chuột ngang
        transform.Rotate(Vector3.up * mouseX);

        // Xoay camera lên/xuống theo chuột dọc, giới hạn góc
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        if (cameraTransform != null)
            cameraTransform.localEulerAngles = new Vector3(pitch, 0f, 0f);
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Di chuyển theo hướng nhân vật đang nhìn (forward/right của chính nhân vật)
        Vector3 moveDir = (transform.right * horizontal + transform.forward * vertical).normalized;

        controller.Move(moveDir * moveSpeed * Time.deltaTime);

        // Trọng lực
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Cập nhật Animator
        float currentSpeed = moveDir.magnitude * moveSpeed;
        animator.SetFloat("Speed", currentSpeed);
    }
}
