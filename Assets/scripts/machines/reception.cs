using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reception : MonoBehaviour,IInteractable
{
    
    public GameObject ProcessPanel;
    public GameObject StopPanel;
    public GameObject warnView;
    public void PerformInteraction(GameObject Player)
    {
        if (OrderManager.Instance.isWorking)
        {
            StopPanel.SetActive(true);
        }
        else if(OrderManager.Instance.ownItemList.Count==0)
        {
            warnView.SetActive(true);
            warnView.GetComponent<TypewriterEffect>().setText("You didn't order anything. No orders");
            warnView.GetComponent<TypewriterEffect>().StartTyping();
        }
        else if (OrderManager.Instance.FailedItemList.Count != 0)
        {
            warnView.SetActive(true);
            warnView.GetComponent<TypewriterEffect>().setText("You need to deal with the return first");
            warnView.GetComponent<TypewriterEffect>().StartTyping();
        }
        else
        { ProcessPanel.SetActive(true); }
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
