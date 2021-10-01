using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [Range(0, 100)] public float pointDensity;
    public int width = 100;
    public int height = 100;

    public int seed = 0;

    public float scale = 1;

    public float originX;
    public float originY;

    public int resolution = 10;

    private Texture2D noiseTexture;
    private Color[] pixels;
    private Renderer renderer;


    private float getPerlinValue(float x, float y)
    {
        float currentX = (originX + seed) + x / noiseTexture.width * scale;
        float currentY = (originY + seed) + y / noiseTexture.width * scale;

        return Mathf.PerlinNoise(currentX, currentY);
    }


    private void InstantiateCell(float sample, float x, float z)
    {
        Debug.Log(sample);
        float y = 0;
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);

        Renderer planeRenderer = plane.GetComponent<Renderer>();
        planeRenderer.material.SetColor("_Color", new Color(sample, sample, sample));
        plane.transform.position = new Vector3(x, y, z);



    }


    private void InitializeCells()
    {
        float y = 0.0F;

        while (y < noiseTexture.height)
        {
            float x = 0.0f; 
            while (x < noiseTexture.width)
            {
                // Perlin noise function
                //float sample = getPerlinValue(x,y);

                float sample = Random.value;

                InstantiateCell(sample, originX + x, originY + y);
                
                pixels[(int)y * noiseTexture.width + (int)x] = new Color(sample, sample, sample);
                x += (noiseTexture.width / resolution);
            }
            y += (noiseTexture.height / resolution);
        }
        noiseTexture.SetPixels(pixels);
        noiseTexture.Apply();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (seed == 0)
        {
            seed = Random.Range(0, 9999);
        }

        Random.InitState(seed);

        renderer = GetComponent<Renderer>();

        noiseTexture = new Texture2D(width, height);
        pixels = new Color[noiseTexture.width * noiseTexture.height];
        renderer.material.mainTexture = noiseTexture;

        InitializeCells();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
