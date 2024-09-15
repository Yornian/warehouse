using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 单例实例
    private static GameManager instance;
    private IGameState currentState;
    public int Money { get; private set; }
    // 公开的静态属性用于访问实例
    public int moneyToday;
    //money
    public TMP_Text MoneyText;
    //building 
    public int powerValue=100;
    public List<BuildingInfo> BuildingList = new List<BuildingInfo>();


    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                // 在场景中查找 GameManager 实例
                instance = FindObjectOfType<GameManager>();

                // 如果没有找到实例，创建一个新的 GameObject 并添加 GameManager 组件
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    // 游戏状态枚举
    public enum GameState { StartMenu, Playing, Paused, GameOver }
    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        // 确保只有一个 GameManager 实例
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 保持 GameManager 在加载新场景时不被销毁
        }
        else
        {
            Destroy(gameObject); // 销毁重复的 GameManager 实例
        }
    }

    private void Start()
    {

        moneyToday = 0;
          Money = 600;
        GameObject moneyObject = GameObject.Find("OwnMoney");
        if (moneyObject != null)
        {
            MoneyText = moneyObject.GetComponent<TMP_Text>();
        }
       
        UpdateMoneyUI();
        ChangeState(new StartMenuState());
     

    }
    public void setPlayerState(PlayerState playerState)
    {
        GameObject.Find("Player").GetComponent<PlayerControler>().currentState = playerState;
        

    }
    public void UpdateMoneyUI()
    {
        GameObject moneyObject = GameObject.Find("OwnMoney");
        if (moneyObject != null)
        {
            MoneyText = moneyObject.GetComponent<TMP_Text>();
        }
        if (MoneyText != null)
        {
            MoneyText.text = Money.ToString();
           

        }
       
    }

    public void AddMoney(int amount)
    {
        Money += amount;
        UpdateMoneyUI();
        // 可以在这里更新UI或其他逻辑
    }

    public bool TrySpendMoney(int amount)
    {
        if (Money >= amount)
        {
            Money -= amount;
            UpdateMoneyUI();
          
            return true;
        }
        else
        {
            Debug.Log("金钱不足，无法完成此次购买！");
            return false;
        }
    }


    public void MoneySettlement()
    {

        AddMoney(moneyToday);
        moneyToday = 0;
    }
    // 更改游戏状态的方法
    public void ChangeState(IGameState newState)
    {
        currentState?.OnExit();
        currentState = newState;
        currentState.OnEnter();
    }
}
