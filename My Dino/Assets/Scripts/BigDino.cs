using UnityEngine;
using System.Collections;

public class BigDino : MonoBehaviour
{
    public Vector3 startPosition = new Vector3(-9f, -19f, 0f);
    public Vector3 targetPosition = new Vector3(-6f, -19f, 0f);
    public float moveSpeed = 1f;
    private bool isMoving = true;

    private Vector3 newStartPosition;

    void Start()
    {
        transform.position = startPosition;
    }

    void Update()
    {
        if (GameManager.Instance.gameOver)
        {
            // Stop everything when game is over
            StopAllCoroutines();
            isMoving = false;
            return;
        }

        if (isMoving)
        {
            // Di chuyển BigDino đến targetPosition
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Khi đến nơi thì dừng lại
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }

        if (GameManager.Instance.score >= 330 && GameManager.Instance.score < 530 && !isMoving)
        {
            StartCoroutine(MoveRoutine());
        }
    }

    IEnumerator MoveRoutine()
    {
        newStartPosition = targetPosition;
        if (!isMoving)
        {
            // 1. Di chuyển sang phải tới một vị trí ngẫu nhiên X từ -6f đến -2f
            float randomX = Random.Range(-6f, 2f);
            Vector3 randomTarget = new Vector3(randomX, newStartPosition.y, newStartPosition.z);
            yield return StartCoroutine(MoveToPosition(randomTarget));

            // 2. Quay về vị trí ban đầu
            yield return StartCoroutine(MoveToPosition(newStartPosition));
        }

        isMoving = true;
    }

    IEnumerator MoveToPosition(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Đảm bảo vị trí chính xác
        transform.position = target;
    }

    // public void StartMovingToRandomTarget()
    // {
    //     float randomX = Random.Range(-6f, -2f); // vị trí đích ngẫu nhiên trong khoảng
    //     targetPosition = new Vector3(randomX, startPosition.y, 0f);

    //     transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);


    //     transform.position = startPosition;
    //     isMoving = true;
    //     gameObject.SetActive(true); // đảm bảo BigDino xuất hiện
    // }
}
