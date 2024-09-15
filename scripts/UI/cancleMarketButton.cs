using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cancleMarketButton : MonoBehaviour
{
    public Button myButton;
    public GameObject closePanel;
    // Start is called before the first frame update
    void Start()
    {
        myButton.onClick.AddListener(OnButtonClick);
    }


    // 这个方法将由按钮的OnClick()事件调用
    public void ClosePanelUI()
    {
        closePanel.SetActive(false); // 隐藏面板
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void OnButtonClick()
    {


        OrderManager.Instance.DeliverGoods();
        ClosePanelUI();
    }
}
