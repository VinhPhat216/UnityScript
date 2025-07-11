using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnowBoarderManager : MonoBehaviour
{
    [SerializeField] private float delayLoadScene;
    [SerializeField] private string nameScene = "SampleScene";
    public void DelayLoadScene()
    {
        SceneManager.LoadScene(nameScene);
    }
    public void ReLoadScene()
    {
        Invoke(nameof(DelayLoadScene), delayLoadScene);
    }
}
