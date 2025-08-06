using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalManager : MonoBehaviour
{
    public GameObject stageClearPanel; //クリアパネル
    public GameObject mainCamera; //カメラ
    public AudioSource MainAudio; //メインBGM
    public GameObject GoalPanel; //ゴールパネル
    public AudioSource GoalAudio; //ゴール音
    public float riseSpeed = 0.01f;//上昇速度
    public float targetHeight = 50.0f;//目標の高さ

    private Transform playerTransform;//playerの位置
    private Rigidbody2D playerRigidbody;//playerのrigidbody
    private float initialXPosition; // ゴール時のX座標を保持
    private bool goalReached = false;//ゴールに到達したかどうか

    // Start is called before the first frame update
    // void Start()
    // {
    //     //stageClearPanel.SetActive(false);
    //     MainAudio = mainCamera.GetComponent<AudioSource>();
    //     GoalAudio = GoalPanel.GetComponent<AudioSource>();
    // }

    void Awake()
    {
        //stageClearPanel.SetActive(false);
        MainAudio = mainCamera.GetComponent<AudioSource>();
        GoalAudio = GoalPanel.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // 如果玩家已经到达目标
        if (goalReached)
        {
            // 检查角色是否已经达到目标高度
            if (playerTransform.position.y < targetHeight)
            {
                // 让角色上升
                playerTransform.position = new Vector3(
                    initialXPosition, // 固定X坐标
                    playerTransform.position.y + riseSpeed, // 每帧增加Y坐标
                    playerTransform.position.z
                );
            }
            else
            {
                // 一旦到达目标高度，显示通关面板
                ShowStageClearPanel();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // プレイヤーがゴールに到達した場合
        //if (collision.CompareTag("Player"))
        //{
        //    playerTransform = collision.transform;
        //    playerRigidbody = collision.GetComponent<Rigidbody2D>();
        //    playerRigidbody.gravityScale = 0; // 重力を無効化
        //    playerTransform.GetComponent<PlayerController>().DisableMovement(); // プレイヤーの動きを止める
        //    initialXPosition = playerTransform.position.x; // 現在のX座標を保持

        //    // goalReached = true;
        //    StartCoroutine(RisePlayer());
        //}
        // 确保只有玩家碰到目标
        if (collision.CompareTag("Player") && !goalReached)
        {
            playerTransform = collision.transform;
            playerRigidbody = collision.GetComponent<Rigidbody2D>();

            // 禁用重力并停止玩家移动
            playerRigidbody.gravityScale = 0;
            playerTransform.GetComponent<PlayerController>().DisableMovement();

            // 记录玩家初始X坐标
            initialXPosition = playerTransform.position.x;

            // 设置goalReached为true，防止重复触发
            goalReached = true;
        }
    }
    private void ShowStageClearPanel()
    {
        // 显示通关面板
        if (stageClearPanel != null)
        {
            stageClearPanel.SetActive(true);
            MainAudio.Stop();
            GoalAudio.Play();
        }

        // 停止时间
        //Time.timeScale = 0;

        // 恢复重力
        playerRigidbody.gravityScale = 1;

        // 可以在此处添加其他游戏结束逻辑
    }
    //private IEnumerator RisePlayer()
    //{
    //    while (playerTransform.position.y < targetHeight)
    //    {
    //        Debug.Log(playerTransform.position.y + riseSpeed);
    //        playerTransform.position = new Vector3(
    //            initialXPosition, // X座標を固定
    //            playerTransform.position.y + riseSpeed, // Y軸方向に上昇
    //            playerTransform.position.z
    //        );
    //        yield return null;
    //    }

    //    //上昇完了後にクリアパネルを表示
    //    stageClearPanel.SetActive(true);
    //    Time.timeScale = 0;//時間を止める

    //    playerRigidbody.gravityScale = 1; // 重力を元に戻す
    //}

    //public void OnReturnToStart()
    //{
    //    PlayerController.pipeNumber_remember = 0;//パイプの番号をリセット
    //    SceneManager.LoadScene("Scene_1");//スタートシーンに戻る
    //    Time.timeScale = 1.0f;//時間を再開する
    //}

    //public void OnReturnToMenu()
    //{
    //    PlayerController.pipeNumber_remember = 0;//パイプの番号をリセット
    //    SceneManager.LoadScene("menu");//メニューシーンに戻る
    //    Time.timeScale = 1.0f;//時間を再開する
    //}
    //public void OnExitGame()
    //{
    //    Application.Quit();//ゲームを終了する
    //}
}
