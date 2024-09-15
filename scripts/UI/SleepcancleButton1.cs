using UnityEngine;
using TMPro; // ����TextMeshPro�����ռ�
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
        closePanel.SetActive(false); // �������
    }

    // ����������ɰ�ť��OnClick()�¼�����
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
