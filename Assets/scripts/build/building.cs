using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int width;
    public int height;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetCollider(bool isTrigger)
    {
        // 尝试获取 BoxCollider2D 组件
        BoxCollider2D collider =GetComponent<BoxCollider2D>();

        // 检查是否成功获取到了 BoxCollider2D 组件
        if (collider != null)
        {
            // 设置 isTrigger 属性
       
            collider.isTrigger = isTrigger;
            Debug.Log(collider.isTrigger);
        }
        else
        {
            // 如果没有找到 BoxCollider2D 组件，输出错误信息
            Debug.LogError("BoxCollider2D not found  " );
        }
    }
}
