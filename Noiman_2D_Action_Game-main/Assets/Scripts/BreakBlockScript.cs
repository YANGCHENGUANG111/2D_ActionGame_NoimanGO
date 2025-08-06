using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class BreakBlockScript : MonoBehaviour
{
    //public ParticleSystem breakEffect; // 拖入破坏效果的粒子系统（可选）
    private Tilemap tilemap;
    public TileBase genericTile1; // 普通方块的Tile
    public TileBase genericTile2; // 普通方块的Tile
    public TileBase genericTile3; // 普通方块的Tile
    public TileBase mahjongTile1; //麻将方块的Tile
    public TileBase mahjongTile2; //麻将方块的Tile
    public GameObject genericBreakEffectPrefab; // 通用粒子效果
    public GameObject specialBreakEffectPrefab; // 特殊粒子效果（可以有多个）

    public AudioSource BreakAudio; //破坏音效

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // 确定碰撞点在玩家的顶部（即问号方块的底部）
            Vector3 hitPosition = Vector3.zero; //初期化
            foreach (ContactPoint2D hit in collision.contacts)
            {
                // 如果碰撞点的法线朝下，说明是从下方碰撞
                if (hit.normal.y > 0.5f)
                {
                    hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                    hitPosition.y = hit.point.y - 0.01f * hit.normal.y + 1;
                    Vector3Int tilePosition = tilemap.WorldToCell(hitPosition);
                    //Debug.Log("Hit Position: " + hitPosition);
                    //Debug.Log("Tile Position: " + tilePosition);
                    //Debug.Log("Tile at position: " + tilemap.GetTile(tilePosition));
                    if (tilemap.GetTile(tilePosition) == genericTile1)
                    {
                        BreakAudio.Play();
                        Debug.Log(4);
                        // 可以在这里添加其他效果，例如播放音效或动画
                        // 对其他普通瓦片使用通用破坏效果
                        GameObject effect = Instantiate(genericBreakEffectPrefab, tilemap.CellToWorld(tilePosition), Quaternion.identity);
                        // 移除瓦片
                        tilemap.SetTile(tilePosition, null);
                        Destroy(effect, 2f);
                    }
                    if (tilemap.GetTile(tilePosition) == genericTile2)
                    {
                        BreakAudio.Play();
                        Debug.Log(4);
                        // 可以在这里添加其他效果，例如播放音效或动画
                        // 对其他普通瓦片使用通用破坏效果
                        GameObject effect = Instantiate(genericBreakEffectPrefab, tilemap.CellToWorld(tilePosition), Quaternion.identity);
                        // 移除瓦片
                        tilemap.SetTile(tilePosition, null);
                        Destroy(effect, 2f);
                    }
                    if (tilemap.GetTile(tilePosition) == genericTile3)
                    {
                        BreakAudio.Play();
                        Debug.Log(4);
                        // 可以在这里添加其他效果，例如播放音效或动画
                        // 对其他普通瓦片使用通用破坏效果
                        GameObject effect = Instantiate(genericBreakEffectPrefab, tilemap.CellToWorld(tilePosition), Quaternion.identity);
                        // 移除瓦片
                        tilemap.SetTile(tilePosition, null);
                        Destroy(effect, 2f);
                    }
                    if(tilemap.GetTile(tilePosition) == mahjongTile1)
                    {
                        BreakAudio.Play();
                        Debug.Log(5);
                        // 另一个特殊瓦片的破坏效果
                        GameObject effect = Instantiate(specialBreakEffectPrefab, tilemap.CellToWorld(tilePosition), Quaternion.identity);
                        // 移除瓦片
                        tilemap.SetTile(tilePosition, null);
                        Destroy(effect, 2f);
                    }
                    else if (tilemap.GetTile(tilePosition) == mahjongTile2)
                    {
                        BreakAudio.Play();
                        Debug.Log(5);
                        // 另一个特殊瓦片的破坏效果
                        GameObject effect = Instantiate(specialBreakEffectPrefab, tilemap.CellToWorld(tilePosition), Quaternion.identity);
                        // 移除瓦片
                        tilemap.SetTile(tilePosition, null);
                        Destroy(effect, 2f);
                    }
                    //BreakTile(tilePosition);
                }
            }
        }
    }
    //private void BreakTile(Vector3Int tilePosition)
    //{
    //    TileBase tile = tilemap.GetTile(tilePosition);
    //    if (tile != null)
    //    {
    //        // 根据瓦片类型或名称，选择不同的破坏效果
    //        if (tile==genericTile1||genericTile2||genericTile3)
    //        {
    //            // 对其他普通瓦片使用通用破坏效果
    //            GameObject effect=Instantiate(genericBreakEffectPrefab, tilemap.CellToWorld(tilePosition), Quaternion.identity);
    //            // 移除瓦片
    //            tilemap.SetTile(tilePosition, null);
    //            Destroy(effect,2f);
    //        }
    //        else if (tile==mahjongTile1||mahjongTile2)
    //        {
    //            // 另一个特殊瓦片的破坏效果
    //            GameObject effect=Instantiate(specialBreakEffectPrefab, tilemap.CellToWorld(tilePosition), Quaternion.identity);
    //            // 移除瓦片
    //            tilemap.SetTile(tilePosition, null);
    //            Destroy(effect, 2f);
    //        }
    //    }
    //}
}
