using UnityEngine;
using System.Collections;

public class House : Object
{

    int x, y, z;
    int houseWidth = 6;
    int houseHeight = 3;

    public House (int px, int pz, byte[,,] data)
    {
        x = px;
        z = pz;
        int lowestY = 64;
        for (int i = 0; i < lowestY; i++) {
            for (int j = x-1; j < x+houseWidth+1; j++) {
                for (int k = z-1; k < z+houseWidth+1; k++) {
                    if (data [j, i, k] == 0) {
                        lowestY = i;
                    }
                }
            }
        }
        y = lowestY + 2;
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
        for (int i = x - 1; i < x + houseWidth + 1; i++) {
            for (int j = z - 1; j < z + houseWidth + 1; j++) {
                for (int k = y-2; k < y; k++) {
                    data [i, k, j] = 2;
                }
                for (int k = y; k < 64; k++) {
                    data [i, k, j] = 0;
                    if (k < y + houseHeight) {
                        if (i == x || i == x + houseWidth - 1 || j == z || j == z + houseWidth - 1) {
                            data [i, k, j] = 3;
                        }
                    }
                }
            }
        }
    }
}
