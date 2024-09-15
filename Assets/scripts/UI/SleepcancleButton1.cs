using UnityEngine;
using TMPro; // 引入TextMeshPro命名空间
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class sleepCancleButton : MonoBehaviour
{
    public Button myButton;
    public GameObject newdayText;
    public bed bed;
    public GameObject closePanel;
    // Start is called before the first frame update
    void Start()
    {
        myButton.onClick.AddListener(OnButtonClick);
    }
    public void ClosePanelUI()
    {
        closePanel.SetActive(false); // 隐藏面板
    }

    // 这个方法将由按钮的OnClick()事件调用
    private IEnumerator DelayedAction(System.Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnButtonClick()
    {
        bed.settlementViewClosed();
        ClosePanelUI();
       
    }
}
