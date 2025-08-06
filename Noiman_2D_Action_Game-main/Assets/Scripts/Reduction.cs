using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Reduction : MonoBehaviour
{
    //public GameObject GameClearPanel;
    //public GameObject GameOverPanel;
    //public void Awake()
    //{
    //    // 找到Canvas对象
    //    GameObject canvas = GameObject.Find("Canvas");
    //    if (canvas != null)
    //    {
    //        // 在Canvas的子物体中查找带有特定名称或标签的对象
    //        GameClearPanel = canvas.transform.Find("GameClearPanel")?.gameObject;
    //        GameOverPanel = canvas.transform.Find("GameOverPanel")?.gameObject;

    //        // 处理找不到面板的情况
    //        if (GameClearPanel == null)
    //            Debug.LogWarning("GameClearPanel 未找到！");
    //        if (GameOverPanel == null)
    //            Debug.LogWarning("GameOverPanel 未找到！");
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Canvas 未找到！");
    //    }
    //}
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
    public void onclick1()
    {
        PlayerController.pipeNumber_remember = 0;
        // 重置场景，加载当前场景
        SceneManager.LoadScene("Scene_1");
        PlayerHealth.health = 4;
        Timer.ResetTimer(); // 重置计时器
        ScoreManager.score = 0;

    }
    public void onclick2()
    {
        PlayerController.pipeNumber_remember = 0;
        // 重置场景，加载当前场景
        SceneManager.LoadScene("menu");
        PlayerHealth.health = 4;
        Timer.ResetTimer(); // 重置计时器
        ScoreManager.score = 0;
    }
    public void onclick3()
    {
        PlayerController.pipeNumber_remember = 0;
        Application.Quit();
        PlayerHealth.health = 4;
        Timer.ResetTimer(); // 重置计时器
        ScoreManager.score = 0;
    }
    //public void onclick1()
    //{
    //    SetPanelsInactive();
    //    StartCoroutine(ReloadSceneAndReset("Scene_1"));
    //}

    //public void onclick2()
    //{
    //    SetPanelsInactive();
    //    StartCoroutine(ReloadSceneAndReset("menu"));
    //}

    //public void onclick3()
    //{
    //    SetPanelsInactive();
    //    Application.Quit();
    //}
    //private void SetPanelsInactive()
    //{
    //    // 设置面板不可见，增加检查以防面板为空
    //    if (GameClearPanel != null)
    //    {
    //        GameClearPanel.SetActive(false);
    //    }
    //    if (GameOverPanel != null)
    //    {
    //        GameOverPanel.SetActive(false);
    //    }
    //}
    //private IEnumerator ReloadSceneAndReset(string sceneName)
    //{
    //    // 异步加载场景
    //    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

    //    // 等待场景加载完成
    //    while (!asyncLoad.isDone)
    //    {
    //        yield return null;
    //    }

    //    // 场景加载完成后重置逻辑
    //    PlayerHealth.health = 4;
    //    Timer.ResetTimer();
    //    ScoreManager.score = 0;
    //}
}
