using UnityEngine;
using System.Collections;
using Unity.Mathematics;

public class BombController : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private KeyCode inputKey = KeyCode.Space;
    [SerializeField] private float bombFuseTime = 2.5f;
    [SerializeField] private int bombAmount = 1;
    
    private int bombsRemaining;

    private void OnEnable()
    {
        bombsRemaining = bombAmount;
    }

    private void Update()
    {
        if (bombsRemaining > 0&& Input.GetKeyDown(inputKey))
        {
            StartCoroutine(PlaceBomb());
        }
    }

    private IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        GameObject bomb = Instantiate(bombPrefab,position,transform.rotation);
        bombsRemaining--;

        yield return new WaitForSeconds(bombFuseTime);

        Destroy(bomb);
        bombsRemaining++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer==LayerMask.NameToLayer("Bomb"))
        {
            collision.isTrigger = false;
        }
    }
}
