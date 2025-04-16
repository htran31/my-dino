using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] spritesDino;
    private SpriteRenderer spriteRenderer;
    private int frame;
    public Sprite[] spritesTrex;
    private Sprite[] sprites;

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
    public void startAnimTrex()
    {
        CancelInvoke();
        sprites = spritesTrex;
        Invoke(nameof(Animate), 0f);
    }
}


