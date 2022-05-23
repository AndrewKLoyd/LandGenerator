using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandGen : MonoBehaviour
{
    public Texture2D Texture
    {
        get
        {
            Texture2D texture = new Texture2D(256, 256, TextureFormat.RGBA32, 0, true);

            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    float xCoord = xShift + (float)x / (float)texture.width * textureScale;
                    float yCoord = yShift + (float)y / (float)texture.height * textureScale;
                    float sample = Mathf.PerlinNoise(xCoord, yCoord);
                    Color pointColor = new Color(sample, sample, sample);
                    texture.SetPixel(x, y, pointColor);
                }
            }
            texture.Apply();
            return texture;
        }
    }

    public int yShift;
    public int xShift;
    public int textureScale;


    public int minHG;
    public int maxHG;

}
