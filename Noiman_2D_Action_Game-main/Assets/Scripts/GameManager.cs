using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // シングルトンインスタンス

    public int coin = 0; // コインの数
    public int score = 0; // スコア
    public int life = 4; // ライフの数
    public float timeElapsed = 0; // 経過時間

    void Awake()
    {
        // シングルトンパターンの設定
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // シーンをまたいでも破棄しない
        }
        else
        {
            Destroy(gameObject); // インスタンスがすでに存在している場合は破棄
        }
    }

    // コインを追加する
    public void AddCoin()
    {
        coin++;
    }

    // ライフを減らす
    public void DecreaseLife()
    {
        life--;
        Debug.Log("残機: " + life);

        if (life <= 0)
        {
            // ゲームオーバー処理
            GameOver();
        }
    }

    // タイムを計測する
    void Update()
    {
        timeElapsed += Time.deltaTime;
    }

    // ゲームオーバー処理
    void GameOver()
    {
        // ゲームオーバー処理
        Debug.Log("ゲームオーバー");
    }

}
