using UnityEngine;

public class ColliderDetection : MonoBehaviour
{
    [SerializeField]private bool hasPackage = false;
    [SerializeField]private CarController carController;
    [SerializeField]private SpriteRenderer carColor;
    [SerializeField]private Color hasPackageColor=Color.red; 
    [SerializeField]private Color noPackageColor=Color.purple;

    private void Start()
    {
        carController = GetComponent<CarController>();
        carColor = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
        //collision is game object player touch.
    {
        if (collision.gameObject.CompareTag("Package")&& hasPackage ==false)
        {
            Debug.Log("Pickup Package");
            hasPackage = true;
            Destroy(collision.gameObject,2);
            carColor.color=hasPackageColor;
        }
        if(collision.gameObject.CompareTag("Customer")&& hasPackage ==true)
        {
            Debug.Log("Package Delivered");
            hasPackage = false;
            Destroy(collision.gameObject, 2);
            carColor.color = noPackageColor;
        }
        if (collision.gameObject.CompareTag("Slow Speed"))
        {
            carController.SetSlowSpeed();
            Destroy(collision.gameObject, 2);
        }
        if (collision.gameObject.CompareTag("Default Speed")&& carController.speedMove == carController.slowSpeed)
        {
            carController.SetDefaultSpeed();
            Destroy(collision.gameObject, 2);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
        //game object is player
    {
        if (collision.gameObject.CompareTag("House")|| collision.gameObject.CompareTag("Rock"))
        {
            Destroy(gameObject);
        }
    }
}
