using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 4f;
    public float rotationSpeed = 10f;
    public float gravity = -9.81f;

    [Header("References")]
    public Transform cameraTransform; // Kéo Main Camera vào đây, để trống thì tự tìm Camera.main

    private CharacterController controller;
    private Animator animator;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // Lấy input WASD / mũi tên
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(horizontal, 0f, vertical).normalized;

        Vector3 moveDir = Vector3.zero;

        if (inputDir.magnitude >= 0.1f)
        {
            // Tính góc di chuyển theo hướng camera (kiểu 3rd person chuẩn)
            float camYaw = cameraTransform != null ? cameraTransform.eulerAngles.y : 0f;
            float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + camYaw;

            // Xoay nhân vật mượt theo hướng di chuyển
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        // Di chuyển
        controller.Move(moveDir * moveSpeed * Time.deltaTime);

        // Trọng lực đơn giản để nhân vật không lơ lửng
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Cập nhật Animator
        float currentSpeed = inputDir.magnitude * moveSpeed;
        animator.SetFloat("Speed", currentSpeed);
    }
}
