using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private float destructionTime = 1f;

    private void Start()
    {
        Destroy(gameObject,destructionTime); 
    }
}
