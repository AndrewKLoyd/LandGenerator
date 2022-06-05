using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandGenParams
{
    public Texture2D texture;
    public NoiseType noiseType;
    public long seed = 0;
    public int yShift = 0;              // Texture "Y" axis shift 
    public int xShift = 0;              // Texture "X" axis shift 
    public int zShift = 0;
    public int textureScale = 2;        // Texture scale

    public int worldSize = 500;         // Size of the whole land "X" and "Z"

    public int minHG = 0;               // Minimun hg of the land
    public int maxHG = 100;             // Maximum hg of the land
}
