using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerEnemyController : Enemy
{
    //private void Update()
    //{
    //    if(transform.position==startPosition)
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    //        StartCoroutine(FlowerWait(1f));
    //    }
    //    if(transform.position==targetPosition)
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
    //        StartCoroutine(FlowerWait(1f));
    //    }
    //}
    //private IEnumerator FlowerWait(float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //}
    public float moveDistance = 2.0f;  // 移动的距离
    public float moveSpeed = 1.0f;     // 移动的速度
    public float pauseTime = 1.0f;     // 停顿的时间
    public float cycleTime = 5.0f;     // 每次循环的总时间

    private Vector3 startPosition;     // 食人花的初始位置
    private bool movingUp = true;      // 控制移动方向
    private bool isPaused = false;     // 控制是否暂停

    public float damageCooldown = 1.0f; // 冷却时间（秒）
    private float lastDamageTime; // 上次掉血的时间

    public GameObject player;
    private void Start()
    {
        Init();
        startPosition = transform.position; // 记录初始位置
        StartCoroutine(MovePiranhaPlant());
        player = GameObject.FindWithTag("Player");
    }
    protected override void Init()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private IEnumerator MovePiranhaPlant()
    {
        while (true)
        {
            if (isPaused)
            {
                yield return new WaitForSeconds(pauseTime); // 在最高点停顿 1 秒
                isPaused = false; // 停止暂停状态，开始移动回到原点
                movingUp = false; // 改变方向
            }

            // 计算目标位置
            Vector3 targetPosition = movingUp ? startPosition + Vector3.up * moveDistance : startPosition;

            // 移动食人花
            while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null; // 等待下一帧
            }

            // 如果到达了最高点
            if (movingUp)
            {
                isPaused = true;  // 进入暂停状态
            }
            else
            {
                // 如果回到了起始点，开始新一轮的循环
                movingUp = true; // 重置方向
                yield return new WaitForSeconds(cycleTime - pauseTime); // 等待新的循环
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Time.time >= lastDamageTime + damageCooldown)
        {
            player.GetComponent<PlayerController>().DamageAudio.Play();
            // _player.myAnim.SetBool("Death", true);
            //Destroy(gameObject);
            PlayerHealth.health -= 1;
            lastDamageTime = Time.time; // 记录上次掉血时间
            //StartCoroutine(wait(1.5f));
        }
    }
    //public IEnumerator wait(float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //}
}
