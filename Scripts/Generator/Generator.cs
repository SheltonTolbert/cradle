using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [Range(0, 10)] public float scale = 1;
    private int width = 100;
    private int height = 100;


    public int seed = 0;

    public float noiseScale = 1;

    public float originX;
    public float originY;

    public int resolution = 10;
    public bool regenerate = false;

    private Texture2D noiseTexture;
    private Color[] pixels;
    private Renderer meshRenderer;
    public Cell cell;
    private Cell[] cells;

    private float getPerlinValue(float x, float y)
    {
        float currentX = (originX + seed) + x / noiseTexture.width * noiseScale;
        float currentY = (originY + seed) + y / noiseTexture.width * noiseScale;

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
        int gridCellsWidth = 3;
        int gridCellsHeight = 3;

        cells = new Cell[gridCellsHeight * gridCellsWidth];
        Color[] cellColors = new Color[9]
        {
            Color.red,
            Color.blue,
            Color.green,
            Color.cyan,
            Color.yellow,
            Color.magenta,
            Color.grey,
            Color.red,
            Color.white
        };

        int i = 0;
        for (int z = 0; z < gridCellsHeight; z++)
        {
            for (int x = 0; x < gridCellsWidth; x++)
            {
                cells[i] = Instantiate(cell, new Vector3(0, 0, 0), Quaternion.identity, transform);
                cells[i].SetSize(width, scale);
                cells[i].SetOrigin(new Vector3(width * x * scale, 0 ,height * z * scale));
                cells[i].GenerateMesh();
                cells[i].SetBiome();
                cells[i].SetDebugColor(cellColors[i]);
                cells[i].RenderBiome();
                i++;
            }
        }
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
        if( regenerate)
        {
            regenerate = false;
            Start();
        }
    }
}
