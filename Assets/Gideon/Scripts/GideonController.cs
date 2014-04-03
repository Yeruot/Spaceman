using UnityEngine;
using System.Collections;

public class GideonController : MonoBehaviour {
	private Animator animator;
	//Public player Attributes below
	public float jumpSpeed;
	public float gravity;
	
	private float verticalSpeed;
	private Vector3 movementVector;
	private Vector3 additionVector;
	
	private AudioSource[] audioSources;
	private AudioSource comeOn;
	
	private CollisionFlags flags;
	private CharacterController charController;
	
	private Time timer;

	private static GideonController instance = null;
	public static GideonController Instance
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

	// Use this for initialization
	void Start () {
		audioSources = this.GetComponents<AudioSource>();
		animator = this.GetComponent<Animator>();
		movementVector = new Vector3(0f,0f,0f);
		animator.SetInteger("Direction", 2);
	}
	
	// Update is called once per frame
	void Update () {
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

	public void QuestComplete(){
		animator.SetBool("Glasses", true);
	}
}
