using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class MoveG1 : MonoBehaviour
{
    static public Vector2 PlayerG1pos;

    DatabaseManager databasemanager;
   
    public DynamicJoystick joy;
    public FixedJoystick fjoy;

    public AudioClip Walk;

    public AudioSource audioSrc;
    //public AudioSource audioSrc2;


    public float speed  = 5f;
    float x;
    float z;

    Vector2 moveVec;



    public new Rigidbody2D rigidbody { get; private set; }
    private Vector2 direction = Vector2.down;
    public float gameTime;
    float time_tmp;
    public float BombTime = 3.0f;
    public bool isDead;
    public bool isWalk;

    public Vector2 position;

  
    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;

    //public AnimateG1 spriteRendererUp;
    //public AnimateG1 spriteRendererDown;
    public AnimateG1 spriteRendererLeft;
    public AnimateG1 spriteRendererRight;
    public AnimateG1 spriteRendererDeath;



    private AnimateG1 activeSpriteRenderer;

    public NuclearB nuclearB;

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
        nuclearB = GetComponent<NuclearB>();
        rigidbody = GetComponent<Rigidbody2D>();
        databasemanager = GameObject.Find("DatabaseManager").GetComponent<DatabaseManager>();
       
        activeSpriteRenderer = spriteRendererRight;

        position = PlayerG1pos;

        audioSrc = GetComponent<AudioSource>();
        //audioSrc2 = GetComponent<AudioSource>();

        isDead = false;
        gameTime = 0.0f;
        
    }

    private void Start()
    {
        gameTime = 0;
        time_tmp = 0;
        audioSrc.clip = Walk;
        //audioSrc2.clip = Die;
        InvokeRepeating("walkAudio", 0, 0.2f);
    }
    void FixedUpdate()
    {
        x = fjoy.Horizontal;
        z = fjoy.Vertical;

        Vector2 position = rigidbody.position;
        Vector2 translation = new Vector2(x,z) * speed * Time.fixedDeltaTime;


        rigidbody.MovePosition(position + translation);
        isWalk = true;
 
        if (translation.sqrMagnitude == 0)
        {
            isWalk = false;

            return;
        }

        //walkAudio();
    }

    private void Update()
    {
        PlayerG1pos = transform.position;


        SetSpeed();

        JoyControl2();
        GameTimer();

    }
    
    void walkAudio()
    {
        if (isWalk)
        {
            audioSrc.Play();
        }
        else {
        audioSrc.Stop();
        }
    }
    private void SetSpeed()
    {
        if(gameTime >= 0.0f && gameTime < 20.0f)
        {
            speed = 4.2f;
        }
        else if(gameTime >= 20.0f && gameTime < 40.0f)
        {
            speed = 3.4f;
        }
        else if(gameTime >= 40.0f)
        {
            speed = 2.7f;
        }
    }
    private void JoyControl2()
    {


        if (x > 0)
        {

            SetDirection(Vector2.right, spriteRendererRight);
        }
        else if (x < 0)
        {

            SetDirection(Vector2.left, spriteRendererLeft);
        }
       
        else if (x == 0)
        {
            SetDirection(Vector2.zero, activeSpriteRenderer);
        }
        else
        {
            SetDirection(Vector2.zero, activeSpriteRenderer);
        }
    }
    private void JoyControl()
    {
        float xabs = Mathf.Abs(x);
        float zabs = Mathf.Abs(z);

        if (xabs > zabs && x>=0)
        {

            SetDirection(Vector2.right, spriteRendererRight);
        }
        else if (xabs > zabs && x <= 0)
        {

            SetDirection(Vector2.left, spriteRendererLeft);
        }
        else if (xabs < zabs && z <= 0)
        {
         
           // SetDirection(Vector2.down, spriteRendererDown);
        }
        else if (xabs < zabs && z >= 0)
        {

           // SetDirection(Vector2.up, spriteRendererUp);
        }
        else if(xabs == 0 && zabs == 0)
        {
            SetDirection(Vector2.zero, activeSpriteRenderer);
        }
        else
        {
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



    public void SetBomb()
    {
        StartCoroutine(RandBomb());
    }


    private IEnumerator RandBomb()
    {
        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);

        yield return new WaitForSeconds(BombTime);

        Destroy(bomb);

    }


    public void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0)
        {
            return;
        }

        position += direction;

        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))
        {
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

    private void SetDirection(Vector2 newDirection, AnimateG1 spriteRenderer)
    {
        direction = newDirection;

        //spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
        //spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
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

        //spriteRendererUp.enabled = false;
        //spriteRendererDown.enabled = false;
        spriteRendererLeft.enabled = false;
        spriteRendererRight.enabled = false;
        spriteRendererDeath.enabled = true;

        Invoke("OnDeathSquenceEnded",1.25f);
    }
      
    private void OnDeathSquenceEnded()
    {
        isDead = true;
        databasemanager.readScore("Game1");
    }
}
