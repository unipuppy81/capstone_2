using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class BombG1 : MonoBehaviour
{
    [Header("Bomb")]
    public GameObject bombPrefab;
    public KeyCode inputKey = KeyCode.Space;
    public float bombFuseTime = 3f;

    [Header("Explosion")]
    public Explosion01 explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;

    public void Awake()
    {
        
    }

    public void Start()
    {
        ///StartCoroutine("RandBomb");
    }

    private void Update()
    {

    }

    private IEnumerator RandBomb()
    {
        Vector2 position = MoveG1.PlayerG1pos;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        UnityEngine.Debug.Log("RandBomb µé¾î¿È");

        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        UnityEngine.Debug.Log("Bomb instantiate");

        yield return new WaitForSeconds(3.0f);


        Explosion01 explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);

        UnityEngine.Debug.Log("explosion instantiate");

        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);


        UnityEngine.Debug.Log("444");


        Destroy(bomb);

    }



    private IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        //bombsRemaining--;

        yield return new WaitForSeconds(bombFuseTime); 

        position = bomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);


        Explosion01 explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);
        
        Destroy(bomb);
        //bombsRemaining++;
    }

    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0)
        {
            return;
        }

        position += direction;
        
        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))
        {
            //ClearDestructible(position);
            return;
        }
        
        Explosion01 explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, direction, length - 1);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;
        }
    }
}