using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] spritesDino;
    public Sprite[] spritesDino2;
    public Sprite[] spritesDino3;
    private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private int frame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        sprites = spritesDino;
        Invoke(nameof(Animate), 0f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Animate()
    {
        frame++;

        if (frame >= sprites.Length)
        {
            frame = 0;
        }

        if (frame >= 0 && frame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[frame];
        }

        Invoke(nameof(Animate), 1f / GameManager.Instance.gameSpeed);
    }

    public void startDinoGame2()
    {
        CancelInvoke();
        sprites = spritesDino2;
        Invoke(nameof(Animate), 0f);
    }

    public void startDinoGame3()
    {
        CancelInvoke();
        sprites = spritesDino3;
        Invoke(nameof(Animate), 0f);
    }
}


