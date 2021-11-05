using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityBiome : Biome
{

    public GameObject[] structures;
    public int numStructures;
    public int numStreetsX = 2;
    public int numStreetsY = 1;
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

    private void GenerateBlock(Vector3 blockOrigin, float blockWidth, float blockHeight)
    {
        GameObject block = new GameObject("Block" + blockOrigin);
        block.transform.position = blockOrigin;
        block.transform.parent = parentTransform;

        int buildingWidth = 30;

        for (float z = 0; z < blockHeight; z += buildingWidth)
        {
            for (float x = 0; x < blockWidth; x += buildingWidth)
            {
                GameObject building = Instantiate(GetStructure(), new Vector3(x + blockOrigin.x, 0 + biomeOrigin.y, z + blockOrigin.z), Quaternion.identity) as GameObject;
                building.transform.parent = block.transform;
            }
        }
    }

    private void GenerateCity()
    {
        int streetWidth = 100;
        float blockWidth = 60;
        float blockHeight = (bounds.y - (numStreetsX * streetWidth)) / (numStreetsX + 1);

        for (float z = 0; z < (bounds.y - blockHeight); z += blockHeight + streetWidth)
        {

            for (float x = 0; x < (bounds.x - blockWidth); x += blockWidth + (streetWidth / 2))
            {
                GenerateBlock(new Vector3(x + biomeOrigin.x, biomeOrigin.y, z + biomeOrigin.z), blockWidth, blockHeight);
            }
        }
    }

    private void GenerateStructures()
    {
        GenerateCity();
    }

    public override void Render()
    {
        GenerateStructures();
    }
}
