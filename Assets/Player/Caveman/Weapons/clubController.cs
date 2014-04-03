using UnityEngine;
using System.Collections;

public class clubController : MonoBehaviour
{

    // Use this for initialization
    void Start ()
    {
        Destroy (gameObject, 0.2f);
    }

    void OnCollisionEnter (Collision collision)
    {
        Collider other = collision.collider;
        if (other.tag == "Voxel") {
            Chunk chunk = other.gameObject.GetComponent<Chunk> () as Chunk;
            float xoffset = 0;
            float yoffset = 0;
            float zoffset = 0;
            var relativePosition = transform.InverseTransformPoint (collision.contacts [0].point);
            if (relativePosition.x > 0) {
                
                xoffset -= 0.1f;
                
            } else if (relativePosition.x < 0) {
                
                xoffset += 0.1f;
                
            }
            
            if (relativePosition.z > 0) {
                zoffset -= 0.1f;
                
            } else {
                zoffset += 0.1f;
                
            }
            chunk.hit (collision.contacts [0].point.x + xoffset, 
                      collision.contacts [0].point.y + yoffset, 
                      collision.contacts [0].point.z + zoffset);
            Destroy (gameObject);
        }

    }

}
