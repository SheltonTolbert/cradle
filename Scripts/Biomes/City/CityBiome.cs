using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityBiome : Biome
{

    public GameObject[] structures;
    public int numStructures;
    public int numStreetsX = 2;
    public int numStreetsY = 1;

    private Vector2 bounds;
    private Vector3 biomeOrigin;

    public override void SetBounds(Vector2 cellBounds)
    {
        bounds = cellBounds;
    }

    public override void SetOrigin(Vector3 origin)
    {
        biomeOrigin = origin;
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

    private void InstantiateCityBlocks()
    {
        int buildingWidth = 30;
        int streetWidth = 20;
        float blockWidth = (bounds.x - (numStreetsY * streetWidth)) / numStreetsY + 1;
        float blockHeight = (bounds.y - (numStreetsX * streetWidth)) / numStreetsX + 1;

        for (float z = 0; z < bounds.y; z += blockHeight)
        {
            for (float x = 0; x < bounds.x; x += blockWidth)
            {
                Instantiate(GetStructure(), new Vector3(x + biomeOrigin.x, 0 + biomeOrigin.y, z + biomeOrigin.z), Quaternion.identity);
                //Instantiate(GetStructure(), new Vector3((blockWidth - streetWidth) + x + biomeOrigin.x, 0 + biomeOrigin.y, z + biomeOrigin.z), Quaternion.identity);
                //Instantiate(GetStructure(), new Vector3(x + biomeOrigin.x, 0 + biomeOrigin.y, (blockHeight - streetWidth) + z + biomeOrigin.z), Quaternion.identity);
                //Instantiate(GetStructure(), new Vector3((blockWidth - streetWidth) + x + biomeOrigin.x, 0 + biomeOrigin.y, (blockHeight - streetWidth) + z + biomeOrigin.z), Quaternion.identity);
            }
        }

    }

    private void GenerateStructures()
    {
       
        InstantiateCityBlocks();


        /*
        for (int i = 0; i < numStructures; i++)
        {
            float x = Random.Range(0.0f, bounds[0]);
            float z = Random.Range(0.0f, bounds[1]);

            Instantiate(GetStructure(), new Vector3(x + biomeOrigin.x, 0 + biomeOrigin.y, z + biomeOrigin.z), Quaternion.identity);
        }
        */
    }

    public override void Render()
    {
        GenerateStructures();
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("rendering city biome");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
