using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class purchasePoint : MonoBehaviour,IInteractable
{
    public GameObject marketPanel;
    
    public void PerformInteraction(GameObject Player)
    {
        marketPanel.SetActive(true);
        GameManager.Instance.UpdateMoneyUI();
    }

    public void PerformInteractionClick(GameObject Player)
    {
        
    }

    public void PerformInteractionF(GameObject Player)
    {
       
    }

    public void PerformInteractionLongPress(GameObject Player)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
