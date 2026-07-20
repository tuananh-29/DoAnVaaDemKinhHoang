using UnityEngine;

// Gắn vào TỪNG giá sách (BookShelf_1, BookShelf_2, BookShelf_3)
public class BookShelf_TuanAnh : MonoBehaviour
{
    [Header("Số thứ tự giá sách này (1, 2 hoặc 3)")]
    [SerializeField] private int shelfNumber;

    [Header("Đúng giá sách theo câu đối là số mấy")]
    [SerializeField] private int correctShelfNumber = 3;

    [Header("Các quyển sách trong giá này (chỉ hiện tương tác được nếu đúng giá)")]
    [SerializeField] private BookItem_TuanAnh[] booksInShelf;

    [Header("Tương tác")]
    [SerializeField] private Transform player;
    [SerializeField] private float interactRange = 2.5f;

    private bool booksRevealed = false;

    void Start()
    {
        if (player == null && Camera.main != null)
        {
            player = Camera.main.transform;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !booksRevealed)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= interactRange)
            {
                InspectShelf();
            }
        }
    }

    private void InspectShelf()
    {
        if (shelfNumber == correctShelfNumber)
        {
            booksRevealed = true;
            // Cho phép các sách trong giá này được tương tác
            foreach (var book in booksInShelf)
            {
                if (book != null) book.SetInteractable(true);
            }
            Debug.Log($"Giá sách {shelfNumber}: đây đúng là giá cần tìm, sách hiện ra để lấy.");
        }
        else
        {
            Debug.Log($"Giá sách {shelfNumber}: không có gì đặc biệt ở đây.");
        }
    }
}
