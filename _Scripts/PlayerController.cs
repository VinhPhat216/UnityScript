using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D PlayerRb;
    [SerializeField] private Vector2 direction = Vector2.down;
    public float moveSpeed;

    [SerializeField] private KeyCode inputDown = KeyCode.S;
    [SerializeField] private KeyCode inputUp = KeyCode.W;
    [SerializeField] private KeyCode inputLeft = KeyCode.A;
    [SerializeField] private KeyCode inputRight = KeyCode.D;

    [SerializeField] private AnimatedSpriteRenderer spriteRendererUp; 
    [SerializeField] private AnimatedSpriteRenderer spriteRendererDown; 
    [SerializeField] private AnimatedSpriteRenderer spriteRendererLeft; 
    [SerializeField] private AnimatedSpriteRenderer spriteRendererRight;

    private AnimatedSpriteRenderer activeSpriteRenderer;

    private void Awake()
    {
        if (PlayerRb == null)
        {
            PlayerRb = GetComponent<Rigidbody2D>();
        }
        activeSpriteRenderer = spriteRendererDown;
    }

    private void Update()
    {
        //Move.
        if (Input.GetKey(inputDown))
        {
            SetDirection(Vector2.down,spriteRendererDown);
        }
        else if (Input.GetKey(inputUp))
        {
            SetDirection(Vector2.up,spriteRendererUp);
        }
        else if (Input.GetKey(inputLeft))
        {
            SetDirection(Vector2.left,spriteRendererLeft);
        }
        else if (Input.GetKey(inputRight))
        {
            SetDirection(Vector2.right,spriteRendererRight);
        }
        else
        {
            SetDirection(Vector2.zero,activeSpriteRenderer);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = PlayerRb.position;
        Vector2 transform = direction * moveSpeed * Time.deltaTime;

        PlayerRb.MovePosition(position+transform);
    }

    private void SetDirection(Vector2 newDirection , AnimatedSpriteRenderer spriteRenderer)
    {
        direction = newDirection;

        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

        activeSpriteRenderer = spriteRenderer;
        activeSpriteRenderer.idle = direction == Vector2.zero;
    }
}
