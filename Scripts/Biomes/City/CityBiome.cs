using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityBiome : MonoBehaviour
{

    public GameObject[] structures;
    public int numStructures;

    private Vector2 bounds;
    private Vector3 biomeOrigin;

    public void SetBounds(Vector2 cellBounds)
    {
        bounds = cellBounds;
    }

    public void setOrigin(Vector3 origin)
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

    private void GenerateStructures()
    {
        for (int i = 0; i < numStructures; i++)
        {
            float x = Random.Range(0.0f, bounds[0]);
            float z = Random.Range(0.0f, bounds[1]);

            Instantiate(GetStructure(), new Vector3(x + biomeOrigin.x, 0 + biomeOrigin.y, z + biomeOrigin.z), Quaternion.identity);
        }
    }

    public void Render()
    {
        GenerateStructures();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
