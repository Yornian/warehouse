using UnityEngine;
using TMPro;

public class TimeDisplayUI : MonoBehaviour
{
    public TextMeshProUGUI timeText; 

    void Start()
    {
       
        
            GameTime.Instance.OnTimeUpdated += UpdateTimeUI;
          
        
     

        // 初始设置UI显示的时间
        UpdateTimeUI();
    }

    void UpdateTimeUI()
    {
        // 更新UI显示的时间
        timeText.text = GameTime.Instance.GetCurrentTime();
     
    }


    void OnDestroy()
    {
        // 确保在对象销毁时取消事件订阅，避免内存泄漏
        if (GameTime.Instance != null)
        {
            GameTime.Instance.OnTimeUpdated -= UpdateTimeUI;
        }
    }
}
