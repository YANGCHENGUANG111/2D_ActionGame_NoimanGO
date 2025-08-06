using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static int score = 0;  // 静态变量用于存储积分

    public static void AddScore(int points)
    {
        score += points;  // 增加积分
    }
}
