using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] public float speedMove = 30;
    [SerializeField] public float defaultSpeed = 30;
    [SerializeField] public float steerMove = 100;
    [SerializeField] public float slowSpeed = 5;

    private void Update()
    {
        float speedAmount = Input.GetAxis("Vertical")* speedMove * Time.deltaTime;
        float steerAmount = Input.GetAxis("Horizontal")* steerMove * Time.deltaTime;

        transform.Translate(0,speedAmount,0);
        transform.Rotate(0,0,-steerAmount);
    }
    public void SetSlowSpeed()
    {
        speedMove=slowSpeed;
    }
    public void SetDefaultSpeed()
    {
        speedMove=defaultSpeed;
    }
}
