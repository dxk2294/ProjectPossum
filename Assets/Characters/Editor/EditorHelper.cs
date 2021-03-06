using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorHelper : MonoBehaviour
{
    [MenuItem("EditorHelper/SliceSprites")]
    static void SliceSprites()
    {
        // Change the below for the with and height dimensions of each sprite within the spritesheets
        int sliceWidth = 32;
        int sliceHeight = 32;

        // Change the below for the path to the folder containing the sprite sheets (warning: not tested on folders containing anything other than just spritesheets!)
        // Ensure the folder is within 'Assets/Resources/' (the below example folder's full path within the project is 'Assets/Resources/ToSlice')
        string folderPath = "Spritesheets/Eyes/Lipstick";

        Object[] spriteSheets = Resources.LoadAll(folderPath, typeof(Texture2D));
        Debug.Log("spriteSheets.Length: " + spriteSheets.Length);

        for (int z = 0; z < spriteSheets.Length; z++)
        {
            Debug.Log("z: " + z + " spriteSheets[z]: " + spriteSheets[z]);

            string path = AssetDatabase.GetAssetPath(spriteSheets[z]);
            TextureImporter ti = AssetImporter.GetAtPath(path) as TextureImporter;
            ti.isReadable = true;
            ti.spriteImportMode = SpriteImportMode.Multiple;

            List<SpriteMetaData> newData = new List<SpriteMetaData>();

            Texture2D spriteSheet = spriteSheets[z] as Texture2D;

            int numCols = spriteSheet.width / sliceWidth;
            int numRows = spriteSheet.height / sliceHeight;
            Debug.Log("Num Rows = " + numRows);
            Debug.Log("Num Cols = " + numCols);
            for (int i = 0; i < spriteSheet.width; i += sliceWidth)
            {
                for (int j = spriteSheet.height; j > 0; j -= sliceHeight)
                {
                    SpriteMetaData smd = new SpriteMetaData();
                    smd.pivot = new Vector2(0.5f, 0.5f);
                    smd.alignment = 9;
                    int row = (spriteSheet.height - j) / sliceHeight;
                    int col = i / sliceWidth;
                    //smd.name = (spriteSheet.height - j) / sliceHeight + ", " + ;
                    Debug.Log("Row = " + row);
                    Debug.Log("Col = " + col);
                    int index = (row * numCols) + col;
                    Debug.Log("Index = " + index);
                    smd.name = spriteSheet.name + "_" + (row * numCols + col);
                    smd.rect = new Rect(i, j - sliceHeight, sliceWidth, sliceHeight);

                    newData.Add(smd);
                }
            }

            ti.spritesheet = newData.ToArray();
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }
        Debug.Log("Done Slicing!");
    }
}