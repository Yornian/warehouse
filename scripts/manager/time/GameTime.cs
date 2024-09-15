using UnityEngine;
using System;

public class GameTime : MonoBehaviour
{
    public static GameTime Instance { get; private set; }

    public int hours;
    public int minutes;
    public int days;
    public float timeScale = 60f; // 游戏时间和现实时间的比率
    private float timeCounter;
    public bool ifSleep;
    // 定义时间更新事件
    public event Action OnTimeUpdated;

    void Awake()
    {
        // 确保单例模式
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 如果你希望在场景切换时保留时间对象
            // 设置初始时间为早上 8 点
            hours = 8;
            minutes = 0;
            days = 0;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        timeCounter += Time.deltaTime * timeScale;

        if (timeCounter >= 60f) // 1 分钟过去了
        {
            timeCounter -= 60f; // 确保剩余时间保留
            minutes++;

            if (minutes >= 60)
            {
                minutes = 0;
                hours++;
                if (hours >= 24)
                {
                    hours = 0;
                    days++;
                }
            }

            // 当时间更新时，触发事件
            OnTimeUpdated?.Invoke();

            // 检查是否超过晚上 11 点，如果是则强制睡觉
            if (hours >= 23)
            {
                ForceSleep();
            }
        }
    }

    public string GetCurrentTime()
    {
        return $"{hours:D2}:{minutes:D2}";
    }

    public void Sleep()
    {
        hours = 8;
        minutes = 0;
        days = 0;
        OnTimeUpdated?.Invoke();
    }

    private void ForceSleep()
    {
        Sleep();
    }
}