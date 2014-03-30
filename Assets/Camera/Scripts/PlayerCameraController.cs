using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCameraController : MonoBehaviour {
	
	// The below is code to make the camera controller a singleton
	// this will allow multiple objects to refer to a single camera
	// object and act accordingly
	private static PlayerCameraController instance = null;
	public static PlayerCameraController Instance
	{
		get {return instance;}
	}

	//1 represents top-down camera
	//2 represents sidescrolling camera
	public static int CameraOrientation = 1;

	void Awake(){
		// this code makes sure that only one instance of the camera exists
		// at a time
		if (instance != null && instance != this){
			Destroy(this.gameObject);
			return;
		}
		else{
			instance = this;
		}
		
		// this will allow our camera object to persist between
		// scenes(levels)
		DontDestroyOnLoad(this.gameObject);
	}
	
	public float DistanceToPlayer = 5.0f;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit[] hits;
		// you can also use CapsuleCastAll()
		// TODO: setup your layermask it improve performance and filter your hits.
		hits = Physics.RaycastAll(transform.position, transform.forward, DistanceToPlayer);
		foreach(RaycastHit hit in hits)
		{
			Renderer R = hit.collider.renderer;
			if (R == null)
				continue; // no renderer attached? go to next hit
			// TODO: maybe implement here a check for GOs that should not be affected like the player
			
			Voxel AT = R.GetComponent<Voxel>();
			if (AT == null) // if no script is attached, attach one
			{
				AT = R.gameObject.AddComponent<Voxel>();
			}
			AT.BeTransparent(); // get called every frame to reset the falloff
		}
	}
	
	public void ChangeView(){
		print ("Change View Triggered");
		if (CameraOrientation == 1) {
			transform.Rotate (-45, 0, 0, Space.World);
			transform.Translate (0, -1.3F, -2, Space.World);
			CameraOrientation = 2;
		} 
		else {
			transform.Rotate (45, 0, 0, Space.World);
			transform.Translate (0, 1.3F, 2,  Space.World);
			CameraOrientation = 1;
		}
	}
}
