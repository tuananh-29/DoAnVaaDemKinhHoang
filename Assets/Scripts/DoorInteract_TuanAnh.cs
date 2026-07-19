using System.Collections;
using UnityEngine;

public class DoorInteract_TuanAnh : MonoBehaviour
{
    [Header("Cài đặt Cửa")]
    [SerializeField] float openAngle = 90f;
    [SerializeField] float openSpeed = 2f;
    [SerializeField] bool isOpen = false;

    [Header("Liên kết cửa đôi (Bỏ trống nếu là cửa đơn)")]
    [SerializeField] DoorInteract_TuanAnh linkedDoor; // Kéo cánh cửa còn lại vào đây

    private Quaternion _closedRotation;
    private Quaternion _openRotation;
    private Coroutine _currentCoroutine;

    void Start()
    {
        _closedRotation = transform.localRotation;
        _openRotation = _closedRotation * Quaternion.Euler(0, openAngle, 0);
    }

    // Player gọi hàm này khi bấm E vào cánh cửa này
    public void Interact()
    {
        // 1. Mở cánh cửa hiện tại (cánh bị Raycast bắn trúng)
        ToggleSingleDoor();

        // 2. Mở cánh cửa được liên kết (nếu có)
        if (linkedDoor != null)
        {
            linkedDoor.ToggleSingleDoor();
        }
    }

    // Hàm này chỉ thực hiện mở 1 cánh (tách riêng để không bị lặp vô tận khi 2 cửa gọi nhau)
    public void ToggleSingleDoor()
    {
        if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(ToggleDoorCoroutine());
    }

    private IEnumerator ToggleDoorCoroutine()
    {
        Quaternion targetRotation = isOpen ? _closedRotation : _openRotation;
        Quaternion startRotation = transform.localRotation;
        isOpen = !isOpen;

        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime * openSpeed;
            transform.localRotation = Quaternion.Slerp(startRotation, targetRotation, time);
            yield return null;
        }
        
        transform.localRotation = targetRotation;
    }
}