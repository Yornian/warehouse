using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject LoadPrefab(string prefabName)
    {
        // ʹ�� Resources.Load ����Ԥ����
       GameObject loadedPrefab = Resources.Load<GameObject>(prefabName);

        // ���Ԥ�����Ƿ�ɹ�����
        if (loadedPrefab != null)
        {
            return loadedPrefab;
        }
        else
        {
            Debug.LogError("�޷�����Ԥ����: " + prefabName);
            return null;
        }
    }
}
