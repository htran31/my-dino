using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
public class Ground : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private GameManager gameManager;
    public float moveSpeed = 12f;
    private bool isMovingOut = false;
    private bool canScroll = true;
    private Coroutine moveOutCoroutine;
    private Vector3 initialPosition;
    private Vector2 initialTextureOffset;

    public GameObject prefab;
    private GameObject obj;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        initialPosition = transform.position;
        initialTextureOffset = meshRenderer.material.mainTextureOffset;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        if (canScroll)
        {
            float speed = GameManager.Instance.gameSpeed / transform.localScale.x;
            meshRenderer.material.mainTextureOffset += speed * Time.deltaTime * Vector2.right;
        }

        if (gameManager.score >= 80 && !isMovingOut)
        {
            StartMovingOut();
        }

        if (GameManager.Instance.gameOver) StopAllCoroutines();
    }

    private void StartMovingOut()
    {
        isMovingOut = true;
        canScroll = false;
        moveOutCoroutine = StartCoroutine(MoveOutRoutine());
    }

    private IEnumerator MoveOutRoutine()
    {
        // Vector3 targetPosition = transform.position + Vector3.left * 30f; // Move left out of view
        Vector3 start = transform.position;
        Vector3 end = start + Vector3.left * 30f;
        obj = Instantiate(prefab, new Vector3(11f, 0, 0), Quaternion.identity, gameObject.transform);
        //obj.transform.position = new Vector3(0.45f, 0, 0);
        float duration = 5f; // Total time to move out
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(start, end, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = end;
        // while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        // {
        //     transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        //     yield return null;
        // }
    }

    public void ResetGround()
    {
        // Stop any ongoing move animation
        if (moveOutCoroutine != null)
        {
            StopCoroutine(moveOutCoroutine);
        }
        if (obj != null)
        {
            Destroy(obj);
        }

        transform.position = initialPosition;
        meshRenderer.enabled = true;
        meshRenderer.material.mainTextureOffset = Vector2.zero;

        isMovingOut = false;
        canScroll = true;
    }

    public void EnableScroll()
    {
        canScroll = true;
    }

    public void DisableScroll()
    {
        canScroll = false;
    }

}
