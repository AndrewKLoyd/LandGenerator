using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TerrainGenerator
{

    public class NoiseParams
    {
        public static Dictionary<System.Guid, NoiseParams> Parameters = new Dictionary<System.Guid, NoiseParams>();
        #region Vars
        private Texture2D texture = new Texture2D(256, 256);
        private float frequency = 1;
        private int octaves = 1;
        private float redistribution = 1;
        #endregion

        #region Props

        public Texture2D Texture
        {
            get
            {
                if (texture == null) GenerateTexture();
                return texture;
            }
        }
        public bool Invert { get; set; }

        public float XShift { get; set; }
        public float YShift { get; set; }
        public float Frequency
        {
            get => frequency;
            set
            {
                frequency = value > 0 ? value : frequency;
            }
        }

        public int Octaves
        {
            get => octaves;
            set
            {
                octaves = value >= 1 ? value : octaves;
            }
        }

        public float Redistribution
        {
            get => redistribution;
            set
            {
                redistribution = (value > 0.001 && value < 10) ? value : redistribution;
            }
        }

        #endregion
        #region Ctors
        public NoiseParams()
        {
            XShift = 0;
            YShift = 0;
            Invert = false;
        }

        //TODO: Write copy ctor
        public NoiseParams(NoiseParams noise)
        {
            XShift = 0;
            YShift = 0;
            Invert = false;
            texture = CopyTexture(noise.Texture);
        }
        #endregion

        #region Methods
        #region StaticMethods

        public static float InvertValue(float value) => 1 - value;
        public static Texture2D CopyTexture(Texture2D texture)
        {
            Texture2D finTexture = new Texture2D(256, 256);

            for (int i = 0; i < texture.width; i++)
            {
                for (int j = 0; j < texture.height; j++)
                {
                    finTexture.SetPixel(i, j, texture.GetPixel(i, j));
                }
            }
            finTexture.Apply();
            return finTexture;
        }
        #endregion

        #region PublicMethods
        public void RegenerateTexture() => texture = null;

        #endregion

        #region PrivateMethods
        private void GenerateTexture()
        {
            texture = new Texture2D(256, 256, TextureFormat.RGBA32, 0, true);

            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    float xCoord = XShift + (float)x / (float)texture.width * Frequency;
                    float yCoord = YShift + (float)y / (float)texture.height * Frequency;
                    //float sample = Mathf.PerlinNoise(xCoord, yCoord);
                    float sample = CalcElevationValue(xCoord, yCoord);
                    if (Invert) sample = InvertValue(sample);
                    Color pointColor = new Color(sample, sample, sample);
                    texture.SetPixel(x, y, pointColor);
                }
            }
            texture.Apply();
        }

        private float CalcElevationValue(float x, float y)
        {
            float sample = 0f;
            float amplitudeSum = 0f;
            for (int i = 0; i < octaves; i++)
            {
                float amplitude = 1 / (float)(i + 1);
                amplitudeSum += amplitude;
                sample += amplitude * Mathf.PerlinNoise((float)(i + 1) * x, (float)(i + 1) * y);
            }
            sample /= amplitudeSum;
            sample = Mathf.Pow(sample, redistribution);
            return sample;
        }

        #endregion

        #endregion
    }
}