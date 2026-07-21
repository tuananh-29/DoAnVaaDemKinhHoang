using UnityEngine;

// Gắn vào object bàn phím / mặt trước máy tính số 1
public class ComputerKeyboardHint_TuanAnh : MonoBehaviour
{
    [Header("Tham chiếu tới script USB phía sau màn hình cùng máy tính này")]
    [SerializeField] private ComputerUSB_TuanAnh backOfScreen;

    [Header("Tương tác")]
    [SerializeField] private Transform player;
    [SerializeField] private float interactRange = 2f;

    void Start()
    {
        if (player == null && Camera.main != null)
        {
            player = Camera.main.transform;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= interactRange && backOfScreen != null)
            {
                backOfScreen.ShowKeyboardHint();
            }
        }
    }
}
