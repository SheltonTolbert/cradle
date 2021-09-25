using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [Range(0, 100)] public float pointDensity;
    public int width;
    public int height;

    public float seed = 0.0f;

    public float scale = 1;

    public float originX;
    public float originY;

    private Texture2D noiseTexture;
    private Color[] pixels;
    private Renderer renderer;

    void CalculateNoise()
    {
        float y = 0.0F;

        while (y < noiseTexture.height)
        {
            float x = 0.0f; 
            while (x < noiseTexture.width)
            {
                float currentX = (originX + seed) + x / noiseTexture.width * scale;
                float currentY = (originY + seed) + y / noiseTexture.width * scale;

                float sample = Mathf.PerlinNoise(currentX, currentY);
                pixels[(int)y * noiseTexture.width + (int)x] = new Color(sample, sample, sample);
                x++;
            }
            y++;
        }
        noiseTexture.SetPixels(pixels);
        noiseTexture.Apply();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (seed == 0)
        {
            seed = Random.Range(0.0f, 9999999.0f);
        }

        renderer = GetComponent<Renderer>();

        noiseTexture = new Texture2D(width, height);
        pixels = new Color[noiseTexture.width * noiseTexture.height];
        renderer.material.mainTexture = noiseTexture;
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateNoise();
    }
}
