using UnityEngine;
using System;

public class GameTime : MonoBehaviour
{
    public static GameTime Instance { get; private set; }

    public int hours;
    public int minutes;
    public int days;
    public float timeScale = 60f; // ��Ϸʱ�����ʵʱ��ı���
    private float timeCounter;
    public bool ifSleep;
    // ����ʱ������¼�
    public event Action OnTimeUpdated;

    void Awake()
    {
        // ȷ������ģʽ
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // �����ϣ���ڳ����л�ʱ����ʱ�����
            // ���ó�ʼʱ��Ϊ���� 8 ��
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

        if (timeCounter >= 60f) // 1 ���ӹ�ȥ��
        {
            timeCounter -= 60f; // ȷ��ʣ��ʱ�䱣��
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

            // ��ʱ�����ʱ�������¼�
            OnTimeUpdated?.Invoke();

            // ����Ƿ񳬹����� 11 �㣬�������ǿ��˯��
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