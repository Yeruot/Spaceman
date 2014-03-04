using UnityEngine;
using System.Collections;

public class DPadLeftController : InteractiveController {

	public override void OnTouched(){
		print ("left");
		PlayerController.Instance.moveLeft();
	}
}
