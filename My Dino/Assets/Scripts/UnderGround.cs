using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class UnderGround : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private float initialAngle = 0f;
    private float targetAngle = 20f;
    private float rotationSpeed = 3f;
    private float currentRotation;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        currentRotation = initialAngle;
    }

    private void Update()
    {
        if (GameManager.Instance == null || GameManager.Instance.gameOver || !GameManager.Instance.enabled)
            return;


        float speed = GameManager.Instance.gameSpeed / transform.localScale.x;
        meshRenderer.material.mainTextureOffset += speed * Time.deltaTime * Vector2.right;
        // if (currentRotation != targetAngle)
        // {
        //     currentRotation = Mathf.MoveTowards(currentRotation, targetAngle, rotationSpeed * Time.deltaTime);
        //     transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);
        // }
    }

    public void Rotate()
    {
        if (GameManager.Instance.gameOver) return; // remove?

        if (currentRotation != targetAngle)
        {
            currentRotation = Mathf.MoveTowards(currentRotation, targetAngle, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);
            // transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    public void ResetRotation()
    {
        if (GameManager.Instance.gameOver) return; // remove?


        if (currentRotation != initialAngle)
        {
            currentRotation = Mathf.MoveTowards(currentRotation, initialAngle, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);
            // transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}
