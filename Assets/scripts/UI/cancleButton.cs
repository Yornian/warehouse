using UnityEngine;
using TMPro; // 引入TextMeshPro命名空间
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class cancle : MonoBehaviour
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
        
      

        ClosePanelUI();
    }
}
