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
    private Renderer meshRenderer;
    public Cell cell;
    private Cell[] cells;

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
        cells = new Cell[9];
        
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i] = Instantiate(cell, new Vector3(0,0,0), Quaternion.identity);
            cells[i].SetSize(width);
        }

        cells[1].transform.Translate(width, 0, 0);
        cells[1].SetDebugColor(Color.red);

        cells[2].transform.Translate(width * 2, 0, 0);
        cells[2].SetDebugColor(Color.blue);

        cells[3].transform.Translate(0 , 0, width);
        cells[3].SetDebugColor(Color.green);

        cells[4].transform.Translate(width, 0, width);
        cells[4].SetDebugColor(Color.cyan);

        cells[5].transform.Translate(width * 2, 0, width);
        cells[5].SetDebugColor(Color.yellow);

        cells[6].transform.Translate(0, 0, width * 2);
        cells[6].SetDebugColor(Color.magenta);

        cells[7].transform.Translate(width, 0, width * 2);
        cells[7].SetDebugColor(Color.grey);

        cells[8].transform.Translate(width * 2, 0, width * 2);
        cells[8].SetDebugColor(Color.red);

    }

    // Start is called before the first frame update
    void Start()
    {
        if (seed == 0)
        {
            seed = Random.Range(0, 9999);
        }

        Random.InitState(seed);

        meshRenderer = GetComponent<Renderer>();

        noiseTexture = new Texture2D(width, height);
        pixels = new Color[noiseTexture.width * noiseTexture.height];
        meshRenderer.material.mainTexture = noiseTexture;

        InitializeCells();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
