using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiManager : MonoBehaviour
{
    public GameObject warn;
    public GameObject market;
    public GameObject order;
    public Scroll marketScroll;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void showWarn(string txt)
    {
        GameObject waenGameObject = GameObject.Find("warnView");

        waenGameObject.SetActive(true);
        waenGameObject.GetComponent<TypewriterEffect>().setText(txt);
        waenGameObject.GetComponent<TypewriterEffect>().StartTyping();

    }
    public void InitMarketTotalListInUI(List<ItemStruct> TotalItemList)
    {
        GameObject marketGameObject = GameObject.Find("market");
        marketGameObject.GetComponent<Scroll>().ClearContent();
        if (TotalItemList != null && TotalItemList.Count != 0)
        {
            foreach (ItemStruct item in TotalItemList)
            {
                marketGameObject.GetComponent<Scroll>().AddItemUIPrefab(item);

            }
        }

        //

    }
}
