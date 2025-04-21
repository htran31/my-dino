using UnityEngine;

public class BigDino : MonoBehaviour
{
    public Vector3 startPosition = new Vector3(-9f, -19.19f, 0f);
    public Vector3 targetPosition = new Vector3(-6f, -19.19f, 0f);
    public float moveSpeed = 1f;
    private bool isMoving = true;

    void Start()
    {
        transform.position = startPosition;
    }

    void Update()
    {
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
    }
}
