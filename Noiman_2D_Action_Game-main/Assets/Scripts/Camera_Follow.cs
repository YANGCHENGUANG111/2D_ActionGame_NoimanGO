using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public Transform target; // 跟随的目标物体  追従すべき目標物
    public float damping = 1; // 跟随的平滑程度  スムーズな追従性
    public GameObject LeftEdge; // 左端の境界オブジェクト
    public GameObject RightEdge; // 右端の境界オブジェクト
    public GameObject UpEdge; // 上端の境界オブジェクト
    public GameObject DownEdge; // 下端の境界オブジェクト
    public GameObject StageEdge; // ステージの境界オブジェクト
    
    // 現在は地上のステージのみを考慮しているため、ステージの上下左右の端を設定
    private void LateUpdate()
    {
        if (target != null)
        {   
            // 计算目标位置在屏幕上的坐标  スクリーン上のターゲット位置の座標を計算する
            Vector3 targetPosition = Camera.main.WorldToViewportPoint(target.position);
            // 计算摄像机应该移动的向量  カメラが移動すべきベクトルを計算する
            Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, targetPosition.z));
            // 计算摄像机的目标位置  カメラの目標位置を計算する
            Vector3 destination = transform.position + delta;
            // 使用平滑阻尼移动摄像机  スムーズなダンピングでカメラを動かす
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, destination, damping * Time.deltaTime);
            smoothedPosition.y = StageEdge.transform.position.y;

            // 左右端を超えた場合、カメラの位置を修正する 
            if (smoothedPosition.x <= LeftEdge.transform.position.x)
            {
                smoothedPosition.y = StageEdge.transform.position.y;
                smoothedPosition.x = LeftEdge.transform.position.x;
            }
            else if (smoothedPosition.x >= RightEdge.transform.position.x)
            {
                smoothedPosition.x = RightEdge.transform.position.x;
            }
            else if (smoothedPosition.y < UpEdge.transform.position.y)
            {
                smoothedPosition.y = StageEdge.transform.position.y;
            }

            // 修正後の位置をカメラに適用する
            transform.position = smoothedPosition;
        }
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // LateUpdate函数将在Update函数之后调用，用于处理摄像机的跟随逻辑  LateUpdate関数はUpdate関数の後に呼び出され、カメラのフォローロジックを処理します。
    }

}
