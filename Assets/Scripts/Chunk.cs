﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private List<Vector3> newVertices = new List<Vector3>();
    private List<int> newTriangles = new List<int>();
    private List<Vector2> newUV = new List<Vector2>();

    private float tUnit = 0.25f;
    private Vector2 tStone = new Vector2(0,3);
    private Vector2 tGrass = new Vector2(0, 1);

    private Mesh mesh;
    private MeshCollider col;

    private int faceCount;

    public GameObject worldGO;
    private World world;
    public int chunkSize;

    public int chunkX;
    public int chunkY;
    public int chunkZ;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        col = GetComponent<MeshCollider>();
        
        world = worldGO.GetComponent("World") as World;
        GenerateMesh();
        
    }

    public void SetScale(float x)
    {
        transform.localScale = new Vector3(x, x, x);
    }

    byte Block(int x, int y, int z)
    {
        return world.Block(x + chunkX, y + chunkY, z + chunkZ);
    }

    void GenerateMesh()
    {

        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    //This code will run for every block in the chunk
                    
                    if (Block(x, y, z) != 0)
                    {
                        //If the block is solid
                        
                        if (Block(x, y + 1, z) == 0 || y>=chunkSize-1)
                        {
                            //Block above is air
                            CubeTop(x, y, z, Block(x, y, z));
                        }

                        if (Block(x, y - 1, z) == 0 || y <= 0)
                        {
                            //Block below is air
                            CubeBot(x, y, z, Block(x, y, z));

                        }

                        if (Block(x + 1, y, z) == 0 || x >= chunkSize - 1)
                        {
                            //Block east is air
                            CubeEast(x, y, z, Block(x, y, z));

                        }

                        if (Block(x - 1, y, z) == 0 || x <= 0)
                        {
                            //Block west is air
                            CubeWest(x, y, z, Block(x, y, z));

                        }

                        if (Block(x, y, z + 1) == 0 || z >= chunkSize - 1)
                        {
                            //Block north is air
                            CubeNorth(x, y, z, Block(x, y, z));

                        }

                        if (Block(x, y, z - 1) == 0 || z <= 0)
                        {
                            //Block south is air
                            CubeSouth(x, y, z, Block(x, y, z));

                        }

                    }

                }
            }
        }

        UpdateMesh();
    }

    void Cube(Vector2 texturePos)
    {

        newTriangles.Add(faceCount * 4); //1
        newTriangles.Add(faceCount * 4 + 1); //2
        newTriangles.Add(faceCount * 4 + 2); //3
        newTriangles.Add(faceCount * 4); //1
        newTriangles.Add(faceCount * 4 + 2); //3
        newTriangles.Add(faceCount * 4 + 3); //4

        newUV.Add(new Vector2(tUnit * texturePos.x + tUnit, tUnit * texturePos.y));
        newUV.Add(new Vector2(tUnit * texturePos.x + tUnit, tUnit * texturePos.y + tUnit));
        newUV.Add(new Vector2(tUnit * texturePos.x, tUnit * texturePos.y + tUnit));
        newUV.Add(new Vector2(tUnit * texturePos.x, tUnit * texturePos.y));

        faceCount++; // Add this line
    }

    void CubeTop(int x, int y, int z, byte block)
    {

        newVertices.Add(new Vector3(x, y, z + 1));
        newVertices.Add(new Vector3(x + 1, y, z + 1));
        newVertices.Add(new Vector3(x + 1, y, z));
        newVertices.Add(new Vector3(x, y, z));

        Vector2 texturePos;

        texturePos = tStone;

        Cube(texturePos);

    }

    void CubeNorth(int x, int y, int z, byte block)
    {

        newVertices.Add(new Vector3(x + 1, y - 1, z + 1));
        newVertices.Add(new Vector3(x + 1, y, z + 1));
        newVertices.Add(new Vector3(x, y, z + 1));
        newVertices.Add(new Vector3(x, y - 1, z + 1));

        Vector2 texturePos;

        texturePos = tStone;

        Cube(texturePos);

    }

    void CubeEast(int x, int y, int z, byte block)
    {

        newVertices.Add(new Vector3(x + 1, y - 1, z));
        newVertices.Add(new Vector3(x + 1, y, z));
        newVertices.Add(new Vector3(x + 1, y, z + 1));
        newVertices.Add(new Vector3(x + 1, y - 1, z + 1));

        Vector2 texturePos;

        texturePos = tStone;

        Cube(texturePos);

    }

    void CubeSouth(int x, int y, int z, byte block)
    {

        newVertices.Add(new Vector3(x, y - 1, z));
        newVertices.Add(new Vector3(x, y, z));
        newVertices.Add(new Vector3(x + 1, y, z));
        newVertices.Add(new Vector3(x + 1, y - 1, z));

        Vector2 texturePos;

        texturePos = tStone;

        Cube(texturePos);

    }

    void CubeWest(int x, int y, int z, byte block)
    {

        newVertices.Add(new Vector3(x, y - 1, z + 1));
        newVertices.Add(new Vector3(x, y, z + 1));
        newVertices.Add(new Vector3(x, y, z));
        newVertices.Add(new Vector3(x, y - 1, z));

        Vector2 texturePos;

        texturePos = tStone;

        Cube(texturePos);

    }

    void CubeBot(int x, int y, int z, byte block)
    {

        newVertices.Add(new Vector3(x, y - 1, z));
        newVertices.Add(new Vector3(x + 1, y - 1, z));
        newVertices.Add(new Vector3(x + 1, y - 1, z + 1));
        newVertices.Add(new Vector3(x, y - 1, z + 1));

        Vector2 texturePos;

        texturePos = tStone;

        Cube(texturePos);

    }

    

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = newVertices.ToArray();
        mesh.uv = newUV.ToArray();
        mesh.triangles = newTriangles.ToArray();
        ;
        mesh.RecalculateNormals();

        col.sharedMesh = null;
        col.sharedMesh = mesh;

        newVertices.Clear();
        newUV.Clear();
        newTriangles.Clear();

        faceCount = 0; //Fixed: Added this thanks to a bug pointed out by ratnushock!
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
