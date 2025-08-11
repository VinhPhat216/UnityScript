using UnityEngine;
using System.Collections;
using Unity.Mathematics;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private KeyCode inputKey = KeyCode.Space;
    [SerializeField] private float bombFuseTime = 2.5f;
    [SerializeField] private int bombAmount = 1;
    private int bombsRemaining;

    [Header("Explosion")]
    [SerializeField] private Explosion explosionPrefab;
    [SerializeField] private LayerMask explosionLayerMask;
    [SerializeField] private float explosionDuration = 1f;
    [SerializeField] private int explosionRadius = 1;

    [Header("Destructible")]
    [SerializeField] private Destructible destructiblePrefab;
    [SerializeField] private Tilemap destructibleTiles;
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

        position = bomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        Explosion explosion = Instantiate(explosionPrefab,position,transform.rotation);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);
        Destroy(explosion.gameObject,explosionDuration);

        Explode(position,Vector2.up,explosionRadius);
        Explode(position,Vector2.down,explosionRadius);
        Explode(position,Vector2.left,explosionRadius);
        Explode(position,Vector2.right,explosionRadius);

        Destroy(bomb);
        bombsRemaining++;
    }

    private void Explode(Vector2 position,Vector2 direction,int length)
    {
        if (length <= 0 )
        {
            return;
        }
        position += direction;

        if (Physics2D.OverlapBox(position,Vector2.one/2f,0f,explosionLayerMask))
        {
            ClearDestructible(position);
            return;
        }

        Explosion explosion = Instantiate(explosionPrefab, position, transform.rotation);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDuration);

        Explode(position,direction,length - 1);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer==LayerMask.NameToLayer("Bomb"))
        {
            collision.isTrigger = false;
        }
    }

    private void ClearDestructible(Vector2 position)
    {
        Vector3Int cell = destructibleTiles.WorldToCell(position);
        TileBase tile = destructibleTiles.GetTile(cell);

        if (tile != null)
        {
            Instantiate(destructiblePrefab,position,Quaternion.identity);
            destructibleTiles.SetTile(cell,null);
        }
    }

}
