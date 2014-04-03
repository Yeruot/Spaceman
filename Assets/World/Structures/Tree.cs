using UnityEngine;
using System.Collections;

public class Tree : Object {
    
    
    int x, y, z;
    int treeheight;
    
    public Tree (int px, int pz, int treeheight, byte[,,] data)
    {
        x = px;
        z = pz;
        this.treeheight = treeheight;
        for (int i = 0; i < 64; i++) {
            if (data [x, i, z] == 0) {
                y = i;
                break;
            }
        }
    }
    
    // Use this for initialization
    void Start ()
    {
        
    }
    
    // Update is called once per frame
    void Update ()
    {
        
    }
    
    public void Build (byte[,,] data)
    {
        for (int h = 0; h < treeheight; h++) {
            data[x,y+h,z] = 4;
            if(h == treeheight-1) {
                data[x+1,y+h+1,z] = 5;
                data[x-1,y+h+1,z] = 5;
                data[x,y+h+1,z+1] = 5;
                data[x,y+h+1,z-1] = 5;
                data[x+1,y+h,z] = 5;
                data[x-1,y+h,z] = 5;
                data[x,y+h,z+1] = 5;
                data[x,y+h,z-1] = 5;
                data[x+2,y+h+1,z] = 5;
                data[x-2,y+h+1,z] = 5;
                data[x,y+h+1,z+2] = 5;
                data[x,y+h+1,z-2] = 5;
                data[x+1,y+h+1,z+1] = 5;
                data[x-1,y+h+1,z-1] = 5;
                data[x+1,y+h+1,z-1] = 5;
                data[x-1,y+h+1,z+1] = 5;
                data[x+1,y+h+2,z] = 5;
                data[x-1,y+h+2,z] = 5;
                data[x,y+h+2,z+1] = 5;
                data[x,y+h+2,z-1] = 5;
                data[x,y+h+3,z] = 5;
            }
        }
    }
}
