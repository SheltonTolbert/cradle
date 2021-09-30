﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    private Vector2[] uv;
    private int[] triangles;
    private Vector3 origin;
    private MeshFilter meshFilter;
    public int sideLength = 20;

    public bool renderDebug = false;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GenerateMesh();
    }

    private void GenerateVertices()
    {
        vertices = new Vector3[(sideLength + 1) * (sideLength + 1)];

        for (int index = 0, z = 0; z <= sideLength; z++)
        {
            for (int x = 0; x <= sideLength; x++)
            {
                float height = Mathf.PerlinNoise(x * 0.3f, z * 0.3f) * 2.0f;

                vertices[index] = new Vector3(x, height, z);
                index++;
            }
        }
    }

    private void CalculateTriangles()
    {
        triangles = new int[sideLength * sideLength * 6];
        int vertex = 0;
        int tris = 0;

        for (int x = 0; x <  sideLength; x++)
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

    private void OnDrawGizmos() {
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
}
