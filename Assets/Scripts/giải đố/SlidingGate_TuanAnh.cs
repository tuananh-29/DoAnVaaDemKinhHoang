using UnityEngine;
using System.Collections;

public class SlidingGate_TuanAnh : MonoBehaviour
{
    [Header("Khoảng cách trượt sang phải (mét)")]
    [SerializeField] private float slideDistance = 2f;

    [Header("Tốc độ trượt")]
    [SerializeField] private float slideSpeed = 2f;

    [Header("Khóa (chỉ mở được khi Unlock() được gọi)")]
    [SerializeField] private bool isLocked = true;

    private bool isOpen = false;
    private Vector3 closedPosition;
    private Vector3 openPosition;
    private Coroutine slideCoroutine;

    void Awake()
    {
        closedPosition = transform.position;
        // Trượt sang phải theo trục X cục bộ của chính cổng (đúng hướng "phải" của object này,
        // không phải trục X toàn cục — quan trọng nếu cổng bị xoay theo hướng khác trong scene)
        openPosition = closedPosition + transform.right * slideDistance;
    }

    // Gọi hàm này khi player nhập đúng mã (từ KeypadDoor_TuanAnh)
    public void ToggleGate()
    {
        if (isLocked)
        {
            Debug.Log("Cổng đang khóa, cần nhập đúng mã trước.");
            return;
        }

        isOpen = !isOpen;

        if (slideCoroutine != null) StopCoroutine(slideCoroutine);
        slideCoroutine = StartCoroutine(SlideGate(isOpen ? openPosition : closedPosition));
    }

    private IEnumerator SlideGate(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * slideSpeed);
            yield return null;
        }
        transform.position = targetPosition;
    }

    // Gọi từ KeypadDoor_TuanAnh khi nhập đúng mã
    public void Unlock()
    {
        isLocked = false;
    }
}
