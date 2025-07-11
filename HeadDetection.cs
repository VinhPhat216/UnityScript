using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HeadDetection : MonoBehaviour
{
    [SerializeField] private AudioSource audioHeadDetect;
    [SerializeField] private ParticleSystem headEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Debug.Log("Hit my head");
            headEffect.Play();
            audioHeadDetect.Play();
            FindFirstObjectByType<SnowBoarderManager>()?.ReLoadScene();
        }
    }
}
