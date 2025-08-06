using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PipeTransport : MonoBehaviour
{
    public float delayBeforeTransition = 0.5f;
    public float descentSpeed = 2.0f;//下降速度
    public float targetDepth = -6.0f;//目標の深さ

    private Collider2D playerCollider; // プレイヤーのコライダーを保存する変数
    private Rigidbody2D playerRigidbody;//プレイヤーのrigidbody
    private bool isPlayerInPipe = false; // プレイヤーがパイプに触れているかどうかのフラグ
    private bool isEnteringPipe = false; // パイプに入る処理を開始しているかどうかのフラグ
    public int pipeNumber;//1:手前のドカン、2:奥のドカン

    public AudioSource PipeSource;

    public string undergroundSceneName = "UndergroundScene";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーがパイプに触れている間に下キーが押された場合
        if (isPlayerInPipe && !isEnteringPipe && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxis("Vertical") < 0))
        {
            PipeSource.Play();
            // パイプに入る処理を開始
            isEnteringPipe = true;

            playerRigidbody = playerCollider.GetComponent<Rigidbody2D>();
            playerRigidbody.gravityScale = 0; // 重力を無効化

            // x軸方向の速度を0に設定して停止
            playerRigidbody.velocity = new Vector2(0, playerRigidbody.velocity.y);

            playerCollider.GetComponent<PlayerController>().DisableMovement(); // プレイヤーの動きを止める
            // プレイヤーのX座標をパイプの中心に合わせる
            float pipeCenterX = transform.position.x; // パイプの中心のX座標
            playerCollider.transform.position = new Vector3(pipeCenterX, playerCollider.transform.position.y, playerCollider.transform.position.z);

            PlayerController.pipeNumber_remember = pipeNumber; // パイプの番号を記憶
            playerCollider.enabled = false; // プレイヤーのコライダーを無効化
            Debug.Log(isPlayerInPipe);
            StartCoroutine(EnterPipe()); // 遅延時間後にシーンを切り替える
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // プレイヤーがパイプに触れた場合
        if (collision.CompareTag("Player"))
        {
            playerCollider = collision; // プレイヤのコライダーを保存
            isPlayerInPipe = true;
            // Debug.Log(isPlayerInPipe);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        // プレイヤーがパイプから離れた場合
        if (collision.CompareTag("Player"))
        {
            isPlayerInPipe = false;
            // Debug.Log(isPlayerInPipe);
            // Debug.Log("Player is out of pipe detection area");
        }
    }

    private IEnumerator EnterPipe()
    { 
        // プレイヤーがパイプの中に徐々に移動する
        Vector3 targetPosition = new Vector3(transform.position.x, targetDepth, playerCollider.transform.position.z);
        // while (playerCollider.transform.position.y > targetDepth)
        // {
        //     // プレイヤーのX座標は固定し、Y座標だけを移動させる
        //     playerCollider.transform.position = Vector3.MoveTowards(
        //         playerCollider.transform.position, 
        //         targetPosition, 
        //         descentSpeed * Time.deltaTime);
        //     yield return null;
        // }
        // 経過時間を計測するための変数
        float elapsedTime = 0f;
        while (elapsedTime < 2.0f)
        {
            // プレイヤーが目標深さまで移動する
            if (playerCollider.transform.position.y > targetDepth)
            {
                // プレイヤーのX座標は固定し、Y座標だけを移動させる
                playerCollider.transform.position = Vector3.MoveTowards(
                    playerCollider.transform.position, 
                    targetPosition, 
                    descentSpeed * Time.deltaTime);
            }
            // 経過時間を更新
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(delayBeforeTransition);

        // プレイヤーがパイプに入る処理が完了したためフラグをリセット
        isPlayerInPipe = false;
        isEnteringPipe = false;
        playerCollider.enabled = true; // プレイヤーの当たり判定を再度有効化
        SceneManager.LoadScene(undergroundSceneName);
    }
}
