using UnityEngine;
using System.Collections;

public class World : MonoBehaviour
{
    
    public GameObject chunk;
	public GameObject cow;
	public GameObject glasses;
    public GameObject[,,] chunks;
    public int chunkSize = 16;
    public byte[,,] data;
    public int worldX = 16;
    public int worldY = 16;
    public int worldZ = 16;

    private House house;
    
    // Use this for initialization
    void Start ()
    {
        
        data = new byte[worldX, worldY, worldZ];
        
        for (int x=0; x<worldX; x++) {
            for (int z=0; z<worldZ; z++) {
                int stone = PerlinNoise (x, 0, z, 10, 3, 1.2f);
                stone += PerlinNoise (x, 300, z, 20, 8, 1) + 10;
                int dirt = PerlinNoise (x, 100, z, 70, 9, 1) + 1;
                
                for (int y=0; y<worldY; y++) {
                    if (y <= stone) {
                        data [x, y, z] = 1;
                    } else if (y <= dirt + stone) {
                        data [x, y, z] = 2;
                    }
                    
                }
            }
        }
        
        for(int i = 0; i < 20; i++) {
            int x = Random.Range(5, 59);
            int z = Random.Range(5, 59);
            int height = Random.Range(3, 7);
            Tree t = new Tree(x, z, height, data);
            t.Build(data);
        }

        house = new House (worldX - 20, worldZ - 20, data);
        house.Build (data);
        
        
        chunks = new GameObject[Mathf.FloorToInt (worldX / chunkSize), Mathf.FloorToInt (worldY / chunkSize), Mathf.FloorToInt (worldZ / chunkSize)];
        
        for (int x=0; x<chunks.GetLength(0); x++) {
            for (int y=0; y<chunks.GetLength(1); y++) {
                for (int z=0; z<chunks.GetLength(2); z++) {
                    
                    chunks [x, y, z] = Instantiate (chunk, new Vector3 ((x * chunkSize)/2, (y * chunkSize)/2, (z * chunkSize)/2), new Quaternion (0, 0, 0, 0)) as GameObject;
                    Chunk newChunkScript = chunks [x, y, z].GetComponent ("Chunk") as Chunk;
                    newChunkScript.worldGO = gameObject;
                    newChunkScript.chunkSize = chunkSize;
                    newChunkScript.chunkX = x * chunkSize;
                    newChunkScript.chunkY = y * chunkSize;
                    newChunkScript.chunkZ = z * chunkSize;
      
                }
            }
        }

        for (int i = 0; i < worldY; i++) {
            if((data[worldX/2, i, worldZ/2]) == 0) {
                PlayerController.Instance.gameObject.transform.position = new Vector3 (worldX / 4, i/2+1, worldZ / 4);
                break;
            }
        }

		for (int i = 0; i < worldY; i++) {
			if((data[worldX-16, i, worldZ-16]) == 0) {
				GideonController.Instance.gameObject.transform.position = new Vector3 ((worldX - 16)/2, i/2+1, (worldZ - 16)/2);
				break;
			}
		}

		for (int i = 0; i < worldY; i++) {
			if((data[14, i, 14]) == 0) {
				GameObject gObject = Instantiate(glasses, new Vector3 (6.75f, i/2+0.25f, 7.25f), Quaternion.identity) as GameObject;

                PlayerController.Instance.setGlassesPosition(gObject.transform.position, gObject);
				break;
			}
		}

		for(int k = 0; k < 50; k++){
			for (int i = 0; i < worldY; i++) {
				int tempX = Random.Range(1,50);
                int tempZ = Random.Range (1, 50);
				if((data[tempX, i, tempZ]) == 0) {
					Instantiate(cow, new Vector3 (tempX/2, i/2+1, tempZ/2), Quaternion.identity);
					break;
				}
			}
		}
    }
  
    int PerlinNoise (int x, int y, int z, float scale, float height, float power)
    {
        float rValue;
        rValue = Noise.Noise.GetNoise (((double)x) / scale, ((double)y) / scale, ((double)z) / scale);
        rValue *= height;
   
        if (power != 0) {
            rValue = Mathf.Pow (rValue, power);
        }
   
        return (int)rValue;
    }
  
  
    // Update is called once per frame
    void Update ()
    {
  
    }
  
    public byte Block (int x, int y, int z)
    {
   
        if (x >= worldX || x < 0 || y >= worldY || y < 0 || z >= worldZ || z < 0) {
            return (byte)1;
        }
   
        return data [x, y, z];
    }

    public void DestroyBlock(float x, float y, float z) {
        int dx = Mathf.FloorToInt (x * 2);
        int dy = Mathf.CeilToInt (y * 2);
        int dz = Mathf.FloorToInt (z * 2);
        data [dx, dy, dz] = 0;
        Chunk ch = chunks[dx/16, dy/16, dz/16].GetComponent<Chunk>() as Chunk;
        ch.GenerateMesh();
        if(dx % 16 == 0 && dx >= 16) {
            ch = chunks[dx/16 - 1, dy/16, dz/16].GetComponent<Chunk>() as Chunk;
            ch.GenerateMesh();
        }
        else if(dx % 16 == 15 && dx < worldX-16) {
            ch = chunks[dx/16 + 1, dy/16, dz/16].GetComponent<Chunk>() as Chunk;
            ch.GenerateMesh();
        }
        if(dz % 16 == 0 && dz >= 16) {
            ch = chunks[dx/16, dy/16, dz/16-1].GetComponent<Chunk>() as Chunk;
            ch.GenerateMesh();
        }
        else if(dz % 16 == 15 && dz < worldX-16) {
            ch = chunks[dx/16, dy/16, dz/16+1].GetComponent<Chunk>() as Chunk;
            ch.GenerateMesh();
        }
    }
}
