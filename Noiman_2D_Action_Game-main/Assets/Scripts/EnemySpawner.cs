using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
 // 敌人预制体
    public GameObject enemyPrefab;

    // 敌人生成范围的偏移
    public Vector2 spawnOffset = new Vector2(2f, 2f);

    // 敌人生成范围
    public Vector2 spawnAreaSize = new Vector2(3f, 3f);

    // 敌人生成数量
    public int enemyCount = 4;

    private bool hasSpawned = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查进入的是否是玩家（根据 Tag 判断）
        if (collision.CompareTag("Player")&&!hasSpawned)
        {
            SpawnEnemies();
            hasSpawned = true;
        }
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            // 计算随机位置
            Vector2 randomPosition = new Vector2(
                transform.position.x + spawnOffset.x + Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                transform.position.y + spawnOffset.y + Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
            );

            // 实例化敌人
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        }
    }
}
