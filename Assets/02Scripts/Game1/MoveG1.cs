using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class MoveG1 : MonoBehaviour
{
    static public Vector2 PlayerG1pos;
    

    public new Rigidbody2D rigidbody { get; private set; }
    private Vector2 direction = Vector2.down;
    public float speed = 5f;
    public float gameTime;
    public bool isDead;

    public Vector2 position;

  
    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;

    public AnimateG1 spriteRendererUp;
    public AnimateG1 spriteRendererDown;
    public AnimateG1 spriteRendererLeft;
    public AnimateG1 spriteRendererRight;
    public AnimateG1 spriteRendererDeath;

    private AnimateG1 activeSpriteRenderer;


    [Header("Bomb")]
    public GameObject bombPrefab;
    public KeyCode inputKey = KeyCode.Space;
    public float bombFuseTime = 3f;

    [Header("Explosion")]
    public Explosion01 explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererDown;
        position = PlayerG1pos;
        isDead = false;
        gameTime = 0.0f;
    }


    private void Update()
    {
        PlayerG1pos = transform.position;
        GameTimer();
        BombTimer();


        if (Input.GetKey(inputUp)){
            SetDirection(Vector2.up, spriteRendererUp);
        }
        else if (Input.GetKey(inputDown)){
            SetDirection(Vector2.down, spriteRendererDown);
        }
        else if (Input.GetKey(inputLeft)){
            SetDirection(Vector2.left, spriteRendererLeft);
        }
        else if (Input.GetKey(inputRight)){
            SetDirection(Vector2.right, spriteRendererRight);
        }
        else{
            SetDirection(Vector2.zero, activeSpriteRenderer);
        }
         
    }

    private void GameTimer()
    {
        if(!isDead)
        {
            gameTime += Time.deltaTime;
        }
    }

    private void BombTimer()
    {
        if (Input.GetKeyDown(inputKey))
        {
            //StartCoroutine(PlaceBomb());
           
            //StartCoroutine(RandBomb());
            SetBomb();
            
        }
    }

    public void SetBomb()
    {
        StartCoroutine(RandBomb());
    }

    public IEnumerator RandBomb()
    {
        //Vector2 position = MoveG1.PlayerG1pos;
        
        
        //position.x = Mathf.Round(position.x) + 0.5f;
        //position.y = Mathf.Round(position.y) + 0.5f;

       

        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
       

        yield return new WaitForSeconds(3.0f);


        Explosion01 explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);



        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);




        Destroy(bomb);

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

    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);
    }


    private void SetDirection(Vector2 newDirection, AnimateG1 spriteRenderer)
    {
        direction = newDirection;

        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

        activeSpriteRenderer = spriteRenderer;
        activeSpriteRenderer.idle = direction == Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            DeathSequence();
        }
    }

    private void DeathSequence()
    {
        enabled = false;
        GetComponent<BombG1>().enabled = false;

        spriteRendererUp.enabled = false;
        spriteRendererDown.enabled = false;
        spriteRendererLeft.enabled = false;
        spriteRendererRight.enabled = false;
        spriteRendererDeath.enabled = true;

        Invoke("OnDeathSquenceEnded",1.25f);
    }
      
    private void OnDeathSquenceEnded()
    {
        isDead = true;
        gameObject.SetActive(false);

    }
}
