using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public Transform weaponLocation;
	public Rigidbody weapon;

	private Animator animator;
	
	private AudioSource[] audioSources;
	private AudioSource grunt;
	
	private CharacterController charController;
	
	//Private player attributes
	private CollisionFlags flags;
	private int currentDirection;
	
	//Public player Attributes below
	public float jumpSpeed;
	public float gravity;
	
	private float verticalSpeed;
	
	private Vector3 movementVector;

	// The below is code to make the player controller a singleton
	// this will allow multiple objects to refer to a single player
	// object and act accordingly
	private static PlayerController instance = null;
	public static PlayerController Instance
	{
		get {return instance;}
	}

	void Awake(){
		// this code makes sure that only one instance of the player exists
		// at a time
		if (instance != null && instance != this){
			Destroy(this.gameObject);
			return;
		}
		else{
			instance = this;
		}

		// this will allow our player object to persist between
		// scenes(levels)
		DontDestroyOnLoad(this.gameObject);
	}

	void Start () {
		instance.audioSources = GetComponents<AudioSource>();
		instance.grunt = audioSources[0];
		instance.movementVector = new Vector3(0f,0f,0f);

		currentDirection = 2;

		animator = GetComponent<Animator>(); //initialize the animator
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("w")) {
			moveUp ();
		} else if (Input.GetKey ("s")) {
			moveDown ();
		} else if (Input.GetKey ("a")) {
			moveLeft ();
		} else if (Input.GetKey ("d")) {
			moveRight();
		}
		charController = instance.GetComponent<CharacterController>();

		instance.verticalSpeed -= gravity;
		if(instance.verticalSpeed <= -1){
			instance.verticalSpeed = -1;
		}
		instance.movementVector.y = instance.verticalSpeed;

		instance.flags = charController.Move(instance.movementVector * Time.deltaTime);

		instance.movementVector.x = 0;
		instance.movementVector.z = 0;
	}

	public void attack(){
		print ("Attack Triggered");
		Rigidbody attackingWeapon;
		attackingWeapon = (Rigidbody)Instantiate(weapon, weaponLocation.position, weaponLocation.rotation);
		switch(currentDirection){
		case 0:
			attackingWeapon.AddForce(Vector3.forward * 100f);
			break;
		case 1:
			attackingWeapon.AddForce(Vector3.right * 100f);
			break;
		case 2:
			attackingWeapon.AddForce(Vector3.back * 100f);
			break;
		case 3:
			attackingWeapon.AddForce(Vector3.left * 100f);
			break;
		default:
			print ("currentDirection not properly set");
			break;
		}
		grunt.Play();
		instance.animator.SetTrigger("Attack");
	}

	public void jump(){
		if((flags & CollisionFlags.Below) != 0)
			instance.verticalSpeed = jumpSpeed;
	}

	public void moveLeft(){
		instance.animator.SetInteger("Direction", 3);

		currentDirection = 3;
		instance.movementVector += Vector3.left;
	}

	public void moveRight(){
		instance.animator.SetInteger("Direction", 1);

		currentDirection = 1;
		instance.movementVector += Vector3.right;

	}

	public void moveUp(){
		instance.animator.SetInteger("Direction", 0);

		currentDirection = 0;
		instance.movementVector += Vector3.forward;
	}

	public void moveDown(){
		instance.animator.SetInteger("Direction", 2);

		currentDirection = 2;
		instance.movementVector += Vector3.back;
	}
}
