using UnityEngine;

public class CarCameraFollow : MonoBehaviour
{
    [SerializeField]GameObject myCar;
    [SerializeField] Vector3 offSet = new Vector3(0,0,-10);
    private void Update()
    {
        if (myCar !=null)
        {
            transform.position = myCar.transform.position+offSet;
        }
    }
}
