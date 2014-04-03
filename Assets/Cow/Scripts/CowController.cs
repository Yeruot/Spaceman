using UnityEngine;
using System.Collections;

public class CowController : MonoBehaviour {
	private Animator animator;
	//Public player Attributes below
	public float jumpSpeed;
	public float gravity;
	
	private float verticalSpeed;
	private Vector3 movementVector;

	private AudioSource[] audioSources;
	private AudioSource moo;
	private AudioSource mooing;
	private AudioSource hurt;
	private AudioSource dying;

	private CollisionFlags flags;
	private CharacterController charController;

	private Time timer;

	public int health;
	// Use this for initialization
	void Start () {
		audioSources = this.GetComponents<AudioSource>();
		moo = audioSources[0];
		mooing = audioSources[1];
		hurt = audioSources[2];
		dying = audioSources[3];
		animator = this.GetComponent<Animator>();
		movementVector = new Vector3(0f,0f,0f);
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.frameCount%1200 == 0){
			animator.SetTrigger("Eat");
		}

		if(Time.frameCount%1100 == 0){
			if((Random.Range(0,10) % 2) == 0){
				moo.Play();
			} else{
				mooing.Play();
			}
		}

		charController = this.GetComponent<CharacterController>();
		
		verticalSpeed -= gravity;
		if(verticalSpeed <= -5){
			verticalSpeed = -5;
		}
		movementVector.y = verticalSpeed;
		
		flags = charController.Move(movementVector * Time.deltaTime);
		
		movementVector.x = 0;
		movementVector.z = 0;
	}

	public void hit(){
		health--;
		hurt.Play();
		if(health <= 0){
			dying.Play();
			animator.SetTrigger("Hit");
		}
		Destroy(gameObject, 5f);
	}
}
