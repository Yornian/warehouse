using UnityEngine;
using TMPro;

public class TimeDisplayUI : MonoBehaviour
{
    public TextMeshProUGUI timeText; 

    void Start()
    {
       
        
            GameTime.Instance.OnTimeUpdated += UpdateTimeUI;
          
        
     

        // ��ʼ����UI��ʾ��ʱ��
        UpdateTimeUI();
    }

    void UpdateTimeUI()
    {
        // ����UI��ʾ��ʱ��
        timeText.text = GameTime.Instance.GetCurrentTime();
     
    }


    void OnDestroy()
    {
        // ȷ���ڶ�������ʱȡ���¼����ģ������ڴ�й©
        if (GameTime.Instance != null)
        {
            GameTime.Instance.OnTimeUpdated -= UpdateTimeUI;
        }
    }
}
