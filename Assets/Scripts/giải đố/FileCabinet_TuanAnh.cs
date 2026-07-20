using UnityEngine;

// Gắn vào TỪNG ngăn tủ (Cabinet_1, Cabinet_2, Cabinet_3...)
public class FileCabinet_TuanAnh : MonoBehaviour
{
    [Header("Số thứ tự ngăn tủ này")]
    [SerializeField] private int cabinetNumber;

    [Header("Đúng ngăn tủ theo câu đối là số mấy")]
    [SerializeField] private int correctCabinetNumber = 2;

    [Header("Các hồ sơ trong ngăn tủ này (chỉ bật tương tác nếu mở đúng ngăn)")]
    [SerializeField] private FileFolder_TuanAnh[] filesInCabinet;

    [Header("Tương tác")]
    [SerializeField] private Transform player;
    [SerializeField] private float interactRange = 2.5f;

    private bool cabinetOpened = false;

    void Start()
    {
        if (player == null && Camera.main != null)
        {
            player = Camera.main.transform;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !cabinetOpened)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= interactRange)
            {
                OpenCabinet();
            }
        }
    }

    private void OpenCabinet()
    {
        if (cabinetNumber == correctCabinetNumber)
        {
            cabinetOpened = true;
            foreach (var file in filesInCabinet)
            {
                if (file != null) file.SetInteractable(true);
            }
            Debug.Log($"Ngăn tủ {cabinetNumber}: mở ra, có hồ sơ bên trong.");
        }
        else
        {
            Debug.Log($"Ngăn tủ {cabinetNumber}: bị khóa hoặc trống rỗng.");
        }
    }
}
