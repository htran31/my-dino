using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float leftEdge;
    public float moveSpeedMultiplier = 1f;

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
        if (CompareTag("Seaweed"))
        {
            moveSpeedMultiplier = 0.3f; // Move at 30% speed
        }
    }

    private void Update()
    {
        transform.position += GameManager.Instance.gameSpeed * moveSpeedMultiplier * Time.deltaTime * Vector3.left;

        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }
}