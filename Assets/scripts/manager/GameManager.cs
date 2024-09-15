using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ����ʵ��
    private static GameManager instance;
    private IGameState currentState;
    public int Money { get; private set; }
    // �����ľ�̬�������ڷ���ʵ��
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
                // �ڳ����в��� GameManager ʵ��
                instance = FindObjectOfType<GameManager>();

                // ���û���ҵ�ʵ��������һ���µ� GameObject ����� GameManager ���
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    // ��Ϸ״̬ö��
    public enum GameState { StartMenu, Playing, Paused, GameOver }
    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        // ȷ��ֻ��һ�� GameManager ʵ��
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ���� GameManager �ڼ����³���ʱ��������
        }
        else
        {
            Destroy(gameObject); // �����ظ��� GameManager ʵ��
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
        // �������������UI�������߼�
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
            Debug.Log("��Ǯ���㣬�޷���ɴ˴ι���");
            return false;
        }
    }


    public void MoneySettlement()
    {

        AddMoney(moneyToday);
        moneyToday = 0;
    }
    // ������Ϸ״̬�ķ���
    public void ChangeState(IGameState newState)
    {
        currentState?.OnExit();
        currentState = newState;
        currentState.OnEnter();
    }
}
