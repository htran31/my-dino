using UnityEngine;

public class FallingBackground : MonoBehaviour
{
    public float fallSpeed = 2.5f;
    private bool shouldFall = false;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }
    void Update()
    {
        if (shouldFall)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

            if (transform.position.y < -25f)
            {
                shouldFall = false;
                // gameObject.SetActive(false);
            }
        }
    }

    public void StartFalling()
    {
        shouldFall = true;
    }

    public void ResetBackground()
    {
        shouldFall = false;
        transform.position = initialPosition;
        gameObject.SetActive(true);
    }
}
