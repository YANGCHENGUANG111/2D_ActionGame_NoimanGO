using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private float lastDamageTime; // 上次掉血的时间
    public float damageCooldown = 1.0f; // 冷却时间（秒）

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int points = 100;  // 碰到物体后获得的积分

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Time.time >= lastDamageTime + damageCooldown)  // 检查碰撞对象是否是玩家
        {
            if (gameObject.CompareTag("Coin"))
            {
                player.GetComponent<PlayerController>().CoinAudio.Play();
            }
            else
            {
                player.GetComponent<PlayerController>().FruitAudio.Play();
            }

            ScoreManager.AddScore(points);  // 增加积分
            Destroy(gameObject);  // 消失物体
            lastDamageTime = Time.time; // 记录上次掉血时间
        }
    }
}
