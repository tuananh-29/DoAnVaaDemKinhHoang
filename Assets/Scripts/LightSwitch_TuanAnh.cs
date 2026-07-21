using System.Collections;
using UnityEngine;

// Gắn script này vào TỪNG công tắc riêng (3 object: SwitchA, SwitchB, SwitchC)
// Mỗi công tắc cần biết mình là số thứ tự mấy trong chuỗi (order)
public class LightSwitch_TuanAnh : MonoBehaviour
{
    [Header("Vị trí của công tắc này trong chuỗi đúng (0 = A, 1 = B, 2 = C)")]
    [SerializeField] private int mySwitchIndex;

    [Header("Tham chiếu tới bộ quản lý puzzle chung")]
    [SerializeField] private SwitchPuzzleManager_TuanAnh puzzleManager;

    [Header("Tương tác")]
    [SerializeField] private Transform player;
    [SerializeField] private float interactRange = 2.5f;

    private bool alreadyPressed = false;

    void Start()
    {
        if (player == null && Camera.main != null)
        {
            player = Camera.main.transform;
        }
        if (puzzleManager == null)
        {
            puzzleManager = FindObjectOfType<SwitchPuzzleManager_TuanAnh>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !alreadyPressed)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= interactRange)
            {
                PressSwitch();
            }
        }
    }

    private void PressSwitch()
    {
        alreadyPressed = true;
        puzzleManager.OnSwitchPressed(mySwitchIndex, this);
    }

    // Gọi từ Manager khi cần reset công tắc này (sau khi bấm sai)
    public void ResetSwitch()
    {
        alreadyPressed = false;
    }
}
