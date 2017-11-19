using UnityEngine;
using UnityEditor;
using System.Linq;
using System.IO;

public class TextureGenerator
{
    [MenuItem("Assets/Create/Dither Textures")]
    static void CreateDitherTextures()
    {
        CreateTexture("Bayer2x2", new int [] {
            0, 2,
            3, 1
        });

        CreateTexture("Bayer3x3", new int [] {
            0, 7, 3,
            6, 5, 2,
            4, 1, 8
        });

        CreateTexture("Bayer4x4", new int [] {
             0,  8,  2, 10,
            12,  4, 14,  6,
             3, 11,  1,  9,
            15,  7, 13,  5
        });

        CreateTexture("Bayer8x8", new int [] {
            0, 48, 12, 60,  3, 51, 15, 63,
           32, 16, 44, 28, 35, 19, 47, 31,
            8, 56,  4, 52, 11, 59,  7, 55,
           40, 24, 36, 20, 43, 27, 39, 23,
            2, 50, 14, 62,  1, 49, 13, 61,
           34, 18, 46, 30, 33, 17, 45, 29,
           10, 58,  6, 54,  9, 57,  5, 53,
           42, 26, 38, 22, 41, 25, 37, 21
        });
    }

    static void CreateTexture(string name, int [] matrix)
    {
        var bmp = matrix.Select(x => (byte)((x + 0.5f) * 256 / matrix.Length)).
                         Select(x => new Color32(x, x, x, x));

        var width = (int)Mathf.Sqrt(matrix.Length);
        var texture = new Texture2D(width, width);
        texture.SetPixels32(bmp.ToArray());
        texture.Apply();

        var bytes = ImageConversion.EncodeToPNG(texture);
        File.WriteAllBytes("Assets/" + name + ".png", bytes);

        Texture2D.DestroyImmediate(texture);
    }
}
