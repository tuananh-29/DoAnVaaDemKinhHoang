using UnityEngine;
using System.Collections;

public class DoorInteract_TuanAnh : MonoBehaviour
{
    [Header("Góc xoay cửa")]
    [SerializeField] private float openAngle = 90f;      // cửa mở bao nhiêu độ
    [SerializeField] private float openSpeed = 2f;        // tốc độ mở/đóng
    [SerializeField] private bool isLocked = false;        // dùng cho cửa cần chìa khóa/mật mã trước

    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private Coroutine rotateCoroutine;

    private void Awake()
    {
        closedRotation = transform.rotation;
        // Xoay quanh trục Y, đổi sang trục khác nếu bản lề cửa không nằm dọc Y
        openRotation = closedRotation * Quaternion.Euler(0f, openAngle, 0f);
    }

    // Gọi hàm này từ script tương tác của player (raycast + phím E)
    public void ToggleDoor()
    {
        if (isLocked)
        {
            Debug.Log("Cửa đang khóa, cần chìa khóa hoặc mật mã trước.");
            return;
        }

        isOpen = !isOpen;

        if (rotateCoroutine != null) StopCoroutine(rotateCoroutine);
        rotateCoroutine = StartCoroutine(RotateDoor(isOpen ? openRotation : closedRotation));
    }

    private IEnumerator RotateDoor(Quaternion targetRotation)
    {
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.5f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
            yield return null;
        }
        transform.rotation = targetRotation;
    }

    // Gọi hàm này khi player nhặt được chìa khóa / giải xong puzzle
    public void Unlock()
    {
        isLocked = false;
    }
}
