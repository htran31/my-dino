using UnityEngine;

public class FallingBackground : MonoBehaviour
{
    private Vector3 initialPosition;
    public float fallSpeed = 2.5f;
    private bool shouldFall = false;
    private bool shouldFade = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        initialPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        // if (shouldFall)
        // {
        //     transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        //     if (transform.position.y < -25f)
        //     {
        //         shouldFall = false;
        //         // gameObject.SetActive(false);
        //     }
        // }

        if (shouldFade)
        {
            // Làm mờ dần đối tượng
            Color color = spriteRenderer.color;
            color.a -= Time.deltaTime * 0.6f; // tốc độ mờ dần
            color.a = Mathf.Clamp01(color.a); // giữ trong khoảng [0,1]
            spriteRenderer.color = color;

            if (color.a <= 0f)
            {
                shouldFade = false;
            }
        }
    }

    public void StartFalling()
    {
        // shouldFall = true;
        shouldFade = true;
    }

    public void ResetBackground()
    {
        // shouldFall = false;
        shouldFade = false;
        transform.position = initialPosition;

        // reset độ trong suốt về 1 (hiện rõ lại)
        Color color = spriteRenderer.color;
        color.a = 1f;
        spriteRenderer.color = color;

        gameObject.SetActive(true);
    }
}
