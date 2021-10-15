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

    private void GenerateBlock(Vector3 blockOrigin, float blockWidth, float blockHeight){
        int buildingWidth = 30;

        for (float z = 0; z < blockHeight; z += buildingWidth){
            for (float x = 0; x < blockWidth; x += buildingWidth){
                Instantiate(GetStructure(), new Vector3(x + blockOrigin.x, 0 + biomeOrigin.y, z + blockOrigin.z), Quaternion.identity);
            }
        }
    }

    private void GenerateCity()
    {
        
        int streetWidth = 100;
        float blockWidth = (60);
        float blockHeight = (bounds.y - (numStreetsX * streetWidth)) / (numStreetsX + 1);

        for (float z = 0; z < (bounds.y - blockHeight); z += blockHeight + streetWidth)
        {
            
            for (float x = 0; x < (bounds.x - blockWidth); x += blockWidth + (streetWidth / 2))
            {
                GenerateBlock(new Vector3(x + biomeOrigin.x, biomeOrigin.y, z + biomeOrigin.z ), blockWidth, blockHeight);
            }
        }
    }

    private void GenerateStructures()
    {
       
        GenerateCity();


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
