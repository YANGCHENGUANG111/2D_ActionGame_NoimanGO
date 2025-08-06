using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class QuestionBlockScript : MonoBehaviour
{
    public Tilemap tilemap; // 需要将Tilemap拖入
    public TileBase questionTile; // 问号方块的Tile
    public TileBase normalTile; // 普通方块的Tile
    public GameObject prefab1;
    public GameObject prefab2;

    public AudioSource QuestionAudio;
    // Start is called before the first frame update
    void Start()
    {
        // 获取Tilemap组件
        tilemap = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //// 检测玩家是否从下方碰撞
        //if (collision.collider.CompareTag("Player"))
        //{
        //    //Debug.Log(1);
        //    Vector3 hitPosition = Vector3.zero;
        //    foreach (ContactPoint2D hit in collision.contacts)
        //    {
        //        //Debug.Log(2);
        //        hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
        //        hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
        //        Vector3Int tilePosition = tilemap.WorldToCell(hitPosition);
        //        //Debug.Log(2);
        //        // 检查是否是问号方块
        //        if (tilemap.GetTile(tilePosition) == questionTile)
        //        {
        //            Debug.Log(3);
        //            // 更换为普通方块
        //            tilemap.SetTile(tilePosition, normalTile);

        //            // 如果你想在碰撞后执行其他操作，例如播放音效或动画，可以在这里添加
        //        }
        //    }
        //}
        // 检测是否与玩家碰撞
        if (collision.collider.CompareTag("Player"))
        {
            // 确定碰撞点在玩家的顶部（即问号方块的底部）
            Vector3 hitPosition = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts)
            {
                // 如果碰撞点的法线朝下，说明是从下方碰撞
                if (hit.normal.y > 0.5f)
                {
                    hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                    hitPosition.y = hit.point.y - 0.01f * hit.normal.y+1;
                    Vector3Int tilePosition = tilemap.WorldToCell(hitPosition);
                    Debug.Log("Hit Position: " + hitPosition);
                    Debug.Log("Tile Position: " + tilePosition);
                    Debug.Log("Tile at position: " + tilemap.GetTile(tilePosition));
                    // 检查是否是问号方块
                    if (tilemap.GetTile(tilePosition) == questionTile)
                    {
                        QuestionAudio.Play();
                        Debug.Log(3);
                        // 更换为普通方块
                        tilemap.SetTile(tilePosition, normalTile);

                        // 可以在这里添加其他效果，例如播放音效或动画

                        // 生成预制体的逻辑
                        float randomValue = Random.value;

                        if (randomValue <= 0.3f)
                        {
                            // 30% 的概率生成加1血的预制体
                            Debug.Log("Prefab 1 (HP +1) instantiated!");
                            Vector3 prefabPosition = new Vector3(hitPosition.x, hitPosition.y + 1f, hitPosition.z);
                            GameObject prefabInstance = Instantiate(prefab1, prefabPosition, Quaternion.identity);
                        }
                        else if (randomValue <= 0.4f)
                        {
                            // 10% 的概率生成加2血的预制体
                            Debug.Log("Prefab 2 (HP +2) instantiated!");
                            Vector3 prefabPosition = new Vector3(hitPosition.x, hitPosition.y + 1f, hitPosition.z);
                            GameObject prefabInstance = Instantiate(prefab2, prefabPosition, Quaternion.identity);
                        }
                    }
                }
            }
        }
    }
}
