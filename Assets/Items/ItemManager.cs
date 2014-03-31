using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ItemManager : MonoBehaviour {
	public List<Item> items = new List<Item>();
	private static ItemManager instance = null;


	// The below is code to make the player controller a singleton
	// this will allow multiple objects to refer to a single player
	// object and act accordingly
	public static ItemManager Instance
	{
		get {return instance;}
	}
	
	void Awake(){
		if (instance != null && instance != this){
			Destroy(this.gameObject);
			return;
		} else{
			instance = this;
		}

		DontDestroyOnLoad(this.gameObject);
	}

}
