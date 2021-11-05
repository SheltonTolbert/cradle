using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlainsBiome : Biome
{
    public GameObject[] structures;
    public int numStructures;
    public Material terrainMaterial;

    private Vector2 bounds;
    private Vector3 biomeOrigin;
    private Transform parentTransform;

    public override Material GetTerrainMaterial()
    {
        return terrainMaterial;
    }

    public override void SetBounds(Vector2 cellBounds)
    {
        bounds = cellBounds;
    }

    public override void SetOrigin(Vector3 origin)
    {
        biomeOrigin = origin;
    }

    public override void SetParent(string parent)
    {
        parentTransform = GameObject.Find(parent).transform;
    }

    public void SetNumStructures(int newNumStructures)
    {
        numStructures = newNumStructures;
    }

    private GameObject GetStructure()
    {
        int index = Random.Range(0, structures.Length);

        return structures[index];
    }

    private void GenerateStructures()
    {
        for (int i = 0; i < numStructures; i++)
        {
            float x = Random.Range(0.0f, bounds[0]);
            float z = Random.Range(0.0f, bounds[1]);

            Instantiate(GetStructure(), new Vector3(x + biomeOrigin.x, 0 + biomeOrigin.y, z + biomeOrigin.z), Quaternion.identity, parentTransform);
        }
    }

    public override void Render()
    {
        GenerateStructures();
    }
}
