using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlainsHouseGenerator : MonoBehaviour
{

    public GameObject[] firstFloors;
    public GameObject[] roofs;
    public bool regenerate = false;


    void GenerateStructure()
    {
        int roofIndex = Random.Range(0,roofs.Length);
        int floorIndex = Random.Range(0, firstFloors.Length);

        for (int i = 0; i < roofs.Length; i++)
        {

            if (i == roofIndex)
            {
                roofs[i].SetActive(true);
            }
            else
            {
                roofs[i].SetActive(false);
            }
        }

        for (int i = 0; i < firstFloors.Length; i++)
        {

            if (i == floorIndex)
            {
                firstFloors[i].SetActive(true);
            }
            else
            {
                firstFloors[i].SetActive(false);
            }
        }
    }

    void Start()
    {
        GenerateStructure();
    }

    void Update()
    {
        if(regenerate == true)
        {
            GenerateStructure();
            regenerate = false;
        }
    }
}
