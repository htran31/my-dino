using UnityEngine;

public class FallingObstacle : MonoBehaviour
{
    private float bottomEdge;

    [SerializeField] private float fallSpeedMultiplier = 1f; 

    private void Start()
    {
        bottomEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).y - 2f;
    }

    private void Update()
    {
        transform.position += GameManager.Instance.gameSpeed * fallSpeedMultiplier * Time.deltaTime * Vector3.down;

        if (transform.position.y < bottomEdge)
        {
            Destroy(gameObject);
        }
    }
}