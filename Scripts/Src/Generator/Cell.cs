using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* @docs 


### Cell 

--- 

The Cell class is responsible for generating biomes.

The Cell class contains the following public variables:

```

- Biome defaultBiome
    - A fall back biome should the Cell encounter a runtime exception. If left empty a default empty biome will be used. 
- Biome[] possibleBiomes
    - An array of all possible biomes. The Cell can choose from. 
- int sideLength
    - A default Cell size. This is typically overwritten by the Generator but is useful  for debugging a Cell outside of a parent Generator. 
- bool renderDebug
    - This bool acts a toggle for rendering the Cell debug view. 
```


*/

public class Cell : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    private Vector2[] uv;
    private int[] triangles;
    private Vector3 origin;
    private MeshFilter meshFilter;
    private Biome currentBiome;
    private Color debugColor = Color.gray;
    private Renderer meshRenderer;
    private float scale = 1.0f;

    public Biome defaultBiome;
    public Biome[] possibleBiomes;
    public int sideLength = 20;
    public bool renderDebug = false;

    private int currentBiomeIndex = -1;

    // Getters + Setters 

    /* @docs
    ## void SetSize(int size, float scale)

    > Sets the initial size and scale of the cell 

    */
    public void SetSize(int biomeSize, float Scale)
    {
        sideLength = biomeSize;
        scale = Scale;
    }

    /* @docs
    ## void SetOrigin(Vector3 origin)

    > Sets origin of the cell 

    */
    public void SetOrigin(Vector3 origin)
    {
        transform.position = origin;
    }

    // Biomes

    /* @docs
## void SetBiomes(Biome[] biomes)

> Sets possible biomes of the cell 

*/
    public void SetBiomes(Biome[] biomes)
    {
        possibleBiomes = biomes;
    }


    /* @docs
    ## void ClearBiomes(int length = 0)

    > Clears possible biomes array 

    */
    public void ClearBiomes(int length = 0)
    {
        possibleBiomes = new Biome[length];
    }

    /* @docs
    ## int GetNumBiomes()

    > Returns number of biomes in possible biomes array 

    */
    public int GetNumBiomes()
    {
        return possibleBiomes.Length;
    }


    /* @docs
    ## Biome[] GetBiomes()

    > Returns the possible biomes array 

    */
    public Biome[] GetBiomes()
    {
        return possibleBiomes;
    }

    /* @docs
    ## Biome GetBiome()

    > Returns the active biome 

    */
    public Biome GetBiome()
    {
        return currentBiome;
    }

    /* @docs
    ## Vector3 GetBounds()

    > Returns the size of the Cell

    */
    public Vector3 GetBounds()
    {
        return gameObject.GetComponent<Renderer>().bounds.size;
    }

    /* @docs
    ## void SetBiome(int index)

    > Sets the active biome by index

    */
    public void SetBiome(int index)
    {
        if (index == -1)
        {
            currentBiomeIndex = Random.Range(0, possibleBiomes.Length);
        }
        else
        {
            currentBiomeIndex = index;
        }


        currentBiome = defaultBiome;

        if (possibleBiomes.Length == 0)
        {
            throw new System.Exception("No biomes set. Falling back to default biome.");
        }
        else if (currentBiomeIndex >= possibleBiomes.Length)
        {
            throw new System.Exception("Index out of range. Falling back to default biome.");
        }
        else
        {
            currentBiome = possibleBiomes[currentBiomeIndex];
        }

    }

    /* @docs
    ## void SetBiome(Biome biome)

    > Sets the active biome Biome

    */
    public void SetBiome(Biome biome)
    {
        currentBiome = biome;
    }

    /* @docs
    ## void RenderBiome()

    > Renders the current biome

    */
    public void RenderBiome()
    {
        currentBiome.SetBounds(new Vector2(sideLength * scale, sideLength * scale));
        currentBiome.SetOrigin(transform.position);
        currentBiome.SetParent(transform.name);
        currentBiome.Render();
        // Render Debug Color
        Renderer meshRenderer = GetComponent<Renderer>();
        meshRenderer.material.SetColor("_Color", debugColor);
    }


    // Mesh Generation
    private void GenerateVertices()
    {
        vertices = new Vector3[(sideLength + 1) * (sideLength + 1)];

        int index = 0;

        for (float z = 0; z <= sideLength; z++)
        {
            for (float x = 0; x <= sideLength; x++)
            {
                // TODO implement noise height
                //float height = Mathf.PerlinNoise((x + transform.position.x) * 0.1f, (z + transform.position.z) * 0.1f) * 2.0f;

                vertices[index] = new Vector3(x * scale, 0, z * scale);
                index++;
            }
        }
    }

    private void CalculateTriangles()
    {
        triangles = new int[sideLength * sideLength * 6];
        int vertex = 0;
        int tris = 0;

        for (int x = 0; x < sideLength; x++)
        {
            for (int z = 0; z < sideLength; z++)
            {
                int vert_1 = vertex;
                int vert_2 = vertex + sideLength + 1;
                int vert_3 = vertex + sideLength + 2;
                int vert_4 = vertex + 1;

                triangles[0 + tris] = vert_1;
                triangles[1 + tris] = vert_2;
                triangles[2 + tris] = vert_4;
                triangles[3 + tris] = vert_4;
                triangles[4 + tris] = vert_2;
                triangles[5 + tris] = vert_3;

                vertex++;
                tris += 6;
            }
            vertex++;
        }
    }

    public void GenerateMesh()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GenerateVertices();
        CalculateTriangles();
        UpdateMesh();
    }


    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
    }

    // Utility
    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }

        if (renderDebug == true)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                Gizmos.DrawSphere(vertices[i], 0.1f);
            }
        }
    }

    public void SetDebugColor(Color color)
    {
        debugColor = color;
    }
}
