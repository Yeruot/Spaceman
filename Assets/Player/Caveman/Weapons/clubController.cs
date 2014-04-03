using UnityEngine;
using System.Collections;

public class clubController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 0.2f);
	}

    void OnCollisionEnter(Collision collision) {
        Collider other = collision.collider;
        if(other.tag == "Voxel"){
            Chunk chunk = other.gameObject.GetComponent<Chunk>() as Chunk;
            print (collision.contacts[0].point);
            chunk.hit(collision.contacts[0].point.x, collision.contacts[0].point.y, collision.contacts[0].point.z);
            Destroy( gameObject );
        }
    }

}
