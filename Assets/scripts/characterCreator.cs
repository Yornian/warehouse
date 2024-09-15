using UnityEngine;
using System.IO;

public class CharacterDressUp : MonoBehaviour
{
    public Texture2D characterBody; // ��ɫbody��ͼ��
    public Texture2D characterClothing; // ��ɫ��װ��ͼ��

    void Start()
    {
        Texture2D dressedCharacter = ApplyClothingToCharacter(characterBody, characterClothing);
        SaveTextureToDisk(dressedCharacter, "DressedCharacter.png");
    }

    Texture2D ApplyClothingToCharacter(Texture2D body, Texture2D clothing)
    {
        // ȷ����װ��bodyͼ������ͬ�ĳߴ�
        if (body.width != clothing.width || body.height != clothing.height)
        {
            Debug.LogError("Body and clothing textures must have the same dimensions.");
            return null;
        }

        // ����һ���µ�Texture2D���ڴ�Ž��
      
        Texture2D result = new Texture2D(body.width, body.height, TextureFormat.RGBA32, false);
        Graphics.CopyTexture(body, result);



        // Ӧ�÷�װ�����ص�bodyͼ��
        for (int x = 0; x < body.width; x++)
        {
            for (int y = 0; y < body.height; y++)
            {
                Color bodyColor = result.GetPixel(x, y);
                Color clothingColor = clothing.GetPixel(x, y);
                // ʹ�÷�װ��alpha�������Ƿ�Ҫ����body������
                if (clothingColor.a > 0)
                {
                    result.SetPixel(x, y, clothingColor);
                }
            }
        }

        // Ӧ���������ظ���
        result.Apply();

        return result;
    }

    void SaveTextureToDisk(Texture2D texture, string fileName)
    {
        byte[] bytes = texture.EncodeToPNG();
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllBytes(filePath, bytes);
        Debug.Log($"Saved dressed character image to {filePath}");
    }
}
