using UnityEngine;
using System.Collections;

public class AttackButtonController : InteractiveController {

	void Start () {
	}

	public override void OnTouched(){
		print ("Attack button pressed");
		PlayerController.Instance.attack();
	}
}
