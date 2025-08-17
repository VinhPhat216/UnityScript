using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private Vector2 direction = Vector2.down;//(0,-1)
    [SerializeField] private float speed;
    [SerializeField] private KeyCode inputUp=KeyCode.W;
    [SerializeField] private KeyCode inputDown=KeyCode.S;
    [SerializeField] private KeyCode inputRight=KeyCode.D;
    [SerializeField] private KeyCode inputLeft=KeyCode.A;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        Vector2 position = playerRb.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;
        playerRb.MovePosition( position + translation );
    }

    private void Move()
    {
        if (Input.GetKey(inputUp))
        {
            SetDirection(Vector2.up);
        }
        else if (Input.GetKey(inputDown))
        {
            SetDirection(Vector2.down);
        }
        else if (Input.GetKey(inputRight))
        {
            SetDirection(Vector2.right);
        }
        else if (Input.GetKey(inputLeft))
        {
            SetDirection(Vector2.left);
        }
        else //not moving
        {
            SetDirection(Vector2.zero);
        }
    }

    private void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }
}
