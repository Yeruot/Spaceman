using UnityEngine;
using System.Collections;

public class DPadUpController : InteractiveController {

	public override void OnTouched(){
		print ("up");
		PlayerController.Instance.moveUp();
	}
}
