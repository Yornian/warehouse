using UnityEngine;
using TMPro; // ����TextMeshPro�����ռ�
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
   

    // ����������ɰ�ť��OnClick()�¼�����
    public void ClosePanelUI()
    {
        closePanel.SetActive(false); // �������
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
