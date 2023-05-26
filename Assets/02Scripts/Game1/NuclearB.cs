using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuclearB : MonoBehaviour
{
    MoveG1 moveG1;
    static bool isFinish = false;

    [Header("Bomb")]
    public GameObject bombPrefab;
    public KeyCode inputKey = KeyCode.Space;
    public float bombFuseTime;

    [Header("Explosion")]
    public Explosion01 explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration;
    public int explosionRadius = 1;
    public AudioSource audioSrc;

    public void Awake()
    {
        moveG1 = GameObject.Find("Player").GetComponent<MoveG1>();
        bombFuseTime = 3.0f;

    }

    public void Start()
    {
        explosionDuration = 1;
        StartCoroutine("Rand1Bomb");
        /*
        if(moveG1.gameTime >= 0.0f && moveG1.gameTime <= 10.0f) { 
        StartCoroutine("Rand1Bomb");
        }
        else if (moveG1.gameTime >= 10.0f && moveG1.gameTime <= 20.0f)
        {
            StartCoroutine("Rand2Bomb");
        }
        else
        {
            StartCoroutine("Rand3Bomb");
        }*/
    }

    public void Update()
    {
        setLevel();
    }

    void setLevel()
    {
        if(moveG1.gameTime > 3.0f)
        {
            bombFuseTime = 0.5f;

        }
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
        


        Destroy(this);

        isFinish = true;
    }


    private IEnumerator Rand2Bomb()
    {
        //Vector2 position = MoveG1.PlayerG1pos;
        //position.x = Mathf.Round(position.x) + 0.5f;
        //position.y = Mathf.Round(position.y) + 0.5f;

        // UnityEngine.Debug.Log("Rand1Bomb µé¾î¿È");

        // GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        // UnityEngine.Debug.Log("Bomb 1 instantiate");

        yield return new WaitForSeconds(0.2f);

        Explosion01 explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);

        // UnityEngine.Debug.Log("explosion instantiate");

        Explode(transform.position, Vector2.up, explosionRadius);
        Explode(transform.position, Vector2.down, explosionRadius);
        Explode(transform.position, Vector2.left, explosionRadius);
        Explode(transform.position, Vector2.right, explosionRadius);



        Destroy(this);

        isFinish = true;
    }


    private IEnumerator Rand3Bomb()
    {
        //Vector2 position = MoveG1.PlayerG1pos;
        //position.x = Mathf.Round(position.x) + 0.5f;
        //position.y = Mathf.Round(position.y) + 0.5f;

        // UnityEngine.Debug.Log("Rand1Bomb µé¾î¿È");

        // GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        // UnityEngine.Debug.Log("Bomb 1 instantiate");

        yield return new WaitForSeconds(1.0f);

        Explosion01 explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);

        // UnityEngine.Debug.Log("explosion instantiate");

        Explode(transform.position, Vector2.up, explosionRadius);
        Explode(transform.position, Vector2.down, explosionRadius);
        Explode(transform.position, Vector2.left, explosionRadius);
        Explode(transform.position, Vector2.right, explosionRadius);



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
