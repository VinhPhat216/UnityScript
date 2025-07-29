using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float BulletSpeed;
    [SerializeField] private Vector2 BulletDirection;
    [SerializeField] private Rigidbody2D bulletRb;
    [SerializeField] private GameObject impactEffect;

    private void Start()
    {
        if (bulletRb==null)
        {
            bulletRb = GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        BulletVelocity();
    }

    private void BulletVelocity()
    {
        bulletRb.linearVelocity = BulletDirection * BulletSpeed;
    }

    public void SetDirection(Vector2 newDirection)
    {
        BulletDirection = newDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }
}
