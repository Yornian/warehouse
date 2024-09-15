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
        SetPreviewActive(true); // ����Ԥ��Ϊ�״̬
    }
    public void build()
    {
        SetTransparency(previewInstance, 1.0f);
        SetTrigger(previewInstance, false);
        previewInstance = null;

      
    }
    public void ClearPreview()
    {
        if (previewInstance != null) // ���� previewInstance �ǽ���Ԥ����ʵ��
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
        // ���Ի�ȡ BoxCollider2D ���
        BoxCollider2D collider = obj.GetComponent<BoxCollider2D>();

        // ����Ƿ�ɹ���ȡ���� BoxCollider2D ���
        if (collider != null)
        {
            // ���� isTrigger ����
            collider.isTrigger = isTrigger;
        }
        else
        {
            // ���û���ҵ� BoxCollider2D ��������������Ϣ
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
