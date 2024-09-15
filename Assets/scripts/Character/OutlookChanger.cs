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
        // ��Resources�ļ����м������о���
        Sprite[] sprites = Resources.LoadAll<Sprite>(resourcePath);

        // ���·���Ƿ���ȷ�Լ��Ƿ��о��鱻����
        if (sprites.Length == 0)
        {
            Debug.LogWarning($"No sprites found at path: {resourcePath}. Please check the path correctness.");
            return;
        }

        // ͨ��ѭ����������ӵ��ֵ��У���������maxSpritesָ��������
        for (int i = 0; i < sprites.Length && i < maxSprites; i++)
        {
            // �������ÿ�������������Ψһ��
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
