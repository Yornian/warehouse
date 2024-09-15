using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class startOrQuit : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;
    // Start is called before the first frame update
    void Start()
    {
        startButton. onClick.AddListener(OnStartButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
    }
    public void QuitGame()
    {
        // 退出游戏
        Application.Quit();
            // 模拟退出游戏的效果
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
        // Update is called once per frame
        void Update()
    {
        
    }
    public void OnQuitButtonClick()
    {



        QuitGame();
    }
    public void OnStartButtonClick()
    {

        SceneManager.LoadScene("PostOffice");

     
    }
}
