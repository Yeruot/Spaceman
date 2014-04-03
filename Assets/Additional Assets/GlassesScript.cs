using UnityEngine;
using System.Collections;

public class GlassesScript : MonoBehaviour {
	public Item item;

	// Use this for initialization
	void Start () {
		item = ItemManager.Instance.GetItemWithId(4);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision){
		print ("suuupsicle");
	}
}
