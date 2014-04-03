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

    public string words = null;
    private float wordsShown = 0;

    public GUISkin skin;

    private int state = 0;

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

    
    void OnGUI()
    {
        if(words != null)
        {
            if(wordsShown == 0) wordsShown = Time.time;
            GUI.Box(new Rect(Screen.width/2-100,Screen.height-200,300,100), "<color=#ffffff><size=30>" + words + "</size></color>");
            if(Time.time - wordsShown > 2) {
                words = null;
                wordsShown = 0;
                if(state == 1) {
                    PlayerController.Instance.fly();
                }
            }
        }
    }

    public void hit() {
        if (state == 0) {
            words = "HEY! Get my glasses.";
        } else if (state == 1) {
            words = "Thanks bub. You can go now";
        }
    }

	public void QuestComplete(){
        state = 1;
		animator.SetBool("Glasses", true);
	}
}
