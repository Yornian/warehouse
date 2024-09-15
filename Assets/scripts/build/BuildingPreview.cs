using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPreview : MonoBehaviour
{
    private GameObject previewInstance;

    public void CreatePreview(GameObject buildingPrefab)
    {
        if (previewInstance != null)
        {
            Destroy(previewInstance);
        }

        previewInstance = Instantiate(buildingPrefab);
        SetTrigger(previewInstance, true);
        SetTransparency(previewInstance, 0.9f);
        SetPreviewActive(true); // 设置预览为活动状态
    }
    public void build()
    {
        SetTransparency(previewInstance, 1.0f);
        SetTrigger(previewInstance, false);
        previewInstance = null;

      
    }
    public void ClearPreview()
    {
        if (previewInstance != null) // 假设 previewInstance 是建筑预览的实例
        {
            Destroy(previewInstance);
            previewInstance = null;
        }
    }

    private void SetTransparency(GameObject obj, float alpha)
    {
       SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
       
        if(spriteRenderer!=null)
        {
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }

            
        
    }
    
    public void SetPreviewPosition(Vector3 position)
    {
        if (previewInstance != null)
        {
            previewInstance.transform.position = position;
        }
    }
    public void SetTrigger(GameObject obj, bool isTrigger)
    {
        // 尝试获取 BoxCollider2D 组件
        BoxCollider2D collider = obj.GetComponent<BoxCollider2D>();

        // 检查是否成功获取到了 BoxCollider2D 组件
        if (collider != null)
        {
            // 设置 isTrigger 属性
            collider.isTrigger = isTrigger;
        }
        else
        {
            // 如果没有找到 BoxCollider2D 组件，输出错误信息
            Debug.LogError("BoxCollider2D not found on " + obj.name);
        }
    }
    public void SetPreviewActive(bool isActive)
    {
        if (previewInstance != null)
        {
            previewInstance.SetActive(isActive);
        }
    }

    
}
