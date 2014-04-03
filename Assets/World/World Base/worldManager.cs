using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class worldManager : MonoBehaviour {

	public Transform voxelPrefab;
	public Transform worldBasePrefab;

	private Transform worldBase;
	
	private const float unit = 0.5f;
	
	private int height, width, length;
	private int heightVar = 2;
	private int randVar = 0;

	private static worldManager instance;

	// Use this for initialization
	void Start () {
		instance = this;
		
		height = Random.Range(3, 9);
		width = Random.Range(51, 55);
		length = Random.Range(50, 55);

		instance.worldBase = (Transform)Instantiate(worldBasePrefab, new Vector3(0f,0f,0f), Quaternion.identity);
		instance.worldBase.localScale = new Vector3(width, instance.worldBase.localScale.y, length);

		int[,] heights = new int[width, length];
		heights[0, 0] = getRand (height, -2, 6);
		heights[width-1, 0] = getRand(height, -2, 6);
		heights[0, length-1] = getRand(height, -2, 6);
		heights[width-1, length-1] = getRand(height, -2, 6);
		constructMap (heights, 0, 0, length-1, width-1);

		for (int x = 0; x < width; x++) {
			for(int z = 0; z < length; z++) {
				//if(heights[x, z] == 0) {
					smooth(heights, x, z, width, length);
				//}
			}
		}

		for(int x = 0; x < width; x++){
			for(int z = 0; z < length; z++){
				for(int y = 1; y <= heights[x, z]; y++){
					Instantiate(voxelPrefab, new Vector3(x*unit, y*unit, z*unit), Quaternion.identity);
				}
			}
		}
	}

	void smooth(int[,] map, int x, int z, int width, int length) {
		int numAdj = 0;
		int total = 0;
		if (x > 0 && map [x - 1, z] > 0) {
			numAdj++;
			total += map[x-1,z];
		}
		if(x < width-1 && map[x+1, z] > 0) {
			numAdj++;
			total += map[x+1, z];
		}
		if(z > 0 && map[x, z-1] > 0) {
			numAdj++;
			total += map[x, z-1];
		}
		if(z < length-1 && map[x, z+1] > 0) {
			numAdj++;
			total += map[x, z+1];
		}
		map [x, z] = total / numAdj;
	}

	void constructMap(int[,] map, int top, int left, int bottom, int right) {
		if(top+1 >= bottom) return;
		if(left+1 >= right) return;
		int midX = (left+right)/2;
		int midY = (top+bottom)/2;
		int meanHeight = (map[left, top] + map[right, top] + map[left, bottom] + map[right, bottom])/4;
		map[midX, midY] = getRand (meanHeight, -heightVar, heightVar);
		map [midX, top] = getRand ((map [left, top] + map [right, top]) / 2, -heightVar, heightVar);
		map [midX, bottom] = getRand ((map [left, bottom] + map [right, bottom]) / 2, -heightVar, heightVar);
		map [left, midY] = getRand ((map [left, top] + map [left, bottom]) / 2, -heightVar, heightVar);
		map [right, midY] = getRand ((map [right, top] + map [right, bottom]) / 2, -heightVar, heightVar);
		constructMap (map, top, left, midY, midX);
		constructMap (map, top, midX, midY, right);
		constructMap (map, midY, left, bottom, midX);
		constructMap (map, midY, midX, bottom, right);
	}

	int getRand(int val, int off1, int off2) {
		int rand = Random.Range (off1 + randVar, off2 + randVar);
		int height = val + rand;
		return (height < 1 ? 1 : height);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
