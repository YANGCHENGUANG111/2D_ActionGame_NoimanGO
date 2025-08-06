using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavotEnemyController : Enemy
{
    public float damageCooldown = 1.0f; // 冷却时间（秒）
    private float lastDamageTime; // 上次掉血的时间
    private float lastturnTime;
    public float turnCooldown = 0.5f;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHit && !isDead)
        {
            Move();
        }
        if (isHit)
        {
            GetHit();
        }
    }
    protected override void Init()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemyRig = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
    protected override void GetHit()
    {
        player.GetComponent<PlayerController>().EnemyDeathAudio.Play();
        //Debug.Log("7");
        anim.SetTrigger("Die");
        CloseCollidersInChild(this.transform);
        enemyRig.isKinematic = true;
        Debug.Log("6");
        Destroy(gameObject, 1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.CompareTag("Player") && Time.time >= lastDamageTime + damageCooldown)
        {
            _CheckPlayerPos();
            lastDamageTime = Time.time; // 记录上次掉血时间
        }
        else if (collision.CompareTag("Border") || collision.gameObject.layer == enemyLayer&&Time.time>=lastturnTime+turnCooldown)
        {
            ChangeMoveDir();
            lastturnTime = Time.time;
        }
    }
    private void _CheckPlayerPos()
    {
        var playerPos = _player.transform.position;
        var curPos = transform.position;
        if (playerPos.y - curPos.y > 1)
        {
            isHit = true;
        }
        else
        {
            player.GetComponent<PlayerController>().DamageAudio.Play();
            //_player.myAnim.SetBool("Death", true);
            Destroy(gameObject);
            PlayerHealth.health -= 1;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "InstaDeath" || collision.gameObject.tag == "InstaGoal")
            Destroy(gameObject);
    }
}
