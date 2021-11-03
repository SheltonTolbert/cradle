using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{

    [Range(0, 10)] public float scale = 1;

    public Cell cell;
    public bool regenerate = false;
    public bool soloCell = false;
    public float noiseScale = 1;
    public float originX;
    public float originY;
    public int biomeIndex = -1;
    public int resolution = 10;
    public int seed = 0;

    private Cell[] cells;
    private Color[] pixels;
    private Renderer meshRenderer;
    private Texture2D noiseTexture;
    private int height = 100;
    private int width = 100;
    private int gridCellsHigh = 3;
    private int gridCellsWide = 3;


    /* @docs 
    ## public float getPerlinValue(float x, float y)

    Returns a coherent noise value between 0 and 1 based on the origin, seed and noise scale of the generator
    */

    public float GetPerlinValue(float x, float y)
    {
        float currentX = (originX + (float)seed) + x;
        float currentY = (originY + (float)seed) + y;

        return Mathf.PerlinNoise(currentX, currentY);
    }

    /* @docs
    ## public (int,int) GetDimensions()

    returns a tuple with the demensions of the grid
    */
    public (int, int) GetDimensions()
    {
        (int gridWidth, int gridHeight) gridDimensions = (gridCellsWide * width, gridCellsHigh * height);

        return gridDimensions;
    }

    /* @docs 
    ##private void InstantiateCell(float sample, float x, float z)

    Doesn't do anything at the moment. This is a test of the pre-commit hook
    */

    private void InstantiateCell(float sample, float x, float z)
    {

        float y = 0;
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);

        Renderer planeRenderer = plane.GetComponent<Renderer>();
        planeRenderer.material.SetColor("_Color", new Color(sample, sample, sample));
        plane.transform.position = new Vector3(x, y, z);
    }


    private void InitializeCells()
    {
        if (soloCell)
        {
            gridCellsHigh = 1;
            gridCellsWide = 1;
        }
        else
        {
            // Biomes should not be specified in grid view
            biomeIndex = -1;
        }

        cells = new Cell[gridCellsHigh * gridCellsWide];

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
        for (int z = 0; z < gridCellsHigh; z++)
        {
            for (int x = 0; x < gridCellsWide; x++)
            {
                cells[i] = Instantiate(cell, new Vector3(0, 0, 0), Quaternion.identity, transform);
                cells[i].transform.parent = transform;
                cells[i].name = "Cell(" + x + "," + z + ")";
                cells[i].SetSize(width, scale);
                cells[i].SetOrigin(new Vector3(width * x * scale, 0, height * z * scale));
                cells[i].GenerateMesh();
                cells[i].SetBiome(biomeIndex);
                cells[i].SetDebugColor(cellColors[i]);
                cells[i].RenderBiome();
                i++;
            }
        }
    }

    private void InitializeData()
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
    }


    public void DestroyAllChildren()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeData();
        InitializeCells();
    }

    // Update is called once per frame
    void Update()
    {
        if (regenerate)
        {
            DestroyAllChildren();
            Start();

            regenerate = false;
        }
    }
}
