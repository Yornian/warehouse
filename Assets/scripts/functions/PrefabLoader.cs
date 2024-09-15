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
        // 使用 Resources.Load 加载预制体
       GameObject loadedPrefab = Resources.Load<GameObject>(prefabName);

        // 检查预制体是否成功加载
        if (loadedPrefab != null)
        {
            return loadedPrefab;
        }
        else
        {
            Debug.LogError("无法加载预制体: " + prefabName);
            return null;
        }
    }
}
