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


    /* @docs 
    ## private float getPerlinValue(float x, float y)

    Returns a coherent noise value between 0 and 1 based on the origin, seed and noise scale of the generator
    */

    private float getPerlinValue(float x, float y)
    {
        float currentX = (originX + seed) + x / noiseTexture.width * noiseScale;
        float currentY = (originY + seed) + y / noiseTexture.width * noiseScale;

        return Mathf.PerlinNoise(currentX, currentY);
    }

    /* @docs 
    private void InstantiateCell(float sample, float x, float z)

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
        int gridCellsWidth = 3;
        int gridCellsHeight = 3;

        if (soloCell)
        {
            gridCellsHeight = 1;
            gridCellsWidth = 1;
        }
        else
        {
            // Biomes should not be specified in grid view
            biomeIndex = -1;
        }

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
        if (regenerate)
        {
            regenerate = false;
            Start();
        }
    }
}
