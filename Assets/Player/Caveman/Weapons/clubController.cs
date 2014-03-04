using UnityEngine;
using System.Collections;

public class clubController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 0.2f);
	}

	void OnTriggerEnter(Collider other){
		print ("got triggered");
		if(other.tag == "Voxel"){
			print ("destroying");
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}

}
