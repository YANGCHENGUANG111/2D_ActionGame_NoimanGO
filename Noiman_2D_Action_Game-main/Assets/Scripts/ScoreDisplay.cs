using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text scoreText;  // UI文本组件
    // Start is called before the first frame update
    void Start()
    {
        scoreText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame

    private void Update()
    {
        scoreText.text = "ポイント:" + ScoreManager.score;  // 更新积分文本
    }
}
