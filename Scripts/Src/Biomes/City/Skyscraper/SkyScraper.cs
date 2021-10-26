using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyScraper : MonoBehaviour
{

    public GameObject[] firstFloors;
    public GameObject[] roofs;
    public GameObject[] middleFloors;
    public int numMiddleFloors = 1;
    public int maxNumMiddleFloors = 10;

    public bool regenerate = false;
    public float floorOffset = 1.0f;
    public bool randomizeNumberOfFloors = false;
    void GenerateStructure()
    {
        int roofIndex = Random.Range(0, roofs.Length);
        int floorIndex = Random.Range(0, firstFloors.Length);
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;


        GameObject firstFloor = Instantiate(firstFloors[floorIndex], new Vector3(x, y, z), Quaternion.identity, transform);

        for (int i = 0; i < numMiddleFloors; i++)
        {
            GameObject middleFloor = middleFloors[Random.Range(0, middleFloors.Length)];

            Instantiate(middleFloor, new Vector3(x, y + floorOffset * i,z), Quaternion.identity, transform);
        }

        GameObject roof = Instantiate(roofs[roofIndex], new Vector3(x, y + floorOffset * (numMiddleFloors - 1), z), Quaternion.identity, transform);

    }

    private void DestroyChildren()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    void Start()
    {
        if (randomizeNumberOfFloors)
        {
            numMiddleFloors = Random.Range(1, maxNumMiddleFloors);
        }
        GenerateStructure();
    }

    void Update()
    {
        if (regenerate == true)
        {
            DestroyChildren();
            GenerateStructure();
            regenerate = false;
        }
    }
}
