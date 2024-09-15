using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class WorkingView : MonoBehaviour
{
    public Button ProcessButton;
    public Button CancleButton;
    public GameObject Panel;
  
    // Start is called before the first frame update
    void Start()
    {
        if(ProcessButton!=null)
        { ProcessButton.onClick.AddListener(OnProcessButtonClick); }
        if (CancleButton != null)
        { CancleButton.onClick.AddListener(OnCancleButtonClick); }
            
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnProcessButtonClick()
    {

        OrderManager.Instance.StartWorking();

        ClosePanelUI();
    }
    public void OnCancleButtonClick()
    {

        OrderManager.Instance.StopWorking();

        ClosePanelUI();
    }
    public void ClosePanelUI()
    {
        Panel.SetActive(false); // Òþ²ØÃæ°å
    }
}
