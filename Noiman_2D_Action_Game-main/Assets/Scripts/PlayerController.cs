using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    private Rigidbody2D myRigidbody;
    public Animator myAnim;
    private BoxCollider2D myFeet;
    private bool isGround;
    private SpriteRenderer sr;

    public AudioSource JumpAudio; //ジャンプ音
    public AudioSource CoinAudio; //コイン音
    public AudioSource FruitAudio; //フルーツ音
    public AudioSource BatteryAudio; //バッテリー音
    public AudioSource EnemyDeathAudio; //敵死亡音
    public AudioSource DamageAudio; //ダメージ音

    //パイプの移動関係
    public static int pipeNumber_remember;//パイプの番号を記憶する変数
    public Vector3 GroudPipe1Position;//出てくる位置を設定する変数(地上パイプ1)
    public Vector3 GroudPipe2Position;//出てくる位置を設定する変数(地上パイプ2)
    public Vector3 UnderPipe1Position;//出てくる位置を設定する変数(地下パイプ1)
    public Vector3 UnderPipe2Position;//出てくる位置を設定する変数(地下パイプ2)

    //プレイヤーが動けるかどうか
    private bool canMove = true;

    private float lastDamageTime; // 上次掉血的时间
    public float damageCooldown = 3.0f; // 冷却时间（秒）
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        // シーン1に戻ったとき、パイプの番号を保持していたらプレイヤーの位置を移動
        if (SceneManager.GetActiveScene().name == "Scene_1" & pipeNumber_remember != 0)
        {
            if (pipeNumber_remember == 1)
            {
                transform.position = GroudPipe1Position;
            }
            if (pipeNumber_remember == 2)
            {
                transform.position = GroudPipe2Position;
            }
        }

        // 地下シーンに戻ったとき、パイプの番号を保持していたらプレイヤーの位置を移動
        if (SceneManager.GetActiveScene().name == "UndergroundScene" & pipeNumber_remember != 0)
        {
            if (pipeNumber_remember == 1)
            {
                transform.position = UnderPipe1Position;
            }
            if (pipeNumber_remember == 2)
            {
                transform.position = UnderPipe2Position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Run();
            //flip();
            Jump();
            CheckGrounded();
            JumptoIdle();
            Flip();
            if (myAnim.GetBool("Death") == true)
            {
                runSpeed = 0;
                jumpSpeed = 0;
                pipeNumber_remember = 0;//パイプの番号を初期化
                StartCoroutine(ResetSceneAfterDelay(1.5f));
            }
        }
    }
    void CheckGrounded()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        //Debug.Log(isGround);
    }
    //void flip()
    //{
    //    bool playerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
    //    if(playerHasXAxisSpeed)
    //    {
    //        if(myRigidbody.velocity.x > 0.1f)
    //        {
    //            Debug.Log("right");
    //            transform.rotation = Quaternion.Euler(0, 0, 0);
    //        }
    //        if (myRigidbody.velocity.x < -0.1f)
    //        {
    //            Debug.Log("left");
    //            transform.rotation = Quaternion.Euler(0, 180, 0);
    //        }
    //    }
    //}
    void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0)
            sr.flipX = false;
        if (Input.GetAxis("Horizontal") < 0)
            sr.flipX = true;
    }
    void Run()
    {
        float move01r = Input.GetAxis("Horizontal");
        Vector2 playerVel = new Vector2(move01r * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVel;
        bool playerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnim.SetBool("Run", playerHasXAxisSpeed);
    }
    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(isGround)
            {
                myAnim.SetBool("Jump", true);
                Vector2 jumpvel = new Vector2(0.0f, jumpSpeed);
                myRigidbody.velocity = Vector2.up * jumpvel;
                JumpAudio.Play();
            }
        }
    }
    void JumptoIdle()
    {
        if (Mathf.Abs(myRigidbody.velocity.y) < Mathf.Epsilon)
        {
            myAnim.SetBool("Jump", false);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "InstaDeath" && Time.time >= lastDamageTime + damageCooldown)
        {
            myAnim.SetBool("Death", true);
            PlayerHealth.health -= 1;
            lastDamageTime = Time.time; // 记录上次掉血时间
            //StartCoroutine(ResetSceneAfterDelay(1.5f));
            //SceneManager.LoadScene("Scene_1");
        }
    }

    public void DisableMovement()
    {
        canMove = false;
    }

    public IEnumerator ResetSceneAfterDelay(float delay)
    {
        // 等待指定的时间（例如2秒）
        yield return new WaitForSeconds(delay);

        // 重置场景，加载当前场景
        SceneManager.LoadScene("Scene_1");
    }

    public void CoinSourcePlay()
    {
        CoinAudio.Play();
    }

    public void FruitSourcePlay()
    {
        FruitAudio.Play();
    }

    public void BatterySourcePlay()
    {
        BatteryAudio.Play();
    }

    public void EnemyDeathSourcePlay()
    {
        EnemyDeathAudio.Play();
    }

    public void DamageSourcePlay()
    {
        DamageAudio.Play();
    }
}
