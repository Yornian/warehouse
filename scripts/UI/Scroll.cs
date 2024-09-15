using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroll : MonoBehaviour
{  // 引用到你要添加的预制体
    public GameObject uiPrefab;
    public GameObject BuildingPrefab;

    public Transform contentTransform;
    public RectTransform contentRectTransform;
    public RectTransform viewportRectTransform;
    public Scrollbar verticalScrollbar;
    public Scrollbar horizontalScrollbar; // 水平滚动条
    public VerticalLayoutGroup layoutGroup;
   
    private float contentHeight;
    private float contentWidth; 
    private float viewHeight;
    private float viewWidth;

    public bool ifUpdateToTop = false;//是否在每次添加物体后更新到边界

    private void Start()
    {
        
       
        UpdateDimensions();
       
    }
    public void ClearContent()
    {
        // 获取所有子对象
        foreach (Transform child in contentTransform)
        {
            // 销毁每个子对象
            Destroy(child.gameObject);
        }
    }

    void Update()
    {
        UpdateDimensions();
      
        UpdateContentPosition();
    }


    void UpdateContentPosition()
    {
        // 计算隐藏的内容高度（内容高度减去视口高度）
        float hiddenContentHeight;
        float hiddenContentWidth;
        if (verticalScrollbar!=null)
        {
            hiddenContentHeight = Mathf.Max(0, contentHeight - viewHeight);
        }
        else
        {
            hiddenContentHeight = 0;
        }
        if (horizontalScrollbar != null)
        {
           hiddenContentWidth = Mathf.Max(0, contentWidth - viewWidth);
        }
        else
        {
            hiddenContentWidth = 0;
        }
       
        // 如果没有隐藏的内容，则不需要更新位置
        if (hiddenContentHeight <= 0 && hiddenContentWidth <= 0)
        {
            contentRectTransform.anchoredPosition = Vector2.zero;
            return;
        }

        // 计算新的内容位置
        // 使用 scrollbar.value 确保滚动方向正确
        float newVerticalPosition=0;
        float newHorizontalPosition=0;
        if (verticalScrollbar != null)
        {
             newVerticalPosition = verticalScrollbar.value * hiddenContentHeight;
            contentRectTransform.anchoredPosition = new Vector2(contentRectTransform.anchoredPosition.x, newVerticalPosition);
        }
      
        if (horizontalScrollbar != null)
        {
             newHorizontalPosition = horizontalScrollbar.value * hiddenContentWidth;
            contentRectTransform.anchoredPosition = new Vector2(newHorizontalPosition, contentRectTransform.anchoredPosition.y);

        }

   

     
    }

    void UpdateDimensions()
    {
        // 获取视窗和内容的高度
        viewHeight = viewportRectTransform.rect.height;
        viewWidth = viewportRectTransform.rect.width;
        contentWidth = contentRectTransform.rect.width;
        contentHeight = contentRectTransform.rect.height;
        if (verticalScrollbar != null)
        {
            verticalScrollbar.interactable = contentHeight > viewHeight;
        }

        if (horizontalScrollbar != null)
        {
            horizontalScrollbar.interactable = contentWidth > viewWidth;

        }
       
    }


    public void AddBuildingMarketUIPrefab(BuildingInfo Struct)
    {
        if (BuildingPrefab != null && contentTransform != null)
        {
            // 实例化预制体
            GameObject uiInstance = Instantiate(BuildingPrefab);
            
            // 将实例化的对象设置为 Panel 的子对象
            uiInstance.transform.SetParent(contentTransform, false);
            uiInstance.GetComponent<BuildingMarketUISlot>().InitSlot(Struct);

        }
        else
        {
            Debug.LogWarning("UIPrefab or PanelTransform is not assigned.");
        }
    }

    public void AddItemUIPrefab(ItemStruct Struct)
    {
        if (uiPrefab != null && contentTransform != null)
        {
            // 实例化预制体
            GameObject uiInstance = Instantiate(uiPrefab);

            // 将实例化的对象设置为 Panel 的子对象
            uiInstance.transform.SetParent(contentTransform, false);
            uiInstance.GetComponent<MarketUISlot>().InitItemSlot(Struct);
            UpdateDimensions();
            // 选项：如果需要对实例化对象进行任何额外的设置，可以在这里完成
            // 例如：uiInstance.transform.localScale = Vector3.one;
        }
        else
        {
            Debug.LogWarning("UIPrefab or PanelTransform is not assigned.");
        }
    }
   
   
    public void AddOrderUIPrefab(List<ItemStruct> listOfOrders)
    {
       
            if (uiPrefab != null && contentTransform != null)
            {
                // 实例化预制体
                GameObject uiInstance = Instantiate(uiPrefab);
                uiInstance.transform.SetParent(contentTransform, false);

                uiInstance.GetComponent<OrderUI>().initItemList(listOfOrders);




            }

            UpdateDimensions();
        }
     
    
}
