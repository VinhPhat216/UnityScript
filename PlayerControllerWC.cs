using UnityEngine;

public class PlayerControllerWC : MonoBehaviour
{
    [SerializeField] private GameObject goStanding;
    [SerializeField] private GameObject goBall;
    [SerializeField] private BulletController bulletPrefab;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private LayerMask layerToCheck;
    [SerializeField] private Transform groundPoint;
    [SerializeField] private Animator animStandingState;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float becomeBallTime;
    [SerializeField] private SpriteRenderer playerSR;
    [SerializeField] private SpriteRenderer playerDashEffectSR;
    [SerializeField] private float dashEffectLifeTime;
    [SerializeField] private float timeBetweenEachDashEffect;
    [SerializeField] private float dashCoolDownTime;

    private float becomeBallCounter;
    private float dashCoolDownCounter;
    private float dashEffectCounter;
    private float dashCounter;
    private bool isOnGround;
    private bool isDoubleJump;

    private int speedParam = Animator.StringToHash("speed");
    private int isOnGroundParam = Animator.StringToHash("isOnGround");
    private int shotParam = Animator.StringToHash("shot");
    private int doubleJumpParam = Animator.StringToHash("doubleJump");

    void Start()
    {

    }

    void Update()
    {
        Dash();
        Jump();
        Shoot();
    }
    private void Dash()
    {
        if (dashCoolDownCounter > 0)
        {
            dashCoolDownCounter -= Time.deltaTime;
        }
        else
        {
            if (Input.GetButtonDown("Fire2"))
            {
                dashCounter = dashTime;
                ShowDashEffect();
            }
        }
        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            playerRb.linearVelocity = new Vector2(dashSpeed * transform.localScale.x, playerRb.linearVelocity.y);
            dashEffectCounter -= Time.deltaTime;
            if (dashEffectCounter <= 0)
            {
                ShowDashEffect();
            }
            dashCoolDownCounter = dashCoolDownTime;
        }
        else
        {
            Move();
        }
    }

    private void Move()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        playerRb.linearVelocity = new Vector2(xAxis * moveSpeed, playerRb.linearVelocity.y);

        if (playerRb.linearVelocityX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (playerRb.linearVelocityX > 0)
        {
            transform.localScale = Vector3.one;
        }
        animStandingState.SetFloat(speedParam, Mathf.Abs(playerRb.linearVelocityX));
    }

    private void Jump()
    {
        isOnGround = Physics2D.OverlapCircle(groundPoint.position, 0.2f, layerToCheck);
        if (Input.GetButtonDown("Jump") && (isOnGround || isDoubleJump))
        {
            if (isOnGround)
            {
                isDoubleJump = true;
            }
            else
            {
                animStandingState.SetTrigger(doubleJumpParam);
                isDoubleJump = false;
            }

            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, jumpForce);
        }
        animStandingState.SetBool(isOnGroundParam, isOnGround);
    }

    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            BulletController bullet = Instantiate(bulletPrefab, shootPosition.position, shootPosition.rotation);
            bullet.SetDirection(new Vector2(transform.localScale.x, 0));
            animStandingState.SetTrigger(shotParam);
        }
    }
    private void ShowDashEffect()
    {
        SpriteRenderer spriteRenderer = Instantiate(playerDashEffectSR, transform.position, transform.rotation);
        spriteRenderer.sprite = playerSR.sprite;
        spriteRenderer.transform.localScale = transform.localScale;
        Destroy(spriteRenderer.gameObject, dashEffectLifeTime);
        dashEffectCounter = timeBetweenEachDashEffect;
    }
}
