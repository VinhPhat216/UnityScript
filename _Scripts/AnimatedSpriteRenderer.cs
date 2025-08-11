using UnityEngine;

public class AnimatedSpriteRenderer : MonoBehaviour
{
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite[] animationSprites;
    [SerializeField] private float animationTime = 0.25f;
    [SerializeField] private bool loop = true;

    private SpriteRenderer spriteRenderer;
    private int animationFrame;
    public bool idle = true;

    private void Awake()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }

    private void Start()
    {
        InvokeRepeating(nameof(NexFrame), animationTime, animationTime);
    }

    private void NexFrame()
    {
        animationFrame++;

        if (loop && animationFrame >=animationSprites.Length)
        {
            animationFrame = 0;
        }
        if (idle)
        {
            spriteRenderer.sprite = idleSprite;
        }
        else if (animationFrame >= 0 && animationFrame < animationSprites.Length)
        {
            spriteRenderer.sprite = animationSprites[animationFrame];
        }
    }
}
