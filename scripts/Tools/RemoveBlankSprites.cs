#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class RemoveBlankSprites : ScriptableObject
{
    [MenuItem("Tools/Remove Blank Sprites")]
    static void RemoveBlanks()
    {
        string folderPath = "Assets/Resources/png/character/Body"; // ���������ļ���·��
        var spritePaths = Directory.GetFiles(folderPath, "*.png", SearchOption.AllDirectories);

        foreach (var spritePath in spritePaths)
        {
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);
            if (sprite != null && IsSpriteBlank(sprite))
            {
                AssetDatabase.DeleteAsset(spritePath);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    static bool IsSpriteBlank(Sprite sprite)
    {
        // ע�⣺�˷�����Ҫ��ȡ/д��Ȩ���ڵ�������������
        string path = AssetDatabase.GetAssetPath(sprite.texture);
        TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;

        if (importer != null && importer.isReadable)
        {
            // Check the pixels of the sprite, return true if all are transparent
            // Warning: This can be a resource-intensive operation
            Color[] pixels = sprite.texture.GetPixels(
                (int)sprite.textureRect.x,
                (int)sprite.textureRect.y,
                (int)sprite.textureRect.width,
                (int)sprite.textureRect.height);

            return pixels.All(color => color.a == 0);
        }

        return false;
    }
}
#endif
