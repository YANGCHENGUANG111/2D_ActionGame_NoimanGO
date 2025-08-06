using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public static Timer instance; // 单例实例
    public static float timeLimit = 300f; // 总时间为600秒 合計時間は600秒
    private static float timeRemaining; // 剩余时间
    public GameObject GameOverPanel;

    public GameObject mainCamera; //カメラ
    public AudioSource MainAudio; //メインBGM
    public AudioSource GameOverAudio; //ゲームオーバー音
    void Awake()
    {
        // 确保只存在一个 GameManager 实例
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 在场景切换时不销毁
        }
        else
        {
            Destroy(gameObject); // 如果已经存在，销毁这个实例
        }
    }
    void Start()
    {
        MainAudio = mainCamera.GetComponent<AudioSource>();
        GameOverAudio = GameOverPanel.GetComponent<AudioSource>();
        timeRemaining = timeLimit; // 初始化剩余时间 remainingTime = totalTime;
        //GameOverPanel = GameObject.FindWithTag("gameover");
    }

    // Update is called once per frame
    void Update()
    {
        // 计时
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime; // 每帧减少剩余时间
        }
        else
        {
            TimeOut(); // 时间到时调用通关失败逻辑
        }
    }
    void TimeOut()
    {
        Debug.Log("Time's up! You failed."); // 打印失败信息
        // 可以在这里添加通关失败的逻辑，比如加载失败场景
        //SceneManager.LoadScene("Scene_1"); // 假设你有一个名为 GameOverScene 的场景
        GameOverPanel.SetActive(true);
        MainAudio.Stop();
        GameOverAudio.Play();
    }
    // 重置计时器
    public static void ResetTimer()
    {
        timeRemaining = timeLimit; // 将剩余时间重置为总时间
    }
    public static float GetTimeRemaining()
    {
        return timeRemaining; // 供其他脚本获取剩余时间
    }
}
