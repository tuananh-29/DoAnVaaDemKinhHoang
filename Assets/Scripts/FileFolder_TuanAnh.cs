using UnityEngine;

// Gắn vào TỪNG hồ sơ trong ngăn tủ (File_1, File_2, File_3, File_4...)
public class FileFolder_TuanAnh : MonoBehaviour
{
    [Header("Số thứ tự hồ sơ này")]
    [SerializeField] private int fileNumber;

    [Header("Đúng hồ sơ theo câu đối là số mấy")]
    [SerializeField] private int correctFileNumber = 4;

    [Header("Chìa khóa hiện ra khi lấy đúng hồ sơ")]
    [SerializeField] private GameObject keyItem;

    [Header("Tương tác")]
    [SerializeField] private Transform player;
    [SerializeField] private float interactRange = 2.5f;

    private bool isInteractable = false;
    private bool alreadyTaken = false;

    void Start()
    {
        if (player == null && Camera.main != null)
        {
            player = Camera.main.transform;
        }
        if (keyItem != null) keyItem.SetActive(false);
    }

    void Update()
    {
        if (!isInteractable || alreadyTaken) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= interactRange)
            {
                TakeFile();
            }
        }
    }

    public void SetInteractable(bool value)
    {
        isInteractable = value;
    }

    private void TakeFile()
    {
        alreadyTaken = true;

        if (fileNumber == correctFileNumber)
        {
            Debug.Log($"Lấy đúng hồ sơ số {fileNumber}. Tìm thấy chìa khóa!");
            if (keyItem != null) keyItem.SetActive(true);
        }
        else
        {
            Debug.Log($"Hồ sơ số {fileNumber}: không có gì đặc biệt.");
        }

        gameObject.SetActive(false);
    }
}
