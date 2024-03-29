﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public byte[,,] data;
    public int worldX = 16;
    public int worldY = 16;
    public int worldZ = 16;

    public GameObject chunk;
    public GameObject[,,] chunks;
    public int chunkSize = 100;
    public bool UseChunks = true;
    public string ArquivoPoros = "poros";
    public float scale = 1f;
    public GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        data = new byte[worldX, worldY, worldZ];

        data = WorldGetter.GetWorldData(ArquivoPoros);

        if (UseChunks)
        {
            CreateWithChunks();
        }
        else
        {
            CreateWithCubes();
        }

    }

    void CreateWithCubes()
    {
        for (int x = 0; x < worldX; x++)
        {
            for (int y = 0; y < worldY; y++)
            {
                for (int z = 0; z < worldZ; z++)
                {
                    if (data[x,y,z] == 2)
                    {
                        Instantiate(cube, new Vector3(x, y, z), Quaternion.identity);
                    }
                }
            }
        }
    }

    void CreateWithChunks(){
        chunks = new GameObject[Mathf.FloorToInt(worldX / chunkSize), Mathf.FloorToInt(worldY / chunkSize), Mathf.FloorToInt(worldZ / chunkSize)];

        for (int x = 0; x < chunks.GetLength(0); x++)
        {
            for (int y = 0; y < chunks.GetLength(1); y++)
            {
                for (int z = 0; z < chunks.GetLength(2); z++)
                {

                    chunks[x, y, z] = Instantiate(chunk,
                     new Vector3(x * chunkSize * scale, y * chunkSize * scale, z * chunkSize * scale),
                     new Quaternion(0, 0, 0, 0)) as GameObject;

                    Chunk newChunkScript = chunks[x, y, z].GetComponent("Chunk") as Chunk;


                    newChunkScript.worldGO = gameObject;
                    newChunkScript.chunkSize = chunkSize;
                    newChunkScript.chunkX = x * chunkSize;
                    newChunkScript.chunkY = y * chunkSize;
                    newChunkScript.chunkZ = z * chunkSize;
                    newChunkScript.SetScale(scale);

                }
            }
        }
    }



    public byte Block(int x, int y, int z)
    {

        if (x >= worldX || x < 0 || y >= worldY || y < 0 || z >= worldZ || z < 0)
        {
            return (byte)0;
        }

        return data[x, y, z];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
