using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroll : MonoBehaviour
{  // ���õ���Ҫ��ӵ�Ԥ����
    public GameObject uiPrefab;
    public GameObject BuildingPrefab;

    public Transform contentTransform;
    public RectTransform contentRectTransform;
    public RectTransform viewportRectTransform;
    public Scrollbar verticalScrollbar;
    public Scrollbar horizontalScrollbar; // ˮƽ������
    public VerticalLayoutGroup layoutGroup;
   
    private float contentHeight;
    private float contentWidth; 
    private float viewHeight;
    private float viewWidth;

    public bool ifUpdateToTop = false;//�Ƿ���ÿ������������µ��߽�

    private void Start()
    {
        
       
        UpdateDimensions();
       
    }
    public void ClearContent()
    {
        // ��ȡ�����Ӷ���
        foreach (Transform child in contentTransform)
        {
            // ����ÿ���Ӷ���
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
        // �������ص����ݸ߶ȣ����ݸ߶ȼ�ȥ�ӿڸ߶ȣ�
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
       
        // ���û�����ص����ݣ�����Ҫ����λ��
        if (hiddenContentHeight <= 0 && hiddenContentWidth <= 0)
        {
            contentRectTransform.anchoredPosition = Vector2.zero;
            return;
        }

        // �����µ�����λ��
        // ʹ�� scrollbar.value ȷ������������ȷ
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
        // ��ȡ�Ӵ������ݵĸ߶�
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
            // ʵ����Ԥ����
            GameObject uiInstance = Instantiate(BuildingPrefab);
            
            // ��ʵ�����Ķ�������Ϊ Panel ���Ӷ���
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
            // ʵ����Ԥ����
            GameObject uiInstance = Instantiate(uiPrefab);

            // ��ʵ�����Ķ�������Ϊ Panel ���Ӷ���
            uiInstance.transform.SetParent(contentTransform, false);
            uiInstance.GetComponent<MarketUISlot>().InitItemSlot(Struct);
            UpdateDimensions();
            // ѡ������Ҫ��ʵ������������κζ�������ã��������������
            // ���磺uiInstance.transform.localScale = Vector3.one;
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
                // ʵ����Ԥ����
                GameObject uiInstance = Instantiate(uiPrefab);
                uiInstance.transform.SetParent(contentTransform, false);

                uiInstance.GetComponent<OrderUI>().initItemList(listOfOrders);




            }

            UpdateDimensions();
        }
     
    
}
