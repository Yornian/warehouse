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
            // ʵ����Ԥ����
            GameObject uiInstance = Instantiate(uiPrefab);

            // ��ʵ�����Ķ�������Ϊ Panel ���Ӷ���
            uiInstance.transform.SetParent(contentTransform, false);
            uiInstance.GetComponent<MarketUISlot>().InitOrderSlot(Struct);
           
            // ѡ������Ҫ��ʵ������������κζ�������ã��������������
            // ���磺uiInstance.transform.localScale = Vector3.one;
        }
        else
        {
            Debug.LogWarning("UIPrefab or PanelTransform is not assigned.");
        }
    }
}
