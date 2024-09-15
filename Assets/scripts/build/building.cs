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
        // ���Ի�ȡ BoxCollider2D ���
        BoxCollider2D collider =GetComponent<BoxCollider2D>();

        // ����Ƿ�ɹ���ȡ���� BoxCollider2D ���
        if (collider != null)
        {
            // ���� isTrigger ����
       
            collider.isTrigger = isTrigger;
            Debug.Log(collider.isTrigger);
        }
        else
        {
            // ���û���ҵ� BoxCollider2D ��������������Ϣ
            Debug.LogError("BoxCollider2D not found  " );
        }
    }
}
