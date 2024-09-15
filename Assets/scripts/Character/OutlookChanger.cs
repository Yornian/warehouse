using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlookChanger : MonoBehaviour
{
    public Dictionary<string, Sprite> hair = new Dictionary<string, Sprite>();
    public Dictionary<string, Sprite> outfit = new Dictionary<string, Sprite>();
    public Dictionary<string, Sprite> body = new Dictionary<string, Sprite>();
    public Dictionary<string, Sprite> accessories = new Dictionary<string, Sprite>();
    // Start is called before the first frame update
    void Start()
    {

        LoadSpritesIntoDictionary("png/character/Body/Body_01", 4,body);
        


    }
    void LoadSpritesIntoDictionary(string resourcePath, int maxSprites,  Dictionary<string, Sprite> dic)
    {
        // 从Resources文件夹中加载所有精灵
        Sprite[] sprites = Resources.LoadAll<Sprite>(resourcePath);

        // 检查路径是否正确以及是否有精灵被加载
        if (sprites.Length == 0)
        {
            Debug.LogWarning($"No sprites found at path: {resourcePath}. Please check the path correctness.");
            return;
        }

        // 通过循环将精灵添加到字典中，但不超过maxSprites指定的数量
        for (int i = 0; i < sprites.Length && i < maxSprites; i++)
        {
            // 这里假设每个精灵的名称是唯一的
            if (!dic.ContainsKey(sprites[i].name))
            {
                dic.Add(sprites[i].name, sprites[i]);
            }
            else
            {
                Debug.LogWarning($"Duplicate sprite name found: {sprites[i].name}. Skipping...");
            }
        }
    }
        // Update is called once per frame
        void Update()
    {
        
    }
}
