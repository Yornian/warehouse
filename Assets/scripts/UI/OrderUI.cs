using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderUI : MonoBehaviour
{
    public Transform contentTransform;
    public GameObject uiPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void initItemList(List<ItemStruct> itemStructs)
    {
        foreach (var item in itemStructs)
        {
           AddUIPrefab(item);


        }


    }


    public void AddUIPrefab(ItemStruct Struct)
    {
        if (uiPrefab != null && contentTransform != null)
        {
            // 实例化预制体
            GameObject uiInstance = Instantiate(uiPrefab);

            // 将实例化的对象设置为 Panel 的子对象
            uiInstance.transform.SetParent(contentTransform, false);
            uiInstance.GetComponent<MarketUISlot>().InitOrderSlot(Struct);
           
            // 选项：如果需要对实例化对象进行任何额外的设置，可以在这里完成
            // 例如：uiInstance.transform.localScale = Vector3.one;
        }
        else
        {
            Debug.LogWarning("UIPrefab or PanelTransform is not assigned.");
        }
    }
}
