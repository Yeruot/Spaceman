using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

	private GUILayer gui;
	private static InputController instance = null;
	public static InputController Instance
	{
		get {return instance;}
	}
	
	void Awake(){
		if (instance != null && instance != this){
			Destroy(this.gameObject);
			return;
		}
		else{
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	// Use this for initialization
	void Start () {
		gui = Camera.main.GetComponent<GUILayer>();
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < Input.touchCount; i++){
			Touch touch = Input.GetTouch(i);
			GUIElement guiObject = gui.HitTest(touch.position);
			RaycastHit hitObject;

			if(guiObject != null){
				guiObject.gameObject.GetComponent<InteractiveController>().OnTouched();
			}
			else if(Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out hitObject, Mathf.Infinity)){
				//hitObject.collider.gameObject.GetComponent<InteractiveController>().OnTouched();
			}
		}

		if(Input.GetMouseButton(0)){
			GUIElement guiObject = gui.HitTest(Input.mousePosition);
			RaycastHit hitObject;
			
			if(guiObject != null){
				guiObject.gameObject.GetComponent<InteractiveController>().OnTouched();
			}
			else if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitObject, Mathf.Infinity)){
				//hitObject.collider.gameObject.GetComponent<InteractiveController>().OnTouched();
			}
		}
	}
}
