using UnityEngine;
using System.Collections;

public class SecondaryButtonController : InteractiveController {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void OnTouched(){
		print ("Secondary Button Clicked");
		PlayerController.Instance.jump();
	}
}
