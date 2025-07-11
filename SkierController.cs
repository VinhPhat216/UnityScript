using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkierController : MonoBehaviour
{
    [SerializeField]Rigidbody2D playerRb;
    [SerializeField] private float leftTorqueAmount;
    [SerializeField] private float rightTorqueAmount;
    [SerializeField] private float boostSpeed;
    [SerializeField] private float defaultSpeed;
    [SerializeField] private SurfaceEffector2D surfaceEffector2D;
    // Start is called before the first frame update
    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        ChangePlayerSpeed();
    }
    private void ChangePlayerSpeed()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            surfaceEffector2D.speed = boostSpeed;
        }
        else
        {
            surfaceEffector2D.speed = defaultSpeed;
        }
    }
    private void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerRb.AddTorque(leftTorqueAmount);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            playerRb.AddTorque(rightTorqueAmount);
        }
    }
}
