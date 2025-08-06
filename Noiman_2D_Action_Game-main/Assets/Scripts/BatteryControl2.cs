using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryControl2 : MonoBehaviour
{
    private float lastDamageTime; // 上次加血的时间
    public float damageCooldown = 1.0f; // 冷却时间（秒）public float moveSpeed = 5f; // 移动速度
    public float moveSpeed = 5f; // 移动速度
    private Vector3 startPosition; // 初始位置
    private float moveTime = 3.0f; // 每个方向移动的时间
    private float timer = 0f; // 计时器
    private bool movingRight = true; // 控制是否向右移动

    public GameObject player;

    void Start()
    {
        startPosition = transform.position; // 记录预制体的起始位置
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        MoveLeftRight();
    }

    private void MoveLeftRight()
    {
        timer += Time.deltaTime;

        if (movingRight)
        {
            // 向右移动
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

            if (timer >= moveTime) // 向右移动 2 秒后，切换方向
            {
                movingRight = false;
                timer = 0f; // 重置计时器
            }
        }
        else
        {
            // 向左移动
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

            if (timer >= moveTime) // 向左移动 2 秒后，销毁预制体
            {
                Destroy(gameObject); // 销毁预制体
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Time.time >= lastDamageTime + damageCooldown)
        {
            player.GetComponent<PlayerController>().BatteryAudio.Play();
            if (PlayerHealth.health <= 2&&PlayerHealth.health>0)
            {
                PlayerHealth.health += 2;
            }
            if(PlayerHealth.health==3)
            {
                PlayerHealth.health += 1;
            }
            lastDamageTime = Time.time;
            // 销毁预制体，防止重复加血
            Destroy(gameObject);
        }
    }
}
