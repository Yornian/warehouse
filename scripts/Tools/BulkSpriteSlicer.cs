using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

using UnityEngine.U2D.Interface;
using UnityEditor.U2D.Sprites;

public class BulkSpriteSlicer
{
    [MenuItem("Tools/Bulk Sprite Slicer")]
    [System.Obsolete]
    static void SliceSprites()
    {
        Texture2D[] textures = Selection.GetFiltered<Texture2D>(SelectionMode.DeepAssets);

        foreach (Texture2D texture in textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            var importer = AssetImporter.GetAtPath(path) as TextureImporter;

            importer.spriteImportMode = SpriteImportMode.Multiple;

            var spritesheet = new List<SpriteMetaData>();
            int sliceWidth = 48;
            int sliceHeight = 96;
            int horizontalSlices = texture.width / sliceWidth;
            int verticalSlices = texture.height / sliceHeight;
            int num = 0;
            for (int y = 0; y < verticalSlices; y++)
            {
                for (int x = 0; x < horizontalSlices; x++)
                {
                    num++;
                    SpriteMetaData metaData = new SpriteMetaData
                    {
                       
                        pivot = new Vector2(0f, 1f), // Pivot in top-left
                        alignment = (int)SpriteAlignment.Custom,
                        rect = new Rect(x * sliceWidth, (verticalSlices - y - 1) * sliceHeight, sliceWidth, sliceHeight),
                        name = $"{texture.name}_{num}"
                    };

                    spritesheet.Add(metaData);
                }
            }

            importer.spritesheet = spritesheet.ToArray();
            EditorUtility.SetDirty(importer);
            importer.SaveAndReimport();
        }

        AssetDatabase.Refresh();
    }
}
