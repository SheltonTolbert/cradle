using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlainsBiome : MonoBehaviour
{
    public GameObject[] structures;
    public int numStructures;

    private Vector2 bounds;

    public void SetBounds(Vector2 cellBounds)
    {
        bounds = cellBounds;
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

            Instantiate(GetStructure(), new Vector3(x, 0, z), Quaternion.identity);
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
