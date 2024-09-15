using UnityEngine;
using System.IO;

public class CharacterDressUp : MonoBehaviour
{
    public Texture2D characterBody; // 角色body的图像
    public Texture2D characterClothing; // 角色服装的图像

    void Start()
    {
        Texture2D dressedCharacter = ApplyClothingToCharacter(characterBody, characterClothing);
        SaveTextureToDisk(dressedCharacter, "DressedCharacter.png");
    }

    Texture2D ApplyClothingToCharacter(Texture2D body, Texture2D clothing)
    {
        // 确保服装和body图像有相同的尺寸
        if (body.width != clothing.width || body.height != clothing.height)
        {
            Debug.LogError("Body and clothing textures must have the same dimensions.");
            return null;
        }

        // 创建一个新的Texture2D用于存放结果
      
        Texture2D result = new Texture2D(body.width, body.height, TextureFormat.RGBA32, false);
        Graphics.CopyTexture(body, result);



        // 应用服装的像素到body图像
        for (int x = 0; x < body.width; x++)
        {
            for (int y = 0; y < body.height; y++)
            {
                Color bodyColor = result.GetPixel(x, y);
                Color clothingColor = clothing.GetPixel(x, y);
                // 使用服装的alpha来决定是否要覆盖body的像素
                if (clothingColor.a > 0)
                {
                    result.SetPixel(x, y, clothingColor);
                }
            }
        }

        // 应用所有像素更改
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
