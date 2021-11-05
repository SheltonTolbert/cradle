using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* @docs 
### Generator

---

The Generator class is responsible for generating and managing the cells which make up the game environment. 
The Generator class contains the following public variables: 

```
- Cell cell
    - The Cell should be populated with a Cell that is a component of a prefab. This way default biomes can be set by the user.
- Bool regenerate
    - This Boolean serves a toggle for regenerating the grid at runtime. This is useful when changing seeds, boundaries and other rendering variables.
- Bool soloCell 
    - This Boolean acts as a toggle for only generating one cell, rather than generating the 9 in a standard grid. This is useful for debugging. 
- Float noiseScale
    - This variable controls the horizontal (x,z) scale of the perlin noise function.
- Float originX
    - The x coordinate of the grid.
- Float originZ
    - The z coordinate of the grid.
- Int biomeIndex
    - Enables the specification of a biome to render, based on the possible biomes array of the associated cell. Only works in solo cell mode. 
- Int resolution
    - A scalar value which controls the resolution of the terrain mesh. 
- int seed
    - Global seed value for generating predictable random numbers. Useful for creating reproduceable procedural environments. 

```

*/
public class Generator : MonoBehaviour
{

    [Range(0, 10)] public float scale = 1;

    public Cell cell;
    public bool regenerate = false;
    public bool soloCell = false;
    public float noiseScale = 1;
    public float originX;
    public float originZ;
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
    # public float getPerlinValue(float x, float y)

    > Returns a coherent noise value between 0 and 1 based on the origin, seed and noise scale of the generator
    */

    public float GetPerlinValue(float x, float y)
    {
        float currentX = (originX + (float)seed) + x;
        float currentY = (originZ + (float)seed) + y;

        return Mathf.PerlinNoise((float)currentX, (float)currentY);
    }

    /* @docs
    # public (int,int) GetDimensions()

    > returns a tuple with the demensions of the grid
    */
    public (int, int) GetDimensions()
    {
        (int gridWidth, int gridHeight) gridDimensions = (gridCellsWide * width, gridCellsHigh * height);

        return gridDimensions;
    }

    /* @docs 
    #private void InstantiateCell(int index, float x, float z, Color color)

    > Instantiates a cell at a point x,z with a terrain color specified by the color argument. Cell is added to the cell array at the specified index
    */

    private void InstantiateCell(int index, float x, float z, Color color)
    {
        cells[index] = Instantiate(cell, new Vector3(0, 0, 0), Quaternion.identity, transform);
        cells[index].transform.parent = transform;
        cells[index].name = "Cell(" + x + "," + z + ")";
        cells[index].SetSize(width, scale);
        cells[index].SetOrigin(new Vector3(width * x * scale, 0, height * z * scale));
        cells[index].GenerateMesh();
        cells[index].SetBiome(biomeIndex);
        cells[index].SetDebugColor(color);
        cells[index].RenderBiome();
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
                InstantiateCell(i, x, z, cellColors[i]);
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
