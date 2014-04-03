using UnityEngine;
using System.Collections;

public class AttackButtonController : InteractiveController {

	void Start () {
	}

	public override void OnTouched(){
		PlayerController.Instance.attack();
	}
}
