using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Switch_Scene_Script : MonoBehaviour
{
    public GameObject operation_panel;
    public GameObject start_button;
    public GameObject operation_button;
    public GameObject exit_button;
    public GameObject titile;
    public GameObject button;    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Switch_GameStart()
    {
        SceneManager.LoadScene("Scene_1");
    }

    public void Switch_Operation()
    {
        operation_panel.SetActive(true);
        start_button.SetActive(false);
        operation_button.SetActive(false);
        exit_button.SetActive(false);
        titile.SetActive(false);
        button.SetActive(true);
    }
    public void Switch_Back()
    {
        operation_panel.SetActive(false);
        start_button.SetActive(true);
        operation_button.SetActive(true);
        exit_button.SetActive(true);
        titile.SetActive(true);
        button.SetActive(false);
    }
    public void On_exit_Game()
    {
        Application.Quit();
    }
}
