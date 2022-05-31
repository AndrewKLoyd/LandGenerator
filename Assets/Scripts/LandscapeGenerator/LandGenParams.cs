using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandGenParams : ScriptableObject
{
    public int yShift;              // Texture "Y" axis shift 
    public int xShift;              // Texture "X" axis shift 
    public int textureScale;       // Texture scale

    public int worldSize;         // Size of the whole land "X" and "Z"

    public int minHG;              // Minimun hg of the land
    public int maxHG;            // Maximum hg of the land
}
