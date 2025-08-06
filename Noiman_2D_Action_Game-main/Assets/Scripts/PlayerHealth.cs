using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    // 静态变量，保存角色的血量
    public static int health = 4; // 假设满血为4
    //public int maxHealth = 4;
    // 血条图片的不同状态
    public Sprite HealthSprite1;     // 满血图片
    public Sprite HealthSprite2;   // 中等血量图片
    public Sprite HealthSprite3;      // 低血量图片
    public Sprite HealthSprite4;   //空血量图片
    // 引用UI中用于显示血量的图片
    public Image healthBar;
    public GameObject GameOverPanel;
    public GameObject mainCamera; //カメラ
    public AudioSource MainAudio; //メインBGM
    public AudioSource GameOverAudio; //ゲームオーバー音

    void Start()
    {
        MainAudio = mainCamera.GetComponent<AudioSource>();
        GameOverAudio = GameOverPanel.GetComponent<AudioSource>();
    }
    void Update()
    {
        // 初始化角色的血量
        //health = maxHealth;
        UpdateHealthBar();
    }

    //// 减少血量的方法
    //public void TakeDamage(int damage)
    //{
    //    health -= damage;
    //    if (health < 0) health = 0;
    //    UpdateHealthBar();
    //}

    //// 恢复血量的方法
    //public void Heal(int amount)
    //{
    //    health += amount;
    //    if (health > maxHealth) health = maxHealth;
    //    UpdateHealthBar();
    //}

    // 更新血条显示
    void UpdateHealthBar()
    {
        if (health == 4)
        {
            healthBar.sprite = HealthSprite1;
        }
        if(health==3)
        {
            healthBar.sprite = HealthSprite2;
        }
        if(health==2)
        {
            healthBar.sprite = HealthSprite3;
        }
        if(health==1)
        {
            healthBar.sprite = HealthSprite4;
        }
        else if(health==0)
        {
            GameOverPanel.SetActive(true);
            MainAudio.Stop();
            GameOverAudio.Play();
        }
    }
}
