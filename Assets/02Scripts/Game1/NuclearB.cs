using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuclearB : MonoBehaviour
{
    static bool isFinish = false;

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
       StartCoroutine("Rand1Bomb");
    }

    private void Update()
    {

    }

    private IEnumerator Rand1Bomb()
    {
        //Vector2 position = MoveG1.PlayerG1pos;
        //position.x = Mathf.Round(position.x) + 0.5f;
        //position.y = Mathf.Round(position.y) + 0.5f;

       // UnityEngine.Debug.Log("Rand1Bomb µé¾î¿È");

       // GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
       // UnityEngine.Debug.Log("Bomb 1 instantiate");

        yield return new WaitForSeconds(3.0f);


        Explosion01 explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);

       // UnityEngine.Debug.Log("explosion instantiate");

        Explode(transform.position, Vector2.up, explosionRadius);
        Explode(transform.position, Vector2.down, explosionRadius);
        Explode(transform.position, Vector2.left, explosionRadius);
        Explode(transform.position, Vector2.right, explosionRadius);


      //  UnityEngine.Debug.Log("444");


        Destroy(this);

        isFinish = true;
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
}
