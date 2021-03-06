﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chunk : MonoBehaviour
{
    
    public GameObject worldGO;
    private World world;
    private List<Vector3> newVertices = new List<Vector3> ();
    private List<int> newTriangles = new List<int> ();
    private List<Vector2> newUV = new List<Vector2> ();
    private float tUnit = 0.25f;
    private Vector2 tStone = new Vector2 (3, 0);
    private Vector2 tGrass = new Vector2 (1, 0);
    private Vector2 tDirt = new Vector2 (2, 0);
    private Vector2 tGrassTop = new Vector2 (0, 0);
    private Vector2 tWood = new Vector2 (0, 1);
    private Vector2 tLeaves = new Vector2 (1, 1);
    private Vector2 tTree = new Vector2 (2, 1);
    private Mesh mesh;
    private MeshCollider col;
    private int faceCount;
    public int chunkSize = 16;
    public int chunkX;
    public int chunkY;
    public int chunkZ;
    
    // Use this for initialization
    void Start ()
    { 
        
        world = worldGO.GetComponent ("World") as World;
   
        mesh = GetComponent<MeshFilter> ().mesh;
        col = GetComponent<MeshCollider> ();
   
        GenerateMesh ();
   
   
    }
  
    // Update is called once per frame
    void Update ()
    {
   
    }
  
    public void GenerateMesh ()
    {
   
        for (int x=0; x<chunkSize; x++) {
            for (int y=0; y<chunkSize; y++) {
                for (int z=0; z<chunkSize; z++) {
                    //This code will run for every block in the chunk
      
                    if (Block (x, y, z) != 0) {
                        if (Block (x, y + 1, z) == 0) {
                            //Block above is air
                            CubeTop (x, y, z, Block (x, y, z));
                        }
       
                        if (Block (x, y - 1, z) == 0) {
                            //Block below is air
                            CubeBot (x, y, z, Block (x, y, z));
        
                        }
       
                        if (Block (x + 1, y, z) == 0) {
                            //Block east is air
                            CubeEast (x, y, z, Block (x, y, z));
        
                        }
       
                        if (Block (x - 1, y, z) == 0) {
                            //Block west is air
                            CubeWest (x, y, z, Block (x, y, z));
        
                        }
       
                        if (Block (x, y, z + 1) == 0) {
                            //Block north is air
                            CubeNorth (x, y, z, Block (x, y, z));
        
                        }
       
                        if (Block (x, y, z - 1) == 0) {
                            //Block south is air
                            CubeSouth (x, y, z, Block (x, y, z));
        
                        }
       
                    }
      
                }
            }
        }
   
        UpdateMesh ();
    }
  
    byte Block (int x, int y, int z)
    {
        return world.Block (x + chunkX, y + chunkY, z + chunkZ);
    }
  
    void CubeTop (int x, int y, int z, byte block)
    {
        float vX = (float)x / 2, vY = (float)y / 2, vZ = (float)z / 2;
        newVertices.Add (new Vector3 (vX, vY, vZ + 0.5f));
        newVertices.Add (new Vector3 (vX + 0.5f, vY, vZ + 0.5f));
        newVertices.Add (new Vector3 (vX + 0.5f, vY, vZ));
        newVertices.Add (new Vector3 (vX, vY, vZ));
   
        Vector2 texturePos = new Vector2 (0, 0);
   
        if (Block (x, y, z) == 1) {
            texturePos = tStone;
        } else if (Block (x, y, z) == 2) {
            texturePos = tGrassTop;
        } else if (Block (x, y, z) == 3) {
            texturePos = tWood;
        } else if (Block (x, y, z) == 4) {
            texturePos = tTree;
        } else if (Block (x, y, z) == 5) {
            texturePos = tLeaves;
        }
   
        Cube (texturePos);
   
    }
  
    void CubeNorth (int x, int y, int z, byte block)
    {
        float vX = (float)x / 2, vY = (float)y / 2, vZ = (float)z / 2;
   
        newVertices.Add (new Vector3 (vX + 0.5f, vY - 0.5f, vZ + 0.5f));
        newVertices.Add (new Vector3 (vX + 0.5f, vY, vZ + 0.5f));
        newVertices.Add (new Vector3 (vX, vY, vZ + 0.5f));
        newVertices.Add (new Vector3 (vX, vY - 0.5f, vZ + 0.5f));
   
        Vector2 texturePos = new Vector2 (0, 0);
   
        if (Block (x, y, z) == 1) {
            texturePos = tStone;
        } else if (Block (x, y, z) == 2) {
            if(Block (x, y+1, z) == 0) {
                texturePos = tGrass;
            } else {
                texturePos = tDirt;
            }
        } else if (Block (x, y, z) == 3) {
            texturePos = tWood;
        } else if (Block (x, y, z) == 4) {
            texturePos = tTree;
        } else if (Block (x, y, z) == 5) {
            texturePos = tLeaves;
        }
   
        Cube (texturePos);
   
    }
  
    void CubeEast (int x, int y, int z, byte block)
    {
        float vX = (float)x / 2, vY = (float)y / 2, vZ = (float)z / 2;
   
        newVertices.Add (new Vector3 (vX + 0.5f, vY - 0.5f, vZ));
        newVertices.Add (new Vector3 (vX + 0.5f, vY, vZ));
        newVertices.Add (new Vector3 (vX + 0.5f, vY, vZ + 0.5f));
        newVertices.Add (new Vector3 (vX + 0.5f, vY - 0.5f, vZ + 0.5f));
   
        Vector2 texturePos = new Vector2 (0, 0);
   
        if (Block (x, y, z) == 1) {
            texturePos = tStone;
        } else if (Block (x, y, z) == 2) {
            if(Block (x, y+1, z) == 0) {
                texturePos = tGrass;
            } else {
                texturePos = tDirt;
            }
        } else if (Block (x, y, z) == 3) {
            texturePos = tWood;
        } else if (Block (x, y, z) == 4) {
            texturePos = tTree;
        } else if (Block (x, y, z) == 5) {
            texturePos = tLeaves;
        }
   
        Cube (texturePos);
   
    }
  
    void CubeSouth (int x, int y, int z, byte block)
    {
        float vX = (float)x / 2, vY = (float)y / 2, vZ = (float)z / 2;
   
        newVertices.Add (new Vector3 (vX, vY - 0.5f, vZ));
        newVertices.Add (new Vector3 (vX, vY, vZ));
        newVertices.Add (new Vector3 (vX + 0.5f, vY, vZ));
        newVertices.Add (new Vector3 (vX + 0.5f, vY - 0.5f, vZ));
   
        Vector2 texturePos = new Vector2 (0, 0);
   
        if (Block (x, y, z) == 1) {
            texturePos = tStone;
        } else if (Block (x, y, z) == 2) {
            if(Block (x, y+1, z) == 0) {
                texturePos = tGrass;
            } else {
                texturePos = tDirt;
            }
        } else if (Block (x, y, z) == 3) {
            texturePos = tWood;
        } else if (Block (x, y, z) == 4) {
            texturePos = tTree;
        } else if (Block (x, y, z) == 5) {
            texturePos = tLeaves;
        }
   
        Cube (texturePos);
   
    }
  
    void CubeWest (int x, int y, int z, byte block)
    {
        float vX = (float)x / 2, vY = (float)y / 2, vZ = (float)z / 2;
   
        newVertices.Add (new Vector3 (vX, vY - 0.5f, vZ + 0.5f));
        newVertices.Add (new Vector3 (vX, vY, vZ + 0.5f));
        newVertices.Add (new Vector3 (vX, vY, vZ));
        newVertices.Add (new Vector3 (vX, vY - 0.5f, vZ));
   
        Vector2 texturePos = new Vector2 (0, 0);
   
        if (Block (x, y, z) == 1) {
            texturePos = tStone;
        } else if (Block (x, y, z) == 2) {
            if(Block (x, y+1, z) == 0) {
                texturePos = tGrass;
            } else {
                texturePos = tDirt;
            }
        } else if (Block (x, y, z) == 3) {
            texturePos = tWood;
        } else if (Block (x, y, z) == 4) {
            texturePos = tTree;
        } else if (Block (x, y, z) == 5) {
            texturePos = tLeaves;
        }
   
        Cube (texturePos);
   
    }
  
    void CubeBot (int x, int y, int z, byte block)
    {
        float vX = (float)x / 2, vY = (float)y / 2, vZ = (float)z / 2;
   
        newVertices.Add (new Vector3 (vX, vY - 0.5f, vZ));
        newVertices.Add (new Vector3 (vX + 0.5f, vY - 0.5f, vZ));
        newVertices.Add (new Vector3 (vX + 0.5f, vY - 0.5f, vZ + 0.5f));
        newVertices.Add (new Vector3 (vX, vY - 0.5f, vZ + 0.5f));
   
        Vector2 texturePos = new Vector2 (0, 0);
   
        if (Block (x, y, z) == 1) {
            texturePos = tStone;
        } else if (Block (x, y, z) == 2) {
            texturePos = tGrass;
        } else if (Block (x, y, z) == 3) {
            texturePos = tWood;
        } else if (Block (x, y, z) == 4) {
            texturePos = tTree;
        } else if (Block (x, y, z) == 5) {
            texturePos = tLeaves;
        }
   
        Cube (texturePos);
   
    }
  
    void Cube (Vector2 texturePos)
    {
   
        newTriangles.Add (faceCount * 4); //1
        newTriangles.Add (faceCount * 4 + 1); //2
        newTriangles.Add (faceCount * 4 + 2); //3
        newTriangles.Add (faceCount * 4); //1
        newTriangles.Add (faceCount * 4 + 2); //3
        newTriangles.Add (faceCount * 4 + 3); //4
   
        newUV.Add (new Vector2 (tUnit * texturePos.x + tUnit, tUnit * texturePos.y));
        newUV.Add (new Vector2 (tUnit * texturePos.x + tUnit, tUnit * texturePos.y + tUnit));
        newUV.Add (new Vector2 (tUnit * texturePos.x, tUnit * texturePos.y + tUnit));
        newUV.Add (new Vector2 (tUnit * texturePos.x, tUnit * texturePos.y));
   
        faceCount++; // Add this line
    }
  
    void UpdateMesh ()
    {
   
        mesh.Clear ();
        mesh.vertices = newVertices.ToArray ();
        mesh.uv = newUV.ToArray ();
        mesh.triangles = newTriangles.ToArray ();
        mesh.Optimize ();
        mesh.RecalculateNormals ();
   
        col.sharedMesh = null;
        col.sharedMesh = mesh;
   
        newVertices.Clear ();
        newUV.Clear ();
        newTriangles.Clear ();
        faceCount = 0;
    }

    public void hit(float x, float y, float z) {
        world.DestroyBlock (x, y, z);
        //GenerateMesh ();
    }

}
